using Merlin2d.Game.Actions;
using MerlinCor.Actors;
using MerlinCor.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Spells.Effects
{
    public class Freeze<T> : IAction<T> where T : ICharacter
    {

        private int counter;

        public void Execute(T t)
        {
            if (counter++ == 0)
            {
                t.SetSpeedStrategy(new FreezeSpeedStrategy());
            }
            if (counter == 180)
            {
                t.SetSpeedStrategy(new NormalSpeedStrategy());
                t.RemoveEffect((IAction<AbstractCharacter>)this);
            }
        }
    }
}
