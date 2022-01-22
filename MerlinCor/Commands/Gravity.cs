using Merlin2d.Game;
using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Commands
{
    public class Gravity : IPhysics
    {
        private IWorld myWorld;
        private Fall<IActor> fall;
        private int fallSpeed;
        public Gravity()
        {
            fallSpeed = 2;
            fall = new Fall<IActor>(fallSpeed);
        }

        public void Execute()
        {
            List<IActor> actors = myWorld.GetActors()
                .Where(x => x.IsAffectedByPhysics())
                .ToList();

            foreach (IActor actor in actors)
            {
                fall.Execute(actor);
            }
        }

        public void SetWorld(IWorld world)
        {
            myWorld = world;
        }
    }
}

