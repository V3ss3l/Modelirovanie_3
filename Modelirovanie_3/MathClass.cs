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
        public double[] arrM { get; set; }
        public double[] arrP { get; set; }
        private Random rand;

        public MathClass(Form1 mainForm)
        {
            _mainForm = mainForm;
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

        /// <summary>
        /// Метод для генерации чисел
        /// </summary>
        private void GenerateSequenceForArrM(int lenOfSeq)
        {
            for (int i = 0; i < lenOfSeq; i++)
            {
                var number = rand.Next(100);
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
                p = arrM[i] / (double) lenOfSeq;
                mathExpected += i * p;
                disp += DispersionOfSequence(mathExpected, i, p);
            }
            return new[] { mathExpected, disp };
        }

        /// <summary>
        /// Метод для нахождения Дисперсии СВ Х
        /// </summary>
        private double DispersionOfSequence(double ExpectedValue, int Xi, double Pi) => Math.Pow(Xi - ExpectedValue, 2) * Pi;
    }
}
