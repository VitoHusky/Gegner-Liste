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
        public int UserID = -1;

        public Enemy(string name, string reason, int timeskilled)
        {
            this.Username = name;
            this.Reason = reason;
            this.TimesKilled = timeskilled;
        }
        public Enemy(Enemy enemy, int id)
        {
            this.Username = enemy.Username;
            this.Reason = enemy.Reason;
            this.TimesKilled = enemy.TimesKilled;
            this.UserID = id;
        }
    }
}
