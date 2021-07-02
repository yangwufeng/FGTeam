namespace PLCCommunication.PLCComponent.HslComponent
{
    /// <summary>
    /// 构建西门子PLCmodel类
    /// 端口默认102
    /// 请按如下配置传递solt和rack：S400:0,3;S1200:0,0;S300:0,2;S1500:0,0;其他按实际传递；
    /// </summary>
    public class SiemensPLCBuildModel
    {
        public SiemensPLCTypeS SiemensPLCS { get; set; }

        public string IP { get; set; }

        public int Port { get; set; } = 102;

        public int Rack { get; set; } = 0;

        public int Slot { get; set; } = 0;

        public string Name { get { return Name; } set { value = IP; } }
    }
}