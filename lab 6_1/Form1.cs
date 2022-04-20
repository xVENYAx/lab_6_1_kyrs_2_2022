using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Threading;

namespace lab_6_1
{
    public partial class Form1 : Form
    {
        Mutex mut = new Mutex();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false; // вимикаємо перевірку в Debug

            for (int i = 0; i < 10; i++)
            {
                Thread myThread = new Thread(changeLabel);
                string name = i.ToString();
                myThread.Start(name);
            }
        }

        void changeLabel(object obj)
        {
            while (true)
            {
                if (obj is string str)
                {
                    mut.WaitOne();

                    label1.Text = str;
                    int seed = Convert.ToInt32(str);
                    Random fixRand = new Random(seed);
                    label1.BackColor = Color.FromArgb(fixRand.Next(255), fixRand.Next(255), fixRand.Next(255));


                    Thread.Sleep(1000);

                    mut.ReleaseMutex();
                }
            }
        }
    }
}
