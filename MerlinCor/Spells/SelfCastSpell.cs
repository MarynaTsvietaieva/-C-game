using Merlin2d.Game.Actions;
using Merlin2d.Game.Actors;
using MerlinCor.Actors;
using MerlinCor.Spells.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Spells
{
    public class SelfCastSpell : ISpell
    {
        private IWizard wizard;
        private int cost;
        private IEnumerable<IAction<AbstractCharacter>> effects;
        public SelfCastSpell(IWizard wizard, IEnumerable<IAction<AbstractCharacter>> effects,int cost)
        {
            this.wizard = wizard;
            this.cost = cost;
            this.effects = effects;
        }
        public ISpell AddEffect(IAction<AbstractCharacter> effect)
        {
            ((AbstractCharacter)wizard).AddEffect(effect);
            return this;
        }

        public void AddEffects(IEnumerable<IAction<AbstractCharacter>> effects)
        {
            foreach (IAction<AbstractCharacter> effect in effects)
            {
                AddEffect(effect);
            }
        }

        public void Cast()
        {
            this.AddEffects(effects);
        }

        public int GetCost()
        {
            return cost;
        }
    }
}
