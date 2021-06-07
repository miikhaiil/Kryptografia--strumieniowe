using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace strumieniowe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        public class LFSR
        {
            List<int> elements;
            int[] register;
            int xor;
            public LFSR(string polynomial, string seed)
            {
                register = new int[seed.Length];
                for(int i = 0; i < seed.Length; i++)
                {
                    register[i] = seed[i] - '0';
                }
                elements = new List<int>();
                for(int i = 0; i < polynomial.Length; i++)
                {
                    if(polynomial[i] == '1')
                    {
                        elements.Add(i);
                    }
                }
            }

            public void swapFirst(int x)
            {
                register[0] = x;
            }

            public void Shift()
            {
                for (int i = register.Length - 1; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        register[i] = xor;
                    }
                    else
                    {
                        register[i] = register[i - 1];
                    }
                }
            }

            public int Xor()
            {
                xor = 0;
                foreach (int i in elements)
                {
                    xor = xor ^ register[i];
                }

                return xor;
            }
        }


        public string CA(string data, string polynomial, string seed)
        {
            string result = "";
            int x;
            LFSR generator = new LFSR(polynomial, seed);
            for(int i = 0; i < data.Length; i ++)
            {
                x = generator.Xor() ^ (data[i] - '0');
                result += x;
                generator.Shift();
                generator.swapFirst(x);
            }
            return result;
        }

        public string CAdec(string data, string polynomial, string seed)
        {
            string result = "";
            int x;
            LFSR generator = new LFSR(polynomial, seed);
            for (int i = 0; i < data.Length; i++)
            {
                x = generator.Xor() ^ (data[i] - '0');
                result += x;
                generator.Shift();
                generator.swapFirst(data[i] - '0');
            }
            return result;
        }

        public string SSC(string data, string polynomial, string seed)
        {
            string result = "";
            LFSR generator = new LFSR(polynomial, seed);
            for (int i = 0; i < data.Length; i++)
            {
                result += (data[i] - '0') ^ generator.Xor();
                generator.Shift();
            }
            return result;
        }

        private void onClickCipher(object sender, RoutedEventArgs e)
        {
            SSCresult.Text = SSC(Data.Text, SSCpolynomial.Text, SSCseed.Text);
            SSCdecipher.Text = SSC(SSCresult.Text, SSCpolynomial.Text, SSCseed.Text);
            CAresult.Text = CA(Data.Text, CApolynomial.Text, CAseed.Text);
            CAdecipher.Text = CAdec(CAresult.Text, CApolynomial.Text, CAseed.Text);

        }
    }
}
