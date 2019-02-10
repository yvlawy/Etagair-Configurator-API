using System;

namespace DevUseFromNugetApp
{
    /// <summary>
    /// To test the nuget package.
    /// todo: import the nuget!!
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
