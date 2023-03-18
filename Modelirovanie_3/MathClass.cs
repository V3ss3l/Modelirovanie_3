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
        #region Const
        private const int a = 16807;
        private const int m = 2147483647;
        private const int q = 127773;
        private const int r = 2836;
        #endregion
        private Form1 _mainForm;
        public double[] arrM { get; set; }
        public double[] arrP { get; set; }
        private Random rand;
        private int seed;


        public MathClass(Form1 mainForm, int seed)
        {
            _mainForm = mainForm;
            this.seed = seed;
            this.arrM = new double[100];
            this.arrP = new double[100];
            rand = new Random();
        }

        public double[] Start(int lenOfSeq)
        {
            arrM = new double[100];
            GenerateSequenceForArrM(lenOfSeq);
            double[] buff = ExpectedValue(lenOfSeq);
            TransitValuesFromArrM();
            GenerateSequenceForArrP();
            return buff;
        }

        public int GetLehmerNumber() => (int)((100 - 0) * Lehmer() + 0);
        /// <summary>
        /// Метод для генерации чисел
        /// </summary>
        private void GenerateSequenceForArrM(int lenOfSeq)
        {
            int number = 0;
            for (int i = 0; i < lenOfSeq; i++)
            {
                if (_mainForm._randomType) number = rand.Next(100);
                else number = GetLehmerNumber();
                arrM[number]++;
            }

        }

        private void TransitValuesFromArrM()
        {
            for (int i = 0; i < arrM.Length; i++)
                arrM[i] = arrM[i] / arrM.Length;
        }

        private void GenerateSequenceForArrP()
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
        private double[] ExpectedValue(int lenOfSeq)
        {
            double mathExpected = 0;
            double p = 0;
            double disp = 0;
            for (int i = 0; i < arrM.Length; i++)
            {
                p = arrM[i] / (double)lenOfSeq;
                mathExpected += i * p;
                disp += DispersionOfSequence(mathExpected, i, p);
            }
            return new[] { mathExpected, disp };
        }

        /// <summary>
        /// Метод для нахождения Дисперсии СВ Х
        /// </summary>
        private double DispersionOfSequence(double ExpectedValue, int Xi, double Pi) => Math.Pow(Xi - ExpectedValue, 2) * Pi;

        private double Lehmer()
        {
            int hi = seed / q;
            int lo = seed % q;
            seed = (a * lo) - (r * hi);
            if (seed <= 0)
                seed = seed + m;
            return (seed * 1.0) / m;
        }
    }
}    
