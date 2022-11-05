using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public interface ManageBook
    {
        public void AddNewBook();
        public void UpdateBook();
        public void DeleteBook();
        public void ViewAllBook();
    }
}
