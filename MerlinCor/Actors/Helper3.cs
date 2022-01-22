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
    public class Helper3 : Mediator
    {
        private HelpersLeader helper3;
        private Animation helper;
        private SpellDirector director;
        public Helper3(HelpersLeader helpersLeader)
        {
            helper3 = helpersLeader.GetClone();
            helper3.Name = "helper2";
            helper3.Y -= helper3.Player.GetHeight();
            helper3.X -= helper3.Player.GetWidth();
            helper = new Animation("resources/helper3.png", 47, 47);
            helper3.SetAnimation(helper);
            helper.Start();
            helper3.SetPhysics(false);
            helper3.SetPosition(helper3.X, helper3.Y);
            helper3.Player.GetWorld().AddActor(helper3);
            helper3.SetSpeedStrategy(new NormalSpeedStrategy());
            this.Helper = helper3;
            director = new SpellDirector(helper3);
        }
        public HelpersLeader Helper { get; set; }
        public Helper3 SetHelper3
        {
            get { return this; }
        }

        public void Cast(ISpell spell)
        {
          if (spell != null)
            spell.Cast();
        }

        public void Notify(int x, int y)
        {
            helper3.SetPosition(x, y);
        }
        public void Notify()
        {
            Cast(director.Build("FreezeBall"));
        }

        public override void Send(string message, AbstractCharacter player)
        {
            helper3.Mediator.Send(message, helper3);
        }

        public override void Update()
        {
        }
    }
}
