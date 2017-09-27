using System;
using System.Runtime.InteropServices;

namespace Pong
{  
    public static class Program
    {
        //For Debugging
        //Get console to show up.
        //source: https://stackoverflow.com/questions/4362111/how-do-i-show-a-console-output-window-in-a-forms-application
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [STAThread]
        static void Main()
        {
            AllocConsole();
            Pong game = new Pong();
            game.Run();
        }
    }
}