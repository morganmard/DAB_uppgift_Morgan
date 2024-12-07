namespace DAB;

static class Program
{
        static void Main()
        {
                Propagate.Run();

                bool done = false;
                while (!done)
                {
                        const string m = "What do you want to do?\n1. Add Book\n2. List books\n3. Checkout book\n4. Return book";
                        const string m2 = "5. Remove book\n6. Show loan-history of book\n7. Exit program";

                        Console.WriteLine(m);
                        Console.WriteLine(m2);

                        switch (Console.ReadKey().Key)
                        {
                                case ConsoleKey.D1:
                                        AddData.Run(); break;

                                case ConsoleKey.D2:
                                        ReadData.Run(); break;

                                case ConsoleKey.D3:
                                        Checkout.Run(); break;

                                case ConsoleKey.D4:
                                        BookReturn.Run(); break;

                                case ConsoleKey.D5:
                                        BookRemove.Run(); break;

                                case ConsoleKey.D6:
                                        ShowHistory.Run(); break;

                                case ConsoleKey.D7:
                                        done = true;
                                        Console.Clear(); break;

                                default: // "Invalid choice"
                                        break;
                        }

                        Console.WriteLine("Press any key to continue");
                        _ = Console.ReadKey();
                        Console.Clear();
                }
        }
}
