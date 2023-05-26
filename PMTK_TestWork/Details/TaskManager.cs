namespace PMTK_TestWork.Details
{
    internal class TaskManager
    {

        public static void start(string[] args)
        {
            switch(args[0])
            {
                case "1":
                    Tasks.Task1();
                    break;
                case "2":
                    Tasks.Task2(args.ToList<string>().GetRange(1, args.Length - 1).ToArray());
                    break;
                case "3":
                    Tasks.Task3();
                    break;
                case "4":
                    Tasks.Task4();
                    break;
                case "5":
                    Tasks.Task5();
                    break;
                case "6":
                    Tasks.Task6();
                    break;
                default:
                    throw new ArgumentException(String.Format("Incorrect number of tusk. " +
                        "You need to write 1-6, but you wrote {0}", args[0]));
            }
        }
    }
}
