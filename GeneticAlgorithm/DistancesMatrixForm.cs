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
    public partial class DistancesMatrixForm : Form
    {
        private CitiesMatrix tspMatrix;

        public DistancesMatrixForm(CitiesMatrix citiesMatrix)
        {
            InitializeComponent();
            this.tspMatrix = citiesMatrix;
        }

        private void DistancesMatrixForm_Load(object sender, EventArgs e)
        {
            txtMatrix.ReadOnly = true;
            txtMatrix.WordWrap = false;
            txtMatrix.Text = tspMatrix.PrintArray();
        }

        private void txtMatrix_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
