using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BinarySerializationExtraInfo
{
    [Serializable]
    public class emp
    {
       // [NonSerialized]
        public int id;
        public string Name { get; set; }
        public List<string> Benefits { get; set; }=new List<string>(){ };

        public override string ToString()
        {
            Console.WriteLine($"{id}  , {Name}");
            foreach (var it in Benefits)
                Console.Write($"{it} ");
            return null;

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var e1 = new emp()
            {
                Name = "abdo",
                id = 1,
                Benefits = new List<string>() { "persion", "healthey " }
            };
             
            using (FileStream m = new FileStream("text.txt",FileMode.Create))
            {
                BinaryFormatter bn = new BinaryFormatter();
                bn.Serialize(m, e1); 
            }
            Console.WriteLine(e1.ToString());

            e1 = null;
            using (FileStream m = new FileStream("text.txt", FileMode.Open))
            {
                BinaryFormatter bn = new BinaryFormatter();
               e1= bn.Deserialize(m)as emp;
            }
            Console.WriteLine(e1.ToString());

        }


    }
        
    
}
