using System;
using System.Threading;

namespace editor
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Mutex mutex = new System.Threading.Mutex(false, System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location));
            try
            {
                if (mutex.WaitOne(0, false))
                {
                    // Run the application
                    using (var game = new Game1())
                        game.Run();
                }
                else
                {
                    //MessageBox.Show("An instance of the application is already running.");
                }
            }
            finally
            {
                if (mutex != null)
                {
                    mutex.Close();
                    mutex = null;
                }
            }
            
        }
    }
}