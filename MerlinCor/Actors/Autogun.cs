using Merlin2d.Game;
using MerlinCor.Commands;
using MerlinCor.Spells;
using MerlinCor.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public class Autogun: AbstractCharacter, IWizard, IMovable 
    {
        private Animation autogun;
        private Animation autogunShoot;
        private SpellDirector director;
        private ActorOrientation orientation;
        private Move moveLeft;
        private Move moveRight;
        private ActorOrientation orientationTop;
        private int counter;
        public Autogun(string name, int x, int y)
        {
            this.SetName(name);
            if (name == "AutogunR")
            {
                orientation = ActorOrientation.FacingRight;
                autogun = new Animation("resources/laserRight.png", 70, 70);
                autogunShoot = new Animation("resources/laserRightShoot.png", 70, 70);
                this.SetAnimation(autogun);
            }
            else if (name == "AutogunL")
            {
                orientation = ActorOrientation.FacingLeft;
                autogun = new Animation("resources/laserLeft.png", 70, 70);
                autogunShoot = new Animation("resources/laserLeftShoot.png", 70, 70);
                this.SetAnimation(autogun);
            }
            else
            {
                orientation = ActorOrientation.Undeindefined;
                autogun = new Animation("resources/laserDown.png", 70, 70);
                autogunShoot = new Animation("resources/laserDownShoot.png", 70, 70);
                this.SetAnimation(autogun);
                moveLeft = new Move(this, 1, -1, 0);
                moveRight = new Move(this, 1, 1, 0);
                orientationTop = ActorOrientation.FacingRight;
            }
            director = new SpellDirector(this);
            this.SetPosition(x,y);
            this.SetSpeedStrategy(new NormalSpeedStrategy());
        }

        public void Cast(ISpell spell)
        {
            if (spell != null)
                spell.Cast();
        }

        public void ChangeMana(int delta){ }

        public int GetMana(){ return 100;}

        public ActorOrientation GetSide() { return orientation; }

        public override void Update()
        {
            if (((counter == 60 || counter == 70) && this.GetName() == "AutogunR") ||
                ((counter == 120 || counter == 130) && this.GetName() == "AutogunL") ||
                ((counter == 180 || counter == 190) && this.GetName() == "AutogunT"))
            {
                if (counter%60 == 0)
                {
                    this.SetAnimation(autogunShoot);
                    Cast(director.Build("Frostball"));
                }
                else
                {
                    this.SetAnimation(autogun);
                }
            }
            if (counter%3 == 0 && this.GetName() == "AutogunT")
            {
                if (orientationTop == (ActorOrientation)1)
                {

                    if (this.GetX() == 1680)
                    {
                        moveLeft.Execute();
                        orientationTop = (ActorOrientation)0;
                    }
                    else
                    {
                        moveRight.Execute();
                    }
                }
                else
                {
                    if (this.GetX() == 1190)
                    {
                        moveRight.Execute();
                        orientationTop = (ActorOrientation)1;
                    }
                    else
                    {
                        moveLeft.Execute();
                    }
                }
            }
            counter++;
            counter = counter == 190 ? 0 : counter;
        }
    }
}
