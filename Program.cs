using System;
using System.IO;

namespace snowman
{
    class Program
    {
        static void Main(string[] args)
        {
            int gamesWon = 0;
            int gamesLost = 0;

            string userInput = GetMenuChoice();
            while (userInput != "3")
            {
                Route(userInput, ref gamesWon, ref gamesLost);
                userInput = GetMenuChoice();
            }

            Goodbye(ref gamesWon, ref gamesLost);
        }

        public static string GetMenuChoice()
        {
            DisplayMenu();
            string userInput = Console.ReadLine();

            while (!ValidMenuChoice(userInput))
            {
                Console.WriteLine("Invalid menu choice.  Please Enter a Valid Menu Choice");
                Console.WriteLine("Press any key to continue....");
                Console.ReadKey();

                DisplayMenu();
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        public static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("1:   Play Snowman");
            Console.WriteLine("2:   See Scoreboard");
            Console.WriteLine("3:   Exit Game");
        }

        public static bool ValidMenuChoice(string userInput)
        {
            /*Step 1 update ValidMenuChoice to return true if the user 
            entered 1, 2 or 3 and return false if they entered anything else.
            */



            if (userInput == "1" || userInput == "2" || userInput == "3")
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public static void Route(string userInput, ref int gamesWon, ref int gamesLost)
        {
            /*Step 2: Update to call Snowman if the user entered 1 and 
             * ScoreBoard if they entered 2
             */
            if (userInput == "1")
            {
                SnowMan(ref gamesWon, ref gamesLost);
            }
            else if (userInput == "2")
            {
                ScoreBoard(gamesWon, gamesLost);
            }


        }

        public static void SnowMan(ref int gamesWon, ref int gamesLost)
        {
            Console.Clear();
            string word = GetRandomWord();
            char[] displayWord = SetDisplayWord(word);
            int missed = 0;
            string guessed = "No Letters Guessed Yet";

            while (KeepGoing(displayWord, missed))
            {
                ShowBoard(displayWord, ref missed, guessed);
                Console.WriteLine();
                char pickedLetter = Console.ReadLine().ToUpper()[0];
                CheckChoice(displayWord, word, ref missed, ref guessed, pickedLetter);
            }

            if (missed == 7)
            {
                ShowBoard(displayWord, ref missed, guessed);
                Console.WriteLine("Sorry you lost");
                gamesLost++;
            }
            else
            {
                Console.Clear();
                ShowBoard(displayWord, ref missed, guessed);
                Console.WriteLine("You Won!");
                gamesWon++;
            }
            Console.WriteLine("Press any key to continue.....");
            Console.ReadKey();
        }

        public static void CheckChoice(char[] displayWord, string word, ref int missed,
                                      ref string guessed, char pickedLetter)
        {
            /*Update Check choice.  It should check to see if the letter picked is in the 
             * word.  If it is, it should updated the guessed array (remember to handle 
             * the situation where it is the first letter picked) and clear the 
             * console.  If it is not, it should tell the user the letter was not 
             * found, to press a key to continue.  Update the guessed letters array 
             * and clear the console. 
             */

            int index = -1;

            if (guessed == "No Letters Guessed Yet")
            {
                guessed = "";
            }

            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] == pickedLetter)
                {
                    guessed += pickedLetter;
                    index = i;
                    displayWord[i] = word[i];
                }
            }
            if (index == -1)
            {
                guessed += pickedLetter;
                Console.WriteLine("Letter not found");
                missed++;
            }







        }

        public static bool KeepGoing(char[] displayWord, int missed)
        {
            /*Update to return true if they have missed less than 7 guesses 
             * AND there are still underscores left meaning they have not 
             * fully guessed the word
             */

            int index = -1;

            for (int i = 0; i < displayWord.Length; i++)
            {
                if (displayWord[i] == '_')
                {
                    index = i;
                }
            }

            if (missed < 7 && index != -1)
            {
                return true;
            }
            else
            {
                return false;
            }



        }

        public static void ShowBoard(char[] displayWord, ref int missed, string guessed)
        {
            Console.WriteLine("Word to guess: ");
            for (int i = 0; i < displayWord.Length; i++)
            {
                Console.Write(displayWord[i]);
            }

            Console.WriteLine();
            Console.WriteLine("Letters Guessed => " + guessed);

            Console.WriteLine("Currently missed " + missed);

        }


        public static char[] SetDisplayWord(string word)
        {
            /*SetDisplayWord to return a character array of 
             * underscores and spaces to match the word returned in step 3
             */

            int arrayLen = word.Length;

            char[] wordArray = new char[arrayLen];

            for (int i = 0; i < arrayLen; i++)
            {
                wordArray[i] = '_';

            }
            return wordArray;


        }
        public static string GetRandomWord()
        {
            /* Return a random word to be used in the game
             */
            string[] words = new string[10];
            int count = GetAllWords(words);

            Random randomNumber = new Random();
            int randomWord;
            randomWord = randomNumber.Next(count);
            return words[randomWord];



        }

        static int GetAllWords(string[] words)
        {
            int count = 0;

            StreamReader inFile = new StreamReader("words.txt"); //prime

            string line = inFile.ReadLine();

            while (line != null)
            {
                words[count] = line;
                count++;
                line = inFile.ReadLine(); // update
            }

            inFile.Close();
            return count;

        }
        public static void ScoreBoard(int gamesWon, int gamesLost)
        {
            Console.Clear();
            Console.WriteLine("You have won " + gamesWon + " games");
            Console.WriteLine("You have lost " + gamesLost + " games");
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }

        public static void Goodbye(ref int gamesWon, ref int gamesLost)
        {
            Console.Clear();
            Console.WriteLine("Thank you for playing... \n" +
                "Press any key for one final look at the scoreboard" +
                " before you go");
            Console.ReadKey();
            ScoreBoard(gamesWon, gamesLost);

        }
    }
}
