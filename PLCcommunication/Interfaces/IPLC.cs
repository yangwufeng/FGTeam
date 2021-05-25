using Models.BLLModel;
using Models.Model;
using PLCcommunication.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCcommunication.Interfaces
{
   public  interface IPLC
    {
        string Name { get; set; }

        string IP { get; set; }

        PLCType PLCType { get; set; }

        /// <summary>
        /// 连接，通常指定PLC的初始化请在连接中进行
        /// </summary>
        /// <returns></returns>
        BLLResult Connect();

        /// <summary>
        /// 断开,通常在断开之前请先停止逻辑处理，延迟后再调用
        /// </summary>
        /// <returns></returns>
        BLLResult DisConnect();

        /// <summary>
        /// 获取连接状态
        /// hack:对于使用OPC实现，此链接状态指示为client与OPC服务器的连接状态；所以使用OPC的情况下需要额外检测读写是否正确。
        /// </summary>
        /// <returns></returns>
        BLLResult GetConnectStatus();

        /// <summary>
        /// 读取地址
        /// </summary>
        /// <param name="equipmentProps"></param>
        /// <returns></returns>
        BLLResult Reads(List<EquipmentProp> equipmentProps);

        /// <summary>
        /// 写入地址
        /// </summary>
        /// <param name="equipmentProps"></param>
        /// <returns></returns>
        BLLResult Writes(List<EquipmentProp> equipmentProps);

        /// <summary>
        /// 读取单个地址
        /// </summary>
        /// <param name="equipmentProp"></param>
        /// <returns></returns>
        BLLResult Read(EquipmentProp equipmentProp);

        /// <summary>
        /// 写入单个地址
        /// </summary>
        /// <param name="equipmentProp"></param>
        /// <returns></returns>
        BLLResult Write(EquipmentProp equipmentProp);
    }
}
