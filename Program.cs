using System.Runtime.CompilerServices;
using System.Text;

namespace BasicLibrary
{
    internal class Program
    {
        static string CurrentUser = "";
        static List<(string BName, string BAuthor, int ID, int Quantity)> Books = new List<(string BName, string BAuthor, int ID, int Quantity)>();
        static List<(string AdminName, string AdminPass)> AdminAuth = new List<(string AdminName, string AdminPass)>();
        static List<(string UserName, string UserPass)> UserAuth = new List<(string UserName, string UserPass)>();
        static string filePath = "C:\\Users\\Codeline User\\Documents\\Codeline Projects\\Files\\Library.txt";
        static string AdminFile = "C:\\Users\\Codeline User\\Documents\\Codeline Projects\\Files\\Admins Registration.txt";
        static string UserFile = "C:\\Users\\Codeline User\\Documents\\Codeline Projects\\Files\\Users Registration.txt";


        static void Main(string[] args)
        {
            LoadBooksFromFile();

            bool ExitFlag = false;


            do
            {

                Console.WriteLine("Press 1 for Admin Menu,  press to 2 for User Menu, press 3 for registration or press 4 to save & exit");
                Console.WriteLine("1- Admin Menu");
                Console.WriteLine("2- User Menu");
                Console.WriteLine("3- Registration");
                Console.WriteLine("4- Save & Exit");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AdminMenu();
                        break;

                    case 2:
                        UserMenu();
                        break;

                    case 3:
                        Registration();
                        break;

                    case 4:
                        SaveBooksToFile();
                        ExitFlag = true;
                        break;

                    default:
                        Console.WriteLine("Please enter a valid choice");
                        break;
                }

            } while (!ExitFlag);

        }

        static void AdminMenu()
        {

            Console.WriteLine("Enter the username: (Hint: admin or masteradmin)");
            string username;
            while (true)
            {
                username = Console.ReadLine();
                if (username == "admin" || username == "masteradmin")
                    break;
                else
                    Console.WriteLine("Invalid username, try again:");
            }

            Console.WriteLine("\nEnter the password: (Hint: 12345 or masterpass)");
            string password;
            while (true)
            {
                password = Console.ReadLine();
                if ((username == "admin" && password == "12345") || (username == "masteradmin" && password == "masterpass"))
                    break;
                else
                    Console.WriteLine("Invalid password, try again:");
            }

            if (username == "masteradmin")
            {
                MasterAdminMenu();
            }
            else
            {

                CurrentUser = "Admin";

                bool ExitFlag = false;


                do
                {
                    Console.WriteLine("Welcome to Library");
                    Console.WriteLine("\n Enter the number of operation you need :");
                    Console.WriteLine("\n 1- Add a New Book");
                    Console.WriteLine("\n 2- Edit Books info");
                    Console.WriteLine("\n 3- Remove a Book");
                    Console.WriteLine("\n 4- Show All Books");
                    Console.WriteLine("\n 5- Search for a Book by Name");
                    Console.WriteLine("\n 6- Display Statistics");
                    Console.WriteLine("\n 7- Save and Exit");

                    if (!int.TryParse(Console.ReadLine(), out int choice))
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                        continue;
                    }

                    switch (choice)
                    {
                        case 1:
                            AddNewBook();
                            break;

                        case 2:
                            EditBooks();
                            break;

                        case 3:
                            RemoveBooks();
                            break;

                        case 4:
                            ShowAllBooks();
                            break;

                        case 5:
                            SearchForBook();
                            break;

                        case 6:
                            DisplayStatistics();
                            break;

                        case 7:
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

                } while (!ExitFlag);

                CurrentUser = "";
            }
        }

        static void UserMenu()

        {

            Console.Write("Enter Your Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Your Password: ");
            string UserPassword = Console.ReadLine();

            bool isAuthenticated = AuthenticateUser(UserFile, username, UserPassword);

            if (isAuthenticated)
            {
                Console.WriteLine($"Welcome {username}");
            }
            else if (!isAuthenticated)
            {
                Console.WriteLine("Authentication failed. Please enter correct info.");
                return;
            }

            CurrentUser = username;

            bool ExitFlag = false;

            do
            {
                Console.WriteLine("\n Enter the number of operation you need :");
                Console.WriteLine("\n 1- Search for a Book by Name");
                Console.WriteLine("\n 2- Borrow a Book");
                Console.WriteLine("\n 3- Return a Book ");
                Console.WriteLine("\n 4- Save and Exit");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

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
            } while (!ExitFlag);

            CurrentUser = "";
        }

