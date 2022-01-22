using Merlin2d.Game;
using Merlin2d.Game.Actors;
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
    class Spider : AbstractCharacter, IMovable, IWizard
    {
        private Animation enemy;
        private Random random;
        private Player myPlayer;

        private Move moveLeft;
        private Move moveRight;
        private Jump<IActor> jump;

        private int direction;
        private int steps;
        private int timer;
        private int counter;
        private int distanceToPlayer;
        private SpellDirector director;
        private ActorOrientation orientation;
        private int mana;
        public Spider(string name, int x, int y)
        {
            enemy = new Animation("resources/spider.png", 76, 53);
            this.SetName(name);
            SetAnimation(enemy);
            enemy.Start();
            this.SetSpeedStrategy(new NormalSpeedStrategy());
            moveLeft = new Move(this, this.GetSpeed(2), -1, 0);
            moveRight = new Move(this, this.GetSpeed(2), 1, 0);
            jump = new Jump<IActor>(31);
            director = new SpellDirector(this);
            orientation = ActorOrientation.FacingRight;

            counter = 0;
            mana = 100;
            timer = 0;
            distanceToPlayer = 280;

            random = new Random();
            direction = random.Next(-1, 2);
            steps = random.Next(20, 31);
            this.SetPosition(x, y);
        }

        public Spider(int x, int y) : this("", x, y)
        {
            this.SetPosition(x, y);
        }

        public void AddPlayer(Player player)
        {
            if (player != null)
            {
                myPlayer = player;
            }
        }

        public ActorOrientation GetSide()
        {
            return orientation;
        }

        public override void Update()
        {
            if (this.GetState() is LivingState)
            {
                base.Update();
                if ((Math.Abs(myPlayer.GetX() - this.GetX()) <= distanceToPlayer) && (Math.Abs(myPlayer.GetY() - this.GetY()) <= 60))
                {
                    if (this.IntersectsWithActor(myPlayer))
                    {
                        myPlayer.Die();
                    }
                    if (myPlayer.GetX() - this.GetX() < 0)
                    {
                        if (orientation != ActorOrientation.FacingLeft)
                            enemy.FlipAnimation();
                        moveLeft.Execute();
                        orientation = ActorOrientation.FacingLeft;
                        if (counter >= 30)
                        {
                            Cast(director.Build("Frostball")); 
                            counter = 0;
                        }
                    }
                    else if (myPlayer.GetX() - this.GetX() > 0)
                    {
                        if (orientation != ActorOrientation.FacingRight)
                            enemy.FlipAnimation();
                        moveRight.Execute();
                        orientation = ActorOrientation.FacingRight;
                        if (counter >= 30)
                        {
                            Cast(director.Build("Frostball"));
                            counter = 0;
                        }
                    }
                    timer = 0;
                }
                else
                {
                    if (timer == 0)
                    {
                        direction = random.Next(-1, 2);
                        steps = random.Next(10, 20);
                    }
                    if (direction == -1 || direction == 1)
                    {
                        if (orientation == ActorOrientation.FacingLeft && direction == 1)
                        {
                            enemy.FlipAnimation();
                            orientation = ActorOrientation.FacingRight;
                        }
                        if (orientation == ActorOrientation.FacingRight && direction == -1)
                        {
                            enemy.FlipAnimation();
                            orientation = ActorOrientation.FacingLeft;
                        }
                        if (timer % 2 == 0)
                        {
                            if (direction == -1)
                            {
                                moveLeft.Execute();
                            }
                            else
                            {
                                moveRight.Execute();
                            }
                        }
                        timer = (steps * 2 <= timer) ? -1 : timer;
                    }
                    else
                    {
                        if (timer < steps)
                        {
                            jump.Execute((IActor)this);
                        }
                        else
                        {
                            timer = -1;
                        }
                    }
                    timer++;
                }
                counter++;
            }
        }
        public void ChangeMana(int delta)
        {
            delta = delta / 2;
            if (this.mana + delta < 100)
            {
                if (this.mana + delta >= 0)
                {
                    this.mana += delta;
                }
            }
            else
            {
                this.mana = 100;
            }
        }

        public int GetMana()
        {
            return this.mana;
        }

        public void Cast(ISpell spell)
        {
            if(spell != null)
            spell.Cast();
        }
    }
}
