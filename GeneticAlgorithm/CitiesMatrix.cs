using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    // Note: For the Genetic Algorithm methods, please see the class Genetic Algo.cs

    public class CitiesMatrix
    {
        // This class is responsible for the initializaton of the Traveling Salesman Problem 

        private int[,] distancesMatrix = new int[50, 50];
        public int[,] DistancesMatrix
        {
            get
            {
                return distancesMatrix;

            }
            set
            {
                distancesMatrix = value;
            }
        }
  
        private static readonly Random randomNumbuh = new Random();

        public CitiesMatrix()   //constructor
        {
            //Loop through each element in x axis
            for (int i = 0; i < 50; i++)
            {
                //Loop through each element in y axis
                for (int j = 0; j < 50; j++)
                {
                    //When x and y have the same index, the value is set to 0
                    if (j == i)
                    {
                        distancesMatrix[i, j] = 0;
                    }

                    else
                    {
                        //generate a random number from 1-25
                        distancesMatrix[i, j] = randomNumbuh.Next(1, 26);

                        // make each pair of x and y indexes interchangeable (eg 1,2 and 2,1) and assign the same value
                        distancesMatrix[j, i] = distancesMatrix[i, j];
                    }
                }

            }

        }

        public string PrintArray()
        {
            // This method prints the two dimensional array into a string


            //create a list to output the values into a string          
            List<string> arrayStringList = new List<string>();

            //Loop through each value in x axis
            for (int i = 0; i < 50; i++)
            {
                //Loop through each value in y axis
                for (int j = 0; j < 50; j++)
                {
                    //convert value to string, concatenate with a tab character 
                    arrayStringList.Add(distancesMatrix[i, j].ToString() + "\t");

                }

                //add a newline character after each row 
                arrayStringList.Add("\r\n");
            }

            //Output the list items into the textbox
            string matrixString = string.Join(" ", arrayStringList.ToArray());

            return matrixString;

        }
        
    }
}
