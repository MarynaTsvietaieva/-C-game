using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Items;
using MerlinCor.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Item
{
    class SummonMagic : AbstractActor, IItem, IUsable
    {
        private Animation summonMagic;
        private HelpersLeader helpersLeader;
        private Helper2 helper2;
        private Helper3 helper3;
        private ConcreteMediator mediator;

        public SummonMagic(string name, int x, int y)
        {
            this.SetName(name);
            summonMagic = new Animation("resources/ice-summon2.png", 60, 60);
            SetAnimation(summonMagic);
            summonMagic.Start();
            this.SetPosition(x, y);
        }
        public void Use(IActor user)
        {
            mediator = ((Player)user).GetMediator();
            helpersLeader = new HelpersLeader("helpersLeader", user.GetX(), user.GetY(), (Player)user, mediator);
            helper2 = (new Helper2(helpersLeader)).SetHelper2;
            helper3 = (new Helper3(helpersLeader)).SetHelper3;
            this.GetWorld().RemoveActor(this);
            mediator.Player = (Player)user;
            mediator.HelpersLeader = helpersLeader;
            mediator.Helper2 = helper2;
            mediator.Helper3 = helper3;

        }

        public override void Update() { }
    }
}
