using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ProblemA
{
    class Person
    {
        //I contain amount of directions and a position.
        //Max amount of instructions/directions == 25.
        Vector2 finalVector;

        public Person(string instruction)
        {
            
            Vector2 startVector;

            float direction = 0; //Set to 0 since we will change this with the start value anyway.
            int maxInstructions = 25;
            int amountOfInstructions = 0;

            // Split string on spaces.
            string[] words = instruction.Split(' ');

            float posX = float.Parse(words[0].Replace('.', ','));
            float posY = float.Parse(words[1].Replace('.', ','));

            startVector = new Vector2(posX, posY);

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
                            Console.WriteLine(amountOfInstructions);
                            continue;
                        case "turn":

                            //Add or subtract angle from current angle.
                            direction += float.Parse(words[i + 1].Replace('.', ','));
                            ++amountOfInstructions;
                            Console.WriteLine(amountOfInstructions);
                            continue;
                        case "walk":

                            //Move amount of units towards the current angle.
                            float unitsToMove = float.Parse(words[i + 1].Replace('.', ','));
                            float cosX = (float)Math.Cos(ConvertToRadians(direction));
                            float sinY = (float)Math.Sin(ConvertToRadians(direction));

                            Vector2 addedVector = new Vector2(cosX, sinY) * unitsToMove;

                            startVector += addedVector; //Add the new vector to the start vector.
                            ++amountOfInstructions;
                            Console.WriteLine(amountOfInstructions);
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

        public Vector2 GetFinalVector()
        {
            return finalVector;
        }
    }
}
