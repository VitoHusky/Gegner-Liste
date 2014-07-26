using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keybinder.KeyHandles
{
    class MainKeys
    {
        private static bool _Alt = false;
        private static bool _Ctrl = false;
        private static bool _Shift = false;

        public static bool Alt
        {
            get { return _Alt; }
        }
        public static bool Ctrl
        {
            get { return _Ctrl; }
        }
        public static bool Shift
        {
            get { return _Shift; }
        }


        public static void init(ref GlobalKeyboardHook gkh)
        {
            gkh.HookedKeys.Add(Keys.LControlKey);
            gkh.HookedKeys.Add(Keys.RControlKey);
            gkh.HookedKeys.Add(Keys.LShiftKey);
            gkh.HookedKeys.Add(Keys.RShiftKey);
            gkh.HookedKeys.Add(Keys.LMenu);
            gkh.HookedKeys.Add(Keys.RMenu);
            gkh.HookedKeys.Add(Keys.T);

            gkh.HookedKeys.Add(Keys.Enter);
            gkh.HookedKeys.Add(Keys.Escape);

            gkh.KeyDown += new KeyEventHandler(HandleDown);
            gkh.KeyUp += new KeyEventHandler(HandleUp);
        }

        public static void HandleDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LMenu)
            {
                _Alt = true;
            }
            if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
            {
                _Shift = true;
            }
            if (e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey)
            {
                _Ctrl = true;
            }
            /*
            if (e.KeyCode == Keys.T && !Saves.Global.KeybinderPaused)
            {
                Saves.Global.KeybinderPaused = true;
            }
            else if ( (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter) && Saves.Global.KeybinderPaused )
            {
                Saves.Global.KeybinderPaused = false;
            }*/
        }

        public static void HandleUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.LMenu || e.KeyCode == Keys.RMenu)
            {
                _Alt = false;
            }
            if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
            {
                _Shift = false;
            }
            if (e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.RControlKey)
            {
                _Ctrl = false;
            }
        }
    }
}
