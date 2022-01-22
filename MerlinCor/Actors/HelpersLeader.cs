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
    public class HelpersLeader : Mediator, IMovable, IWizard
    {
        private Animation helper;
        private int counter;
        private int mana;
        private ActorOrientation orientation;
        private SpellDirector director;
        public HelpersLeader(string name, int x, int y, Player player, ConcreteMediator mediator)
        {
            (this.Name, this.X, this.Y, this.Player, this.Mediator) = (name, x, y, player, mediator);
            this.X += this.Player.GetWidth();
            helper = new Animation("resources/helper2.png", 49, 67);
            this.SetAnimation(helper);
            helper.Start();
            this.SetPhysics(false);
            this.SetPosition(this.X, this.Y);
            this.Player.GetWorld().AddActor(this);
            this.SetSpeedStrategy(new NormalSpeedStrategy());
            orientation = (ActorOrientation)1;
            mana = 100;
            director = new SpellDirector(this);
        }
        public Player Player { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Name { get; set; }
        public ConcreteMediator Mediator { get; set; }
        public HelpersLeader Helper2 { get; set; }

        public void Cast(ISpell spell)
        {
            if (spell != null)
                spell.Cast();
        }

        public void ChangeMana(int delta)
        {
        }

        public HelpersLeader GetClone()
        {
            return (HelpersLeader)this.MemberwiseClone();
        }

        public int GetMana()
        {
            return mana;
        }

        public ActorOrientation GetSide()
        {
            if(Helper2 == null)
            {
                return 0;
            }
            return orientation;
        }

        public void Notify(int x, int y)
        {
            this.SetPosition(x, y);
        }

        public override void Send(string message, AbstractCharacter player)
        {
            Mediator.Send(message, this);
        }

        public override void Update()
        {
            if(counter++ == 600)
            {
                this.GetWorld().RemoveActor(this);
            }
            if (counter%30 == 0)
            {
                Cast(director.Build("FreezeBall"));
                Send("", this);
            }

        }
    }
}
