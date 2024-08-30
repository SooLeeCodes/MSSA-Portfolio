using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms_Calculator
{
    interface ICalculator
    {
        void Add();
        void Subtract();
        void Divide();
        void Multiply();
    }

    internal class Math : ICalculator
    {
        public double numOne { get; set; }
        public double numTwo { get; set; }
        public double numResult { get; set; }
        
        public void Add()
        {
            numResult = numOne + numTwo;
        }

        public void Subtract()
        {
            numResult = numOne - numTwo;
        }

        public void Divide()
        {
            numResult = numOne / numTwo;
        }

        public void Multiply()
        {
            numResult = numOne * numTwo;
        }


    }
}
