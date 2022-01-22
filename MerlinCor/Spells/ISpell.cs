using Merlin2d.Game.Actions;
using MerlinCor.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Spells
{
    public interface ISpell
    {
        ISpell AddEffect(IAction<AbstractCharacter> effect);
        void AddEffects(IEnumerable<IAction<AbstractCharacter>> effects);
        int GetCost();
        void Cast();
    }

}
