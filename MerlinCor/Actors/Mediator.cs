using MerlinCor.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public abstract class Mediator: AbstractCharacter
    {
        public abstract void Send(string message, AbstractCharacter player);
    }
}
