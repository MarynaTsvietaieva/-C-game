using MerlinCor.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public interface IMovable
    {
        void SetSpeedStrategy(ISpeedStrategy strategy);
        double GetSpeed(double speed);

    }
}
