using HslCommunication;
using HslCommunication.Profinet.Siemens;
using Models.BLLModel;
using Models.Model;
using PLCcommunication.Implement;
using PLCcommunication.Interfaces;
using PLCCommunication.PLCComponent.HslComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication.Implement
{
    public class HslSiemensImplement : IPLC
    {
        const int READLIMIT = 10;
        object Lock = new object();

        SiemensS7Net siemensTcpNet;
        public string Name { get; set; }
        public string IP { get; set; }
        public PLCType PLCType { get; set; } = PLCType.Siemens;
        List<HslSiemensDataEntity> _hslSiemensDataEntities = new List<HslSiemensDataEntity>();


        /// <summary>
        /// 初始化连接client，传递PLC构建类型
        /// </summary>
        /// <param name="siemensPLCS"></param>
        /// <exception cref="ArgumentNullException">未传递参数</exception>
        /// <exception cref="Exception">其他可能异常</exception>
        public HslSiemensImplement(SiemensPLCBuildModel siemensPLCBuildModel)
        {
            if (siemensPLCBuildModel == null)
            {
                throw new ArgumentNullException("请传递参数");
            }
            try
            {
                siemensTcpNet = new SiemensS7Net((SiemensPLCS)siemensPLCBuildModel.SiemensPLCS);
                siemensTcpNet.IpAddress = siemensPLCBuildModel.IP;
                siemensTcpNet.Port = siemensPLCBuildModel.Port;
                siemensTcpNet.Rack = byte.Parse(siemensPLCBuildModel.Rack.ToString());
                siemensTcpNet.Slot = byte.Parse(siemensPLCBuildModel.Slot.ToString());
                IP = siemensPLCBuildModel.IP;
                Name = siemensPLCBuildModel.Name;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public BllResult Connect()
        {
            var result = siemensTcpNet.ConnectServer();
            if (result.IsSuccess)
            {
                //连接失败，则失败
                return BllResultFactory.Error($"连接PLC：{siemensTcpNet.IpAddress}失败：{result.Message}");
            }
            return BllResultFactory.Success("连接成功");

        }

        public BllResult DisConnect()
        {
            siemensTcpNet.ConnectClose();
            return BllResultFactory.Success("关闭成功");
        }

        public BllResult GetConnectStatus()
        {
            return BllResultFactory.Success();
        }

        public BllResult Read(EquipmentProp equipmentProp)
        {
            return Reads(new List<EquipmentProp>() { equipmentProp });
        }

        public BllResult Reads(List<EquipmentProp> equipmentProps)
        {
            var valiResult = Validation(equipmentProps);
            if (!valiResult.Success)
            {
                return BllResultFactory.Error(valiResult.Msg);
            }

            List<HslSiemensDataEntity> list = new List<HslSiemensDataEntity>();
            try
            {
                //缓存中已经存在的
                list.AddRange(_hslSiemensDataEntities.Where(t => equipmentProps.Exists(a => a.Id == t.OPCAddressId)).ToList());
            }
            catch (Exception)
            {
                return BllResultFactory.Error($"等待地址初始化完成");
            }
            if (equipmentProps.Count > READLIMIT)
            {
                //进行批量读取
                //构造地址数组与对应字节长度数据
                var adds = new List<string>();
                var sizes = new List<ushort>();
                foreach (var item in list)
                {
                    adds.Add(item.Address);
                    sizes.Add(item.ByteSize);
                }
                var result = siemensTcpNet.Read(adds.ToArray(), sizes.ToArray());
                if (!result.IsSuccess)
                {
                    return BllResultFactory.Error($"读取错误，PLC:{Name},IP:{IP}，参考信息：{result.Message}");
                }
                //获取读取后的数组结果
                var bytes = result.Content.ToList();
                foreach (var item in list)
                {
                    //此数据的buffer
                    var buffer = bytes.Take(item.ByteSize);
                    if (buffer.Count() != (int)item.ByteSize)
                    {
                        //如果没有提取出等量的字节
                        return BllResultFactory.Error($"PLC：{Name},IP：{IP}获取的字节序列个数与属性需求字节个数不匹配");
                    }
                    //剩余待解析
                    item.Buffer = buffer.ToArray();
                    var prop = equipmentProps.Find(t => t.Id == item.OPCAddressId);
                    bytes = bytes.Skip(item.ByteSize).ToList();
                    var transResult = SiemensHelper.TransferBufferToString(item);
                    if (!transResult.Success)
                    {
                        return BllResultFactory.Error($"读取时，PLC:{Name}IP:{IP},地址{prop.Address}，转换数据失败:{transResult.Msg}");
                    }
                    prop.Value = transResult.Data;
                }

            }
            else
            {
                //进行单个读取
                foreach (var item in list)
                {
                    var prop = equipmentProps.Find(t => t.Id == item.OPCAddressId);
                    switch (item.DataType)
                    {
                        case PLCDataType.BYTE:
                            var result = siemensTcpNet.ReadByte(item.Address);
                            if (!result.IsSuccess)
                            {
                                return BllResultFactory.Error($"读取PLC：{Name},IP:{IP},地址{prop.Address}发生错误：{result.Message}");
                            }
                            prop.Value = ((int)result.Content).ToString();
                            break;
                        case PLCDataType.BOOL:
                            var result2 = siemensTcpNet.ReadBool(item.AddressX);
                            if (!result2.IsSuccess)
                            {
                                return BllResultFactory.Error($"读取PLC：{Name},IP:{IP},地址{prop.Address}发生错误：{result2.Message}");
                            }
                            prop.Value = result2.Content.ToString();
                            break;
                        case PLCDataType.DWORD:
                            var result3 = siemensTcpNet.ReadUInt32(item.Address);
                            if (!result3.IsSuccess)
                            {
                                return BllResultFactory.Error($"读取PLC：{Name},IP:{IP},地址{prop.Address}发生错误：{result3.Message}");
                            }
                            prop.Value = result3.Content.ToString();
                            break;
                        case PLCDataType.WORD:
                            var result4 = siemensTcpNet.ReadUInt16(item.Address);
                            if (!result4.IsSuccess)
                            {
                                return BllResultFactory.Error($"读取PLC：{Name},IP:{IP},地址{prop.Address}发生错误：{result4.Message}");
                            }
                            prop.Value = result4.Content.ToString();
                            break;
                        case PLCDataType.INT:
                            var result5 = siemensTcpNet.ReadInt16(item.Address);
                            if (!result5.IsSuccess)
                            {
                                return BllResultFactory.Error($"读取PLC：{Name},IP:{IP},地址{prop.Address}发生错误：{result5.Message}");
                            }
                            prop.Value = result5.Content.ToString();
                            break;
                        case PLCDataType.DINT:
                            var result6 = siemensTcpNet.ReadInt32(item.Address);
                            if (!result6.IsSuccess)
                            {
                                return BllResultFactory.Error($"读取PLC：{Name},IP:{IP},地址{prop.Address}发生错误：{result6.Message}");
                            }
                            prop.Value = result6.Content.ToString();
                            break;
                        case PLCDataType.CHAR:
                            var result7 = siemensTcpNet.ReadString(item.Address, item.ByteSize);
                            if (!result7.IsSuccess)
                            {
                                return BllResultFactory.Error($"读取PLC：{Name},IP:{IP},地址{prop.Address}发生错误：{result7.Message}");
                            }
                            prop.Value = result7.Content.ToString();
                            break;
                        default:
                            return BllResultFactory.Error($"未识别的数据类型:{item.DataType}");
                    }
                }
            }

            return BllResultFactory.Success("读取成功");
        }

        public BllResult Write(EquipmentProp equipmentProp)
        {
            return Writes(new List<EquipmentProp>() { equipmentProp });
        }

        public BllResult Writes(List<EquipmentProp> equipmentProps)
        {
            var valiResult = Validation(equipmentProps);
            if (!valiResult.Success)
            {
                return BllResultFactory.Error(valiResult.Msg);
            }

            List<HslSiemensDataEntity> list = new List<HslSiemensDataEntity>();
            try
            {
                list.AddRange(equipmentProps.Join(_hslSiemensDataEntities, n => n.Id, m => m.OPCAddressId, (n, m) => m));
            }
            catch (Exception)
            {
                return BllResultFactory.Error($"等待地址初始化完成");
            }

            //对于写入，由于地址可能不连续且写入地址不多，会导致覆盖问题，这里不使用批量写入
            foreach (var item in list)
            {
                var prop = equipmentProps.Find(t => t.Id == item.OPCAddressId);
                var transResult = SiemensHelper.TransferStringToBuffer(item.DataType, prop.Value);
                if (!transResult.Success)
                {
                    return BllResultFactory.Error($"转换到PLC数据类型失败，PLC:{Name},IP:{IP},地址：{prop.Address}:{transResult.Msg}");
                }
                var buffer = transResult.Data;
                //如果不是bool，我们都是写字节，如果是bool，我们需要写位，这个调用具体方法实现
                //浮点型暂不考虑
                OperateResult result = null;
                if (item.DataType != PLCDataType.BOOL)
                {
                    result = siemensTcpNet.Write(item.Address, buffer);
                }
                else
                {
                    result = siemensTcpNet.Write(item.AddressX, bool.Parse(prop.Value));
                }
                if (!result.IsSuccess)
                {
                    return BllResultFactory.Error($"写入PLC:{Name},ip:{IP},地址:{prop.Address}，数据：{prop.Value}失败：{result.Message}");
                }
            }


            return BllResultFactory.Success("写入成功");
        }


        /// <summary>
        /// 调用读写函数前的校验，此过程包含了解析地址并缓存
        /// </summary>
        /// <param name="equipmentProps"></param>
        /// <returns></returns>
        private BllResult<List<string>> Validation(List<EquipmentProp> equipmentProps)
        {
            lock (Lock)
            {
                if (equipmentProps == null || equipmentProps.Count() == 0)
                {
                    return BllResultFactory.Error<List<string>>($"未传递属性");
                }
                //选取设备类
                var ips = equipmentProps.Select(t => t.Equipment).Select(t => t.IP).Distinct().ToList();
                var temp = ips.Where(t => siemensTcpNet.IpAddress != t).ToArray();
                if (temp.Length > 0)
                {
                    return BllResultFactory.Error<List<string>>($"存在IP为{string.Join(",", temp)}的属性与当前PLC实例{Name}的IP{siemensTcpNet.IpAddress}不符，请检查传递属性是否有误");
                }

                //缓存中未存在的属性
                var tempProps = equipmentProps.Where(t => !_hslSiemensDataEntities.Exists(a => a.OPCAddressId == t.Id)).ToList();
                //解析属性并加入缓存
                foreach (var item in tempProps)
                {
                    var result = SiemensHelper.ParseAddress(item);
                    if (!result.Success)
                    {
                        return BllResultFactory.Error<List<string>>($"设备：{item.Equipment.Code},属性：{item.EquipmentTypePropTemplateCode},地址解析错误：{result.Msg}");
                    }
                    _hslSiemensDataEntities.Add(result.Data);
                }
                return BllResultFactory.Success(ips);
            }
        }
    }
}
