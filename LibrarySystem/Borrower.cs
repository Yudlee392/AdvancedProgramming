using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    class Borrower:User 
    {
        private string phone;
        private string mail;
        internal List<Detail> details = new List<Detail>();
        internal List<Contract> contracts = new List<Contract>();
        public Borrower(int id, string name, string password, string phone, string mail) : base(id, name, password)
        {
            Phone = phone;
            Mail = mail;
        }
        public Borrower()
        {
            if (Program.borrowers.Count > 0)
            {
                int setID = Program.borrowers.ElementAt(Program.borrowers.Count - 1).Id;
                this.Id = setID + 1;
            }
            else { this.Id = 1; }
        }
        public string Phone 
        { 
            get { return phone; }
            set
            {
                if (value.Length < 4 || value.Length > 6) throw new ArgumentOutOfRangeException(nameof(Phone), "Phone must be have 5 number");
                if(value == null)
                {
                    throw new ArgumentNullException("The phone number can't be null");
                }
                if (!IsNumber(value))
                {
                    throw new ArgumentNullException("The phone number must be numeric");
                }
                phone = value;
            }
        }
        public string Mail 
        { 
            get { return mail; } 
            set 
            {
                if(value == null)
                {
                    throw new ArgumentNullException("Mail not null this field");
                }
                if (!IsValidEmail(value))
                {
                    throw new ArgumentNullException("Mail isn't valided");
                }
                mail = value; 
            } 
        }
        
        
        public override void ViewSystemLibrary()
        {
            Console.WriteLine("===============================Welcome to Library===============================");
            Console.WriteLine("|                            1.View your profile                               |");
            Console.WriteLine("|                            2.Edit your progile                               |");
            Console.WriteLine("|                            3.View List Book                                  |");
            Console.WriteLine("|                            4.Search book                                     |");
            Console.WriteLine("|                            5.Add book to details                             |");
            Console.WriteLine("|                            6.View your details                               |");
            Console.WriteLine("|                            7.Delete book from contract                       |");
            Console.WriteLine("|                            8.Total Fee and Status                            |");
            Console.WriteLine("|                            9.View your Contract                              |");
            Console.WriteLine("|                            10.Logout                                         |");
            Console.WriteLine("================================================================================");
        }
        public void ViewProfile(int id)
        {
            foreach(var Borrower in Program.borrowers)
            {
                if(Borrower.Id == id)
                {
                    Console.WriteLine("===============================Profile===============================");
                    Console.WriteLine($"|           1.Name:{Borrower.Name}                  |");
                    Console.WriteLine($"|           2.PhoneNumber:{Borrower.Phone}          |");
                    Console.WriteLine($"|           3.Mail:{Borrower.Mail}                  |");
                    Console.WriteLine("=====================================================================");
                }
            }
        }
        public void EditProfile(int id)
        {
            Console.WriteLine("Enter your Phone: ");
            string phone = Console.ReadLine();
            Console.WriteLine("Enter your Mail: ");
            string mail = Console.ReadLine();
            try
            {
                Borrower borrower = Program.borrowers.Find(b => b.Id == id);
                borrower.Phone = phone;
                borrower.Mail = mail;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ViewAllBook()
        {
            Console.WriteLine("===============================Books List===============================");
            foreach(var book in Program.books)
            {
                if (book.AvailableBook() == true)
                {
                    Console.WriteLine($"|ID:{book.BookId} Name: {book.Name} Fee:{book.Fee} Quantity:{book.Quantity} Author:{book.Author}|");
                }
            }
            Console.WriteLine("========================================================================");
        }
        public void AddBookToDetail()
        {
            bool checkBook = false;
            int bookid = -1;
            Console.WriteLine("Enter the id of book you want to borrow:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the quantity of the product you want to borrow:");
            int quantity = int.Parse(Console.ReadLine());
            foreach(Book book in Program.books)
            {
                if(book.BookId == id)
                {
                    bookid = book.BookId;
                    for(int i= 0; i < details.Count; i++)
                    {
                        if (details[i].Book == book) {
                            Console.WriteLine("This book in existed in your detail, quantity will increase");
                            checkBook = true;
                            details[i].Quantity += quantity;
                        }    
                    }
                    if (checkBook == true)
                    {
                        break;
                    }
                    else
                    {
                        Detail detail = new Detail(book,quantity);
                        details.Add(detail);
                    }
                }
            }
            if (bookid < 0)
            {
                Console.WriteLine("This book doesn't exist");
            }
        }
        public void ViewDetail()
        {
            Console.WriteLine("===============================Your Details===============================");
            for (int i = 0; i < details.Count; i++)
            {
                Console.WriteLine($"|{i+1}. Name: {details[i].Book.Name}| Fee: {details[i].Book.Fee}|Quantity:{details[i].Quantity},Total:{details[i].TotalFee()}");
            }
            Console.WriteLine("==========================================================================");
        }
        public void DeleteBook()
        {
            Console.WriteLine("Enter the name of book you want to delete");
            string name = Console.ReadLine();
            for(int i = 0; i <details.Count; i++)
            {
                if (details[i].Book.Name == name)
                {
                    details.RemoveAt(i);
                    Console.WriteLine($"The book {name} was deleted from detail");
                }
            }
        }
        public void SearchBook()
        {
            Console.WriteLine("Enter the name of the book your want to Search");
            string name = Console.ReadLine();
            Console.WriteLine("===============================Book Lists===============================");
            foreach(var book in Program.books)
            {
                if (book.Name.Contains(name))
                {
                    Console.WriteLine($"Id:{book.BookId} | Name:{book.Name} |Fee:{book.Fee}|Quantity:{book.Quantity}|Author:{book.Author}");
                }
            }
        }
        public void BookDetail(int idborrower)
        {
            Contract contract = new Contract();
            contract.BorrowerId = idborrower;
            Borrower borrower = Program.borrowers.Find(b => b.Id==idborrower);
            if (borrower.Phone != null && borrower.Mail != null)
            {
                if (details.Count > 0)
                {
                    foreach (Detail details in details)
                    {
                        Detail detail1 = new Detail(details.Book, details.Quantity);
                        contract.details.Add(detail1);
                        Book book = Program.books.Find(b => b.BookId == details.Book.BookId);
                        book.Quantity -= details.Quantity;
                        Console.WriteLine($"Quantity:{details.Quantity}");
                        Console.WriteLine($"Total Fee:{details.TotalFee()}");
                    }
                    decimal total = contract.Fee();
                    Borrowed borrowed = new Borrowed();
                    contract.Status = "Available";
                    Console.WriteLine("Done!!!");
                    Program.contracts.Add(contract);
                    contracts.Add(contract);
                }
                else
                {
                    Console.WriteLine("You don't have book in contract");
                }
            }
            else
            {
                Console.WriteLine("You have add information to profile");
            }
        }
        public void ViewContract(int idCustomer)
        {
            Console.WriteLine("===============================Your Contract===============================");
            foreach(Contract contract in contracts)
            {
                if(contract.ContractId == idCustomer)
                {
                    Console.WriteLine($"Id:{contract.ContractId}");
                    Console.WriteLine($"Day Rented: {contract.DayRented}");
                    Console.WriteLine($"Status: {contract.Status}");
                }
                foreach(Detail detail in contract.details)
                {
                    Console.WriteLine("=================================================================================================================");
                    Console.WriteLine($"|Name: {detail.Book.Name}|Fee:{detail.Book.Fee}|Quantity:{detail.Quantity}|Author:{detail.Book.Author}|TotalFee:{detail.TotalFee()}");
                    Console.WriteLine("=================================================================================================================");
                }
            }
        }
    }
}   
