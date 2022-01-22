using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;
using MerlinCor.Spells;
using MerlinCor.Spells.Effects;
using MerlinCor.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MerlinCor.Actors
{
    public abstract class AbstractCharacter : AbstractActor, ICharacter
    {
        private int health;
        private ISpeedStrategy strategy;
        private List<IAction<AbstractCharacter>> spellEffects = new List<IAction<AbstractCharacter>>();
        private List<IAction<AbstractCharacter>> EffectsToRemove = new List<IAction<AbstractCharacter>>();
        private AbstractState state;
        public AbstractCharacter()
        {
            this.health = 100;
            this.state = new LivingState();

        }
        public void AddEffect(IAction<AbstractCharacter> effect)
        {
            spellEffects.Add(effect);
        }

        public void ChangeHealth(int delta)
        {
            if (this.health + delta < 100)
            {
                if (this.health + delta > 0) {
                    this.health += delta;
                }
                else
                {
                    this.Die();
                }
            }
            else
            {
                this.health = 100;
            }
        }

        public void Die()
        {
            this.health = 0;
            this.state.StateChange(this);
            this.state = new DyingState();
            this.state.StateChange(this);

        }

        public AbstractState GetState()
        {
            return this.state;
        }
        public int GetHealth()
        {
            return this.health;
        }

        public double GetSpeed(double speed)
        {
            return this.strategy.GetSpeed(speed);
        }

        public void RemoveEffect(IAction<AbstractCharacter> effect)
        {
            EffectsToRemove.Add(effect);
        }

        private void RemoveEffects(List<IAction<AbstractCharacter>> effectsToRemove)
        {
            foreach(var effect in effectsToRemove)
            {
                spellEffects.Remove(effect);
            }
        }
        public void SetSpeedStrategy(ISpeedStrategy strategy)
        {
            this.strategy = strategy;
        }

        public override void Update()
        {
            this.state.StateChange(this);

            foreach (var effect in spellEffects)
            {
                effect.Execute(this);
            }
            RemoveEffects(EffectsToRemove);
        }
    }
}
