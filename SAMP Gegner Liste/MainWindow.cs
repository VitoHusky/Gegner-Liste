using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAMP_Gegner_Liste
{
    public partial class MainWindow : Form
    {
        private List<Enemy> enemies = new List<Enemy>();
        private string processName = "";
        private bool ShowEnemyReason = false;
        private IniFile settings = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

            string directory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SysDev/GegnerOverlay";
            string settingsFile = directory + "/settings.ini";
            string enemyFile = directory + "/enemies.file";

            System.IO.Directory.CreateDirectory(directory);
            if (!System.IO.File.Exists(settingsFile))
            {
                System.IO.File.Create(settingsFile);
            }
            if (!System.IO.File.Exists(enemyFile))
            {
                System.IO.File.Create(enemyFile);
            }


            settings = new IniFile(settingsFile);
            EnemyManager.GetInstance().SetFileName(enemyFile);
            EnemyManager.GetInstance().LoadFromFile();
            this.UpdateList();


            processName = settings.IniReadValue("Main", "ProcessName");
            if (settings.IniReadValue("Main", "ShowReason").Equals("1"))
            {
                this.ShowEnemyReason = true;
            }

            if (processName.Equals("rgn_ac_gta"))
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }

            if (processName.Length != 0)
            {
                button1.Text = "Speichern";
                Initialized = true;
            }
        }

        private bool Initialized = false;
        private void MainCheckTimer_Tick(object sender, EventArgs e)
        {
            if (!Initialized)
            {
                return;
            }
            else
            {
                Overlay.GetInstance().Update(this.ShowEnemyReason);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (button1.Text.Equals("Starten"))
            {
                button1.Text = "Speichern";
                Initialized = true;
            }

            if (radioButton2.Checked)
            {
                processName = "rgn_ac_gta";
                settings.IniWriteValue("Main", "ProcessName", processName);
                GTA.GTAProcessName = processName;
                shadowAPI2.API.Init(processName);
            }
            else
            {
                processName = "gta_sa";
                settings.IniWriteValue("Main", "ProcessName", "gta_sa");
                GTA.GTAProcessName = processName;
                shadowAPI2.API.Init(processName);
            }

            if (checkBox1.Checked)
                settings.IniWriteValue("Main", "ShowREason", "1");
            else
                settings.IniWriteValue("Main", "ShowREason", "0");
            button1.Enabled = true;
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            DX9Overlay.DestroyAllVisual();
        }

        private void BTN_AddEnemy_Click(object sender, EventArgs e)
        {
            if (TB_Add_User.Text.Length == 0 || TB_Add_User.Text.Length > 24)
            {
                MessageBox.Show("Bitte einen gültigen Username eingeben.", "Fehlender oder falscher Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (TB_Add_Reason.Text.Length == 0)
            {
                MessageBox.Show("Bitte einen Grund eingeben.", "Fehlender Grund", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (EnemyManager.GetInstance().AddEnemy(TB_Add_User.Text, TB_Add_Reason.Text))
            {
                MessageBox.Show(TB_Add_User.Text + " wurde erfolgreich auf die Gegnerliste gesetzt.");
            }
            else
            {
                MessageBox.Show("´Der " + TB_Add_User.Text + " wurde bereits auf der Gegnerliste gefunden.", "Doppelt!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.UpdateList();
        }

        private void BTN_RemoveEnemy_Click(object sender, EventArgs e)
        {
            if (TB_REM_User.Text.Length == 0 || TB_REM_User.Text.Length > 24)
            {
                MessageBox.Show("Bitte einen gültigen Username eingeben.", "Fehlender oder falscher Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (EnemyManager.GetInstance().RemoveEnemy(TB_REM_User.Text))
            {
                MessageBox.Show(TB_REM_User.Text + " wurde erfolgreich von der Gegnerliste gelöscht.");
            }
            else
            {
                MessageBox.Show("´Der " + TB_REM_User.Text + " wurde nicht auf der Gegnerliste gefunden.", "Doppelt!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            this.UpdateList();
        }

        private void UpdateList()
        {
            listBox1.Items.Clear();
            List<Enemy> enemies = EnemyManager.GetInstance().GetEnemies();
            foreach (Enemy enemy in enemies)
            {
                listBox1.Items.Add(enemy.Username);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
