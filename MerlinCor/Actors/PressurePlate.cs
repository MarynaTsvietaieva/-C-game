using Merlin2d.Game;
using Merlin2d.Game.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public class PressurePlate : AbstractSwitchable, ISwitchable, IObservable
    {
        private Animation buttonRed;
        private Animation buttonPressed;
        private bool isPressed;
        private bool isIntersects;

        private List<IObserver> observers = new();
        public PressurePlate(string name, int x, int y)
        {
            buttonRed = new Animation("resources/buttonRed.png", 70, 70);
            buttonPressed = new Animation("resources/buttonRed_pressed.png", 70, 70);
            this.SetName(name);
            SetAnimation(buttonRed);
            this.SetPosition(x, y);
            isPressed = this.IsOn();
        }
        public PressurePlate(int x, int y) : this("", x, y)
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
            isPressed = false;
            observer.Notify(this);
            isPressed = help;
        }
        public override void Update()
        {
            isIntersects = false;
            foreach (IActor actor in this.GetWorld().GetActors())
            {

                if(actor != this && this.IntersectsWithActor(actor))
                {
                    isIntersects = true;
                }
            }
            if (isIntersects != isPressed)
            {
                this.Toggle();
                isPressed = isIntersects;
            }
        }

        public override void TurnOff()
        {
            isPressed = false;
            base.TurnOff();
            foreach (IObserver s in observers)
            { 
                s.Notify(this);
            }
        }

        public override void TurnOn()
        {
            isPressed = true;
            base.TurnOn();
            foreach (IObserver s in observers)
            {
                s.Notify(this);
            }
        }

        protected override void UpdateAnimation()
        {
            if (isPressed)
            {
                SetAnimation(buttonPressed);
            }
            else
            {
                SetAnimation(buttonRed);
            }
        }
    }
}
