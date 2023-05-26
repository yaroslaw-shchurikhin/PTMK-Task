using PMTK_TestWork.Details;

namespace PMTK_TestWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    throw new ArgumentException("Incorrect cmd argument. You need to write more then 0.");
                }

                TaskManager.start(args);  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
