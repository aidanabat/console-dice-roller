using System;
using System.Text.RegularExpressions;

namespace Dice_Roller
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            void badInput()
            {
                Console.Clear();
                Console.WriteLine("Please Enter a Valid Input! (Ex. 2d6, 4d20, 5d10)");
            }

            bool acceptingInput = true;
            do
            {
                Console.WriteLine("--DICE ROLLER-");
                Console.WriteLine("To roll dice, type (amountofdice)d(typeofdice)");
                Console.WriteLine("For example, to roll 2 six sided dice, type 2d6");
                Console.Write("Waiting for Input...\t");
                string input = Console.ReadLine();

                
                Match validInput = Regex.Match(input, @"^\d+d\d+$", RegexOptions.IgnoreCase);

                if (validInput.Success)
                {
                    string[] splitInput = Regex.Split(input,@"d",RegexOptions.IgnoreCase);
                    if (splitInput.Length == 2)
                    {
                        int numDice;
                        int diceType;
                        //First number
                        if (int.TryParse(splitInput[0], out numDice))
                        {
                            //Second number
                            if (int.TryParse(splitInput[1], out diceType))
                            {
                                string result = "";
                                int total = 0;

                                //Actually roll
                                for (int timesRolled = 0; timesRolled < numDice; timesRolled++)
                                {
                                    int roll = rnd.Next(1, diceType);
                                    total += roll;
                                    if (numDice - 1 == timesRolled)
                                    {
                                        result += roll;
                                    }
                                    else
                                    {
                                        result += string.Format("{0} + ", roll);
                                    }

                                }

                                Console.WriteLine($"{total} = {result}");

                                //Roll again prompt
                                bool rollAgainPromptAnswered = false;
                                do
                                {
                                    Console.WriteLine("Do you want to roll again? [Y/N]");
                                    try
                                    {
                                        char answer = Char.ToLower(Console.ReadKey().KeyChar);
                                        if (answer == 'y')
                                        {
                                            Console.Clear();
                                            rollAgainPromptAnswered = true;
                                        }
                                        else if (answer == 'n')
                                        {
                                            Environment.Exit(0);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Respond with either Y or N");
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Enter a valid input!");
                                    }
                                } while (!rollAgainPromptAnswered);

                            }
                            else
                            {
                                badInput();
                            }
                        }
                        else
                        {
                            badInput();
                        }
                        Console.ReadLine();
                    }
                    else
                    {
                        badInput();
                    }
                }
                else
                {
                    badInput();
                }
            } while (true);
        }
    }
}
