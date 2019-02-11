using System;

namespace DevUseFromNugetApp
{
    /// <summary>
    /// To test the nuget package.
    /// Do Not reference projects! must use nuget to test it.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            DevEtagairEngine devEtagairEngine = new DevEtagairEngine();
            devEtagairEngine.Run();

            Console.WriteLine("Ends.");
        }
    }
}
