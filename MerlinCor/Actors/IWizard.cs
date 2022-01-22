using Merlin2d.Game.Actors;
using MerlinCor.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public interface IWizard : IActor
{
    void ChangeMana(int delta);
    int GetMana();
    void Cast(ISpell spell);
    ActorOrientation GetSide();
    }

}
