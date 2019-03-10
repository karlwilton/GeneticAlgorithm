using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    // Note: For the Genetic Algorithm methods, please see the class Genetic Algo.cs
    public class Population
    {

        #region FIELDS AND PROPERTIES

        private List<Path> paths = new List<Path>();
        public List<Path> Paths
        {
            get 
            {
                return paths;
            }
            set
            {
                paths = value;
            }
        }

        private int populationSize;
        public int PopulationSize
        {
            get
            {
                return populationSize;
            }
            set
            {
                populationSize = value;
            }
        }

        public string PopulationString
        {
            get
            {
                return PrintPopulation();
            }
        }
        
        #endregion

        public Population(int size) //CONSTRUCTOR
        {
           populationSize = size;

        }

        public Population (List<Path> pathList)  //CONSTRUCTOR
        {
            // Creates a population from a list of Paths (chromosomes)
            this.Paths = pathList;
            this.populationSize = pathList.Count;
        }

        public void AddPath(Path path)  
        {   
            // Adds path into population
            Paths.Add(path);
        }

        public void GenerateInitialPopulation(CitiesMatrix tspMatrix)
        {
            // Used in the Genetic Algorithm to create an initial population
            for (int i = 0; i < populationSize; i++)
            {
                this.AddPath(new Path());
            }
        }

        public double GetAverageDistance(CitiesMatrix cityDistancesMatrix)
        {
            // Calculates the average cost (distance) of all the paths (chromosomes) in the population.

            int totalDistance = 0;
            double average;
            for (int i = 0; i < paths.Count; i++)
            {
                totalDistance += paths[i].GetDistance(cityDistancesMatrix);
            }

            average = totalDistance / (double)paths.Count;

            return average;

        }

        private string PrintPopulation()
        {
            // Used for testing. Outputs the paths in the population into a string 

            List<string> lstPopulationString = new List<string>();
            
            foreach (Path path in this.Paths)
            {
               lstPopulationString.Add(path.PathString);
            }

            return string.Join("\r\n", lstPopulationString);

        }
    }
}