        static bool AuthenticateUser(string UserFile, string username, string UserPassword)
        {
            try
            {
                string[] userLines = File.ReadAllLines(UserFile); //Array to store each of the file

                foreach (string line in userLines)
                {
                    string[] parts = line.Split('|'); // Split each line into username and password
                    if (parts.Length == 2)
                    {
                        string storedUsername = parts[0];
                        string storedPassword = parts[1];

                        if (storedUsername == username && storedPassword == UserPassword) // Check if username and password match
                        {
                            return true;
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("User file not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return false;
        }

        static void MasterAdminMenu()
        {
            Console.WriteLine("Welcome Master Admin!");
            bool ExitFlag = false;

            do
            {
                Console.WriteLine("1- Add New Admin");
                Console.WriteLine("2- Remove Admin");
                Console.WriteLine("3- View All Admins");
                Console.WriteLine("4- Show All Books");
                Console.WriteLine("5- Save and Exit");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AdminRegistration();
                        break;
                    case 2:
                        RemoveAdmin();
                        break;
                    case 3:
                        ViewAllAdmins();
                        break;
                    case 4:
                        ShowAllBooks();
                        break;
                    case 5:
                        SaveAdminsToFile();
                        ExitFlag = true;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice.");
                        break;
                }

                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
                Console.Clear();

            } while (!ExitFlag);
        }

        static void RemoveAdmin()
        {
            Console.WriteLine("Enter the admin username you want to remove:");
            string adminToRemove = Console.ReadLine();
            bool adminFound = false;

            for (int i = AdminAuth.Count - 1; i >= 0; i--)
            {
                if (AdminAuth[i].AdminName == adminToRemove)
                {
                    AdminAuth.RemoveAt(i);
                    adminFound = true;
                    Console.WriteLine("Admin removed successfully.");
                    break;
                }
            }

            if (!adminFound)
            {
                Console.WriteLine("Admin not found.");
            }
        }

        static void ViewAllAdmins()
        {
            if (AdminAuth.Count == 0)
            {
                Console.WriteLine("No admins registered.");
            }
            else
            {
                foreach (var admin in AdminAuth)
                {
                    Console.WriteLine($"Admin Username: {admin.AdminName}");
                }
            }
        }

        static void Registration()
        {
            Console.WriteLine("Please choose one of the following:");
            Console.WriteLine("1- Admin Registration");
            Console.WriteLine("2- User Registration");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AdminRegistration();
                    break;

                case 2:
                    UserRegistration();
                    break;

                default:
                    Console.WriteLine("Invalid choice");
                    break;

            }
        }

        static void AdminRegistration()
        {
            string AdminName;
            string AdminPass;
            Console.WriteLine("Enter Admin's Username:");
            AdminName = Console.ReadLine();


            Console.WriteLine("Enter Admin's Password");
            AdminPass = Console.ReadLine();

            AdminAuth.Add((AdminName, AdminPass));
            SaveAdminsToFile();
        }

        static void UserRegistration()
        {
            string UserName;
            string UserPass;
            Console.WriteLine("Enter Users's Username:");
            UserName = Console.ReadLine();


            Console.WriteLine("Enter User's Password");
            UserPass = Console.ReadLine();

            UserAuth.Add((UserName, UserPass));
            SaveUsersToFile();
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
            int Quantity = int.Parse(Console.ReadLine());

            Books.Add((name, author, ID, Quantity));
            Console.WriteLine("Book Added Successfully");

        }

        static void EditBooks()
        {

            Console.WriteLine("Enter the ID of the book you want to edit:");
            int BookIdToEdit;
            if (!int.TryParse(Console.ReadLine(), out BookIdToEdit))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            int bookIndex = -1;
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == BookIdToEdit)
                {
                    bookIndex = i;
                    break;
                }
            }

            if (bookIndex == -1)
            {
                Console.WriteLine("Book with the given ID not found.");
                return;
            }

            var book = Books[bookIndex];

            Console.WriteLine("Editing Book:");
            Console.WriteLine($"Current Name: {book.BName}");
            Console.WriteLine("Enter new Name :");
            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName)) book = (newName, book.BAuthor, book.ID, book.Quantity);

            Console.WriteLine($"Current Author: {book.BAuthor}");
            Console.WriteLine("Enter new Author :");
            string newAuthor = Console.ReadLine();
            if (!string.IsNullOrEmpty(newAuthor)) book = (book.BName, newAuthor, book.ID, book.Quantity);

            Console.WriteLine($"Current Quantity: {book.Quantity}");
            Console.WriteLine("Enter new Quantity :");
            string newQuantityStr = Console.ReadLine();
            if (int.TryParse(newQuantityStr, out int newQuantity))
            {
                book = (book.BName, book.BAuthor, book.ID, newQuantity);
            }

            Books[bookIndex] = book;

