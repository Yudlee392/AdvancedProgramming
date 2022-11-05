using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    internal class Borrowed
    {
        private int borrowedId;
        private decimal totalFee;
        public int BorrowedId { get { return borrowedId; } }
        public decimal TotalFee { get { return totalFee; } set { totalFee = value; } }
        public Borrowed()
        {
            if(Program.borroweds.Count>0)
            {
                int setborrowedID = Program.borroweds.ElementAt(Program.borroweds.Count - 1).borrowedId;
                this.borrowedId = setborrowedID + 1;
            }
            else
            {
                this.borrowedId = 1;
            }
    
        }        
    }
}
