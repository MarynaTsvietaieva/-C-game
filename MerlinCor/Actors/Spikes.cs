using Merlin2d.Game;
using Merlin2d.Game.Actors;
using MerlinCor.Commands;
using MerlinCor.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public class Spikes: AbstractActor
    {
        private Animation spikes;
        private int counter;
        public Spikes(string name, int x, int y)
        {
            this.SetName(name);
            spikes = new Animation("resources/spikes2.png", 70, 70);
            this.SetAnimation(spikes);
            spikes.Start();
            this.SetPosition(x,y);
        }

        public override void Update()
        {
            if (counter++ == 4)
            {
                foreach (IActor actor in this.GetWorld().GetActors())
                {
                    if (this.IntersectsWithActor(actor) && actor is AbstractCharacter)
                    {
                        ((AbstractCharacter)actor).ChangeHealth(-1);
                    }
                }
                counter = 0;
            }
        }
    }
}
