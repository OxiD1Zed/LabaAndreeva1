using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1лаба_Андреева
{
    public partial class Form1 : Form
    {  
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BackColor = Color.AntiqueWhite;
        }

        private void buttonFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\Users\";
            openFileDialog.Filter = "|*.txt";
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileInfo file = new FileInfo(openFileDialog.FileName);
                    using (StreamReader sr = new StreamReader(file.OpenRead()))
                    {
                        textBox1.Text = sr.ReadToEnd();
                    }
                }
                else
                {
                    throw new FormatException("Файл не выбран");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonRandom_Click(object sender, EventArgs e)
        {
            try
            {
                Random r = new Random();
                string array = "";
                int arraySize = r.Next(textBox3.Text != "" ? int.Parse(textBox3.Text.Trim()) : 2,
                                       textBox4.Text != "" ? int.Parse(textBox4.Text.Trim()) : 1000);
                for (int i = 0; i < arraySize; i++)
                {
                    array += r.Next(10000) + " ";
                }
                textBox1.Text = array;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string[] _arr = textBox1.Text.Trim(' ').Split(' ', ',', '.');
            int[] arr = new int[_arr.Length];
            try
            {
                
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = int.Parse(_arr[i]);
                }
                arr = mergeSort(arr);
                textBox2.Text = String.Join(" ", arr);
                timerLabel.Text = (sw.ElapsedMilliseconds / 1000d) + " сек.";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sw.Stop();
            }
            

            int[] mergeSort(int[] array)
            {
                int[] left;
                int[] right;
                int[] result = new int[array.Length];

                if (array.Length <= 1)
                    return array;
    
                int midPoint = array.Length / 2;
                
                left = new int[midPoint];

                if (array.Length % 2 == 0)
                    right = new int[midPoint];
                
                else
                    right = new int[midPoint + 1];
                
                for (int i = 0; i < midPoint; i++)
                    left[i] = array[i];
                 
                int x = 0;
                
            for (int i = midPoint; i < array.Length; i++)
                {
                    right[x] = array[i];
                    x++;
                }
                
                left = mergeSort(left);
                
                right = mergeSort(right);
                //Merge our two sorted arrays
                result = merge(left, right);
                return result;
            }

            //This method will be responsible for combining our two sorted arrays into one giant array
            int[] merge(int[] left, int[] right)
            {
                int resultLength = right.Length + left.Length;
                int[] result = new int[resultLength];
                //
                int indexLeft = 0, indexRight = 0, indexResult = 0;
                //while either array still has an element
                while (indexLeft < left.Length || indexRight < right.Length)
                {
                    //if both arrays have elements  
                    if (indexLeft < left.Length && indexRight < right.Length)
                    {
                        //If item on left array is less than item on right array, add that item to the result array 
                        if (left[indexLeft] <= right[indexRight])
                        {
                            result[indexResult] = left[indexLeft];
                            indexLeft++;
                            indexResult++;
                        }
                        // else the item in the right array wll be added to the results array
                        else
                        {
                            result[indexResult] = right[indexRight];
                            indexRight++;
                            indexResult++;
                        }
                    }
                    //if only the left array still has elements, add all its items to the results array
                    else if (indexLeft < left.Length)
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    //if only the right array still has elements, add all its items to the results array
                    else if (indexRight < right.Length)
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                return result;
            }
        }
    }
}
