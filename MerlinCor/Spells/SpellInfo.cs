using MerlinCor.Spells.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerlinCor.Spells
{
    public class SpellInfo
    {
        public string Name { get; set; }
        public SpellType SpellType { get; set; }
        public IEnumerable<string> EffectNames { get; set; }
        public string AnimationPath { get; set; }
        public int AnimationWidth { get; set; }
        public int AnimationHeight { get; set; }

        public static implicit operator SpellInfo(string data)
        {
            string[] value = data.Split(";");
            return new SpellInfo
            {
                Name = value[0],
                SpellType = value[1] == "projecttitle" ? SpellType.Projecttitle : SpellType.Selfcast,
                AnimationPath = value[2],
                AnimationWidth = int.Parse(value[3]),
                AnimationHeight = int.Parse(value[4]),
                EffectNames = value[5].Split(",")
            };
        }
    }

}
