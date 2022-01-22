using Merlin2d.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public class Door : AbstractSwitchable,  ISwitchable, IObserver
    {
        private Animation door;
        private Animation doorOn;

        private bool isPowered;

        public Door(string name, int x, int y)
        {
            if(name == "WinterDoor")
            {
                door = new Animation("resources/winterDoorOn.png", 70, 70);
            }
            else
            {
                door = new Animation("resources/Door.png", 70, 70);
                doorOn = new Animation("resources/Dooroff.png", 70, 70);
            }

            SetAnimation(door);

            this.SetName(name);
            this.SetPosition(x, y);
        }

        public Door(int x, int y) : this("", x, y)
        {
            this.SetPosition(x, y);
        }

        public void Notify(IObservable observable)
        {
            if (observable is Switch)
            {
                isPowered = ((Switch)observable).IsOn();
                this.Toggle();
            }
            if (observable is PressurePlate)
            {
                isPowered = ((PressurePlate)observable).IsOn();
                this.Toggle();
            }
        }

        public override void TurnOff()
        {
            base.TurnOff();
            for (int number = 0; number < (int)Math.Round((decimal)(this.GetWidth() / this.GetWorld().GetTileWidth())); number++)
            {
                this.GetWorld().SetWall((int)Math.Round((decimal)(this.GetX()) / (decimal)(this.GetWorld().GetTileWidth())) + number,
                    (int)Math.Round((decimal)this.GetY() / this.GetWorld().GetTileHeight()), false);
            }   
        }

        public override void TurnOn()
        {
            base.TurnOn();
            for (int number = 0; number < (int)Math.Round((decimal)(this.GetWidth() / this.GetWorld().GetTileWidth())); number++)
            {
                this.GetWorld().SetWall((int)Math.Round((decimal)(this.GetX()) / (decimal)(this.GetWorld().GetTileWidth())) + number,
                    (int)Math.Round((decimal)this.GetY() / this.GetWorld().GetTileHeight()), true);
            }
        }

        protected override void UpdateAnimation()
        {
            if (isPowered)
            {
                SetAnimation(doorOn);
            }
            else
            {
                SetAnimation(door);
            }
        }
    }
}
