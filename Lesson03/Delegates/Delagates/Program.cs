namespace Delagates
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new App(args);
            app.Run();

            System.Console.WriteLine("Press any key to exit");
            System.Console.ReadKey();
        }
    }
}
