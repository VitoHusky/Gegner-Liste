using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMP_Gegner_Liste
{
    class Overlay
    {
        private static int OverlayID = -1;
        private static Overlay instance = null;
        public static Overlay GetInstance()
        {
            if (instance == null)
            {
                instance = new Overlay();
            }
            return instance;
        }
        public void Update()
        {
            string EnemyStr = "=== Gegner Online ===";
            if (OverlayID == -1)
            {
                DX9Overlay.SetParam("use_window", "1");
                DX9Overlay.SetParam("window", "GTA:SA:MP");
                OverlayID = DX9Overlay.TextCreate("Arial", 6, true, false, 5, 200, 0xFFFFAAAA, EnemyStr, false, true);
            }
            else
            {
                bool first = true;
                int count = 0;
                
                foreach (Enemy enemy in EnemyManager.GetInstance().GetEnemies())
                {
                    int id = shadowAPI2.RemotePlayer.GetInstance().GetPlayerIdByName(enemy.Username, first);
                    if (id != -1)
                    {
                        EnemyStr += "\n";
                        count++;
                        if (count % 2 == 0)
                            EnemyStr += "{BBBBBB}";
                        else
                            EnemyStr += "{AFAFAF}";
                        EnemyStr += "[" + id + "] " + enemy.Username;
                    }
                }
                if (count == 0)
                {
                    EnemyStr += "\n{BBBBBB}Keine Gegner online.";
                }
                DX9Overlay.TextSetString(OverlayID, EnemyStr);
            }
        }
    }
}
