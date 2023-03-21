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
        private const int A = 16807;
        private const int M = 2147483647;
        #endregion
        private Form1 _mainForm;
        public double[] arrM { get; set; }
        public double[] arrP { get; set; }
        private Random rand;
        private double seed;
        private int lenOfSeq;

        public MathClass(Form1 mainForm, double seed)
        {
            _mainForm = mainForm;
            this.seed = seed;
            this.arrM = new double[100];
            this.arrP = new double[100];
            rand = new Random();
        }

        public double[] Start(int lenOfSeq)
        {
            this.lenOfSeq = lenOfSeq;
            arrM = new double[100];
            arrP = new double[100];
            GenerateSequenceForArrM();
            double[] buff = ExpectedValue();
            TransitValuesFromArrM();
            GenerateSequenceForArrP();
            return buff;
        }

        public int LehmerN(int N) => (int) (N * Lehmer());

        /// <summary>
        /// Метод для генерации чисел
        /// </summary>
        private void GenerateSequenceForArrM()
        {
            int number = 0;
            for (int i = 0; i < lenOfSeq; i++)
            {
                if (_mainForm._randomType) number = rand.Next(100);
                else number = LehmerN(100);
                arrM[number]++;
            }

        }

        private void TransitValuesFromArrM()
        {
            for (int i = 0; i < arrM.Length; i++)
                arrM[i] = arrM[i] / lenOfSeq;
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
        private double[] ExpectedValue()
        {
            double mathExpected1 = 0;
            double mathExpected2 = 0;
            for (var xi = 0; xi < arrM.Length; xi++)
            {
                var p = arrM[xi] / lenOfSeq;
                mathExpected1 += xi * p;
                mathExpected2 += xi * xi * p;
            }

            var dsp = mathExpected2 - mathExpected1 * mathExpected1;
            return new[] { mathExpected1, dsp };
        }


        /// <summary>
        /// Метод генерирующий числа по алгоритму Лемера из лекции
        /// </summary>
        private double Lehmer()
        {
            seed = (A * seed) % M;
            return seed / M;
        }
    }
}    
