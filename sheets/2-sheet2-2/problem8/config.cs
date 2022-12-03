using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace problem8
{
    [Serializable]
    internal class config
    {

        public config(string ConnectionString ,
            bool TF_EXCLUDE_ID_CAL_GPA,int RLValue
            )
        {
            this.ConnectionString = ConnectionString;
            this.TF_EXCLUDE_ID_CAL_GPA = TF_EXCLUDE_ID_CAL_GPA;
            this.RLValue = RLValue;

        }
        public string ConnectionString { get; set; }
        public bool TF_EXCLUDE_ID_CAL_GPA { get; set; }
        public DateTime date { get; set; }
        public int RLValue { get; set; }
        



    }
}
