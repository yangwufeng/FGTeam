using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class EquipmentProp : BaseModels
    {

        private int equipmentId;

        [Column(Order = 2)]
        [Required]
        public int EquipmentId
        {
            get { return equipmentId; }
            set { equipmentId = value; }
        }

        private int equipmentTypePropTemplateId;

        [Column("equipmentTypeTemplateId")]
        [Required]
        public int EquipmentTypePropTemplateId
        {
            get { return equipmentTypePropTemplateId; }
            set { equipmentTypePropTemplateId = value; }
        }

        private string equipmentTypePropTemplateCode;

        [Column("equipmentTypeTemplateCode")]
        [MaxLength(50)]
        [Required]
        public string EquipmentTypePropTemplateCode
        {
            get { return equipmentTypePropTemplateCode; }
            set { equipmentTypePropTemplateCode = value; }
        }

        private int serverHandle;

        [Column(Order = 5)]
        public int ServerHandle
        {
            get { return serverHandle; }
            set { serverHandle = value; }
        }

        private string address;

        [Column(Order = 6)]
        [MaxLength(50)]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string value;

        [Column(Order = 7)]
        [MaxLength(50)]
        public string Value
        {
            //在此处做一个去除空格的处理，不再在PLC读写里进行
            get { return value?.Replace("\0", "").Trim(); }
            set { this.value = value; }
        }

        private string remark;

        [Column(Order = 8)]
        [MaxLength(200)]
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        /// <summary>
        /// 是否写入成功
        /// </summary>
        [Column("plcFlag ")]
        public int? PlcFlag { get; set; }
        /// <summary>
	    /// 是否需要写入
	    /// </summary>
        [Column("writeFlag")]
        public bool? WriteFlag { get; set; }
        /// <summary>
	    /// 待写值
	    /// </summary>
        [Column("writeValue")]
        public string WriteValue { get; set; }


        /// <summary>
        /// 逻辑外键--设备实体
        /// </summary>
        public Equipment Equipment { get; set; }

        /// <summary>
        /// 额外对应属性模板，方便读取模板属性
        /// </summary>
        public EquipmentTypePropTemplate EquipmentTypePropTemplate { get; set; }

    }

}
