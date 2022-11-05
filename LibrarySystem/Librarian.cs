using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibrarySystem
{
    class Librarian : User , ManageBook, ManageContract
    {
        private string phone;
        private string mail;
        public string Phone
        {
            get { return phone; }
            set
            {
                if (value.Length < 4 || value.Length > 6) throw new ArgumentOutOfRangeException(nameof(Phone), "Phone must be have 5 number");
                phone = value;
                if (!IsNumber(value))
                {
                    throw new ArgumentNullException("The phone number must be numeric");
                }
                phone = value;
            }
        }
        public string Mail { get { return mail; } set { mail = value; } }
        public Librarian() { }
        public Librarian(int id, string name, string password,string phone, string mail):base(id,name,password)
        {
            Phone = phone;
            Mail = mail;
        }
        public void ViewSystemLibrary()
        {
            Console.WriteLine("===============================Welcome to Library===============================");
            Console.WriteLine("|                            1.View All Contract                               |");
            Console.WriteLine("|                            2.Add new book                                    |");
            Console.WriteLine("|                            3.Update book                                     |");
            Console.WriteLine("|                            4.Remove book                                     |");
            Console.WriteLine("|                            5.View all books                                  |");
            Console.WriteLine("|                            6.View contract                                   |");
            Console.WriteLine("|                            7.Update contract                                 |");
            Console.WriteLine("|                            8.Logout                                          |");
            Console.WriteLine("================================================================================");
        }
        public void viewAllContract()
        {
            Console.WriteLine("===============================List Contract===============================");
            for(int i=0;i< Program.contracts.Count; i++)
            {
                Console.WriteLine($"ID Contract: {Program.contracts[i].ContractId} | ID Borrower: {Program.contracts[i].BorrowerId}" +
                    $" | Day rented: {Program.contracts[i].DayRented} | Status: {Program.contracts[i].Status}");

            }
            Console.WriteLine("===========================================================================");
        }
        
        public void viewDetailsContract()
        {
            bool check = false;
            Console.WriteLine("Please enter ID of contract you want to view: ");
            int id = int.Parse(Console.ReadLine());
            for(int i = 0;i< Program.contracts.Count; i++)
            {
                if (Program.contracts[i].ContractId == id)
                {
                    check = true;
                    Console.WriteLine("===============================Detail===============================");
                    Console.WriteLine($"Id:{Program.contracts[i].ContractId}");
                    Console.WriteLine($"Status:{Program.contracts[i].Status}");
                    Console.WriteLine("=====================================================================");
                    
                    foreach(Borrower borrower in Program.borrowers)
                    {
                        if(borrower.Id == Program.contracts[i].BorrowerId) 
                        {

                            Console.WriteLine("===============================Borrower===============================");
                            Console.WriteLine($"                        Id: {borrower.Id}                            ");
                            Console.WriteLine($"                        Name: {borrower.Name}                        ");
                            Console.WriteLine($"                        Phone: {borrower.Phone}                      ");
                            Console.WriteLine($"                        Mail: {borrower.Mail}                        ");
                            Console.WriteLine($"=====================================================================");
                        }break;
                    }
                    Console.WriteLine("===============================Book Rented===============================");
                    foreach(Detail detail in Program.contracts[i].details)
                    {
                        Console.WriteLine($"Id:{detail.Detailid}|Name: {detail.Book.Name}|Fee:{detail.Book.Fee}|Quantity: {detail.Quantity}|Total:{detail.TotalFee()}");
                    }
                }
            }
        }
        public void EditContract()
        {
            Console.WriteLine("Enter id to edit");
            int id = int.Parse(Console.ReadLine());
            Contract contract = Program.contracts.Find(c => c.ContractId ==id);
            Console.WriteLine("Set status for this oder");
            string status = Console.ReadLine();
            try
            {
                contract.Status = status;
            }catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AddNewBook()
        {
            Console.WriteLine("Enter name of Book: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter fee of Book: ");
            decimal fee = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter quantity of book: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter author of book");
            string author = Console.ReadLine();
            try
            {
                Book book = new Book(name, fee, quantity, author);
                if (!book.CheckBook(name))
                {
                    Program.books.Add(book);
                    Console.WriteLine("Book was added");
                }
                else { Console.WriteLine("Book was existed"); }
            }catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteBook()
        {
            int IDDeletebook = -1;
            Console.WriteLine("Enter Id book to delete: ");
            int id = int.Parse(Console.ReadLine());
            foreach(Book book in Program.books)
            {
                if(book.BookId == id)
                {
                    IDDeletebook = book.BookId;
                }
            }
            if (IDDeletebook > 0)
            {
                Program.books.Remove(Program.books.Find(b => b.BookId == IDDeletebook));
                Console.WriteLine("Book was deleted");
            }
            else
            {
                Console.WriteLine("Book doesn't exist");
            }
        }
        public void UpdateBook()
        {
            int IDUpdateBook = -1;
            Console.WriteLine("Enter Id want to update");
            int id = int.Parse(Console.ReadLine());
            foreach(var book in Program.books)
            {
                if(id == book.BookId)
                {
                    IDUpdateBook = book.BookId;
                }
            }
            if(IDUpdateBook > 0)
            {
                Console.WriteLine("Enter Name of Book:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Fee of Book:");
                decimal fee = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Enter Quantity of Book:");
                int quantity = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter Auhor of Book:");
                string author = Console.ReadLine();
                try
                {
                    Book updatedBook = Program.books.Find(b => b.BookId == IDUpdateBook);
                    updatedBook.Fee = fee;
                    updatedBook.Quantity = quantity;
                    updatedBook.Author = author;
                    updatedBook.Name = name;
                }catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else { Console.WriteLine("Book doesn't exist"); }
        }
        public void ViewAllBook()
        {
            Console.WriteLine("===============================BookList===============================");
            foreach(var book in Program.books)
            {
                Console.WriteLine($"ID: {book.BookId}| Name: {book.Name}|Fee: {book.Fee}|Quantity:{book.Quantity}|Author:{book.Author}");
            }
            Console.WriteLine("======================================================================");
        }
    }
    
}
