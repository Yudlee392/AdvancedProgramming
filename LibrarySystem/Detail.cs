using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    internal class Detail 
    {
        private int detailid;
        private Book book;
        private int quantity;
        public int Detailid { get { return detailid; } }
        public Book Book
        {
            get { return book; }
            set { book = value; }
        }
        public int Quantity { get { return quantity; } 
            set 
            {
                if (value < 0) { throw new ArgumentException("At least 1 "); }
                if (value == null) { throw new ArgumentException("Quantity must be positive number"); }
                    quantity = value; 
            } 
        }
        public Detail() { }
        public Detail(Book book, int quantity)
        {
            if (Program.details.Count > 0)
            {
                int setdetailID = Program.details.ElementAt(Program.details.Count - 1).Detailid;
                this.detailid = setdetailID;
            }
            Book = book;
            Quantity = quantity;
        }
        public decimal TotalFee()
        {
            return this.Quantity * this.book.Fee;
        }
    }
}
