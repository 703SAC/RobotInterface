using CobotApplication;

namespace RunApplication
{
    internal class Run
    {
        static void Main(string[] args)
        {
            CobotApplicationProgram program = new CobotApplicationProgram();
            program.RunProgram();
            
            Console.ReadLine();
        }
    }
}