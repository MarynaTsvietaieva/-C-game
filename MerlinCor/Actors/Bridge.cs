using Merlin2d.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public class Bridge : AbstractSwitchable, ISwitchable, IObserver
    {
        private Animation bridgeOn;
        private Animation bridgeOff;

        private bool isOn;
        private bool isPowered;

        public Bridge(string name, int x, int y)
        {
            bridgeOff = new Animation("resources/bridgeOff.png", 490, 70);
            bridgeOn = new Animation("resources/bridgeOn.png", 490, 70);
            SetAnimation(bridgeOn);

            isOn = true;

            this.SetName(name);
            this.SetPosition(x, y);


        }
        public Bridge(int x, int y) : this("", x, y)
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
            for(int number = 0; number < (int)Math.Round((decimal)(this.GetWidth()/ this.GetWorld().GetTileWidth())); number++)
            {
                this.GetWorld().SetWall((int)Math.Round((decimal)(this.GetX()) / (decimal)(this.GetWorld().GetTileWidth())) + number, 
                    (int)Math.Round((decimal)this.GetY() / this.GetWorld().GetTileHeight()), true);
            }
        }

        public override void TurnOn()
        {
            base.TurnOn();
            for (int number = 0; number < (int)Math.Round((decimal)(this.GetWidth() / this.GetWorld().GetTileWidth())); number++)
            {
                this.GetWorld().SetWall((int)Math.Round((decimal)(this.GetX()) / (decimal)(this.GetWorld().GetTileWidth())) + number,
                    (int)Math.Round((decimal)this.GetY() / this.GetWorld().GetTileHeight()), false);
            }
        }

        protected override void UpdateAnimation()
        {
            if (isPowered && isOn)
            {
                SetAnimation(bridgeOn);
            }
            else
            {
                SetAnimation(bridgeOff);
            }
        }
    }
}
