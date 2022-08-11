using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace MoviePlex
{
    internal class Main
    {
        const int MaxDailyMovies = 10;
        Movie[] Movies = new Movie[MaxDailyMovies];
        int count = 0;
        int noOfMovies { get; set; }
        List<String> TodaysMovieList = new List<String>();
        
        // welcome page of the moviePlex website
        public void Welcome()
        {
            // welcome to movieplex heading design
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n                               ************************************************************************");
            Console.WriteLine("                             *                                                                         *");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                             *                     Welcome to Movieplex Theatre                         *");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("                             *                                                                         *");
            Console.WriteLine("                                 ************************************************************************");

            int counter = 0;
            do
            // main menu where user selects administartor or guest option
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nPlease select from the following option :");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n1. Administrator");
                Console.WriteLine("2. Guest");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nSelect the option : ");
                String UserInput = Console.ReadLine();
                try
                
                {
                    int UserEntry = 0;
                    // converting from string(userInput) to int(user entry) 
                    if (int.TryParse(UserInput, out UserEntry))
                    {
                        if (UserEntry == 1)
                        //for administrator it does password check
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Password password = new Password();
                            password.checkPassword();
                            admin();
                            break;
                        }
                        // guest doesn't require password
                        else if (UserEntry == 2)
                        {
                            guest();
                            break;
                        }
                        else
                        // user selects either 1 or 2
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            throw new Exception("\nYou have selected a wrong option. Please select from 1 or 2.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        UserException userException = new UserException("\nPlease enter numeric value. You have selected wrong option");
                        throw userException;
                    }
                }
                catch (UserException userException)
                {
                    Console.WriteLine(userException.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (counter == 0);
        }
        // administrator page
        public void admin(bool ErrorFlag=false)
        {

            if (!ErrorFlag) { 
            Console.Clear();
           
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n                               *************************************************************************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                             **                         Welcome MoviePlex Administrator                         **");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("                             *************************************************************************************\n");
            Console.ForegroundColor = ConsoleColor.White;
            }
            Console.Write("\nHow Many Movies are playing today? : ");
            noOfMovies = 0;
            string UserInput = Console.ReadLine();
            
            int demoint = 0;
            
            if (int.TryParse(UserInput, out demoint))
            {
                noOfMovies = Convert.ToInt32(UserInput);
                if ((noOfMovies <= 10 && noOfMovies > 0))
                {
                    addMovies();
                }
                else
                {
                    Console.Write("Movies should be an integer and less then 10 or greater then 0.");

                    admin(true);
                }
            }
            
            else
            {
                Console.Write("Movies should be an integer and less then 10 or greater then 0.");

                admin(true);
            }
        }
        // guest page
        public void guest()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n                               ************************************************************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                             **                          Welcome to Guest                          **");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("                             ************************************************************************\n");
            Console.ForegroundColor = ConsoleColor.White;
            showMovies();
            Console.Write("Which Movie would you like to watch:");
            selectMovies();
        }
        // adding movies and their ratings
        public void addMovies()
        {
            string[] MoviesCounter = { "First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eighth", "Ninth", "Tenth" };
            string[] MoviesSelectedCounter = MoviesCounter.Take(noOfMovies).ToArray();

            ArrayList myAL = new ArrayList() { "G", "PG", "PG-13", "R", "NC-17", "2", "4", "6", "8", "10", "12", "14", "16", "18", "20", "22", "24" };
            string userKeyPress; ;
            do
            {
                foreach (string s in MoviesSelectedCounter)
                {
                    Movie movie = new Movie();
                    Console.WriteLine("");
                    Console.Write("Please Enter the " + s + " movie name : ");
                    movie.Name = Console.ReadLine();
                    do
                    {
                        Console.Write("Please Enter the Age Limit or Rating of the " + s + " Movie  : ");
                        userKeyPress = Console.ReadLine().ToUpper();
                    }
                    while (!myAL.Contains(userKeyPress));
                    movie.Rating = userKeyPress;
                    Movies[count] = movie;
                    count++;
                }

            } while (count != noOfMovies);
            Console.WriteLine("");
            showMovies();
            confirmMovies();
        }
        //displaying movies and their ratings
        public void showMovies()
        {
            for (int i = 0; i < noOfMovies; i++)
            {
                TodaysMovieList.Add(Movies[i].Rating);
                Console.WriteLine((i + 1) + ".  " + Movies[i].Name + " {" + Movies[i].Rating + "}");
            }

        }
        // confirmation of movies method
        private void confirmMovies()
        {
            Console.WriteLine("");
            char c;
            Console.Write("Your Movies playing today are Listed above. Are you Satisfied? (Y/N})? ");
            c = Convert.ToChar(Console.ReadLine());
            // storing the user entered values and checking whether to confirm the movies are not
            switch (Char.ToLower(c))
            {
                case 'y':
                    Console.Clear();
                    Welcome();
                    break;
                case 'n':
                    Console.Clear();
                    Main main = new Main();
                    main.admin();
                    break;
                default:
                    Console.WriteLine("Enter either Y or N");
                    confirmMovies();
                    break;
            }
        }
        // when user selects given movies
        public void selectMovies()
        {
            string j = Console.ReadLine();
            // selected movie should be in range of displayed movies
            if(Int32.Parse(j) <= noOfMovies && Int32.Parse(j) > 0)
            {
                ageConfirmation(j);
            }
            else
            // loop repeats until correct input is given
            {
                Console.Write("The movie number you entered doesn't exist. Please enter movie within the list given above: ");
                showMovies();
                selectMovies();
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Press M to go back to the Guest Main menu.");
            Console.WriteLine("Press S  to go back to the Start Page.");
            char ch1;
            ch1 = Convert.ToChar(Console.ReadLine());
            switch (Char.ToLower(ch1))
            {
                case 'm':
                    Console.Clear();
                    guest();
                    break;
                case 's':
                    Console.Clear();
                    Welcome();
                    break;
                default:
                    Console.WriteLine("Exiting the console");
                    break;
            }

        }
        // method for verification of age
        public void ageConfirmation(string j)
        {
            Console.Write("Please enter your age for verification:");
            int ageverify = 0;
            string k = Console.ReadLine();
            if(int.TryParse(k, out ageverify))
            {
                string rating = TodaysMovieList[Int32.Parse(j) - 1];
                // checking whether entered age is permitted or not. If not, movies list is displayed
                // again and asking user to select another movie.
                switch (rating)
                {
                    case "G":
                        if (Int32.Parse(k) > 0)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("Enjoy the Movie!");
                        }
                        else
                        {
                            Console.WriteLine("Your age is less than the required. Please select other movie");
                            showMovies();
                            selectMovies();
                        }
                        break;
                    case "PG":
                        if (Int32.Parse(k) >= 10)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("Enjoy the Movie!");
                        }
                        else
                        {
                            Console.WriteLine("Your age is less than the required. Please select other movie");
                            showMovies();
                            selectMovies();
                        }
                        break;
                    case "PG-13":
                        if (Int32.Parse(k) >= 13)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("Enjoy the Movie!");
                        }
                        else
                        {
                            Console.WriteLine("Your age is less than the required. Please select other movie");
                            showMovies();
                            selectMovies();
                        }
                        break;
                    case "R":
                        if (Int32.Parse(k) >= 15)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("Enjoy the Movie!");

                        }
                        else
                        {
                            Console.WriteLine("Your age is less than the required. Please select other movie");
                            showMovies();
                            selectMovies();
                        }
                        break;
                    case "NC-17":
                        if (Int32.Parse(k) >= 17)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("Enjoy the Movie!");
                        }
                        else
                        {
                            Console.WriteLine("Your age is less than the required. Please select other movie");
                            showMovies();
                            selectMovies();
                        }
                        break;
                }
            }
            else
            {
                Console.WriteLine("Age Should be in Number Format");
                ageConfirmation(j);
            }

           
        }
        class UserException : ApplicationException
        {
            private string msgDetails;
            public UserException() { }
            public UserException(string message)
            {
                msgDetails = message;
            }
            public override string Message => $"{msgDetails}";
        }
    }
}