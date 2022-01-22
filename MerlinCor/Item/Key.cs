using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Enums;
using Merlin2d.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public class Key : AbstractActor, IItem, IUsable
    {
        private Animation key;
        private Door door;
        public Key(string name, int x, int y)
        {
            key = new Animation("resources/keyBlue.png", 70, 70);
            SetAnimation(key);

            this.SetName(name);
            this.SetPosition(x, y);
        }

        public void AddDoor(Door d)
        {
            door = d;
        }
        public override void Update()
        {
        }

        public void Use(IActor user)
        {
            if (door.GetX() - this.GetX() < 70 && door.GetY() - this.GetY() < 70)
            {
                door.TurnOff();
                user.GetWorld().RemoveActor(this);
                user.GetWorld().RemoveActor(door);
            }
        }
    }
}
