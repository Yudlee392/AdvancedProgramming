using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    internal interface ManageContract
    {
        public void viewAllContract();
        public void viewDetailsContract();
        public void EditContract();
    }
}
