using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public abstract class AbstractSwitchable : AbstractActor, ISwitchable
    {
        private bool isOn;

        public AbstractSwitchable()
        {
            isOn = false;
        }
        public virtual bool IsOn()
        {
            return isOn;
        }

        public void Toggle()
        {
            isOn = !isOn;
            if (isOn)
            {
                this.TurnOn();
            }
            else
            {
                this.TurnOff();
            }
        }

        public virtual void TurnOff()
        {
            isOn = false;
            this.UpdateAnimation();
        }

        public virtual void TurnOn()
        {
            isOn = true;
            this.UpdateAnimation();
        }

        public override void Update() { }

        protected abstract void UpdateAnimation();
    }
}
