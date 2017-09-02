using System;

namespace EasyPhoto.ImageProcess
{
    /// <summary>
    /// ����ͳ����
    /// </summary>
    public class Statistics
    {
        private int[] Sequence;
        private int length;
        private int min, max;
        private int minIndex, maxIndex;

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        public int[] Value
        {
            get
            {
                return Sequence;
            }
        }

        /// <summary>
        /// ��ȡ�����⻯�����������
        /// </summary>
        public byte[] Equalizer
        {
            get
            {
                // �ȼ������
                double[] Probability = this.Probability;

                // S Ϊ���ȼ��Ķ����֣�����ɢͼ������ȱ任����
                double[] S = new double[256];

                // L �������ڼ�¼���⻯���������ֵ
                byte[] L = new byte[256];

                // ���о��⻯����
                for (int i = 0; i < 256; i++)
                {
                    if (i == 0)
                    {
                        S[0] = Probability[0];
                    }
                    else
                    {
                        S[i] = S[i - 1] + Probability[i];
                    }

                    L[i] = (byte)(255 * S[i] + 0.5);
                } // i

                return L;
            }
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        public double[] Probability
        {
            get
            {
                double total = (double)this.Sum;
                double[] probability = new double[256];

                // ��������ȼ������ܶ�
                for (int i = 0; i < 256; i++)
                {
                    probability[i] = Sequence[i] / total;
                } // i

                return probability;
            }
        }

        /// <summary>
        /// ��ȡ�����ۼӺ�
        /// </summary>
        public int Sum
        {
            get
            {
                int total = 0;

                for (int i = 0; i < length; i++)
                {
                    total += Sequence[i];
                } // i

                return total;
            }
        }

        /// <summary>
        /// ��ȡ��Ȩƽ����
        /// </summary>
        public double Mean
        {
            get
            {
                int mean = 0;

                for (int i = 0; i < length; i++)
                {
                    mean += i * Sequence[i];
                } // i

                return (double)mean / (double)this.Sum;
            }
        }

        /// <summary>
        /// ��ȡ��׼ƫ��
        /// </summary>
        public double StdDev
        {
            get
            {
                double mean = this.Mean;
                int total = this.Sum;

                double stddev = 0;
                for (int i = 0; i < length; i++)
                {
                    double t = (double)i - mean;
                    stddev += t * t * Sequence[i];
                } // i

                return Math.Sqrt(stddev / total);
            }
        }

        /// <summary>
        /// ��ȡ��ֵ
        /// </summary>
        public int Median
        {
            get
            {
                int halfTotal = this.Sum / 2;

                // ������ֵ
                int total = 0;
                int median = 0;
                while (total < halfTotal)
                {
                    total += Sequence[median];
                    median++;
                } // while

                return median - 1;
            }
        }

        /// <summary>
        /// ��ȡ���ֵ
        /// </summary>
        public int Maximum
        {
            get
            {
                return max;
            }
        }

        /// <summary>
        /// ��ȡ��Сֵ
        /// </summary>
        public int Minimum
        {
            get
            {
                return min;
            }
        }

        /// <summary>
        /// ��ȡ���ֵ����
        /// </summary>
        public int MaxIndex
        {
            get
            {
                return maxIndex;
            }
        }

        /// <summary>
        /// ��ȡ��Сֵ����
        /// </summary>
        public int MinIndex
        {
            get
            {
                return minIndex;
            }
        }


        /// <summary>
        /// ��������ͳ������
        /// </summary>
        /// <param name="sequence">��������</param>
        public Statistics(int[] sequence)
        {
            this.Sequence = sequence;
            length = Sequence.Length;

            MaxMin();
        } // end of Statistics


        /// <summary>
        /// �����ֵ����Сֵ
        /// </summary>
        private void MaxMin()
        {
            max = min = Sequence[0];
            maxIndex = minIndex = 0;

            // ���������Сֵ
            for (int i = 1; i < length; i++)
            {
                int t = Sequence[i];

                if (t > max)
                {
                    max = t;
                    maxIndex = i;
                }

                if (t < min)
                {
                    min = t;
                    minIndex = i;
                }
            } // i
        } // end of MaxMin


    }
}
