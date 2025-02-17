using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class Author
    {
        //«Автор»: ФИО, страна,  ID и т.д. 
        private readonly int _id;
        private string full_name;
        private string contry;
        private static int count = 0;

        int Id
        {
            get { return _id; }
        }


    }
}
