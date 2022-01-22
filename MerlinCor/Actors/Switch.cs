using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public class Switch : AbstractSwitchable, ISwitchable, IObservable, IUsable
    {
        private Animation switchOn;
        private Animation switchOff;

        private bool isOn;
        private List<IObserver> observers = new();
        public Switch(string name, int x, int y)
        {
            switchOn = new Animation("resources/laserSwitchGreenOn.png", 70, 70);
            switchOff = new Animation("resources/laserSwitchGreenOff.png", 70, 70);
            this.SetName(name);
            SetAnimation(switchOff);
            this.SetPosition(x, y);
            isOn = this.IsOn();
        }
        public Switch(int x, int y) : this("", x, y)
        {
            this.SetPosition(x, y);
        }

        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
            observer.Notify(this);
        }

        public void Unsubscribe(IObserver observer)
        {
            observers.Remove(observer);
            bool help = this.IsOn();
            isOn = false;
            observer.Notify(this);
            isOn = help;
        }

        public override void TurnOff()
        {
            isOn = false;
            foreach (IObserver s in observers)
            {
                s.Notify(this);
            }
            base.TurnOff();
        }

        public override void TurnOn()
        {
            isOn = true;
            foreach (IObserver s in observers)
            {
                s.Notify(this);
            }
            base.TurnOn();
        }

        protected override void UpdateAnimation()
        {
            if (isOn)
            {
                SetAnimation(switchOn);
            }
            else
            {
                SetAnimation(switchOff);
            }
        }

        public void Use(IActor user)
        {
            if (this.IntersectsWithActor(user))
            {
                this.Toggle();
            }
        }
    }
}
