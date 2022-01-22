using MerlinCor.Commands;
using Merlin2d.Game;
using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MerlinCor.Strategies;
using MerlinCor.Spells.Effects;
using MerlinCor.Spells;
using Merlin2d.Game.Items;
using MerlinCor.Item;

namespace MerlinCor.Actors
{
    public class Player : Mediator, IMovable, IWizard
    {
        private Animation player;

        private Move moveLeft;
        private Move moveRight;
        private Jump<IActor> jump;
        private double speed = 2;
        private SpellDirector director;
        private int mana;
        private ActorOrientation orientation;
        private Backpack backpack;
        private ProspectMemory m;
        private ConcreteMediator mediator;
        private int counter;

        public Player(string name, int x, int y)
        {
            player = new Animation("resources/pink_alien2.png", 49, 67);
            this.SetName(name);
            SetAnimation(player);
            player.Start();
            this.AddEffect(new Regeneration<AbstractCharacter>());
            this.SetSpeedStrategy(new NormalSpeedStrategy());
            moveLeft = new Move(this, this.GetSpeed(speed), -1, 0);
            moveRight = new Move(this, this.GetSpeed(speed), 1, 0);
            jump = new Jump<IActor>(31);
            this.SetPosition(x, y);
            director = new SpellDirector(this);
            orientation = ActorOrientation.FacingRight;
            this.mana = 100;
            backpack = new Backpack(4, this);

        }
        public Player(int x, int y) : this("", x, y)
        {
            this.SetPosition(x, y);
        }

        public ActorOrientation GetSide()
        {
            return orientation;
        }

        public ConcreteMediator GetMediator()
        {
            return mediator;
        }

        public void ChangeMana(int delta)
        {
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

        public Memento SaveMemento()
        {
            return new Memento(this.GetWorld(), backpack);
        }
        public void RestoreMemento(Memento memento)
        {
            foreach (KeyValuePair<IActor, int[]> x in memento.Actors)
            {
                x.Key.SetPosition(x.Value[0], x.Value[1]);
                if (x.Value.Length >= 3)
                {
                    ((AbstractCharacter)x.Key).ChangeHealth(-((AbstractCharacter)x.Key).GetHealth() + x.Value[2]);
                    if (x.Value.Length == 5)
                    {
                        ((IWizard)x.Key).ChangeMana(-((IWizard)x.Key).GetMana() + x.Value[3]);
                    }
                }
            }
            backpack.SetItems(memento.Items);
        }

        public override void Update()
        {
            if (this.GetState() is LivingState) {
                base.Update();
                foreach (var box in this.GetWorld().GetActors().FindAll(x => x is Box))
                {
                    if (this.IntersectsWithActor(box))
                    {
                        moveLeft = new Move(this, this.GetSpeed(speed), -1, 0);
                        moveRight = new Move(this, this.GetSpeed(speed), 1, 0);
                    }
                }
                this.SetPosition(this.GetX(), this.GetY() + 2);
                if (this.GetWorld().IntersectWithWall(this))
                {
                    moveLeft = new Move(this, this.GetSpeed(speed), -1, 0);
                    moveRight = new Move(this, this.GetSpeed(speed), 1, 0);
                }
                this.SetPosition(this.GetX(), this.GetY() - 2);
                if (Input.GetInstance().IsKeyDown(Input.Key.A))
                {
                    if (orientation != ActorOrientation.FacingLeft)
                        player.FlipAnimation();
                    moveLeft.Execute();
                    orientation = ActorOrientation.FacingLeft;
                }
                if (Input.GetInstance().IsKeyDown(Input.Key.D))
                {
                    if (orientation != ActorOrientation.FacingRight)
                        player.FlipAnimation();
                    moveRight.Execute();
                    orientation = ActorOrientation.FacingRight;
                }
                if (Input.GetInstance().IsKeyDown(Input.Key.W))
                {
                    jump.Execute((IActor)this);
                }


                if (Input.GetInstance().IsKeyPressed(Input.Key.ONE))
                {
                    Cast(director.Build("Fireball"));
                }

                if (Input.GetInstance().IsKeyPressed(Input.Key.TWO))
                {
                    Cast(director.Build("Frostball"));
                }
                if (Input.GetInstance().IsKeyPressed(Input.Key.THREE))
                {
                    Cast(director.Build("SuperHeal"));
                }
                if (Input.GetInstance().IsKeyPressed(Input.Key.FOUR))
                {
                    Cast(director.Build("Heal"));
                }

                if (Input.GetInstance().IsKeyPressed(Input.Key.FIVE))
                {
                    Cast(director.Build("FreezeBall"));
                }
                if (Input.GetInstance().IsKeyPressed(Input.Key.S))
                {
                    foreach (IActor item in this.GetWorld().GetActors())
                    {
                        if (this.IntersectsWithActor(item))
                        {
                            if (item is IUsable)
                            {
                                if (item is SummonMagic)
                                {
                                    mediator = new ConcreteMediator();
                                    counter = 0;
                                }
                                ((IUsable)item).Use(this);
                            }
                        }
                    }
                }
                if (Input.GetInstance().IsKeyPressed(Input.Key.UP)) 
                {
                    foreach (IActor item in this.GetWorld().GetActors())
                    {
                        if (this.IntersectsWithActor(item) && item is IItem)
                        {
                            if (item is IUsable)
                            {
                                backpack.AddItem((IItem)item);
                            }
                        }
                    }
                }

                if (Input.GetInstance().IsKeyPressed(Input.Key.DOWN))
                {
                    backpack.GetItem();
                }

                if (Input.GetInstance().IsKeyPressed(Input.Key.LEFT))
                {
                    backpack.ShiftLeft();
                }


                if (Input.GetInstance().IsKeyPressed(Input.Key.RIGHT))
                {
                    backpack.ShiftRight();
                }


                if (Input.GetInstance().IsKeyPressed(Input.Key.INSERT))
                {
                    m = new ProspectMemory();
                    m.Memento = this.SaveMemento();
                }

                if (Input.GetInstance().IsKeyPressed(Input.Key.SCROLL_LOCK))
                {
                    if (m != null)
                    {
                        this.RestoreMemento(m.Memento);
                    }
                }

                if(mediator != null)
                {
                    Send(this.GetX().ToString() + "," + this.GetY().ToString(), this);
                    if(counter++ == 600)
                    {
                        mediator = null;
                    }
                }
            }
        }

        public override void Send(string message, AbstractCharacter player)
        {
            mediator.Send(message, this);
        }
    }
}

