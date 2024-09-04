using System.Text;

namespace BasicLibrary
{
    internal class Program
    {
        static List<(string BName, string BAuthor, int ID, int quantity)> Books = new List<(string BName, string BAuthor, int ID, int Quantity)>();
        static string filePath = "C:\\Users\\Codeline User\\Documents\\Codeline Projects\\Files\\library.txt";



        static void Main(string[] args)
        {


            bool ExitFlag = false;

            LoadBooksFromFile();

            do
            {

                Console.WriteLine("Press 1 for Admin Menu or press to 2 for User Menu or press 3 to save & exit");
                Console.WriteLine("Admin Menu");
                Console.WriteLine("User Menu");
                Console.WriteLine("Save & Exit");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AdminMenu();
                        break;

                    case 2:
                        UserMenu();
                        break;

                    case 3:
                        SaveBooksToFile();
                        ExitFlag = true;
                        break;

                    default:
                        Console.WriteLine("Please enter a valid choice");
                        break;
                }

            } while (ExitFlag != true);



        static void AdminMenu()
            {

                string AdminUsername = "admin";
                Console.WriteLine("Enter the username:");
                string username;
                while ((username = Console.ReadLine()) != "admin")
                {
                    Console.WriteLine("Invalid username, try again: ");
                }

                Console.WriteLine("\nEnter the password:");
                string AdminPassword = "12345";
                string password;
                while ((password = Console.ReadLine()) != "12345")
                {
                    Console.WriteLine("Invalid password");
                }

                bool ExitFlag = false;

                do
                {
                    Console.WriteLine("Welcome to Library");
                    Console.WriteLine("\n Enter the number of operation you need :");
                    Console.WriteLine("\n 1- Add a New Book");
                    Console.WriteLine("\n 2- Show All Books");
                    Console.WriteLine("\n 3- Search for a Book by Name");
                    Console.WriteLine("\n 4- Save and Exit");

                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            AddNewBook();
                            break;

                        case 2:
                            ViewAllBooks();
                            break;

                        case 3:
                            SearchForBook();
                            break;

                        case 4:
                            SaveBooksToFile();
                            ExitFlag = true;
                            break;

                        default:
                            Console.WriteLine("Please enter a valid choice");
                            break;



                    }

                    Console.WriteLine("press any key to continue");
                    string cont = Console.ReadLine();

                    Console.Clear();

                } while (ExitFlag != true);
            }

        static void UserMenu()

            {
                bool ExitFlag = false;

                do
                {
                    Console.WriteLine("Welcome User");
                    Console.WriteLine("\n Enter the number of operation you need :");
                    Console.WriteLine("\n 1- Search for a Book by Name");
                    Console.WriteLine("\n 2- Borrow a Book");
                    Console.WriteLine("\n 3- Return a Book ");
                    Console.WriteLine("\n 4- Save and Exit");

                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            SearchForBook();
                            break;

                        case 2:
                            BorrowBook();
                            break;

                        case 3:
                            ReturnBook();
                            break;

                        case 4:
                            ExitFlag = true;
                            break;

                        default:
                            Console.WriteLine("Please enter a valid choice");
                            break;

                    }
                } while (ExitFlag != true);
            }

        }

        static void AddNewBook()
            {
                Console.WriteLine("Enter Book Name");
                string name = Console.ReadLine();

                Console.WriteLine("Enter Book Author");
                string author = Console.ReadLine();

                Console.WriteLine("Enter Book ID");
                int ID = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Book Quantity");
                int quantity = int.Parse(Console.ReadLine());

                Books.Add((name, author, ID, quantity));
                Console.WriteLine("Book Added Succefully");

            }

        static void ViewAllBooks()
            {
                StringBuilder sb = new StringBuilder();

                int BookNumber = 0;

                for (int i = 0; i < Books.Count; i++)
                {
                    BookNumber = i + 1;
                    sb.Append("Book ").Append(BookNumber).Append(" name : ").Append(Books[i].BName);
                    sb.AppendLine();
                    sb.Append("Book ").Append(BookNumber).Append(" Author : ").Append(Books[i].BAuthor);
                    sb.AppendLine();
                    sb.Append("Book ").Append(BookNumber).Append(" ID : ").Append(Books[i].ID);
                    sb.AppendLine().AppendLine();
                    Console.WriteLine(sb.ToString());
                    sb.Clear();

                }
            }

        static void SearchForBook()
            {
                Console.WriteLine("Enter the book name you want");
                string name = Console.ReadLine();
                bool flag = false;

                for (int i = 0; i < Books.Count; i++)
                {
                    if (Books[i].BName == name)
                    {
                        Console.WriteLine("Book Author is : " + Books[i].BAuthor);
                        flag = true;
                        break;
                    }
                }

                if (flag != true)
                { Console.WriteLine("book not found"); }
            }

        static void BorrowBook()
        {

        }

        static void ReturnBook()
        {

        }

        static void LoadBooksFromFile()
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        using (StreamReader reader = new StreamReader(filePath))
                        {
                            string line;
                            while ((line = reader.ReadLine()) != null)
                            {
                                var parts = line.Split('|');
                                if (parts.Length == 4)
                                {
                                    Books.Add((parts[0], parts[1], int.Parse(parts[2]), int.Parse(parts[3])));
                                }
                            }
                        }
                        Console.WriteLine("Books loaded from file successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading from file: {ex.Message}");
                }
            }

        static void SaveBooksToFile()
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (var book in Books)
                        {
                            writer.WriteLine($"{book.BName}|{book.BAuthor}|{book.ID} |{book.quantity}");
                        }
                    }
                    Console.WriteLine("Books saved to file successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving to file: {ex.Message}");
                }
            }

     }
 
 }

