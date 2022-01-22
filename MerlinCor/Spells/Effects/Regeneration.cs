using Merlin2d.Game.Actions;
using MerlinCor.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Spells.Effects
{
    public class Regeneration<T>: IAction<T> where T : ICharacter
    {
        private int heal;

        public Regeneration(int a)
        {
            heal = a;
        }
        public Regeneration(): this(1)
        {

        }
        private int counter;
        public void Execute(T t)
        {
            if(heal != 1)
            {
                t.ChangeHealth(heal);
                t.RemoveEffect((IAction<AbstractCharacter>)this);
            }
            if (counter++ == 60)
            {
                t.ChangeHealth(1);
                counter = 0;
            }
        }
    }
}
