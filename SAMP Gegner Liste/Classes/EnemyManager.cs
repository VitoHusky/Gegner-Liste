using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMP_Gegner_Liste
{
    class EnemyManager
    {
        private List<Enemy> Enemies = new List<Enemy>();
        private string FileName = "";


        private static EnemyManager instance = null;
        public static EnemyManager GetInstance()
        {
            if (instance == null)
            {
                instance = new EnemyManager();
            }
            return instance;
        }

        public void SetFileName(string file)
        {
            this.FileName = file;
        }

        public List<Enemy> GetEnemies()
        {
            return Enemies;
        }

        public bool EnemyExistsByName(string username)
        {
            foreach (Enemy enemy in this.Enemies)
            {
                if (enemy.Username.ToLower().Equals(username.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        public bool AddEnemy(string username, string reason)
        {
            if (this.EnemyExistsByName(username)) return false;
            Enemies.Add(new Enemy(username, reason, 0));
            using (System.IO.StreamWriter writer = System.IO.File.AppendText(this.FileName))
            {
                writer.Write(username + "|" + reason + "|0\r\n");
            }
            return true;
        }
        public bool RemoveEnemy(string username)
        {
            if (!this.EnemyExistsByName(username)) return false;

            using (var sr = new System.IO.StreamReader(this.FileName))
            using (var sw = new System.IO.StreamWriter(this.FileName + ".new"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Split('|')[0].ToLower() != username.ToLower())
                        sw.Write(line + "\r\n");
                }
            }
            System.IO.File.Delete(this.FileName);
            System.IO.File.Move(this.FileName + ".new", this.FileName);
            this.LoadFromFile();
            return true;
        }

        public void LoadFromFile()
        {
            Enemies.Clear();
            string line = "";

            System.IO.StreamReader file = new System.IO.StreamReader(this.FileName);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Length == 0) continue;
                Enemies.Add(new Enemy(line.Split('|')[0], line.Split('|')[1], Int32.Parse(line.Split('|')[2])));
            }
            file.Close();
        }
    }
}
