using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{

    // Note: For the Genetic Algorithm methods, please see the class Genetic Algo.cs
    public class Path

        // A path (the route used by the traveling salesman) serves as a chromosome

    {
        #region  FIELDS AND PROPERTIES

        private static readonly Random randomNumbuh = new Random();
        
        
        private int[] citiesPath;
        public int[] CitiesPath
        {
            get
            {
                return citiesPath;
            }
        }

        
        public string PathString
        {

            get
            {
                return PrintPath(citiesPath);
            }
        }

        #endregion

        public Path() // Constructor
        {
            // This constructor creates a randomly generated path.
            citiesPath = GenerateRandomPath();
        }

        public Path(int[] pathArray) // Constructor 
        {
            // This constructor creates a path using the int array
            citiesPath = pathArray;
        }
        
        public int[] GenerateRandomPath()

        {
            //Here we create ONE random path from 1-50 cities 

            int[] pathArray = new int[50];

                for (int i = 0; i < pathArray.Length; i++)
                {
                int next;

                while (true)
                {
                    // Generate a random number from the range of 1 to 50 
                    next = randomNumbuh.Next(1, 51);
                    // Check if the randomly generated number is a unique value within the array
                    if (!CheckArray(pathArray, next)) break;
                }
                // Add the randomly generated unique city into the array.
                pathArray[i] = next;
                
                }


                // Subract 1 from each value, because the matrix array is zero based.
                for (int i = 0; i < pathArray.Length; i++)
                {
                    pathArray[i] = pathArray[i] - 1;
                }

                return pathArray;
        }

        private bool CheckArray(int[] array, int value) //checks if an int value is already in the array
        {
            for (int i = 0; i < 50; i++)
            {
                //Return true if the array contains the value
                if (array[i] == value) return true;
            }

            //otherwise
            return false;
        }

        public int GetDistance (CitiesMatrix cityDistancesMatrix)
        {
            // This method calculates the cost (distance) traveled by the path

            int cityA = 0;
            int cityB = 0;
            int totalDistance = 0;

            for (int i = 0; i < citiesPath.Length - 1; i++)
            {
                cityA = citiesPath[i];
                cityB = citiesPath[i + 1];
                totalDistance = totalDistance + cityDistancesMatrix.DistancesMatrix[cityA, cityB];
            }

            //Add distance from the last city of the solution index to the first city index
            totalDistance = totalDistance + cityDistancesMatrix.DistancesMatrix[citiesPath[49], citiesPath[0]];

            return totalDistance;

        }

        private string PrintPath(int[] pathArray)    
        {
            // For testing purposes. This method returns the cities path into a string.

            List<int> pathList = new List<int>();
            for (int i = 0; i < pathArray.Length; i++)
            {
                //we add 1 because the array is zero based
                pathList.Add(pathArray[i] + 1);
            }
            return string.Join(" - ", pathList) + " - " + pathList[0].ToString();
        }
        
    }
}
