using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication.PLCComponent.HslComponent
{
    //
    // 摘要:
    //     西门子的PLC类型，目前支持的访问类型
    public enum SiemensPLCTypeS
    {
        //
        // 摘要:
        //     1200系列
        S1200 = 1,
        //
        // 摘要:
        //     300系列
        S300 = 2,
        //
        // 摘要:
        //     400系列
        S400 = 3,
        //
        // 摘要:
        //     1500系列PLC
        S1500 = 4,
        //
        // 摘要:
        //     200的smart系列
        S200Smart = 5,
        //
        // 摘要:
        //     200系统，需要额外配置以太网模块
        S200 = 6
    }
}
