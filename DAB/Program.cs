namespace DAB;

static class Program
{
        static void Main()
        {
                bool done = false;
                while (!done)
                {
                        Console.Clear();
                        Console.WriteLine("What do you want to do?\n1. Add data\n2. read data\n3. Exit program");
                        switch (Console.ReadKey().Key)
                        {
                                case ConsoleKey.D1:
                                        AddData.Run();
                                        break;

                                case ConsoleKey.D2:
                                        ReadData.Run();
                                        _ = Console.ReadKey();
                                        break;
                                case ConsoleKey.D3:
                                        done = true;
                                        break;
                                default: // "Invalid choice"
                                        break;
                        }
                }
        }
}


/*
comment
optimize
*/
