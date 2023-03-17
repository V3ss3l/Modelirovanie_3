using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Modelirovanie_3
{
    internal class MathClass
    {
        private Form1 _mainForm;
        public int[] arrM { get; set; }
        public int[] arrP { get; set; }
        private Random rand;

        public MathClass(Form1 mainForm)
        {
            _mainForm = mainForm;
            this.arrM = new int[100];
            this.arrP = new int[100];
            rand = new Random();
        }

        public double[] Start(int lenOfSeq)
        {
            GenerateSequenceForArrM(lenOfSeq);
            GenerateSequenceForArrP();
            return ExpectedValue();
        }

        /// <summary>
        /// Метод для генерации чисел
        /// </summary>
        public void GenerateSequenceForArrM(int lenOfSeq)
        {
            for (int i = 0; i < lenOfSeq; i++)
            {
                var number = rand.Next(100);
                arrM[number]++;
            }

        }

        public void GenerateSequenceForArrP()
        {
            arrP[0] = 0;
            arrP[1] = arrM[0];
            for (int i = 2; i < arrP.Length; i++)
            {
                arrP[i] = arrP[i - 1] + arrM[i - 1];
            }
        }
        /// <summary>
        /// Метод для нахождения математического ожидания СВ Х
        /// </summary>
        public double[] ExpectedValue()
        {
            double mathExpected = 0;
            double p = 0;
            double disp = 0;
            for (int i = 0; i < arrM.Length; i++)
            {
                p = arrM[i] / (double) arrM.Length;
                mathExpected += i * p;
                disp += DispersionOfSequence(mathExpected, i, p);
            }
            return new[] { mathExpected, disp};
        }

        /// <summary>
        /// Метод для нахождения Дисперсии СВ Х
        /// </summary>
        public double DispersionOfSequence(double ExpectedValue, int Xi, double Pi)
        {
            return Math.Pow((Xi - ExpectedValue), 2) * Pi;
        }
    }
}
