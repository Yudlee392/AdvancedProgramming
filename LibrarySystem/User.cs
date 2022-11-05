using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    internal class User
    {
        private int id;
        private string name;
        private string password;
        public User() { }
        public User(int id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }
        public int Id { get { return id; } set { id = value; } }
        public string Name {
            get { return name; } 
            set 
            {
                if (value == " ")
                {
                    throw new ArgumentException("You need type you name");
                }
                if (!CheckWord(value))
                {
                    throw new ArgumentException("The name must be alphabet characters");
                }
                name = value; 
            } 
        }
        public string Password { 
            get { return password; } 
            set 
            {
                if (value.Length < 5 || value.Length > 7)
                {
                    throw new ArgumentOutOfRangeException(nameof(password), "Password must be have 6 words");
                }
                    password = value; 
            } 
        }
        
        public void Registration()
        {
            Console.WriteLine("Enter name to registration: ");
            string name = Console.ReadLine();
            Console.WriteLine("Type password to registration: ");
            string password = Console.ReadLine();
            this.Name = name;
            this.Password = password;
        }
        public virtual void ViewSystemLibrary()
        {
            Console.WriteLine("Let View System Library");
        }
        public bool CheckWord(string str)
        {
            str = str.Trim();
            str = str.ToLower();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= 'a' && str[i] <= 'z')
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsNumber(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= '0' && str[i] <= '9')
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }

}
