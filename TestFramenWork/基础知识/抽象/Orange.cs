using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramenWork
{
    public class Orange : Fruit
    {

        public override float Price
        {
            get
            {
                return 0;
            }
        }

        public override void GrowInArea()
        {
            Console.WriteLine("我只能生长在南方,我的生产商是:" + vendor + ",我的价格是：" + Price);
        }
    }
}
