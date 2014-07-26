using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keybinder.KeyHandles
{
    class TextBinds
    {
        private static string text = "";
        private static bool WaitForEnter = false;
        public static void init(ref GlobalKeyboardHook gkh)
        {
            gkh.KeyDown += new KeyEventHandler(HandleDown);
        }
        public static void HandleDown(object sender, KeyEventArgs e)
        {
            bool handled = false;
            if (!GTA.IsInForeground()) return;
            if (!shadowAPI2.Chat.GetInstance().IsOpen()|| shadowAPI2.Chat.GetInstance().IsDialogOpen())
            {
                text = "";
                return;
            }

            if (!IsAWriteKey(e) && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Back)
                return;

            char val = GetRealChar(e);

            if (e.KeyCode == Keys.Enter)
            {
                WaitForEnter = false;
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (text.Length >= 1)
                    text = text.Remove(text.Length - 1);
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (binds(text))
                {
                    handled = true;
                }
            }
            else
            {
                text += val;
            }
            e.Handled = handled;
        }

        private static bool IsAWriteKey(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                case Keys.LControlKey:
                case Keys.RControlKey:
                case Keys.LMenu:
                case Keys.RMenu:
                case Keys.Back:
                case Keys.Enter:
                    return false;

                default:
                    return true;
            }
        }
        private static char GetRealChar(KeyEventArgs e)
        {
            char back = ' ';
            if (e.KeyCode.ToString().Contains("NumPad"))
            {
                back = e.KeyCode.ToString().Remove(0, 6).ToCharArray()[0];
            }

            switch (e.KeyCode)
            {
                case Keys.Divide:
                    back = '/';
                    break;
                case Keys.OemPeriod:
                    if (MainKeys.Shift) back = ':';
                    else back = '.';
                    break;
                case Keys.OemMinus:
                    if (MainKeys.Shift) back = '_';
                    else back = '-';
                    break;
                case Keys.D0:
                    if (MainKeys.Shift) back = '=';
                    else back = '0';
                    break;
                case Keys.D1:
                    if (MainKeys.Shift) back = '!';
                    else back = '1';
                    break;
                case Keys.D2:
                    if (MainKeys.Shift) back = '"';
                    else back = '2';
                    break;
                case Keys.D3:
                    if (MainKeys.Shift) back = '§';
                    else back = '3';
                    break;
                case Keys.D4:
                    if (MainKeys.Shift) back = '$';
                    else back = '4';
                    break;
                case Keys.D5:
                    if (MainKeys.Shift) back = '%';
                    else back = '5';
                    break;
                case Keys.D6:
                    if (MainKeys.Shift) back = '&';
                    else back = '6';
                    break;
                case Keys.D7:
                    if (MainKeys.Shift) back = '/';
                    else back = '7';
                    break;
                case Keys.D8:
                    if (MainKeys.Shift) back = '(';
                    else back = '8';
                    break;
                case Keys.D9:
                    if (MainKeys.Shift) back = ')';
                    else back = '9';
                    break;


                case Keys.Space:
                    back = ' ';
                    break;


                default:
                    if (MainKeys.Shift)
                        back = (char)e.KeyValue; 
                    else
                    {
                        string lower = e.KeyCode.ToString().ToLower();
                        if (lower.Length == 1)
                        {
                            back = Convert.ToChar(lower);
                        }
                    }
                    break;
            }
            return back;
        }
       
        private static bool binds(string text)
        {

            bool rtn = true;

            switch (text.ToLower())
            {
                case "/addenemy":
                    SendKeys.Send("^a{del}/id{space}");
                    WaitForEnter = true;
                    break;
                default:
                    rtn = false;
                    break;
            }
            return rtn;
        }
    }
}
