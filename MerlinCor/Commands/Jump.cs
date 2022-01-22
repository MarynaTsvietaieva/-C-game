using Merlin2d.Game;
using Merlin2d.Game.Actors;
using MerlinCor.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MerlinCor.Commands
{
    public class Jump<T> : IAction<T> where T : IActor
    {

        private Move moveUp;
        private int jumpHeight;
        private int jumpHeightCopy;
        public Jump(int jumpHeight)
        {
            this.jumpHeight = jumpHeight;
            jumpHeightCopy = jumpHeight;
        }
        public void Execute(T value)
        {
            moveUp = new Move((IMovable)value, 8, 0, 1);

            value.SetPosition(value.GetX(), value.GetY() + 1);
            if (value.GetWorld().IntersectWithWall(value))
            {
                jumpHeight = jumpHeightCopy;
            }
            value.SetPosition(value.GetX(), value.GetY() - 1);

            if (jumpHeight != 0)
            {
                moveUp.Execute();
                jumpHeight--;
            }
        }
    }
}

