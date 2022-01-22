using MerlinCor.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{

    public class ConcreteMediator : Mediator
    {
        private Player player;
        private HelpersLeader helpersLeader;
        private Helper2 helper2;
        private Helper3 helper3;


        public Player Player
        {
            set { player = value; }
        }
        public HelpersLeader HelpersLeader
        {
            set { helpersLeader = value; }
        }
        public Helper2 Helper2
        {
            set { helper2 = value; }
        }
        public Helper3 Helper3
        {
            set { helper3 = value; }
        }

        public override void Send(string message, AbstractCharacter player)
        {
            if (player == this.player)
            {
                string[] m = message.Split(",");
                if (m.Length == 2)
                {
                    helpersLeader.Notify(int.Parse(m[0]) + player.GetWidth(), int.Parse(m[1]));
                    helper2.Notify(int.Parse(m[0]) - player.GetWidth(), int.Parse(m[1]));
                    helper3.Notify(int.Parse(m[0]), int.Parse(m[1]) - player.GetHeight());
                }
            }
            if (player == this.helpersLeader)
            {
                helper2.Notify();
                helper3.Notify();
            }
        }
    }
}
