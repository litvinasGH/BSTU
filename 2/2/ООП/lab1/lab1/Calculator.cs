using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    internal interface Ections
    {
        int AND_Calc();
        int OR_Calc();
        int XOR_Calc();
        int NO_Calc();
    }
    
    internal class Calculator : Ections
    {
        private int first_num;
        private int second_num;

        public Calculator(string first_num, string second_num, int num, bool b)
        {
            this.first_num = Convert.ToInt32(first_num, num);
            if (!b)
            this.second_num = Convert.ToInt32(second_num, num);
        }

        public int AND_Calc()
        {
            return first_num & second_num;
        }

        public int NO_Calc()
        {
            return first_num | second_num;
        }

        public int OR_Calc()
        {
            return first_num ^ second_num;
        }

        public int XOR_Calc()
        {
            return ~first_num;
        }
    }
}
