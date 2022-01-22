using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Strategies
{
    class DecelerationSpeedStrategy : ISpeedStrategy
    {
        public double GetSpeed(double speed)
        {
            return speed*0.6;
        }
    }
}
