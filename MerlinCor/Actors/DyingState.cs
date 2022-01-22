using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public class DyingState : AbstractState
    {
        private Animation spider = new Animation("resources/spider_dead.png", 69 ,51 );
        private Animation player = new Animation("resources/alienPinkDie.png", 69, 92);
        private Message msg;
        public override void StateChange(ICharacter actor)
        {
            if (actor is Spider) {
                ((IActor)actor).SetAnimation(spider);
                msg = new Message("died(", ((IActor)actor).GetX(),((IActor)actor).GetY(), 20 , Color.Red, MessageDuration.Long);
                ((IActor)actor).GetWorld().AddMessage(msg);
            }
            if (actor is Player)
            {
                ((IActor)actor).SetAnimation(player);
                msg = new Message("died(", ((IActor)actor).GetX(), ((IActor)actor).GetY(), 20, Color.Red, MessageDuration.Long);
                ((IActor)actor).GetWorld().AddMessage(msg);
            }
        }
    }
}
