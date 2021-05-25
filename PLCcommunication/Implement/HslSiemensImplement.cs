using Models.BLLModel;
using Models.Model;
using PLCcommunication.Implement;
using PLCcommunication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication.Implement
{
    public class HslSiemensImplement : IPLC
    {

        SiemensS7Net Siemens
        public string Name { get; set; }
        public string IP { get; set; }
        public PLCType PLCType { get; set; } = PLCType.Siemens;

        public BLLResult Connect()
        {
            throw new NotImplementedException();
        }

        public BLLResult DisConnect()
        {
            throw new NotImplementedException();
        }

        public BLLResult GetConnectStatus()
        {
            throw new NotImplementedException();
        }

        public BLLResult Read(EquipmentProp equipmentProp)
        {
            throw new NotImplementedException();
        }

        public BLLResult Reads(List<EquipmentProp> equipmentProps)
        {
            throw new NotImplementedException();
        }

        public BLLResult Write(EquipmentProp equipmentProp)
        {
            throw new NotImplementedException();
        }

        public BLLResult Writes(List<EquipmentProp> equipmentProps)
        {
            throw new NotImplementedException();
        }
    }
}
