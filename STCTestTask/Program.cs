using STCTestTask.Services;

namespace STCTestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentNullException("Укажите через пробел задачу, которую необходимо выполнить");
            }
            else if (args.Length > 1)
            {
                throw new ArgumentException("Укажите только одну задачу, которую необходимо выполнить");
            }

            string pathToFile = "makefile.txt";
            var makeFileText = String.Empty;

            try
            {
                makeFileText = File.ReadAllText(pathToFile);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Необходимый файл \"{pathToFile}\" не найден.");
                throw ex;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Возникла проблема с необходимым файлом \"{pathToFile}\". " + ex.Message);
                throw ex;
            }

            var targetList = ParseMakeFileText.GetTargetList(makeFileText);

            var targetExecution = new TargetExecution(targetList);
            targetExecution.ProcessTargetExecution(args[0]);

            Console.ReadKey();
        }
    }
}
