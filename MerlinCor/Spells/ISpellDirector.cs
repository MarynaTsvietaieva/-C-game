using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Spells
{
    public interface ISpellDirector
    {
        ISpell Build(string spellName);
    }

}
