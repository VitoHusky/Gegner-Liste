using System;
using System.Threading;

public static class GTA
{
    #region Events
    private static bool GettingProcess = false;
    public delegate void Event();
    public delegate void TextEvent(string text);

    /// <summary>
    /// Wird aufgerufen, wenn GTA geschlossen wurde
    /// </summary>
    public static event Event GTAClosed = null;

    /// <summary>
    /// Wird aufgerufen nachdem das Programm gestartet wurde und GTA geöffnet / gestartet wurde
    /// </summary>
    public static event Event GTAOpened = null;
    #endregion

    #region Variablen
    #region public Variablen
    /// <summary>
    /// Der Prozessname des GTA Fensters |
    /// </summary>
    public static string GTAProcessName = "gta_sa";

    /// <summary>
    /// Die ProzessID des GTA Prozesses
    /// </summary>
    public static IntPtr _GTAProcess = IntPtr.Zero;

    /// <summary>
    /// Die Chatlog Datei von SA:MP
    /// </summary>
    public static string SAMPChatlogFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/GTA San Andreas User Files/SAMP/chatlog.txt";
    #endregion
    #region private Variablen

    private static Thread mainthread;
    public static bool chatlogAborted;
    #endregion
    #endregion

    public static void StartThread()
    {
        mainthread = new Thread(new ThreadStart(GTAThread));
        mainthread.Start();
    }
    public static void StopThread()
    {
        if (mainthread != null)
            mainthread.Abort();
    }


    /// <summary>
    /// ist GTA im Vordergrund bzw. das aktive Fenster?
    /// </summary>
    /// <returns></returns>

    [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
    public static extern IntPtr GetForegroundWindow();
    public static bool IsInForeground()
    {
        if (GTA.GTAProcess == IntPtr.Zero) return false;
        if (GetForegroundWindow() != GTA.GTAProcess) return false;
        return true;
    }

    /// <summary>
    /// GTA Prozess zurückgeben
    /// </summary>
    private static IntPtr GTAProcess
    {
        get {
            if (GettingProcess == true) return IntPtr.Zero;
            GettingProcess = true;
            if (_GTAProcess == IntPtr.Zero)
            {
                IntPtr ptr = IntPtr.Zero;
                System.Diagnostics.Process[] list = System.Diagnostics.Process.GetProcessesByName(GTAProcessName);
                foreach (System.Diagnostics.Process p in list)
                {
                    ptr = p.MainWindowHandle;
                }
                if (ptr != IntPtr.Zero)
                {
                    _GTAProcess = ptr;
                    if (GTAOpened != null)
                        GTA.GTAOpened();
                }
            }
            GettingProcess = false;
            return _GTAProcess;
        }
    }

    private static void GTAThread()
    {
        do
        {
            #region prüfen ob GTA geschlossen wurde
            if (_GTAProcess != IntPtr.Zero)
            {
                bool closed = true;
                IntPtr ptr = IntPtr.Zero;
                System.Diagnostics.Process[] list = System.Diagnostics.Process.GetProcessesByName(GTAProcessName);
                foreach (System.Diagnostics.Process p in list)
                {
                    closed = false;
                }
                if (closed == true)
                {
                    _GTAProcess = IntPtr.Zero;
                    if (GTAClosed != null)
                        GTA.GTAClosed();
                }
            }
            #endregion
            Thread.Sleep(200);
        } while (true);
    }
}