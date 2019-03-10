using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticAlgorithm
{
    // Note: For the Genetic Algorithm methods, please see the class Genetic Algo.cs
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();

            CitiesMatrix tspmatrix = new CitiesMatrix();

            Path parent1 = new Path();
            Path parent2 = new Path();
           

            GeneticAlgo ga = new GeneticAlgo(tspmatrix, 1, 3, Mutation.Swap);
      

            label6.Text = parent1.PathString;
            label7.Text = parent2.PathString;
            Path childcrossed = ga.Crossover(parent1, parent2);
            label8.Text = childcrossed.PathString;
            label9.Text = ga.SwapMutation(childcrossed).PathString ;
            label10.Text = ga.InsertionMutation(childcrossed).PathString;

          
           

        }

    }
}
