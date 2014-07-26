using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMP_Gegner_Liste
{
    class Enemy
    {
        public string Username = "";
        public string Reason = "";
        public int TimesKilled = 0;

        public Enemy(string name, string reason, int timeskilled)
        {
            this.Username = name;
            this.Reason = reason;
            this.TimesKilled = timeskilled;
        }
    }
}
