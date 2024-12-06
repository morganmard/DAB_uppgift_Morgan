namespace DAB;

static class Program
{
        static void Main()
        {
                bool done = false;
                while (!done)
                {
                        Console.Clear();
                        Console.WriteLine("What do you want to do?\n1. Add data\n2. read data\n3. Checkout book\n4. Return book\n 5. Remove book\n 6. Exit program");
                        switch (Console.ReadKey().Key)
                        {
                                case ConsoleKey.D1:
                                        AddData.Run();
                                        break;

                                case ConsoleKey.D2:
                                        ReadData.Run();
                                        break;

                                case ConsoleKey.D3:
                                        Checkout.Run();
                                        break;

                                case ConsoleKey.D4:
                                        BookReturn.Run();
                                        break;

                                case ConsoleKey.D5:
                                        BookRemove.Run();
                                        break;

                                case ConsoleKey.D6:
                                        done = true;
                                        break;

                                default: // "Invalid choice"
                                        break;
                        }

                        Console.WriteLine("Press any key to continue");
                        _ = Console.ReadKey();
                }
        }
}


/* Make sure the database is up and running */
// context.Database.EnsureCreated();