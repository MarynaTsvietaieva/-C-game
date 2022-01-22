using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Spells
{
    public class SpellDataProvider : ISpellDataProvider
    {
        private class SpellEffect
        {
            public string Name { get; set; }
            public int Cost { get; set; }
        }
        private Dictionary<string, int> SpellEffects = null;
        private Dictionary<string, SpellInfo> SpellInfo = null;
        private static SpellDataProvider data;

        private SpellDataProvider()
        {
            SpellEffects = GetSpellEffects();
            SpellInfo = GetSpellInfo();
        }
        public Dictionary<string, int> GetSpellEffects()
        {
            if (SpellEffects == null)
            {
                this.LoadSpellEffects();
            }
            return SpellEffects;
    }
        public Dictionary<string, SpellInfo> GetSpellInfo()
        {
            if(SpellInfo == null)
            {
                this.LoadSpellInfo();
            }
            return SpellInfo;
        }

        private void LoadSpellEffects()
        {
            SpellEffects = new Dictionary<string, int>();
            string json = File.ReadAllText("resources/effects.json");
            List<SpellEffect> parsed = JsonConvert.DeserializeObject<List<SpellEffect>>(json);

            foreach (SpellEffect effect in parsed)
            {

                try
                {
                    SpellEffects.Add(effect.Name, effect.Cost);
                }
                catch(ArgumentException e)
                {
                    Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
                }

            }
        }

        private void LoadSpellInfo()
        {
            SpellInfo = new Dictionary<string, SpellInfo>();
            string[] lines = File.ReadAllLines("resources/Spells.csv");
            foreach (string line in lines)
            {
                try
                {
                    SpellInfo.Add(((SpellInfo)line).Name, line);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
                }
            }
        }


        public static ISpellDataProvider GetInstanse()
        {

            if (data == null)
            {
                data = new SpellDataProvider();
            }
            return data;
        }
    }
}
