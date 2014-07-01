﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BullsAndCows
{
    class BullsAndCowsTest
    {
        static void GameInstructions()
        {
            string separator = new String('-', Console.WindowWidth);

            Console.WriteLine("Commands: \n");
            Console.WriteLine(separator);
            Console.WriteLine("{0,-10} --> {1,20}", "restart", "Start new game");
            Console.WriteLine("{0,-10} --> {1,22}", "top", "View scroreboard");
            Console.WriteLine("{0,-10} --> {1,10}", "help", "Help");
            Console.WriteLine("{0,-10} --> {1,16}", "exit", "Quit game\n");
            Console.WriteLine(separator);
            Console.WriteLine("Welcome to “Bulls and Cows” game.\nPlease try to guess my secret 4-digit number.\n");
        }

        static int number, attempts;
        static bool notCheated;

        private static void AddToScoreboard()
        {
            if (scoreboard.Count < 5 || scoreboard.ElementAt(4).Key > attempts)
            {
                Console.WriteLine("Please enter your name for the top scoreboard: ");
                string name = Console.ReadLine().Trim();

                scoreboard.Add(attempts, name);

                if (scoreboard.Count == 6)
                {
                    scoreboard.RemoveAt(5);
                }

                DisplayTop();
            }
        }

        static void ProcessWin()
        {
            Console.WriteLine("Congratulations! You guessed the secret number in {0} attempts.", attempts);

            if (notCheated)
            {
                AddToScoreboard();
            }

            StartNewGame();
        }

        static void ProcessGuess(int guess)
        {
            if (guess == number)
            {
                ProcessWin();
            }
            else
            {
                string snum = number.ToString(), sguess = guess.ToString();
                bool[] isBull = new bool[4];
                int bulls = 0, cows = 0;

                for (int i = 0; i < 4; i++)
                {
                    if (isBull[i] = snum[i] == sguess[i])
                    {
                        bulls++;
                    }
                }

                int[] digs = new int[10];

                for (int d = 0; d < 10; d++)
                {
                    digs[d] = 0;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!isBull[i])
                    {
                        digs[snum[i] - '0']++;
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    if (!isBull[i])
                    {
                        if (digs[sguess[i] - '0'] > 0)
                        {
                            cows++;
                            digs[sguess[i] - '0']--;
                        }
                    }
                }

                Console.WriteLine("\nWrong number! Bulls: {0}, Cows: {1}", bulls, cows);
                attempts++;
            }
        }

        static readonly SortedList<int, string> scoreboard = new SortedList<int, string>();

        static void DisplayTop()
        {
            if (scoreboard.Count() > 0)
            {
                Console.WriteLine("Scoreboard:");
                int i = 1;
                foreach (var t in scoreboard)
                {
                    Console.WriteLine("{0}. {1} --> {2} guesses", i, t.Value, t.Key);
                }
            }
            else
            {
                Console.WriteLine("The scoreboard is empty.");
            }
        }

        static Random randomNumber = new Random();

        static void StartNewGame()
        {
            GameInstructions();
            number = randomNumber.Next(1000, 10000);
            attempts = 1;
            notCheated = true;
            ch = "XXXX";
        }

        static string ch;

        static void Cheat()
        {
            notCheated = false;

            if (ch.Contains('X'))
            {
                int i;

                do{
                    i = randomNumber.Next(0, 4);
                }
                while (ch[i] != 'X');

                char[] cha = ch.ToCharArray();
                cha[i] = number.ToString()[i];
                ch = new string(cha);
            }

            Console.WriteLine("The number looks like {0}.", ch);
        }

        static bool ReadAction()
        {
            Console.WriteLine("Enter your guess or command: ");

            string line = Console.ReadLine().Trim();
            Regex patt = new Regex("[1-9][0-9][0-9][0-9]");

            switch (line)
            {
                case "top":
                    DisplayTop();
                    break;
                case "restart":
                    StartNewGame();
                    break;
                case "help":

                    Cheat();
                    break;
                case "exit":
                    return false;

                default:
                    if (patt.IsMatch(line))
                    {
                        int guess = int.Parse(line);
                        ProcessGuess(guess);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a 4-digit number or");
                        Console.WriteLine("one of the commands: 'top', 'restart', 'help' or 'exit'.");
                    }
                    break;
            }

            return true;
        }

        static void Main(string[] args)
        {
            StartNewGame();
            while (ReadAction())
                ;
        }
    }
}