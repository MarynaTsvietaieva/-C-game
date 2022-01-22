using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public abstract class AbstractState
    {
        public abstract void StateChange(ICharacter actor);
    }
}
