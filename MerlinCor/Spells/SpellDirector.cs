using Merlin2d.Game;
using Merlin2d.Game.Actors;
using MerlinCor.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Spells
{
    public class SpellDirector : ISpellDirector
    {
        private Dictionary<string, int> SpellEffects;
        private Dictionary<string, SpellInfo> SpellInfo;

        private ISpellBuilder builder = null;
        private int cost;
        private Animation animation;
        private IWizard wizard;
        private SpellDataProvider dataProvider;
        public SpellDirector(IWizard wizard)
        {
            this.wizard = wizard;
            dataProvider = (SpellDataProvider)SpellDataProvider.GetInstanse();
            SpellEffects = dataProvider.GetSpellEffects();
            SpellInfo = dataProvider.GetSpellInfo();
        }
    public ISpell Build(string spellName) {

            this.cost = 0;
            animation = null;

            if (SpellInfo[spellName].SpellType == SpellType.Projecttitle)
            {
                builder = new ProjectileSpellBuilder(this.wizard);
            }
            else
            {
                builder = new SelfCastSpellBuilder(this.wizard);
            }

            foreach (var x in SpellInfo[spellName].EffectNames)
            {
                builder = builder.AddEffect(x);
                cost += SpellEffects[x];
            }

            if (SpellInfo[spellName].AnimationPath != "")
            {
                
                try
                {
                    animation = new Animation(SpellInfo[spellName].AnimationPath, SpellInfo[spellName].AnimationWidth, SpellInfo[spellName].AnimationHeight);
                    builder = builder.SetAnimation(animation);
                }
                catch
                {
                }
            }
            builder = builder.SetSpellCost(this.cost);
            if (builder != null) {
                return builder.CreateSpell();
            }
            else
            {
                return null;
            }
        }
}
}