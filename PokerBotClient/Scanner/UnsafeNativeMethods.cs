using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PokerBotClient
{
    internal static class UnsafeNativeMethods
    {
        //class = RichEdit20W
        //Welcome to KernstownII
        //> Disconnect all ins are not allowed in this game! Any disconnects will be treated as a fold by the player.
        //> rulanek bet for €0.20
        //> FFFish0 folded
        //> rulanek mucked
        //> rulanek wins €0.39
        //> Rexus1 is waiting for the big blind to p


        //Welcome to Anonymous - €10 Max
        //You have joined an Anonymous Table. Your identity and those of other playersCollection are hidden.
        //> Disconnect all ins are not allowed in this game! Any disconnects will be treated as a fold by the player.
        //> Player 4 folded
        //> Player


        public const int WM_GETTEXT = 13;

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "GetWindowTextLength", SetLastError = true)]
        internal static extern int GetWindowTextLength(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hwnd, String lpString);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lclassName, string windowTitle);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll" ,SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int msg, int wParam, [Out] StringBuilder sb);

    }
}
