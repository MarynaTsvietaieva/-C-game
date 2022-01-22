using Merlin2d.Game;
using MerlinCor.Commands;
using MerlinCor.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public class Spinner: AbstractCharacter
    {
        private Animation spinner;
        private Move moveLeft;
        private Move moveRight;
        private int counter;
        private ActorOrientation orientation;
        public Spinner(string name, int x, int y)
        {
            spinner = new Animation("resources/spinner2.png", 63, 62);
            this.SetName(name);
            SetAnimation(spinner);
            spinner.Start();
            this.SetSpeedStrategy(new NormalSpeedStrategy());
            this.SetPosition(x, y);
            moveLeft = new Move(this, 1, -1, 0);
            moveRight = new Move(this, 1, 1, 0);
            orientation = ActorOrientation.FacingRight;
        }

        public override void Update()
        {
            if (counter++ == 3)
            {
                if(orientation == (ActorOrientation)1)
                {
                    this.SetPosition(this.GetX() + 1, this.GetY());

                    if (this.GetWorld().IntersectWithWall(this))
                    {
                        moveLeft.Execute();
                        orientation = (ActorOrientation)0;
                    }
                    else
                    {
                        moveRight.Execute();
                    }
                }
                else
                {
                    this.SetPosition(this.GetX() - 1, this.GetY());

                    if (this.GetWorld().IntersectWithWall(this))
                    {
                        moveRight.Execute();
                        orientation = (ActorOrientation)1;
                    }
                    else
                    {
                        moveLeft.Execute();
                    }
                }
                counter = 0;
            }
        }
    }
}
