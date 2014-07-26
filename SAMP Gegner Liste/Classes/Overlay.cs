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
        public void Update(bool showReason)
        {
            string EnemyStr = "=== Gegner Online ===";
            if (OverlayID == -1)
            {
                DX9Overlay.SetParam("use_window", "1");
                DX9Overlay.SetParam("window", "GTA:SA:MP");
                OverlayID = DX9Overlay.TextCreate("Arial", 6, true, false, 5, 200, 0xFFFF3333, EnemyStr, false, true);
            }
            else
            {
                bool first = true;
                List<Enemy> EnemyCopy = new List<Enemy>();


                foreach (Enemy enemy in EnemyManager.GetInstance().GetEnemies())
                {
                    int id = shadowAPI2.RemotePlayer.GetInstance().GetPlayerIdByName(enemy.Username, first);
                    if (id != -1)
                    {
                        EnemyCopy.Add(new Enemy(enemy, id));
                    }
                }
                
                if (EnemyCopy.Count == 0)
                {
                    EnemyStr += "\n{BBBBBB}Keine Gegner online.";
                }
                else
                {
                    int count = 0;
                    List<Enemy> SortedEnemies = EnemyCopy.OrderBy(o => o.UserID).ToList();
                    foreach (Enemy enemy in SortedEnemies)
                    {
                        EnemyStr += "\n";
                        count++;
                        if (count % 2 == 0)
                            EnemyStr += "{BFBFBF}";
                        else
                            EnemyStr += "{EFEFEF}";
                        EnemyStr += "[" + enemy.UserID + "] " + enemy.Username;
                        if (showReason)
                        {
                            EnemyStr += "(" + enemy.Reason + ")";
                        }
                    }
                    EnemyStr += "\n{FF3333}Es sind " + count + " Gegner online.";
                }
                DX9Overlay.TextSetString(OverlayID, EnemyStr);
            }
        }
    }
}
