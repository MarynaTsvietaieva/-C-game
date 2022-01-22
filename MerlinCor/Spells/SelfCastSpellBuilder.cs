using Merlin2d.Game;
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
    class SelfCastSpellBuilder : ISpellBuilder
    {
        private ISpell spell;
        private List<IAction<AbstractCharacter>> spellEffects = new();
        private int cost;
        private IWizard wizard;
        public SelfCastSpellBuilder(IWizard wizard)
        {
            this.wizard = wizard;
        }
        public ISpellBuilder AddEffect(string effectName)
        {
            string[] effect = effectName.Split("-");
            if (effect[0] == "Acceleration")
            {
                spellEffects.Add(new Acceleration<AbstractCharacter>());
            }
            else if (effect[0] == "Deceleration")
            {
                spellEffects.Add(new Deceleration<AbstractCharacter>());
            }
            else if (effect[0] == "Damage")
            {
                spellEffects.Add(new Damage<AbstractCharacter>(int.Parse(effect[1])));
            }
            else if (effect[0] == "DoTEffect")
            {
                spellEffects.Add(new DoTEffect<AbstractCharacter>(int.Parse(effect[1]), int.Parse(effect[2])));
            }
            else if (effect[0] == "Regeneration")
            {
                spellEffects.Add(new Regeneration<AbstractCharacter>(int.Parse(effect[1])));
            }
            return this;
        }

        public ISpell CreateSpell()
        {
            spell = new SelfCastSpell(wizard, spellEffects,this.cost);
            return spell;
        }

        public ISpellBuilder SetAnimation(Animation animation)
        {
            return this;
        }

        public ISpellBuilder SetSpellCost(int cost)
        {
            this.cost = (int)(0.7 * cost);

            if (wizard.GetMana() < this.cost)
            {
                new Exception();
            }
            else
            {
                wizard.ChangeMana(-this.cost);
            }
            return this;
        }
    }
}
