using Merlin2d.Game;
using Merlin2d.Game.Actors;
using MerlinCor.Actors;
using MerlinCor.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Factories
{
    class ActorFactory : IFactory
    {

        IActor IFactory.Create(string actorType, string actorName, int x, int y)
        {
            
            IActor player = null;
            if (actorType == "Player")
            {
                player = new Player(actorName, x, y);
            }
            if (actorType == "Skeleton")
            {
                player = new Spider(actorName, x, y);
            }
            if(actorType == "Bridge")
            {
                player = new Bridge(actorName, x, y);
            }
            if (actorType == "Switch")
            {
                player = new Switch(actorName, x, y);
            }
            if (actorType == "PressurePlate")
            {
                player = new PressurePlate(actorName, x, y);
            }
            if (actorType == "HealingPotion")
            {
                player = new HealingPotion(actorName, x, y);
            }
            if (actorType == "Box")
            {
                player = new Box(actorName, x, y);
            }
            if (actorType == "SummonMagic")
            {
                player = new SummonMagic(actorName, x, y);
            }
            if (actorType == "Spikes")
            {
                player = new Spikes(actorName, x, y);
            }
            if (actorType == "Spinner")
            {
                player = new Spinner(actorName, x, y);
            }
            if (actorType == "Door")
            {
                player = new Door(actorName, x, y);
            }
            if (actorType == "Key")
            {
                player = new Key(actorName, x, y);
            }
            if (actorType == "GravityOff")
            {
                player = new GravityOff(actorName, x, y);
            }
            if (actorType == "Autogun")
            {
                player = new Autogun(actorName, x, y);
            }
            return player;
        }
    }
}
