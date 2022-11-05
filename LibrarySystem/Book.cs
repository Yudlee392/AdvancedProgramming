using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    internal class Book
    {
        private int bookid;
        private string name;
        private decimal fee;
        private int quantity;
        private string author;
        

        public int BookId { get { return bookid; } }
        public string Name { get { return name; } set { name = value; } }
        public decimal Fee { get { return fee; } 
            set 
            {
                if (value == null)
                {
                    throw new ArgumentNullException("The price can't be null");
                }
                fee = value; 
            } 
        }
        public int Quantity
        {
            get { return quantity; }
            set {
                if (value < 0 || value > 5) throw new
                ArgumentOutOfRangeException(nameof(quantity), "You can borrowed only 5 books"); quantity = value; 
            } 
        }
        public string Author { get { return author; } set { author = value; } }
        public Book() { }
        public Book(string name, decimal fee, int quantity, string author)
        {
            if (Program.books.Count > 0)
            {
                int setBookID = Program.books.ElementAt(Program.books.Count - 1).BookId;
                this.bookid = setBookID + 1;
            }
            Name = name;
            Fee = fee;
            Quantity = quantity;
            Author = author;
        }
        
        public bool AvailableBook()
        {
            if (Quantity > 0)
                return true;
            else
                return false;
        }
        public bool CheckBook(string name)
        {
            foreach(Book book in Program.books)
            {
                if(book.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public override string ToString()
        {
            return $"Book ID:{bookid} | Name: {Name} | Fee: {Fee} | Quantity: {Quantity} | Author : {Author}";
        }

    }
}
