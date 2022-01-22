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
    public class GravityOff : AbstractActor, IItem, IUsable
    {
        private bool isUsed;
        private Animation isNotDrunk;
        private Animation isDrunk;
        private int counter;
        private IActor user;
        public GravityOff(string name, int x, int y)
        {
            this.SetName(name);
            isUsed = false;
            isNotDrunk = new Animation("resources/healingpotion2.png", 32, 32);
            isDrunk = new Animation("resources/healingpotion_empty2.png", 32, 32);

            SetAnimation(isNotDrunk);
            isNotDrunk.Start();
            this.SetPosition(x, y);
            counter = 0;
        }
        public override void Update()
        {
            if (isUsed)
            {
                if (counter++ == 300)
                {
                    user.SetPhysics(true);
                }
            }
        }

        public void Use(IActor user)
        {
            if (!isUsed)
            {
                user.SetPhysics(false);
                isUsed = true;
                SetAnimation(isDrunk);
                this.user = user;
            }
        }
    }
}
