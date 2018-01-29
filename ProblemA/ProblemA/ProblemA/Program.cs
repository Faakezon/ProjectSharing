using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ProblemA
{
    class Program
    {
        static void Main(string[] args)
        {

            //One test case contains between 1-20 people.

            uint amountOfPeople = 0;
            amountOfPeople = GetAmountOfPeople();

            if (amountOfPeople > 0 && amountOfPeople < 21) //Check for appropriate input.
            {
                while (amountOfPeople != 0) //So we keep the application alive.
                {
                    //Set up a collection of people in an array.
                    Person[] People = new Person[amountOfPeople];

                    for (int i = 0; i < People.Length; i++)
                    {
                        People[i] = new Person(Console.ReadLine()); //Add a new Person to the People array with the specific input data.
                    }


                    Vector2 avgDestination = new Vector2(GetAverageX(People, amountOfPeople), GetAverageY(People, amountOfPeople));

                    float worstDistance = 0;
                    for(int i = 0; i < People.Length; i++)
                    {
                        float instrucionalDistance = Vector2.Distance(avgDestination, People[i].GetFinalVector());
                        if (instrucionalDistance > worstDistance)
                            worstDistance = instrucionalDistance;
                    }
                    
                    Console.WriteLine(avgDestination.X + " " + avgDestination.Y + " " + worstDistance); //OUTPUT

                    Array.Clear(People, 0, People.Length); //Clear the array for re-use.
                    amountOfPeople = GetAmountOfPeople(); // Last thing we do, we ask again for number of people.

                }

            }

        }

        private static float GetAverageX(Person[] People, uint amountOfPeople)
        {
            float returnVal = 0;
            foreach (Person person in People)
            {
                returnVal += person.GetFinalVector().X;
            }
            return (float)Math.Round(returnVal / amountOfPeople, 4);
        }
        private static float GetAverageY(Person[] People, uint amountOfPeople)
        {
            float returnVal = 0;
            foreach (Person person in People)
            {
                returnVal += person.GetFinalVector().Y;
            }
            return (float)Math.Round(returnVal / amountOfPeople, 4);
        }


        private static uint GetAmountOfPeople()
        {
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                try
                {
                    return (uint)Int16.Parse(line);
                }
                catch
                {
                    throw new ArgumentNullException("Need input");
                }
            }
            return 0;
        }
    }
}
