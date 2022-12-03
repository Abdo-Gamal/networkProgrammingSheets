using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ex1S9Serlization_persionclass__and_deserial
{
    class Client
    {
        public static void Main()
        {
            Person p = new Person("Peter", new Date(1936, 5, 11));
            p.Died(new Date(2007, 5, 10));
            Console.WriteLine("{0}", p);
            using (FileStream strm = new FileStream("person.dat", FileMode.Create))
            {
                IFormatter fmt = new BinaryFormatter();
                fmt.Serialize(strm, p);
            }
            // -----------------------------------------------------------
            p = null;
            Console.WriteLine("Reseting person");
            // -----------------------------------------------------------
            using (FileStream strm = new FileStream("person.dat", FileMode.Open))
            {
                IFormatter fmt = new BinaryFormatter();
                p = fmt.Deserialize(strm) as Person;
            }
            Console.WriteLine("{0}", p);
        }
    }
}
