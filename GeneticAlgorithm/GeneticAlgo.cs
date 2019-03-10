using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm
{
    public class GeneticAlgo
    {
        #region FIELDS AND PROPERTIES

        private static readonly Random randomNumbuh = new Random();
        private CitiesMatrix tspMatrix;
        private Population parentPopulation;
        private Population newGeneration;
        private List<Path> childrenList;
       


        private List<int> bestSolutions = new List<int>();
        public List<int> BestSolutions
        {
            get { return bestSolutions; }

        }

        private List<double> avgSolutions = new List<double>();
        public List<double> AvgSolutions
        {
            get { return avgSolutions; }
        }

        public int BestestSolution
        {
            get { return bestSolutions.Min(); }
        }
        public double AverageOfBestSolutions
        {
            get { return bestSolutions.Average(); }
        }

        #endregion

        public GeneticAlgo(CitiesMatrix matrix, int numberOfIterations, int populationSize, Mutation mutationType) //constructor
        {
            // Use in the entire class the TSP distances matrix that was passed as argument to the constructor 
            this.tspMatrix = matrix;

            //Generate an initial population
            InitialPopulation(populationSize);

           //Create new generations as many as the number of iterations passed as argument to the constructor

            for (int i = 0; i < numberOfIterations; i++)
            {
                //Create a new generation
                CreateNewGeneration(mutationType);

                // Store the average cost for the newly created generation
                avgSolutions.Add(newGeneration.GetAverageDistance(tspMatrix));

                //set the newly generated population as the parent population to create the next generation
                parentPopulation = newGeneration;
            }

        }

        private void InitialPopulation(int populationSize) // INITIAL POPULATION 
        {
            // Generate an Initial Population by creating an instance of the Population class
            // and invoking the GenerateInitialPopulation() extension method
            // This method is found in the Population class.

            Population initialPopulation = new Population(populationSize);
            initialPopulation.GenerateInitialPopulation(tspMatrix);

            // Assign the initial population as the parent population
           
            parentPopulation = initialPopulation;
            
        } 
        
        private void CreateNewGeneration(Mutation mutationType)
        {
            // Evaluate the parent population by invoking EvaluatePopulation() method. 
            // This will calculate the cost of each chromosome (path) and determine 
            // the ranking of the parent population. This ranking will be significant
            // for Rank Selection.

            int[] ranks; // Corresponding rank of each path will be stored here
            EvaluatePopulation(parentPopulation, out ranks);
            

            // Create children
            int parent1Indx;
            int parent2Indx;
            Path child;
            Path mutantChild;
            childrenList = new List<Path>();


            for (int i = 0; i < parentPopulation.PopulationSize; i++)
            {
                // Select a pair of parents using Rank Selection
                RankSelection(ranks, out parent1Indx, out parent2Indx);

                Path parent1 = parentPopulation.Paths[parent1Indx];
                Path parent2 = parentPopulation.Paths[parent2Indx];

                // Produce a child by mating the parents using crossover 
                child = Crossover(parent1, parent2);

                if (mutationType == Mutation.Swap)
                {
                    // Mutate the child using swap mutation
                    mutantChild = SwapMutation(child);
                    // Add the child to the list
                    childrenList.Add(mutantChild);
                }

                if (mutationType == Mutation.Insertion)
                {
                    // Mutate the child using insertion mutation
                    mutantChild = InsertionMutation(child);
                    // Add the child to the list
                    childrenList.Add(mutantChild);
                }

               
            }

            newGeneration = SelectNextGenerationPopulation();
            
        }

        private void RankSelection(int[] ranks, out int parent1indx, out int parent2indx)
        {
            
            // This method selects a pair of indices of parents via rank selection
            parent1indx = SelectParent(ranks);
          

            // parent2 must not be the same as parent1
            do
            {
                parent2indx = SelectParent(ranks);
            }
            while (parent1indx == parent2indx);

            

        }

        private int SelectParent(int[] ranks)
        {
            // helper method for RankSelection

            // calculate the sum of the ranks
            int sumOfRanks = ranks.Sum();

            // generate a random 
            double randomDouble = randomNumbuh.NextDouble() * (sumOfRanks);

            int i;
            double sum;

            sum = 0;
            for (i = 0; i < parentPopulation.PopulationSize; i++)
            {
                sum += ranks[i];
                if (sum > randomDouble)
                    break;
            }
            ////test
            //rankselectedindexes.Add(i);
            
            return i;
        }
                
        private void EvaluatePopulation(Population population, out int[] rankings) //EVALUATE POPULATION 
        {
            // This method gets distance (cost) of each path (chromosome) in the population
            // and ranks the population according to distance (highest to lowest).
            // Path with highest distance gets rank 1, lowest gets rank n (no. of population)

            ArrayList a = new ArrayList();
            foreach (Path path in population.Paths)
            {
                a.Add(path.GetDistance(tspMatrix));
                // Method for calculation of the cost (distance) of a path is written in the Path class
            }

            ArrayList b = (ArrayList)a.Clone();
            b.Sort();
            b.Reverse();

            for (int i = 0; i <= (a.Count - 1); i++)
                a[a.IndexOf(b[i])] = i;

            // The int[] array will contain the corresponding ranks 
            rankings = new int[population.Paths.Count];

            for (int j = 0; j <= (a.Count - 1); j++)
                rankings[j] = (int)a[j] + 1;


        }
                
        public Path Crossover(Path parent1, Path parent2) // ORDER CROSSOVER 
        {

            // This method implements a crossover between two parents.
            
            // Child array will inherit the consecutive values from the array elements of parent1 
            // starting at the element with the startPoint index up to the array element with endpoint index

            // Randomly generate startPoint by picking a number from 0-25
            int startPoint = randomNumbuh.Next(0, 26);
            
            int endPoint = startPoint + 24;
           
            int[] childArray = new int[50];

            //copy the values from parent1
            for (int i = startPoint; i < endPoint+1; i++)
            {
                childArray[i] = parent1.CitiesPath[i];
            }


          

            int childGeneSlot = 0;

            // If the endpoint is 49, then there will be no more slots in the child  following the endpoint.
            // So set the endpoint as the starting index of the child array to which values 
            // from parent2 are copied.
            
            int cityInParent2;
            if (endPoint != 49)
            {
                childGeneSlot = endPoint + 1;
                
                // Check whether each succeeding city from parent2 exists in child array.
                                
                for (int a = endPoint + 1; a < 50; a++)
                {
                    cityInParent2 = parent2.CitiesPath[a];

                    if (!CheckIfExisting(childArray, startPoint, cityInParent2))
                    {
                        childArray[childGeneSlot] = parent2.CitiesPath[a];
                        childGeneSlot++;
                    }
                }
            }
            // Go to first element of parent2 and check if the value exists in the already inherited
            // genes from parent1. If it does not exist then add the value into the child array. Do this
            // for the succeeding elements until the endPoint is reached
            for (int a = 0; a < endPoint + 1; a++)
            {

                if (childGeneSlot <= 49)
                {
                    cityInParent2 = parent2.CitiesPath[a];

                    if (!CheckIfExisting(childArray, startPoint, cityInParent2))
                    {
                        childArray[childGeneSlot] = parent2.CitiesPath[a];
                        childGeneSlot++;
                    }

                }

                // If the slots for genes in child array is filled up to its 49th element,
                // we start copying genes to the child's first element.
                if (childGeneSlot == 50)
                {
                    childGeneSlot = 0;
                }
            }

            // We now create a new child path using the completed child array.
            Path child = new Path(childArray);
            return child;
        }

        private static bool CheckIfExisting(int[] childArray, int startPoint, int value)
        {
            // This method checks whether a value exists in an array

            for (int i = startPoint; i <= 49; i++)
            {
                if (childArray[i] == value) return true;

            }

            return false;
        }

        public Path SwapMutation(Path xPath) // SWAP MUTATION
        {
            
            int index1;
            int index2;

            // Assign values by generating a random number from 0 to 49
            index1 = randomNumbuh.Next(0, 50);

            do
            {
                index2 = randomNumbuh.Next(0, 50);
            }
            while (index1 == index2); // Repeat process if values  are equal

          

            // Swap the values
            int temp = xPath.CitiesPath[index1];
            xPath.CitiesPath[index1] = xPath.CitiesPath[index2];
            xPath.CitiesPath[index2] = temp;

            // Return the mutant path
            return xPath;
        }

        public Path InsertionMutation(Path xPath) //INSERTION MUTATION 
        {
            // Declare variables for origin and destination indices 

            int origin;
            int destination;

            // Assign values by generating a random number from 0 to 49
            origin = randomNumbuh.Next(0, 50);

            do
            {
                destination = randomNumbuh.Next(0, 50);
            }
            while (origin == destination); //repeat process if values  are equal

            
            // Mutate into into a new path
            int[] newPath = new int[50];

            if (origin > destination)
            {
                newPath[destination] = xPath.CitiesPath[origin];
                for (int i = destination; i < origin; i++)
                {
                    newPath[i + 1] = xPath.CitiesPath[i];
                }
                if (destination != 0)
                {
                    for (int i = 0; i < destination; i++)
                    {
                        newPath[i] = xPath.CitiesPath[i];
                    }

                }
                if (origin != 49)
                {
                    for (int i = origin + 1; i < 50; i++)
                    {
                        newPath[i] = xPath.CitiesPath[i];
                    }

                }
            }
            else if (origin < destination)
            {
                newPath[destination] = xPath.CitiesPath[origin];
                for (int i = origin; i < destination; i++)
                {
                    newPath[i] = xPath.CitiesPath[i + 1];
                }

                if (destination != 49)
                {
                    for (int i = destination + 1; i < 50; i++)
                    {
                        newPath[i] = xPath.CitiesPath[i];
                    }
                }

                if (origin != 0)
                {

                    for (int i = 0; i < origin; i++)
                    {
                        newPath[i] = xPath.CitiesPath[i];
                    }

                }

            }

            return new Path(newPath);

        }

        private Population SelectNextGenerationPopulation() //SELECTING NEXT GENERATION POPULATION
        {
            // This method selects the individuals that will make up the next generation by selecting
            // the Elite members of the children and parent population.
           
            // Combine the parents and children in one set.

            var parentsAndChildren = new List<Path>(parentPopulation.Paths.Count +
                childrenList.Count);
                
            parentsAndChildren.AddRange(parentPopulation.Paths);
            parentsAndChildren.AddRange(childrenList);

            Population combinedPopulation = new Population(parentsAndChildren);


            // Rank the combined parents and children population according to cost (distance),
            // from worst (1) to the best (n or number of population).

            int[] ranks;
            EvaluatePopulation(combinedPopulation,out ranks);

            // Based on the ranks, we select those with the lowest costs, so those ranked at the bottom half
            // (because ranking is from 1(worst) to n (best) where n is the number of population)

            List<Path> eliteIndividuals = new List<Path>();

            for (int i = 0; i < combinedPopulation.Paths.Count; i++)
            {
                if (ranks[i] >= (combinedPopulation.Paths.Count / 2) + 1)
                {
                    eliteIndividuals.Add(combinedPopulation.Paths[i]);

                    // Get the best solution value (lowest distance) and store it.
                    if (ranks[i] == combinedPopulation.Paths.Count)
                    {
                        bestSolutions.Add(combinedPopulation.Paths[i].GetDistance(tspMatrix));
                    }
                }
                
            }

            // Return the selected individuals as a new population.
            return new Population(eliteIndividuals);
        }
               
    }

    public enum Mutation
    {
        Swap,
        Insertion
    }


}
