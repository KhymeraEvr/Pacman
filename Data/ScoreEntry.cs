using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Data
{
    [Serializable]
    public class ScoreEntry
    {
        public ScoreEntry()
        {  
        }

        public ScoreEntry(string name, int score)
        {
            Name = name;
            Quant = score;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int MyProperty { get; set; }
        public int Quant { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, Quant);
        }
    }
}
