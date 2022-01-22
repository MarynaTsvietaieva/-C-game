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
    public class Helper2: Mediator
    {
        private HelpersLeader helper2;
        private Animation helper;
        private int counter;
        private SpellDirector director;
        private int mana;
        private ActorOrientation orientation;
        public Helper2(HelpersLeader helpersLeader)
        {
            helper2 = helpersLeader.GetClone();
            helper2.Name = "helper2";
            helper2.X -= 2*helper2.Player.GetWidth();
            helper = new Animation("resources/helper2.png", 49, 67);
            helper.FlipAnimation();
            helper2.SetAnimation(helper);
            helper.Start();
            helper2.SetPhysics(false);
            helper2.SetPosition(helper2.X, helper2.Y);
            helper2.Player.GetWorld().AddActor(helper2);
            helper2.SetSpeedStrategy(new NormalSpeedStrategy());
            director = new SpellDirector(helper2);
            counter = 0;
            mana = 100;
            orientation = 0;
            this.Helper = helper2;
            helpersLeader.Helper2 = helper2;
        }

        public HelpersLeader Helper { get; set; }

        public Helper2 SetHelper2
        {
            get {return this;}
        }

        public void Cast(ISpell spell)
        {
            if (spell != null)
                spell.Cast();
        }

        public void Notify(int x, int y)
        {
            helper2.SetPosition(x, y);
        }

        public void Notify()
        {
            Cast(director.Build("FreezeBall"));
        }

        public override void Send(string message, AbstractCharacter player)
        {
            helper2.Mediator.Send(message, helper2);
        }

        public override void Update()
        {
        }
    }
}
