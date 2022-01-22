using Merlin2d.Game;
using Merlin2d.Game.Actors;
using MerlinCor.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MerlinCor.Commands
{
    public class Fall<T> : IAction<T> where T : IActor
    {

        private int fallSpeed;
        public Fall(int speed)
        {
            fallSpeed = speed;
        }
        public void Execute(T value)
        {
            if (value is ProjectileSpell)
            {
                value.SetPosition(value.GetX(), value.GetY() + 1);
            }
            else
            {
                value.SetPosition(value.GetX(), value.GetY() + fallSpeed);
            }
            while (value.GetWorld().IntersectWithWall(value) && !(value is ProjectileSpell))
            {
                value.SetPosition(value.GetX(), value.GetY() - 1);
            }
        }
    }
}

