using System;

namespace Gonk_Konk
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GonkKonk())
                game.Run();
        }
    }
}
