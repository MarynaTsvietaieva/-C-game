using Merlin2d.Game.Actions;
using MerlinCor.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Spells.Effects
{
    public class Damage<T> : IAction<T> where T : ICharacter
    {
        private int damage;
        public Damage(int damage)
        {
            this.damage = -damage;
        }
        public void Execute(T t)
        {
            t.ChangeHealth(this.damage);
            t.RemoveEffect((IAction<AbstractCharacter>)this);
        }
    }
}
