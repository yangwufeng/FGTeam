using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramenWork
{
    public class Apple : Fruit
    {
        public override float Price
        {
            get
            {
                if (vendor == "红富士")
                    return 100;
                else
                    return 0;

            }
        }

        public override void GrowInArea()
        {
            Console.WriteLine("我在南方北方都能生长,我的生产商是:" + vendor + ",我现在的价格是：" + Price);
        }
    }
}
