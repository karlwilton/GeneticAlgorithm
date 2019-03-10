using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace GeneticAlgorithm
{
    // Note: For the Genetic Algorithm methods, please see the class Genetic Algo.cs
    public partial class Form1 : Form
    {
        CitiesMatrix citiesMatrix;
        GeneticAlgo algo1;
        GeneticAlgo algo2;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Disable sections first
            grpGA1.Enabled = false;
            grpGA2.Enabled = false;
            grp_GA1_btngroup.Enabled = false;
            grp_GA2_btngroup.Enabled = false;
            btn_ViewMatrix.Enabled = false;
            
        }

        private void btn_ViewMatrix_Click(object sender, EventArgs e)
        {
            // Show the matrix in a separate form. Pass the CitiesMatrix as argument.
            DistancesMatrixForm distancesMatrixForm = new DistancesMatrixForm(citiesMatrix);
            distancesMatrixForm.Show();
        }

        private void btn_GA1_ViewGraph_Click(object sender, EventArgs e)
        {
            // Show the graph in a separate form.
            string title = "Rank Selection + Order Crossover + Swap Mutation";
            Graph graph = new Graph(algo1,title);
            graph.Show();
        }

        private void btn_GA2_ViewGraph_Click(object sender, EventArgs e)
        {
            // Show the graph in a separate form.
            string title = "Rank Selection + Order Crossover + Insertion Mutation";
            Graph graph = new Graph(algo2, title);
            graph.Show();
        }

        private void btn_GA1_Click(object sender, EventArgs e)
        {
            grpGA1.Enabled = true;

            // Create an instance of the GeneticAlgo class, pass on the CitiesMatrix and
            // specify the number of iterations, population, and type of mutation. 
            // In this instance the type of mutation is Swap.

            algo1 = new GeneticAlgo(citiesMatrix, 1000, 16, Mutation.Swap);
            
            // Display the corresponding values.
            txtbxGA1_Best_Val.Text = string.Join("\r\n", algo1.BestSolutions.ToArray());
            txtbxGA1_Avgs.Text = string.Join("\r\n", algo1.AvgSolutions.ToArray());
            txtGA1_Best_val.Text = algo1.BestestSolution.ToString();
            txtGA1_Avg.Text = algo1.AverageOfBestSolutions.ToString();

            // Reset the scrollbar of the rich textbox.
            txtbxGA1_Avgs.SelectionStart = 0;
            txtbxGA1_Avgs.ScrollToCaret();
            txtbxGA1_Best_Val.SelectionStart = 0;
            txtbxGA1_Best_Val.ScrollToCaret();
        }

        private void btn_GA2_Click(object sender, EventArgs e)
        {
            grpGA2.Enabled = true;

            // Create an instance of the GeneticAlgo class, pass on the CitiesMatrix and
            // specify the number of iterations, population, and type of mutation. 
            // In this instance the type of mutation is Insertion

            algo2 = new GeneticAlgo(citiesMatrix, 1000, 16, Mutation.Insertion);

            // Display the corresponding values.
            txtbxGA2_Best_Val.Text = string.Join("\r\n", algo2.BestSolutions.ToArray());
            txtbxGA2_Avgs.Text = string.Join("\r\n", algo2.AvgSolutions.ToArray());
            txtGA2_Best_val.Text = algo2.BestestSolution.ToString();
            txtGA2_Avg.Text = algo2.AverageOfBestSolutions.ToString();

            // Reset the scrollbar of the rich textbox.
            txtbxGA2_Avgs.SelectionStart = 0;
            txtbxGA2_Avgs.ScrollToCaret();
            txtbxGA2_Best_Val.SelectionStart = 0;
            txtbxGA2_Best_Val.ScrollToCaret();
        }

        private void btn_NewTSP_Click(object sender, EventArgs e)
        {           
            ClearTextBoxes();

            // Create a new TSP matrix
            citiesMatrix = new CitiesMatrix();

            // Disable some sections first
            grp_GA1_btngroup.Enabled = false;
            grp_GA2_btngroup.Enabled = false;
            btn_ViewMatrix.Enabled = false;

            grpGA1.Enabled = false;
            grpGA2.Enabled = false;

            // Reset the scrollabrs of the rich textboxes.
            txtbxGA1_Avgs.SelectionStart = 0;
            txtbxGA1_Avgs.ScrollToCaret();
            txtbxGA1_Best_Val.SelectionStart = 0;
            txtbxGA1_Best_Val.ScrollToCaret();
            txtbxGA2_Avgs.SelectionStart = 0;
            txtbxGA2_Avgs.ScrollToCaret();
            txtbxGA2_Best_Val.SelectionStart = 0;
            txtbxGA2_Best_Val.ScrollToCaret();

            // Display notification.
            MessageBox.Show("A new Traveling Salesman Problem has been created.", "New TSP Created", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Re-eanble Find Solution buttons/group and View Matrix button
            grp_GA1_btngroup.Enabled = true;
            grp_GA2_btngroup.Enabled = true;
            btn_ViewMatrix.Enabled = true;
        }

        private void ClearTextBoxes()
        {
            // This method clears text inside the textboxes.

            txtbxGA1_Avgs.Clear();
            txtbxGA1_Best_Val.Clear();
            txtbxGA2_Avgs.Clear();
            txtbxGA2_Best_Val.Clear();
            txtGA1_Avg.Clear();
            txtGA1_Best_val.Clear();
            txtGA2_Avg.Clear();
            txtGA2_Best_val.Clear();
           
        }
    }
}
