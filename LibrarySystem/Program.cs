using System;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace LibrarySystem
{
    internal class Program
    {
        public static List<Librarian> librarians = new List<Librarian> ();
        public static List<Borrower> borrowers = new List<Borrower>(); 
        public static List<Contract> contracts = new List<Contract>();
        public static List<Book> books = new List<Book>();
        public static List<Detail> details = new List<Detail>();
        public static List<Borrowed> borroweds = new List<Borrowed>();
        private static void Main(string[] args)
        {
            Librarian librarian = new Librarian ();
            librarian.Name = "librarian";
            librarian.Password = "123456";
            librarians.Add(librarian);
            Book book = new Book("hihi", 312, 4, "hello");
            books.Add(book);
            Start();
        }
        public static void Start()
        {            
            int currentId;
            string choice;
            bool check = true;
            do
            {
                Console.WriteLine("--------------------Welcome to Library--------------------");
                Console.WriteLine("|1. Register                                             |");
                Console.WriteLine("|2. Login as user                                        |");
                Console.WriteLine("|3. Login as librarian                                   |");
                Console.WriteLine("|4. Exit                                                 |");
                Console.WriteLine("----------------------------------------------------------");
                Console.Write("Enter your choice: ");
                choice = (Console.ReadLine());
                switch (choice)
                {
                    case "1":
                        Borrower borrower = new Borrower ();
                        borrower.Registration();
                        borrowers.Add(borrower);
                        break;
                    case "2":
                        string cmd = "";
                        int checkCustomerID = LoginBorrower();
                        Console.WriteLine("Your ID: " + checkCustomerID);
                        if (checkCustomerID >= 0)
                        {
                            Console.WriteLine("You login successfully");
                            currentId = checkCustomerID;
                            Borrower borrower1 = CurrentBorrower(currentId);
                            do
                            {
                                borrower1.ViewSystemLibrary();
                                cmd = Console.ReadLine();
                                switch (cmd)
                                {
                                    case "1":
                                        borrower1.ViewProfile(currentId);
                                        break;
                                    case "2":
                                        borrower1.EditProfile(currentId);
                                        break;
                                    case "3":
                                        borrower1.ViewAllBook();
                                        break;
                                    case "4":
                                        borrower1.SearchBook();
                                        break;
                                    case "5":
                                        borrower1.AddBookToDetail();
                                        break;
                                    case "6":
                                        borrower1.ViewDetail();
                                        break;
                                    case "7":
                                        borrower1.DeleteBook();
                                        break;
                                    case "8":
                                        borrower1.BookDetail(currentId);
                                        break;
                                    case "9":
                                        borrower1.ViewContract(currentId);
                                        break;
                                    case "10":
                                        Console.WriteLine("Goodbye");
                                        break;
                                    default:
                                        Console.WriteLine("Please enter 1 to 10");
                                        break;
                                }

                            } while (cmd != "10");
                        }
                        else
                        {
                            Console.WriteLine("Password or username isn't correct");
                        }
                        break;
                    case "3":
                        string command = "";
                        int checkLibrarianID = LoginLibrarian();
                        if (checkLibrarianID >= 0)
                        {
                            Console.WriteLine("You login successfully");
                            currentId = checkLibrarianID;
                            Librarian librarian = CurrentLibrarian(currentId);
                            do
                            {
                                librarian.ViewSystemLibrary();
                                command = Console.ReadLine();
                                switch (command)
                                {
                                    case "1":
                                        librarian.viewAllContract();
                                        break;
                                    case "2":
                                        librarian.AddNewBook();
                                        break;
                                    case "3":
                                        librarian.UpdateBook();
                                        break;
                                    case "4":
                                        librarian.DeleteBook();
                                        break;
                                    case "5":
                                        librarian.ViewAllBook();
                                        break;
                                    case "6":
                                        librarian.viewDetailsContract();
                                        break;
                                    case "7":
                                        librarian.EditContract();
                                        break;
                                    case "8":
                                        Console.WriteLine("Goodbye");
                                        break;
                                    default:
                                        Console.WriteLine("Please input from 1 to 8");
                                        break;
                                }
                            } while (command != "8");
                        }
                        else
                        {
                            Console.WriteLine("Password or username isn't correct");
                        }
                        break;
                    default:
                        Console.WriteLine("Please enter from 1 to 3");
                        break;
                }
            } while (check == true);

        }
        public static int LoginBorrower()
        {
            Console.WriteLine("Please enter user name");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter password");
            string pass = Console.ReadLine();
            for (int i = 0; i < borrowers.Count; i++)
            {
                if (name == borrowers.ElementAt(i).Name && pass == borrowers.ElementAt(i).Password)
                {
                    return borrowers.ElementAt(i).Id;
                }
            }
            return -1;
        }
        public static int LoginLibrarian()
        {
            Console.WriteLine("Please enter user name");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter password");
            string pass = Console.ReadLine();
            for (int i = 0; i < librarians.Count; i++)
            {
                if (name == librarians.ElementAt(i).Name && pass == librarians.ElementAt(i).Password)
                {
                    return librarians.ElementAt(i).Id;
                }
            }
            return -1;
        }

        public static Librarian CurrentLibrarian(int id)
        {
            for (int i = 0; i < librarians.Count; i++)
            {
                if (librarians[i].Id == id) return librarians[i];
            }
            return librarians[0];
        }

        public static Borrower CurrentBorrower(int id)
        {
            for (int i = 0; i < librarians.Count; i++)
            {
                if (borrowers[i].Id == id) return borrowers[i];
            }
            return borrowers[0];
        }
    }
}