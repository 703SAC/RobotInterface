using CobotApplication;

namespace RunApplication
{
    internal class Run
    {
        static void Main(string[] args)
        {
            CobotApplicationProgram program = new CobotApplicationProgram();
            program.RunConsoleProgram();

            Console.WriteLine("Enter를 누르면 종료 \n run을 입력하면 재실행");
            string command = Console.ReadLine();
            switch(command)
            {
                case "run":
                    program.RunConsoleProgram();
                    break;
            }
            
        }
    }
}