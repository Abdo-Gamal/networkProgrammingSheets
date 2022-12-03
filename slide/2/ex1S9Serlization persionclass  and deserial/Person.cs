using System;
using System.Runtime.Serialization;

namespace ex1S9Serlization_persionclass__and_deserial
{
    [Serializable]
    public class Person
    {
        [NonSerialized]
        private string name;
        private int age; // Redundant
        private Date dateOfBirth, dateOfDeath;
        public Person(string name, Date dateOfBirth)
        {
            this.name = name;
            this.dateOfBirth = dateOfBirth;
            this.dateOfDeath = null;
            age = Date.Today.YearDiff(dateOfBirth);
        }
        public Date DateOfBirth
        {
            get { return new Date(dateOfBirth); }
        }
        public int Age
        {
            get { return Alive ? age : dateOfDeath.YearDiff(dateOfBirth); }
        }
        public bool Alive
        {
            get { return dateOfDeath == null; }
        }
        public void Died(Date d)
        {
            dateOfDeath = d;
        }
        public void Update()
        {
            age = Date.Today.YearDiff(dateOfBirth);
        }
        public override string ToString()// call if print object from person
        {
            return "Person: " + name + " *" + dateOfBirth + (Alive ? "" : " +" + dateOfDeath) +
            " Age: " + Age;
        }




        [OnSerializing()]
        internal void OnSerializingMethod(StreamingContext context)
        {
            Console.WriteLine("serializing . . . .");
        }
        [
      OnSerialized()]
        internal void OnSerializedMethod(StreamingContext context)
        {
            Console.WriteLine("Done !");
        }
        [
      OnDeserializing()]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            Console.WriteLine("deserializing . . . .");
        }
        [
      OnDeserialized()]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            Console.WriteLine("obj Ready");
        }
    }
}