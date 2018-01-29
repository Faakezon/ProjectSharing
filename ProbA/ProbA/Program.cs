using System;
using System.Windows;


class Program
    {
        static void Main()
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


                    Vector avgDestination = new Vector(GetAverageX(People, amountOfPeople), GetAverageY(People, amountOfPeople));

                    float worstDistance = 0;
                    for (int i = 0; i < People.Length; i++)
                    {
                        float instrucionalDistance = CalculateVectorDistance(avgDestination, People[i].GetFinalVector());
                        if (instrucionalDistance > worstDistance)
                            worstDistance = instrucionalDistance;
                    }

                    Console.WriteLine(Math.Round(avgDestination.X, 4) + " " + Math.Round(avgDestination.Y,4) + " " + Math.Round(worstDistance, 5)); //OUTPUT

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
                returnVal += (float)person.GetFinalVector().X;
            }
            return returnVal / amountOfPeople;
        }
        private static float GetAverageY(Person[] People, uint amountOfPeople)
        {
            float returnVal = 0;
            foreach (Person person in People)
            {
                returnVal += (float)person.GetFinalVector().Y;
            }
            return returnVal / amountOfPeople;
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

    private static float CalculateVectorDistance(Vector v1, Vector v2)
    {
        Vector v = new Vector(v1.X - v2.X, v1.Y - v2.Y);
        return (float)Math.Sqrt(v.X * v.X + v.Y * v.Y);
    }
}



class Person
{
    //I contain amount of directions and a position.
    //Max amount of instructions/directions == 25.
    Vector finalVector;

    public Person(string instruction)
    {

        Vector startVector;

        float direction = 0; //Set to 0 since we will change this with the start value anyway.
        int maxInstructions = 25;
        int amountOfInstructions = 0;

        // Split string on spaces.
        string[] words = instruction.Split(' ');

        float posX = float.Parse(words[0].Replace('.', ','));
        float posY = float.Parse(words[1].Replace('.', ','));

        startVector = new Vector(posX, posY);

        for (int i = 0; i < words.Length; i++)
        {
            if (amountOfInstructions < maxInstructions) // Since 0 should not count as an instruction. If there are more instructions than the maximum, they will be discarded with no warning.
            {
                switch (words[i])
                {
                    case "start":

                        //Absolute Value, 90 == north, 0 == east.
                        direction = float.Parse(words[i + 1].Replace('.', ',')); //Parse the value after the "start" keyword.
                        ++amountOfInstructions;
                        continue;
                    case "turn":

                        //Add or subtract angle from current angle.
                        direction += float.Parse(words[i + 1].Replace('.', ','));
                        ++amountOfInstructions;
                        continue;
                    case "walk":

                        //Move amount of units towards the current angle.
                        float unitsToMove = float.Parse(words[i + 1].Replace('.', ','));
                        float cosX = (float)Math.Cos(ConvertToRadians(direction));
                        float sinY = (float)Math.Sin(ConvertToRadians(direction));

                        Vector addedVector = new Vector(cosX, sinY) * unitsToMove;

                        startVector += addedVector; //Add the new vector to the start vector.
                        ++amountOfInstructions;
                        continue;
                    default:
                        break;
                }
            }
        }
        finalVector = startVector; //Just making it easier for me at the moment.

    }

    public double ConvertToRadians(double angle)
    {
        return (Math.PI / 180) * angle;
    }

    public Vector GetFinalVector()
    {
        return finalVector;
    }

}

