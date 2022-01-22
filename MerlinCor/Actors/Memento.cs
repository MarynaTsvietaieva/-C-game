using Merlin2d.Game;
using Merlin2d.Game.Actors;
using Merlin2d.Game.Items;
using MerlinCor.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Actors
{
    public class Memento
    {
        private Backpack backpack;
        private IItem[] items;
        private Dictionary<IActor, int[]> actors; 

        public Memento(IWorld world, Backpack b)
        {
            actors = new Dictionary<IActor, int[]>();
            world.GetActors().ForEach(x => {
                if (x is AbstractCharacter && x is IWizard)
                {
                    actors.Add(x, new[]{x.GetX(), x.GetY(), ((AbstractCharacter)x).GetHealth(), ((IWizard)x).GetMana(), (int)((IWizard)x).GetSide()});
                }
                else if (x is AbstractCharacter)
                {
                    actors.Add(x, new[] { x.GetX(), x.GetY(), ((AbstractCharacter)x).GetHealth()});
                }
                else
                {
                    actors.Add(x, new[] {x.GetX(), x.GetY()});
                }
            });
            items = b.GetItems();
            Backpack = b;
        }

        public Dictionary<IActor, int[]> Actors 
        {
            get { return actors; }
        }

        public Backpack Backpack
        {
            get { return backpack; }
            set { backpack = value; }
        }
        public IItem[] Items
        {
            get {return items;}
        }
    }
}
