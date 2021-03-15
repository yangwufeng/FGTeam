using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramenWork
{
    public abstract class Fruit
    {
        public string vendor { get; set; } //默认为private
        public abstract float Price { get; } //抽象属性必须是公有的

        public abstract void GrowInArea(); //抽象方法必须是公有的
    }
}
