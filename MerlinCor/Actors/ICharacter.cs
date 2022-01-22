using Merlin2d.Game.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MerlinCor.Actors
{
    public interface ICharacter: IMovable
    {
        void ChangeHealth(int delta);
        int GetHealth();
        void Die();
        void AddEffect(IAction<AbstractCharacter> effect);
        void RemoveEffect(IAction<AbstractCharacter> effect);

    }
}
