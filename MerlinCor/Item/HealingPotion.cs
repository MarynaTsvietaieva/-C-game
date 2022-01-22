using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Items;
using MerlinCor.Actors;
using MerlinCor.Spells.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Item
{
    public class HealingPotion : AbstractActor, IItem, IUsable
    {
        private bool isUsed;
        private Animation isNotDrunk;
        private Animation isDrunk;

        public HealingPotion(string name, int x, int y)
        {
            this.SetName(name);
            isUsed = false;
            isNotDrunk = new Animation("resources/healingpotion2.png", 32, 32);
            isDrunk = new Animation("resources/healingpotion_empty2.png", 32, 32);

            SetAnimation(isNotDrunk);
            isNotDrunk.Start();
            this.SetPosition(x, y);
        }
        public HealingPotion(int x, int y) : this("", x, y) { }
   
        public void Use(IActor user)
        {
            if (!isUsed)
            {
                ((AbstractCharacter)user).AddEffect(new Regeneration<AbstractCharacter>(50));
                isUsed = true;
                SetAnimation(isDrunk);
            }
        }
        public override void Update() { }
    }
}
