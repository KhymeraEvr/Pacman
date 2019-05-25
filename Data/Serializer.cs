using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Pacman.Data
{
    static class Serializer
    {
        static XmlSerializer xmlSer = new XmlSerializer(typeof(ScoreEntry));

        public static void Serialize(ScoreEntry se)
        {

            using (FileStream fl = new FileStream("XmlSer.xml", FileMode.OpenOrCreate))
            {

                xmlSer.Serialize(fl, se);


            }
        }
        public static ScoreEntry Deserialize()
        {
            ScoreEntry seRes = new ScoreEntry();
            using (FileStream fl = new FileStream("XmlSer.xml", FileMode.OpenOrCreate))
            {
                seRes = (ScoreEntry)xmlSer.Deserialize(fl);
            }
            return seRes;
        }
    }
}
