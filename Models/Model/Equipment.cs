using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
   public  class Equipment : BaseModels
    {
        private string code;

        [Column(Order = 2)]
        [MaxLength(50)]
        [Required]
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string name;

        [Column(Order = 3)]
        [MaxLength(50)]
        [Required]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int equipmentTypeId;

        [Column(Order = 4)]
        [Required]
        public int EquipmentTypeId
        {
            get { return equipmentTypeId; }
            set { equipmentTypeId = value; }
        }

        private string ip;

        /// <summary>
        /// 此处写到这台设备对应的PLC IP，注意，IP均要配置
        /// </summary>
        [Column(Order = 5)]
        [MaxLength(20)]
        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }

        private string ledip;

        /// <summary>
        /// 关联的LED IP，没有请填null或者""
        /// </summary>
        [Column(Order = 6)]
        [MaxLength(20)]
        public string LEDIP
        {
            get { return ledip; }
            set { ledip = value; }
        }

        private string scanIp;

        /// <summary>
        /// 关联的扫码器IP
        /// </summary>
        [Column(Order = 7)]
        [MaxLength(20)]
        public string ScanIP
        {
            get { return scanIp; }
            set { scanIp = value; }
        }

        private string destinationArea;

        /// <summary>
        /// 所在区域，为兼容转轨堆垛机设定，正常情况下与巷道相同，转轨情况下对应虚拟划分巷道
        /// </summary>
        [Column(Order = 8)]
        [MaxLength(50)]
        public string DestinationArea
        {
            get { return destinationArea; }
            set { destinationArea = value; }
        }

        private int roadWay;

        /// <summary>
        /// 所在巷道，没有填0
        /// </summary>
        [Column(Order = 9)]
        [Required]
        public int RoadWay
        {
            get { return roadWay; }
            set { roadWay = value; }
        }

        [Column(Order = 10)]
        [MaxLength(50)]
        public string BasePlcDB { get; set; }


        private string selfAddress;

        /// <summary>
        /// 自身地址，通常用于站台和口
        /// </summary>
        [Column(Order = 11)]
        [MaxLength(50)]
        public string SelfAddress
        {
            get { return selfAddress; }
            set { selfAddress = value; }
        }

        private string backAddress;

        /// <summary>
        /// 回退地址
        /// </summary>
        [Column(Order = 12)]
        [MaxLength(50)]
        public string BackAddress
        {
            get { return backAddress; }
            set { backAddress = value; }
        }


        private string goAddress;

        /// <summary>
        /// 前进地址，如果配置了则以配置优先，如果没有配置则由路由获取
        /// </summary>
        [Column(Order = 13)]
        [MaxLength(50)]
        public string GoAddress
        {
            get { return goAddress; }
            set { goAddress = value; }
        }

        private int stationIndex;

        /// <summary>
        /// 电控接入接出站台索引，从1到10;当发送口时，同时发送排索引；
        /// </summary>
        [Column(Order = 14)]
        public int StationIndex
        {
            get { return stationIndex; }
            set { stationIndex = value; }
        }

        private int rowIndex1;

        /// <summary>
        /// 货叉索引，没有填0
        /// </summary>
        [Column(Order = 15)]
        [Required]
        public int RowIndex1
        {
            get { return rowIndex1; }
            set { rowIndex1 = value; }
        }

        private int rowIndex2;

        /// <summary>
        /// 货叉索引，没有填0
        /// </summary>
        [Column(Order = 16)]
        public int RowIndex2
        {
            get { return rowIndex2; }
            set { rowIndex2 = value; }
        }

        private int columnIndex;

        /// <summary>
        /// 列索引
        /// 对于堆垛机接出与接入站台，要求必须配置此项，此项用于判断堆垛机是否可以到达站台所在位置
        /// </summary>
        [Column(Order = 17)]
        [Required]
        public int ColumnIndex
        {
            get { return columnIndex; }
            set { columnIndex = value; }
        }

        private int layerIndex;

        /// <summary>
        /// 针对堆垛机，所在的层索引
        /// </summary>
        [Column(Order = 18)]
        [Required]
        public int LayerIndex
        {
            get { return layerIndex; }
            set { layerIndex = value; }
        }

        private string connectName;

        /// <summary>
        /// 连接名，用于OPC
        /// </summary>
        [Column(Order = 19)]
        [MaxLength(50)]
        public string ConnectName
        {
            get { return connectName; }
            set { connectName = value; }
        }

        private string stationGroup;

        /// <summary>
        /// 站台组（双叉会用到）
        /// </summary>
        [Column(Order = 20)]
        [MaxLength(50)]
        public string StationGroup
        {
            get { return stationGroup; }
            set { stationGroup = value; }
        }

        private int stationGroupIndex;

        /// <summary>
        /// 站台组索引（双叉时，小的对应1号货叉，大的对应2号货叉，当存在多个时可以定义为12 45，则表示12可以同时放，45可以同时放）
        /// </summary>
        [Column(Order = 21)]
        [Required]
        public int StationGroupIndex
        {
            get { return stationGroupIndex; }
            set { stationGroupIndex = value; }
        }


        private string description;

        [Column(Order = 22)]
        [MaxLength(200)]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }


        private string fork1AvailableStation;

        /// <summary>
        /// 货叉1可用站台索引
        /// </summary>
        [Column(Order = 23)]
        [MaxLength(50)]
        public string Fork1AvailableStation
        {
            get { return fork1AvailableStation; }
            set { fork1AvailableStation = value; }
        }

        private string fork2AvailableStation;

        /// <summary>
        /// 货叉2可用站台索引
        /// </summary>
        [Column(Order = 24)]
        [MaxLength(50)]
        public string Fork2AvailableStation
        {
            get { return fork2AvailableStation; }
            set { fork2AvailableStation = value; }
        }

        private string warehouseCode;

        /// <summary>
        /// 设备所在仓库编码
        /// </summary>
        [Column(Order = 25)]
        [MaxLength(50)]
        public string WarehouseCode
        {
            get { return warehouseCode; }
            set { warehouseCode = value; }
        }


        /// <summary>
        /// 用于界面显示进行站台分组的
        /// </summary>
        [Column(Order = 26)]
        [MaxLength(50)]
        public string DestinationGroup { get; set; }

        /// <summary>
        /// 维护规格Id
        /// </summary>
        [Column(Order = 27)]
        public int? EquipmentMaintainRuleId { get; set; }


        private bool disable;

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column(Order = 45)]
        [Required]
        public bool Disable
        {
            get { return disable; }
            set { disable = value; }
        }


        /// <summary>
        /// 逻辑外键实体-设备保养规格
        /// </summary>
        //public EquipmentMaintainRule EquipmentMaintainRule { get; set; }

        /// <summary>
        /// 逻辑外键实体-设备类型
        /// </summary>
        public EquipmentType EquipmentType { get; set; }

        private List<EquipmentProp> m_equipmentProps = new List<EquipmentProp>();

        /// <summary>
        /// 逻辑外键实体-设备属性
        /// </summary>
        public List<EquipmentProp> EquipmentProps
        {
            get { return m_equipmentProps; }
            set
            {
                m_equipmentProps = value;
                if (value != null)
                {
                    var maps = new Dictionary<string, EquipmentProp>(value.Count);
                    foreach (var item in value)
                    {
                        maps.Add(item.EquipmentTypePropTemplateCode, item);
                    }
                    m_propMaps = maps;
                }
            }
        }



        #region MES 字段
        /// <summary>
        /// 车间标识
        /// </summary>
        [Column("workshopId")]
        public int WorkshopId { get; set; }
        /// <summary>
	    /// 工厂标识
	    /// </summary>
        [Column("factoryId")]
        public int FactoryId { get; set; }
        /// <summary>
	    /// 线体
	    /// </summary>
        [Column("lineCode")]
        public string LineCode { get; set; }
        /// <summary>
	    /// 线体ID
	    /// </summary>
        [Column("lineId")]
        public int LineId { get; set; }
        /// <summary>
	    /// 工位
	    /// </summary>
        [Column("stationCode")]
        public string StationCode { get; set; }
        /// <summary>
	    /// 工位ID
	    /// </summary>
        [Column("stationId")]
        public int StationId { get; set; }

        private bool enable;

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column("enable")]
        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }



        //[NotMapped]
        ///// <summary>
        ///// 逻辑外键实体-设备对应的站台属性
        ///// </summary>
        //public Station Station { get; set; }

        ////[NotMapped]
        /////// <summary>
        /////// 逻辑外键实体-站台类别属性列表
        /////// </summary>
        ////public List<StepStation> StepStationList { get; set; }

        //[NotMapped]
        ///// <summary>
        ///// 逻辑外键实体-站台属性列表
        ///// </summary>
        //public List<Station> StationList { get; set; } = new List<Station>();

        #endregion
        #region 添加数据字典 键值只需要执行一次初始化 后续不需要再Find 

        private IDictionary<string, EquipmentProp> m_propMaps = new Dictionary<string, EquipmentProp>();
        /// <summary>
        /// 获取设备属性
        /// </summary>
        /// <param name="key">设备属性code </param>
        /// <returns></returns>
        [NotMapped]
        public EquipmentProp this[string key]
        {
            get
            {
                m_propMaps.TryGetValue(key, out EquipmentProp prop);
                return prop;
            }
        }

        #endregion
    }
}
