using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProblemC
{
    class Program
    {
        private static int currentTestcase = 1;
        private static int amountOfTestcases;
        private static int amountOfCats;
        private static int amountOfDogs;
        private static int amountOfVoters;

        private static List<Cat> Cats = new List<Cat>();
        private static List<Dog> Dogs = new List<Dog>();
        private static List<Voter> Voters = new List<Voter>();

        //int.Parse(Regex.Match(sub[1].Substring(1), @"\d+").Value); 

        static void Main(string[] args)
        {

            // First we get the testcases. 
            amountOfTestcases = AmountOfTestcases();
            
            //Then we get the amount of Cats, Dogs and Voters we use in that testcase.
            while (currentTestcase <= amountOfTestcases)
            {
                string catsDogsVoters;
                while ((catsDogsVoters = Console.ReadLine()) != "")
                {
                    if (catsDogsVoters == null)
                        break;

                    string[] sub = catsDogsVoters.Split(' ');

                    int cats = int.Parse(sub[0]);
                    int dogs = int.Parse(sub[1]);
                    int voters = int.Parse(sub[2]);

                    if((cats >= 1 && cats <= 100) && (dogs >= 1 && dogs <= 100) && (voters >= 1 && voters <= 500))
                    {
                        amountOfCats = cats;
                        amountOfDogs = dogs;
                        amountOfVoters = voters;

                        for(int i = 1; i <= amountOfCats; i++) //Create the cats.
                        {
                            Cats.Add(new Cat(i));
                        }
                        for(int i = 1; i <= amountOfDogs; i++) //Create the dogs.
                        {
                            Dogs.Add(new Dog(i));
                        }
                    }
                    break;

                }

                if ((amountOfCats >= 1 && amountOfCats <= 100) && (amountOfDogs >= 1 && amountOfDogs <= 100) && (amountOfVoters >= 1 && amountOfVoters <= 500))
                {
                    string voteLine;
                    while ((voteLine = Console.ReadLine()) != "")
                    {
                        if (voteLine == null)
                            break;

                        string[] sub = voteLine.Split(); // 0 = C1 , 1 = D1
                        char UpVotingAnimalType, DownVotingAnimalType;
                        int UpVotingAnimalIndex, DownVotingAnimalIndex;

                        UpVotingAnimalType = sub[0].Substring(0, 1).ToCharArray()[0]; // C or D
                        UpVotingAnimalIndex = int.Parse(Regex.Match(sub[0].Substring(1), @"\d+").Value);  //Index of the animal as an integer.

                        DownVotingAnimalType = sub[1].Substring(0, 1).ToCharArray()[0]; //C or D
                        DownVotingAnimalIndex = int.Parse(Regex.Match(sub[1].Substring(1), @"\d+").Value);  //Index of the animal as an integer.

                        //Create a voter with animal preference.
                        Voters.Add(new Voter(UpVotingAnimalType, UpVotingAnimalIndex));

                        //Find specified animal to upvote.
                        UpVote(UpVotingAnimalType, UpVotingAnimalIndex);
                        //Find specified animat to downvote.
                        DownVote(DownVotingAnimalType, DownVotingAnimalIndex);

                        if (amountOfVoters == Voters.Count)
                            break;

                    }

                    //Last calculation and output.
                    int maximumNumberOfSatisfiedVoters = 0;
                    //Check which animal got the most votes. Sort the list for most votes.
                    Cats = Cats.OrderByDescending(c => c.Votes).ToList();
                    Dogs = Dogs.OrderByDescending(d => d.Votes).ToList();

                    //Console.WriteLine("C" + Cats[0].Index + ", votes: " + Cats[0].Votes + " - D" + Dogs[0].Index + ", votes: " + Dogs[0].Votes );

                    if (Cats[0].Votes > Dogs[0].Votes)
                    {
                        //Cat Type won
                        //Calculate the amount of voters who voted for that animal.
                        List<Voter> VotersForTheWinningType = Voters.FindAll(v => v.Type == 'C');
                        foreach (Voter voter in VotersForTheWinningType)
                        {
                            if (voter.TypeIndex == Cats[0].Index)
                                maximumNumberOfSatisfiedVoters += 1;
                        }

                    }
                    else if (Dogs[0].Votes > Cats[0].Votes)
                    {
                        //Dog Type won
                        //Calculate the amount of voters who voted for that animal.
                        List<Voter> VotersForTheWinningType = Voters.FindAll(v => v.Type == 'D');
                        foreach (Voter voter in VotersForTheWinningType)
                        {
                            if (voter.TypeIndex == Dogs[0].Index)
                                maximumNumberOfSatisfiedVoters += 1;
                        }
                    }
                    else
                    {
                        //Tie
                        //Amount of Voters / 2 since it was divided decision.
                        if(amountOfVoters != 0)
                            maximumNumberOfSatisfiedVoters = amountOfVoters / 2;
                    }



                    Console.WriteLine(maximumNumberOfSatisfiedVoters);
                    Cats.Clear();
                    Dogs.Clear();
                    Voters.Clear();
                    currentTestcase += 1;
                    //amountOfTestcases = AmountOfTestcases();
                    
                }
            }
            //Console.ReadKey();

        }

        private static int AmountOfTestcases()
        {
            string inputLine;

            while ((inputLine = Console.ReadLine()) != "0")
            {
                if (inputLine != null)
                {
                    var amount = int.Parse(inputLine);
                    if (amount >= 1 && amount <= 100)
                        return amount;
                }

                break;
            }
            return 0;
        }


        private static void UpVote(char type, int index)
        {
            switch (type)
            {
                case 'C':
                    Cats.Find(c => c.Index == index).UpVote();
                    break;
                case 'D':
                    Dogs.Find(d => d.Index == index).UpVote();
                    break;
                default:
                    break;
            }
        }
        private static void DownVote(char type, int index)
        {
            switch (type)
            {
                case 'C':
                    Cats.Find(c => c.Index == index).DownVote();
                    break;
                case 'D':
                    Dogs.Find(d => d.Index == index).DownVote();
                    break;
                default:
                    break;
            }
        }

    }

    internal class Voter
    {
        private char type;
        public char Type { get { return type; } }

        private int typeIndex;
        public int TypeIndex { get { return typeIndex; } }

        public Voter(char type, int typeIndex)
        {
            this.type = type;
            this.typeIndex = typeIndex;
        }

    }

    internal class Cat
    {
        private int index;
        public int Index { get { return index; } }

        private int votes = 0;
        public int Votes { get { return votes; } }


        public Cat(int index)
        {
            this.index = index;
        }

        public void UpVote()
        {
            votes += 1;
        }
        public void DownVote()
        {
            votes -= 1;
        }

    }

    internal class Dog
    {
        private int index;
        public int Index { get { return index; } }

        private int votes = 0;
        public int Votes { get { return votes; } }

        public Dog(int index)
        {
            this.index = index;
        }

        public void UpVote()
        {
            votes += 1;
        }
        public void DownVote()
        {
            votes -= 1;
        }
    }
}
