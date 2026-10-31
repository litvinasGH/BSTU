using System;

namespace lab1
{
    internal interface ICalculator
    {
        int AND_Calc();
        int OR_Calc();
        int XOR_Calc();
        int NO_Calc();
    }

    internal class Calculator : ICalculator
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

        public int OR_Calc()
        {
            return first_num | second_num;
        }

        public int XOR_Calc()
        {
            return first_num ^ second_num;
        }

        public int NO_Calc()
        {
            return ~first_num;
        }
    }
}
