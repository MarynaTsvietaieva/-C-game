using Merlin2d.Game;
using Merlin2d.Game.Actors;
using MerlinCor.Actors;
using MerlinCor.Spells;
using MerlinCor.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Commands
{
    public class Move : ICommand
    {
        IActor actor;

        private int [] jumpSteps = new int[] { 0, 0, 0, 0, 0 , 0 ,0,0,0,0,0,0,0,0,0}; //v skoku môžete urobiť iba jeden krok
        private double xConstant;
        private double yConstant;
        private double speed;
        private int dx;

        public Move(IMovable movable, double step, int dx, int dy)
        {
            if (movable is IActor)
            {

                actor = (IActor)movable;
                speed = step;
                this.dx = dx;
                xConstant = dx * speed;
                yConstant = -dy * speed;
            }
            else
            {
                throw new ArgumentException("objekt nie je typu IActor");
            }

        }
        public void Execute()
        {
            if (yConstant == 0 && !(actor is ProjectileSpell))
            {
                xConstant = dx * ((AbstractCharacter)actor).GetSpeed(speed);
            }
            if (actor.GetType() == typeof(ProjectileSpell))
            {
                actor.SetPosition((int)Math.Round(actor.GetX() + xConstant), (int)Math.Round(actor.GetY() + yConstant));
                if (actor.GetWorld().IntersectWithWall(actor))
                {
                    actor.GetWorld().RemoveActor(actor);
                }
            }
            else
            {
                actor.SetPosition(actor.GetX(), actor.GetY() + 2); //kontrola, či sa urobi krok vo výskoku alebo na povrchu
                if (actor.GetWorld().IntersectWithWall(actor) || !actor.IsAffectedByPhysics())
                {
                    actor.SetPosition((int)Math.Round(actor.GetX() + xConstant), (int)Math.Round(actor.GetY() + yConstant - 2)); //na povrchu
                    jumpSteps = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                }
                else
                {
                    if (jumpSteps.Sum() == jumpSteps.Length)
                    {
                        actor.SetPosition(actor.GetX(), (int)Math.Round(actor.GetY() + yConstant - 2)); //skokový krok už bol urobený

                    }
                    else
                    {
                        if (actor.IsAffectedByPhysics())
                        {
                            actor.SetPosition((int)Math.Round(actor.GetX() + 4 * xConstant), (int)Math.Round(actor.GetY() + yConstant - 2)); //skokový krok
                        }
                        else
                        {
                            actor.SetPosition((int)Math.Round(actor.GetX() + xConstant), (int)Math.Round(actor.GetY() + yConstant - 2)); //skokový krok
                        }
                        int v = Array.IndexOf(jumpSteps, 0);
                        jumpSteps[v] = 1;
                    }
                }

                while (actor.GetWorld().IntersectWithWall(actor))
                {
                    if (xConstant != 0)
                    {
                        actor.SetPosition((int)Math.Round(actor.GetX() - xConstant / Math.Abs(xConstant)), actor.GetY());
                    }
                    else
                    {
                        actor.SetPosition(actor.GetX(), (int)Math.Round(actor.GetY() - yConstant / Math.Abs(yConstant)));
                    }
                }
            }

        }


    }
}

