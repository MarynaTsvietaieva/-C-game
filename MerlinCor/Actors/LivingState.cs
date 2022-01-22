using Merlin2d.Game;
using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public class LivingState : AbstractState
    {
        private Message msg;
        private int x;
        private int y;
        public override void StateChange(ICharacter actor)
        {
            x = ((IActor)actor).GetX();
            y = ((IActor)actor).GetY();
            ((IActor)actor).GetWorld().RemoveMessage(msg);
            if (actor.GetHealth() != 0)
            {
                if (actor is IWizard)
                {
                    msg = new Message(actor.GetHealth() + "/" + ((IWizard)actor).GetMana(), x, y- ((IActor)actor).GetHeight());
                }
                else
                {
                    msg = new Message(actor.GetHealth() + " ", x, y- ((IActor)actor).GetHeight());
                }
                ((IActor)actor).GetWorld().AddMessage(msg);
            }
        }
    }
}
