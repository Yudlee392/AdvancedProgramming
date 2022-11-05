using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    internal class Contract
    {
        private int contractId;
        private int borrowerId;
        private DateTime dayRented;
        private string status;
        private Borrowed borrowed;
        public List<Detail> details = new List<Detail>();
        
        public int ContractId { get { return contractId; } }
        public int BorrowerId { get { return borrowerId; }set { borrowerId = value; } }
        public DateTime DayRented { get { return dayRented; } set { dayRented = value; } }
        public string Status 
        { 
            get { return status; } 
            set 
            {  
                if(value != "Available"&& value != "Unvailable")
                {
                    throw new ArgumentException("This status isn't valid.Valid status is: Available,Unvailable.");
                }
                status = value;
            } 
        }
        public Borrowed Borrowed
        {
            get { return borrowed; }
            set { borrowed = value; }
        }
        
        public Contract() 
        {
            if(Program.contracts.Count > 1)
            {
                int setId = Program.contracts[Program.contracts.Count-1].ContractId;
                this.contractId =setId+1;
            }
            else { this.contractId = 1; }
            this.dayRented = DateTime.Now;
        }
        public List<Detail> ListDetails()
        {
            List<Detail> Detail = new List<Detail>();
            return Detail;
        }
        public decimal Fee()
        {
            decimal total = 0;
            foreach(Detail detail in details)
            {
                total += detail.Book.Fee;
            }
            return total;
        }
    }
}
