using System; //need
using System.IO; //need 
using System.Collections.Generic; //need
using System.Linq; //need
using System.Text; //need
using System.Text.RegularExpressions; //honestly don't remember if I even used Regex and at this point I am afraid to make sure 
using System.Threading.Tasks;

namespace movieRentalApplicationAttempt2
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
            MOVIES are indexed one per line in the following format: 
            TITLE,IN/OUT,CUSTOMER ID IF CHECKED OUT,DUE DATE IF CHECKED OUT
            filepath ..\\..\\movies.txt

            CUSTOMERS are indexed one per line in the following format:
            LAST NAME,FIRST NAME,PHONE NUMBER,CUSTOMER ID,OVERDUE FLAG(0/$)
            filepath ..\\..\\customers.txt
            */

            //METHODS: 
            //Add a new movie to the movie file - AddMovie();
            //Add a new customer to the customer file - AddCustomer(); 
            //Check out a movie - CheckOutMovie(); 
            //Check in a movie - CheckInMovie(); 
            //Get customer number - GetCustomerNum(); 
            //Tell if a movie is overdue = TellIfMovieIsOverdue();
            //search all movies to see if overdue & flags customers AreMoviesOverdue(); 
            //find all flagged customers - FindAllFlaggedCustomers(); *could be formatted nicer but it works for now
            //unflag customer once paid up - UnflagCustomer(); 
            //see all available movies - SeeAllAvailableMovies(); 
            //see who checked out a specific movie - WhoCheckedThisOut(); 

            //TO-DO
            //add get customer ID to check in/out and unflagging for customers with same name?


            string selection = "";
            while (selection != "10")
            {
                Console.WriteLine("REALLY BAD VIDEO RENTAL STORE (TM) MANAGEMENT APP");
                Console.WriteLine("*************************************************");
                Console.WriteLine(); //empty line
                Console.WriteLine("PLEASE MAKE A SELECTION:");
                Console.WriteLine(); //empty line
                Console.WriteLine("0 - List All Available Movies");
                Console.WriteLine("1 - Check out a movie");
                Console.WriteLine("2 - Check a movie back in");
                Console.WriteLine("3 - See who checked out a specific movie.");
                Console.WriteLine(); //empty line
                Console.WriteLine("4 - Daily overdue check & flag accounts");
                Console.WriteLine("5 - Get specific movie due date");
                Console.WriteLine(); //empty line
                Console.WriteLine("6 - Find all customers with overdue acounts");
                Console.WriteLine("7 - Unflag a customer once their late fees are paid");
                Console.WriteLine(); //empty line
                Console.WriteLine("8 - Add a new movie to the store inventory");
                Console.WriteLine("9 - Add a new customer to the customer directory");
                Console.WriteLine(); //empty line
                Console.WriteLine("10 - Exit");
                Console.WriteLine("*************************************************");
                Console.WriteLine();
                selection = Console.ReadLine();

                switch (selection)
                {
                    case "0":
                        SeeAllAvailableMovies();
                        Console.WriteLine(); //empty line
                        break;
                    case "1":
                        CheckOutMovie();
                        Console.WriteLine(); //empty line
                        break;
                    case "2":
                        CheckInMovie();
                        Console.WriteLine(); //empty line
                        break;
                    case "3":
                        WhoCheckedThisOut();
                        Console.WriteLine(); //empty line
                        break;
                    case "4":
                        AreMoviesOverdue();
                        Console.WriteLine(); //empty line
                        break;
                    case "5":
                        TellIfMovieIsOverdue();
                        Console.WriteLine(); //empty line
                        break;
                    case "6":
                        FindAllFlaggedCustomers();
                        Console.WriteLine(); //empty line
                        break;
                    case "7":
                        UnflagCustomer();
                        Console.WriteLine(); //empty line
                        break;
                    case "8":
                        AddMovie();
                        Console.WriteLine(); //empty line
                        break;
                    case "9":
                        AddCustomer();
                        Console.WriteLine(); //empty line
                        break;
                    case "10":
                        break;
                    default:
                        break;
                }
            }


        }//end of main method 



        //TELL WHO CHECKED OUT A MOVIE 
        static void WhoCheckedThisOut()
        {
            List<string> movies = new List<string>();

            StreamReader reader = new StreamReader("..\\..\\movies.txt");
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    movies.Add(line);
                    line = reader.ReadLine();
                }
            }//end of reader

            Console.WriteLine("What movie do you want the customer information for?");
            string searchTitle = Console.ReadLine().ToUpper();
            string result = movies.FirstOrDefault(s => s.Contains(searchTitle)); //searches list for first instance of title
            string custID = "";
            if (movies.Contains(result)) //sets custID to matching customer ID number in movie list
            {
                string[] movieInfo = result.Split(',');
                if (movieInfo[2] != "NONE") //if movie is checked out
                {
                    custID = movieInfo[2];

                    List<string> customers = new List<string>();

                    StreamReader reader2 = new StreamReader("..\\..\\customers.txt");
                    using (reader2)
                    {
                        string line2 = reader2.ReadLine();
                        while (line2 != null)
                        {
                            customers.Add(line2);
                            line2 = reader2.ReadLine();
                        }
                    }//end of reader2

                    string custResult = customers.FirstOrDefault(s => s.Contains(custID)); //search for first instance of customer ID in customer list
                    string[] custInfo = custResult.Split(',');
                    string lastName = custInfo[0];
                    string firstName = custInfo[1];

                    StringBuilder whoCheckedOut = new StringBuilder();
                    whoCheckedOut.Append(searchTitle + " ");
                    whoCheckedOut.Append("was checked out by ");
                    whoCheckedOut.Append(lastName + ", ");
                    whoCheckedOut.Append(firstName + ", ");
                    whoCheckedOut.Append(custID);
                    string sayThis = whoCheckedOut.ToString();
                    Console.WriteLine(sayThis);
                }
                else
                {
                    Console.WriteLine("Error: That movie is not checked out.");
                }
            }

        }//end of method




        //SEE ALL AVAILABLE MOVIES
        static void SeeAllAvailableMovies()
        {
            List<string> movies = new List<string>();

            StreamReader reader = new StreamReader("..\\..\\movies.txt");
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    movies.Add(line);
                    line = reader.ReadLine();
                }
            }//end of reader

            Console.WriteLine("MOVIES AVAILABLE FOR CHECK-OUT:");
            foreach (string title in movies)
            {
                string[] movieInfo = title.Split(',');
                string filmTitle = movieInfo[0];
                if (movieInfo[1] == "IN")
                {
                    Console.WriteLine(filmTitle);
                }
            }
        }//end of method 



        //UNFLAG CUSTOMER
        static void UnflagCustomer()
        {
            List<string> customers = new List<string>();

            StreamReader reader = new StreamReader("..\\..\\customers.txt");
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    customers.Add(line);
                    line = reader.ReadLine();
                }
            }//end of reader

            Console.WriteLine("What is the last name of the customer settling their account?");
            string custLastName = Console.ReadLine().ToUpper();
            Console.WriteLine("What is their first name?");
            string custFirstName = Console.ReadLine().ToUpper();
            string custFullName = custLastName + "," + custFirstName;
            string result = customers.FirstOrDefault(s => s.Contains(custFullName)); //searches list for first instance of customer name
            //add request for ID number for customers with same names, eventually

            string[] customerInfo = result.Split(',');
            string lastName = customerInfo[0];
            string firstName = customerInfo[1];
            string phoneNum = customerInfo[2];
            string custIDNum = customerInfo[3];
            string custFlag = customerInfo[4];

            StringBuilder unflag = new StringBuilder();
            unflag.Append(lastName + ",");
            unflag.Append(firstName + ",");
            unflag.Append(phoneNum + ",");
            unflag.Append(custIDNum + ",");
            unflag.Append("0" + ",");
            string unflaggedCust = unflag.ToString().Trim(',');

            customers.Remove(result);
            customers.Add(unflaggedCust);

            FileStream customersOverwrite = new FileStream("..\\..\\customers.txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(customersOverwrite);
            using (writer)
            {
                foreach (string c in customers)
                {
                    writer.WriteLine(c);
                }
            }//end of writer
        }//end of method 




        //LIST ALL CUSTOMERS WHO ARE FLAGGED AS OVERDUE
        static void FindAllFlaggedCustomers()
        {
            List<string> customers = new List<string>();
            List<string> overdueCustomers = new List<string>();

            StreamReader reader = new StreamReader("..\\..\\customers.txt");
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    customers.Add(line);
                    line = reader.ReadLine();
                }
            }//end of reader


            foreach (string cust in customers) //find customers that are flagged as overdue
            {
                string[] thisCustomer = cust.Split(',');
                if (thisCustomer[4] == "$")
                {
                    overdueCustomers.Add(cust);
                }
            }

            if (overdueCustomers.Count() > 0)
            {
                Console.WriteLine("OVERDUE CUSTOMERS:");
                Console.WriteLine("******************");
                foreach (string overduecust in overdueCustomers) //write out list of overdue customers
                {
                    Console.WriteLine(overduecust);
                }
            }
            else
            {
                Console.WriteLine("There are no customers with overdue flags on their accounts.");
            }


        }//end of method




        //CHECK IF ALL MOVIES ARE OVERDUE AND FLAG CUSTOMERS IF THEY HAVE OVERDUE MOVIES 
        static void AreMoviesOverdue()
        {
            List<string> movies = new List<string>();
            List<string> overdueMovies = new List<string>();

            StreamReader reader = new StreamReader("..\\..\\movies.txt");
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    movies.Add(line);
                    line = reader.ReadLine();
                }
            }//end of reader

            DateTime todaysDate = DateTime.Today;

            foreach (string movieInfo in movies) //for each movie
            {
                string[] thisMovieInfo = movieInfo.Split(',');
                string thisMovieTitle = thisMovieInfo[0]; //title of movie
                if (thisMovieInfo[3] != "NONE") //if movie is checked out
                {
                    string dueDate = thisMovieInfo[3];
                    DateTime dueDateDT = DateTime.Parse(dueDate);
                    if (dueDateDT < todaysDate) //if movie is checked out AND OVERDUE
                    {
                        Console.WriteLine(thisMovieTitle + " is overdue.  Due on " + dueDate);
                        overdueMovies.Add(movieInfo); //adds movie information to new list "overduemovies" to flag customers
                    }
                }
            }//end of foreach loop

            //FLAG OVERDUE ACCOUNTS
            List<string> customers = new List<string>();
            List<string> offendingCustomers = new List<string>();
            List<string> overdueCustomers = new List<string>();

            StreamReader readerNew = new StreamReader("..\\..\\customers.txt");
            using (readerNew)
            {
                string line = readerNew.ReadLine();
                while (line != null)
                {
                    customers.Add(line);
                    line = readerNew.ReadLine();
                }
            }//end of reader

            foreach (string film in overdueMovies) //for each overdue movie 
            {
                string[] thisOverdueMovie = film.Split(',');
                string customerCheckedOutTo = thisOverdueMovie[2]; //the offending customer
                offendingCustomers.Add(customerCheckedOutTo);
            }

            foreach (string oCustomer in offendingCustomers) //for each offending customer ID number
            {
                string result = customers.FirstOrDefault(s => s.Contains(oCustomer)); //searches list for first instance of ID number
                if (customers.Contains(result))
                {
                    string[] offendingCustomer = result.Split(',');
                    string ocLastName = offendingCustomer[0]; //last name
                    string ocFirstName = offendingCustomer[1]; //first name
                    string ocPhone = offendingCustomer[2]; //phone number
                    string ocNumber = offendingCustomer[3]; //customer ID number
                    string ocOverdueFlag = offendingCustomer[4]; //customer overdue flag

                    StringBuilder offendingCustomerFlag = new StringBuilder();
                    offendingCustomerFlag.Append(ocLastName + ",");
                    offendingCustomerFlag.Append(ocFirstName + ",");
                    offendingCustomerFlag.Append(ocPhone + ",");
                    offendingCustomerFlag.Append(ocNumber + ",");
                    offendingCustomerFlag.Append("$" + ",");
                    string customerFlaggedOverdue = offendingCustomerFlag.ToString().Trim(',');

                    customers.Remove(result); //remove previous customer entry
                    overdueCustomers.Add(customerFlaggedOverdue);
                    customers.Add(customerFlaggedOverdue); //add flagged customer entry
                }
            }//end of foreach loop

            Console.WriteLine();//empty line
            Console.WriteLine("CUSTOMERS WITH OVERDUE MOVIES:");
            foreach (string overdueCustomer in overdueCustomers) //print out all overdue customers
            {
                Console.WriteLine(overdueCustomer);
            }


            FileStream customersOverwrite = new FileStream("..\\..\\customers.txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(customersOverwrite);
            using (writer)
            {
                foreach (string c in customers)
                {
                    writer.WriteLine(c);
                }
            }//end of writer

        }//end of method




        //CHECK IF A SPECIFIC MOVIE IS OVERDUE
        static void TellIfMovieIsOverdue()
        {
            List<string> movies = new List<string>();

            StreamReader reader = new StreamReader("..\\..\\movies.txt");
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    movies.Add(line);
                    line = reader.ReadLine();
                }
            }//end of reader

            Console.WriteLine("What movie do you want to check the due date for?");
            string searchTitle = Console.ReadLine().ToUpper();
            string result = movies.FirstOrDefault(s => s.Contains(searchTitle)); //searches list for first instance of title
            if (movies.Contains(result))
            {
                string[] splitResult = result.Split(',');
                string movieTitle = splitResult[0];
                string dateDueBy = splitResult[3]; //retrieve due date
                DateTime dateDueByDT = DateTime.Parse(dateDueBy); //THIS WORKS
                DateTime todaysDate = DateTime.Today;
                if (dateDueByDT < todaysDate && dateDueBy != "NONE") //if the movie is overdue
                {
                    Console.WriteLine(movieTitle + " is overdue!");
                    Console.WriteLine(movieTitle + " was due on " + dateDueBy);
                }
                else
                {
                    Console.WriteLine("That movie isn't overdue yet.");
                    Console.WriteLine(movieTitle + " is due on " + dateDueBy);
                }
            }
            else //if the movie title wasn't found 
            {
                Console.WriteLine("Title not found, please try again.");
            }
        }//end of method



        //CHECK IN A MOVIE 
        static void CheckInMovie()
        {
            List<string> movies = new List<string>();

            StreamReader reader = new StreamReader("..\\..\\movies.txt");
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    movies.Add(line);
                    line = reader.ReadLine();
                }
            }//end of reader 

            Console.WriteLine("CHECK A MOVIE BACK IN.");
            Console.WriteLine();
            Console.WriteLine("What is the title of the movie to be checked in?");
            string searchMovieTitle = Console.ReadLine().ToUpper();
            string result = movies.FirstOrDefault(s => s.Contains(searchMovieTitle)); //searches list for first instance of title
            if (movies.Contains(result))
            {
                string[] movieEntry = result.Split(',');
                string movieTitle = movieEntry[0]; //movie title
                string movieAvailable = movieEntry[1]; //movie in/out "OUT"
                string movieCustomer = movieEntry[2]; //movie customer checked out to "XXXXX"
                string movieDueDate = movieEntry[3]; //movie due date "XX/XX/XXXX"

                StringBuilder checkinMovie = new StringBuilder();
                checkinMovie.Append(movieTitle + ",");
                checkinMovie.Append("IN" + ",");
                checkinMovie.Append("NONE" + ",");
                checkinMovie.Append("NONE" + ",");
                string checkedinMovie = checkinMovie.ToString().Trim(',');

                movies.Remove(result);
                movies.Add(checkedinMovie);  //adds movie entry back to list

                FileStream moviesOverwrite = new FileStream("..\\..\\movies.txt", FileMode.Create);
                StreamWriter writer = new StreamWriter(moviesOverwrite);
                using (writer)
                {
                    foreach (string m in movies)
                    {
                        writer.WriteLine(m);
                    }
                }//end of writer
            }
            else
            {
                Console.WriteLine("Movie title not found.  Please try again.");
            }

        }//end of method 



        //CHECK OUT A MOVIE
        static void CheckOutMovie()
        {
            List<string> movies = new List<string>();

            StreamReader reader = new StreamReader("..\\..\\movies.txt");
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    movies.Add(line);
                    line = reader.ReadLine();
                }
            }//end of reader 

            Console.WriteLine("CHECK OUT A MOVIE.");
            Console.WriteLine();
            Console.WriteLine("What is the title of the movie to be checked out?");
            string searchMovieTitle = Console.ReadLine().ToUpper();
            string result = movies.FirstOrDefault(s => s.Contains(searchMovieTitle)); //searches list for first instance of title
            if (movies.Contains(result))
            {
                string[] movieEntry = result.Split(',');
                if (movieEntry[1] != "OUT") //if movie is not already checked out
                {
                    string movieTitle = movieEntry[0]; //movie title
                    string movieAvailable = movieEntry[1]; //movie in/out "IN"
                    string movieCustomer = movieEntry[2]; //movie customer checked out to "NONE"
                    string movieDueDate = movieEntry[3]; //movie due date "NONE"

                    Console.WriteLine("What is the last name of the customer checking out this movie?");
                    string custLastName = Console.ReadLine().ToUpper();
                    Console.WriteLine("What is the first name of the customer checking out this movie?");
                    string custFirstName = Console.ReadLine().ToUpper();
                    string customerNum = GetCustomerNumber(custLastName, custFirstName); //get customer ID number from customer list
                    string dueDate = DateTime.Now.AddDays(7).ToString("MM/dd/yyyy"); //7-day rental period 


                    StringBuilder checkoutMovie = new StringBuilder();
                    checkoutMovie.Append(movieTitle + ",");
                    checkoutMovie.Append("OUT" + ",");
                    checkoutMovie.Append(customerNum + ",");
                    checkoutMovie.Append(dueDate + ",");
                    string checkedoutMovie = checkoutMovie.ToString().Trim(',');

                    movies.Remove(result);
                    movies.Add(checkedoutMovie);  //adds movie entry back to list

                    FileStream moviesOverwrite = new FileStream("..\\..\\movies.txt", FileMode.Create);
                    StreamWriter writer = new StreamWriter(moviesOverwrite);
                    using (writer)
                    {
                        foreach (string m in movies)
                        {
                            writer.WriteLine(m);
                        }
                    }//end of writer
                }
                else
                {
                    Console.WriteLine("That movie is already checked out.  Please try again.");
                }
            }
            else
            { Console.WriteLine("Movie title not found.  Please try again."); }

        } //end of method



        //RETRIEVE CUSTOMER ID NUMBER 
        static string GetCustomerNumber(string customerLastName, string customerFirstName)
        {
            List<string> customers = new List<string>();

            StreamReader reader = new StreamReader("..\\..\\customers.txt");
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    customers.Add(line);
                    line = reader.ReadLine();
                }
            }//end of reader
            string customerName = customerLastName.ToUpper() + "," + customerFirstName.ToUpper();
            string result = customers.FirstOrDefault(s => s.Contains(customerName));
            if (customers.Contains(result))
            {
                string[] customerInfo = result.Split(',');
                string customerNum = customerInfo[3];
                return customerNum;
            }
            else
            {
                string error = "Customer ID not found.";
                return error;
            }

        }//end of method



        //ADD A MOVIE TO THE INVENTORY 
        static void AddMovie()
        {
            List<string> movies = new List<string>();

            StreamReader reader = new StreamReader("..\\..\\movies.txt");
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    movies.Add(line);
                    line = reader.ReadLine();
                }
            }//end of reader 

            Console.WriteLine("ADD A MOVIE TO THE INVENTORY");
            Console.WriteLine();
            Console.WriteLine("What is the title of the movie to be added to inventory?");
            string newMovieTitle = Console.ReadLine().ToUpper();
            StringBuilder newMovieItem = new StringBuilder();
            newMovieItem.Append(newMovieTitle + ",");
            newMovieItem.Append("IN" + ",");
            newMovieItem.Append("NONE" + ",");
            newMovieItem.Append("NONE" + ",");
            string newMovieAdded = newMovieItem.ToString().Trim(',');
            movies.Add(newMovieAdded);

            StreamWriter writer = new StreamWriter("..\\..\\movies.txt", false);
            using (writer)
            {
                foreach (string m in movies)
                {
                    writer.WriteLine(m);
                }
            }//end of writer
        }//end of method


        //ADD A CUSTOMER TO THE CUSTOMER FILE 
        static void AddCustomer()
        {
            List<string> customers = new List<string>();

            StreamReader reader = new StreamReader("..\\..\\customers.txt");
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    customers.Add(line);
                    line = reader.ReadLine();
                }
            }//end of reader 

            Console.WriteLine("ADD A NEW CUSTOMER");
            Console.WriteLine();
            Console.WriteLine("Enter Customer First Name:");
            string firstName = Console.ReadLine().ToUpper();
            Console.WriteLine("Enter Customer Last Name:");
            string lastName = Console.ReadLine().ToUpper();
            Console.WriteLine("Enter Customer Phone Number XXX-XXX-XXXX:");
            string phoneNumber = Console.ReadLine();
            double customerNum = RandomNumber();
            StringBuilder newCustomer = new StringBuilder();
            newCustomer.Append(lastName + ",");
            newCustomer.Append(firstName + ",");
            newCustomer.Append(phoneNumber + ",");
            newCustomer.Append(customerNum + ",");
            newCustomer.Append("0" + ",");
            string customer = newCustomer.ToString().Trim(',');
            customers.Add(customer);

            StreamWriter writer = new StreamWriter("..\\..\\customers.txt", false);
            using (writer)
            {
                foreach (string c in customers)
                {
                    writer.WriteLine(c);
                }
            }//end of writer
        }//end of method



        //RANDOM NUMBER GENERATOR FOR ASSIGNING CUSTOMER NUMBERS
        static double RandomNumber() //for assigning customer number 
        {
            Random randomNum = new Random();
            double customerNum = randomNum.Next(1, 10000);
            return customerNum;
        }//end of method
    }
}
