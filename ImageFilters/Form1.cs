using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZGraphTools;

namespace ImageFilters
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        byte[,] ImageMatrix;
        byte[,] dummy, dummy2;
        string path;
        string algo1, algo2;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                path = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(path);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);

            }

        }

        private void btnZGraph_Click(object sender, EventArgs e)
        {
            // Make up some data points from the N, N log(N) functions
            int N = int.Parse(textBox4.Text);
            int start = 3;
            int index = ((N - 3) / 2) + 1;
            double[] x_values = new double[index];
            double[] y_values_C = new double[index];
            double[] y_values_Q = new double[index];

            for (int i = 0; start <= N; i++)
            {
                x_values[i] = start;
                int result = Environment.TickCount & Int32.MaxValue;
                dummy = AdaptiveFilter.filter(ImageMatrix, start, "Counting Sort");
                int result2 = Environment.TickCount & Int32.MaxValue;
                y_values_C[i] = result2 - result;
                result = Environment.TickCount & Int32.MaxValue;
                dummy = AdaptiveFilter.filter(ImageMatrix, start, "Quick Sort");
                result2 = Environment.TickCount & Int32.MaxValue;
                y_values_Q[i] = result2 - result;
                start += 2;
            }

            //Create a graph and add two curves to it
            ZGraphForm ZGF = new ZGraphForm("Adaptive Filter", "Window Size", "Execution Time");
            ZGF.add_curve("Using Counting Sort", x_values, y_values_C,Color.Red);
            ZGF.add_curve("Using Quick Sort", x_values, y_values_Q, Color.Blue);
            ZGF.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Make up some data points from the N, N log(N) functions
            int N = int.Parse(textBox4.Text);
            int start = 3;
            int index = ((N - 3) / 2) + 1;
            double[] x_values = new double[index];
            double[] y_values_C = new double[index];
            double[] y_values_Q = new double[index];

            for (int i = 0; start <= N; i++)
            {
                x_values[i] = start;
                int result = Environment.TickCount & Int32.MaxValue;
                dummy2 = AlphaTrim.alphaTrim_Counting(ImageMatrix, start, int.Parse(textBox1.Text));
                int result2 = Environment.TickCount & Int32.MaxValue;
                y_values_C[i] = result2 - result;
                result = Environment.TickCount & Int32.MaxValue;
                dummy2 = AlphaTrim.alphaTrim_KTH(ImageMatrix, start, int.Parse(textBox1.Text));
                result2 = Environment.TickCount & Int32.MaxValue;
                y_values_Q[i] = result2 - result;
                start += 2;
            }

            //Create a graph and add two curves to it
            ZGraphForm ZGF = new ZGraphForm("Alpha Filter", "Window Size", "Execution Time");
            ZGF.add_curve("Using Counting Sort", x_values, y_values_C, Color.Red);
            ZGF.add_curve("Using Kth Element", x_values, y_values_Q, Color.Blue);
            ZGF.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //bdl el path w open image h pass el image matrix elli kharga mn filter
            ImageMatrix = ImageOperations.OpenImage(path);

            if(algo2 == "Counting Sort")
            {
                ImageMatrix = AlphaTrim.alphaTrim_Counting(ImageMatrix, int.Parse(textBox3.Text), int.Parse(textBox1.Text));
            }
            else if(algo2 == "Kth Element")
            {
                ImageMatrix = AlphaTrim.alphaTrim_KTH(ImageMatrix, int.Parse(textBox3.Text), int.Parse(textBox1.Text));
            }
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //bdl el path w open image h pass el image matrix elli kharga mn filter
            ImageMatrix = ImageOperations.OpenImage(path);
            ImageMatrix = AdaptiveFilter.filter(ImageMatrix, int.Parse(textBox2.Text), algo1);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            algo1 = comboBox3.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            algo2 = comboBox2.Text;
        }
    }
}