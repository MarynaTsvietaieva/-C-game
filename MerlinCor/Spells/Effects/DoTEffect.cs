using Merlin2d.Game.Actions;
using MerlinCor.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Spells.Effects
{
    public class DoTEffect<T>: IAction<T> where T : ICharacter
    {
        private int damage;
        private int time;
        public DoTEffect(int a, int b)
        {
            this.damage = a;
            this.time = b;
            counter = 0;
        }
        private int counter;
        public void Execute(T t)
        {
            if (counter++ == 60)
            {
                if (this.time != 0)
                {
                    t.ChangeHealth(-damage);
                    counter = 0;
                    this.time--;
                }
                else
                {
                    t.RemoveEffect((IAction<AbstractCharacter>)this);
                }
            }
        }
    }
}
