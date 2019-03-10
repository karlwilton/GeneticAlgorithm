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
    public partial class Graph : Form
    {
        public Graph(GeneticAlgo algoInstance, string gAtitle)
        {
            InitializeComponent();

            chart1.Series["Series1"].Name = "Best Values";
            for (int i = 0; i < algoInstance.BestSolutions.Count; i++)
            {
                chart1.Series["Best Values"].Points.AddXY(i + 1, algoInstance.BestSolutions[i]);

            }

            chart1.Series["Series2"].Name = "Average";
            for (int i = 0; i < algoInstance.BestSolutions.Count; i++)
            {
                chart1.Series["Average"].Points.AddXY(i + 1, algoInstance.AvgSolutions[i]);

            }

            this.Text = gAtitle;

            chart1.Titles["Title1"].Text = "Distances obtained through Genetic Algorithm using " + gAtitle;

            //chart1.ChartAreas[0].AxisY.Crossing = 900;
            
        }

        private void Graph_Load(object sender, EventArgs e)
        {

        }
    }
}
