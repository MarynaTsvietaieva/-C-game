using Merlin2d.Game.Actors;
using Merlin2d.Game.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor
{
    public class Backpack : IInventory
    {
        private IItem[] items;
        private IItem[] itemsCopy;
        private List<IItem> itemsForSave;
        private int position = 0;
        private int capacity;
        private IActor owner;
        private int index;

        public Backpack(int c, IActor owner)
        {
            items = new IItem[c];
            itemsCopy = new IItem[c];
            this.capacity = c;
            this.owner = owner;
            itemsForSave = new();
        }


        public void AddItem(IItem item)
        {
            if (position < this.capacity)
            {
                items[position] = item;
                itemsForSave.Add(item);
                item.SetPosition(140 * position, 770);
                position++;
            }
        }

        public int GetCapacity()
        {
            return capacity;
        }

        public IItem[] GetItems()
        {
            itemsForSave = new();
            IItem[] itemsForset = new IItem[capacity];
            Array.Copy(items, 0, itemsForset, 0, items.Length);
            return itemsForset;
        }

        public void SetItems(IItem[] itemsToReturn)
        {

            index = 0;
            foreach(IItem item in itemsToReturn)
            {
                if (item != null)
                {
                    this.items[index] = item;
                    index++;
                }
            }
            position = index;
            while (index < this.capacity)
            {
                this.items[index] = null;
                index++;
            }
        }
        public IEnumerator<IItem> GetEnumerator()
        {
            foreach (IItem item in items)
            {
                if(item == null)
                {
                    break;
                }
                else
                {
                    yield return item;
                }
            }
        }

        public IItem GetItem()
        {
            if (items[0] == null) return null;
            items[0].SetPosition(owner.GetX() + owner.GetWidth(), owner.GetY());
            position--;
            RemoveItem(0);
            return items[0];
        }

        public void RemoveItem(IItem item)
        {
            items = items.Where(val => val != item).ToArray();
            items = items.Concat(new IItem[1]).ToArray();
            int p = 0;
            foreach (var i in items)
            {
                if (i == null) break;
                i.SetPosition(140 * p, 770);
                p++;
            }
        }

        public void RemoveItem(int index)
        {
            RemoveItem(items[index]);
        }

        public void ShiftLeft()
        {
            if (items[1] != null)
            {
                IEnumerator<IItem> enumerator = GetEnumerator();
                enumerator.MoveNext();
                IItem lastItem = enumerator.Current;
                index = 0;
                while (enumerator.MoveNext())
                {
                    items[index] = enumerator.Current;
                    items[index].SetPosition(140 * index, 770);
                    index++;
                }
                items[index] = lastItem;
                items[index].SetPosition(140 * index, 770);
            }
        }

        public void ShiftRight()
        {
            if (items[1] != null) {
                itemsCopy = new IItem[capacity];
                IEnumerator<IItem> enumerator = GetEnumerator();
                index = 1;
                while (enumerator.MoveNext())
                {
                    if (index == capacity)
                    {
                        index = 0;
                    }
                    itemsCopy[index] = enumerator.Current;
                    itemsCopy[index].SetPosition(140 * index, 770);
                    index++;
                }
                if (itemsCopy[0] == null)
                {
                    index--;
                    itemsCopy[0] = itemsCopy[index];
                    itemsCopy[0].SetPosition(0, 770);
                    itemsCopy[index] = null;
                }
                items = itemsCopy;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