            Console.WriteLine("Book details updated successfully.");

        }

        static void RemoveBooks()
        {
            Console.WriteLine("Enter the name of the book you want to remove:");
            string bookNameToRemove = Console.ReadLine();
            bool bookFoundByName = false;
            for (int i = Books.Count - 1; i >= 0; i--)
            {
                if (Books[i].BName == bookNameToRemove)
                {
                    Books.RemoveAt(i);
                    bookFoundByName = true;
                }
            }
            if (bookFoundByName)
            {
                Console.WriteLine("Book(s) removed successfully.");
            }
            else
            {
                Console.WriteLine("Book with the given name not found.");
            }

        }

        static void ShowAllBooks()
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
                sb.AppendLine();
                sb.Append("Book ").Append(BookNumber).Append(" Quantity : ").Append(Books[i].Quantity);
                sb.AppendLine().AppendLine();
                Console.WriteLine(sb.ToString());
                sb.Clear();

            }
        }

        static void SearchForBook()
        {
            Console.WriteLine("Enter the book name you want");
            string name = Console.ReadLine();
            bool available = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].BName == name)
                {
                    Console.WriteLine("This book is available");
                    Console.WriteLine("\nBook Author is : " + Books[i].BAuthor);
                    Console.WriteLine("\nBook ID is : " + Books[i].ID);
                    Console.WriteLine("\nQuantity Available : " + Books[i].Quantity);
                    available = true;
                    break;
                }

            }
            if (!available)
            {
                Console.WriteLine("book not found");

            }
        }

        static void BorrowBook()
        {
            foreach (var Book in Books)
            {
                Console.WriteLine(Book.BName);
            }

            Console.WriteLine($"[{CurrentUser}] Enter the name of the book you want to borrow:");
            string bookName = Console.ReadLine();
            bool bookFound = false;
            string borrowedAuthor = "";

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].BName == bookName)
                {
                    bookFound = true;
                    if (Books[i].Quantity > 0)
                    {
                        Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, Books[i].Quantity - 1);
                        borrowedAuthor = Books[i].BAuthor;
                        Console.WriteLine($"[{CurrentUser}] has successfully borrowed the book.");
                    }
                    else
                    {
                        Console.WriteLine("This book is already borrowed.");
                    }
                    break;
                }
            }

            if (!bookFound)
            {
                Console.WriteLine("Book not found.");
            }

            else
            {
                RecommendBooks(borrowedAuthor);
            }
        }

        static void RecommendBooks(string author)
        {

            bool recommendationsFound = false;
            foreach (var book in Books)
            {
                if (book.BAuthor == author && book.Quantity > 0)
                {
                    Console.WriteLine("\nYou may also like these books by the same author:");
                    Console.WriteLine($"- {book.BName} by {book.BAuthor}");
                    recommendationsFound = true;
                    break;
                }
            }

            if (!recommendationsFound)
            {
                Console.WriteLine("No other books by this author are available right now.");
            }
        }

        static void ReturnBook()
        {
            foreach (var Book in Books)
            {
                Console.WriteLine(Book.BName);
            }

            Console.WriteLine($"[{CurrentUser}] Enter the name of the book you want to return:");
            string bookName = Console.ReadLine();
            bool bookFound = false;

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].BName == bookName)
                {
                    Books[i] = (Books[i].BName, Books[i].BAuthor, Books[i].ID, Books[i].Quantity + 1);
                    Console.WriteLine($"[{CurrentUser}] has successfully returned the book.");
                    bookFound = true;
                    break;
                }
            }

            if (!bookFound)
            {
                Console.WriteLine("Book not found.");
            }
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
                        writer.WriteLine($"{book.BName}|{book.BAuthor}|{book.ID} |{book.Quantity}");
                    }
                }
                Console.WriteLine("Books saved to file successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        static void SaveAdminsToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(AdminFile))
                {
                    foreach (var admin in AdminAuth)
                    {
                        writer.WriteLine($"{admin.AdminName}|{admin.AdminPass}");
                    }
                }
                Console.WriteLine("Admin has been registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        static void SaveUsersToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(UserFile))
                {
                    foreach (var user in UserAuth)
                    {
                        writer.WriteLine($"{user.UserName}|{user.UserPass}");
                    }
                }
                Console.WriteLine("User has been registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to file: {ex.Message}");
            }
        }

        static void DisplayStatistics()
        {
            int totalBooks = Books.Count;
            int availableBooks = 0;
            int borrowedBooks = 0;

            foreach (var book in Books)
            {
                if (book.Quantity > 0)
                {
                    availableBooks++;
                }
                else
                {
                    borrowedBooks++;
                }
            }

            Console.WriteLine("Library Statistics:");
            Console.WriteLine($"Total number of books: {totalBooks}");
            Console.WriteLine($"Number of books available: {availableBooks}");
            Console.WriteLine($"Number of books borrowed: {borrowedBooks}");
        }

    }

}



