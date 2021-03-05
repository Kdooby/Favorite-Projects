using Casino;
using Casino.TwentyOne;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace TwentyOne
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("\n\n\t\t\t\t*-*-*-*-*-*TUMBLEWEED'S GAMBLING HALL*-*-*-*-*-*\n\n");
            Console.ReadLine();

            Console.WriteLine("\tIt's a hot summer's day as you're walking the road.  Covered in sweat and the reminence of a hard day's work." +
                "\n\tYou have a little more pep in your step than usual, however, and your pockets\n\tare a little heavier too, it being Pay Day and all." +
                "\n\tThe Boss-Man gave you a little extra for how hard you've been workin' the last few months down in the Mines!" +
                "\n\tYou tell yourself that you probably should save it for a rainy day, or something nice for the Mrs.. \n\tWell, that's what the Mrs. would want at least.\n" +
                "\n\tBut today is different...You can feel it deep in your boots.\n" +
                "\n\tToday, you're feeling lucky...Aren't you punk?\n" +
                "\n======================================================================================================================\n" +
                "======================================================================================================================\n");
            Console.ReadLine();

            Console.WriteLine("\tAfter what seems like all afternoon, you finally get to where your feet are taking you.\n" +
                "\tAs you gaze up at the large sign above the entrance way,\n\tyour eyes battle with the sun, trying to read the writing on the wood.");
            Console.ReadLine();

            Console.WriteLine("\nDEALER: \"Howdy stranger! You made it to Ole TumbleWeed's Gambling Hall! Come on in and get out of that sun!\"");
            Console.ReadLine();

            Console.WriteLine("\n\tYour eyes look down and peer passed the wood doors into the building." +
                "\n\tYou can hear the sound of ragtime coming from the piano inside, and your feet can't help but tap along.");
            Console.ReadLine();

            Console.WriteLine("\tYou enter the Hall and walk toward the Dealer that called you in." +
                "\n\tThe smell of cigar smoke and stale alcohol fills the air, but this only calms your nerves.");
            Console.ReadLine();

            // PLAYER NAME
            Console.Write("\nDEALER: \"What's your name there fella?\"\nPLAYER: ");
            string playerName = Console.ReadLine().ToUpper();
            while (true)
            {
                if (playerName == String.Empty)
                {
                    Console.WriteLine("\nDEALER: \"I beg your pardon? I didn't catch that.\"");
                    Console.ReadLine();
                    Console.WriteLine("\n\tThe Dealer leans in a little closer to here you better.");
                    Console.ReadLine();
                    Console.Write("\nDEALER: \"What do people call you?\"");
                    Console.ReadLine();
                    Console.Write("\nPLAYER: ");
                    playerName = Console.ReadLine().ToUpper();
                }
                else break;

                if (playerName.ToLower() == "admin")   // if player types in "admin".  Accesses database and shows all of the error exceptions.
                                                       // Does not execute the game.
                {
                    List<ExceptionEntity> Exceptions = ReadExceptions();
                    foreach (var exception in Exceptions)
                    {
                        Console.Write(exception.Id + " ");
                        Console.Write(exception.ExceptionType + " ");
                        Console.Write(exception.ExceptionMessage + " ");
                        Console.Write(exception.TimeStamp + " ");
                        Console.WriteLine();
                    }
                    Console.Read();
                    return;
                }
            }

            // STARTING MONEY
            Console.WriteLine("\nDEALER: \"Welcome {0}! You look like you have Lady Luck blessin' you today!" +
                "\n\tMaybe your purse is a little heavier than usual...?\"", playerName);
            Console.ReadLine();
            bool validAnswer = false;
            int bank = 0;
            while (!validAnswer)
            {
                Console.Write("DEALER: \"How much you fittin' to bet with anyhow?\"\n{0}: $", playerName);
                validAnswer = int.TryParse(Console.ReadLine(), out bank);
                if (!validAnswer) Console.Write("DEALER: \"Oh come on now. Please enter digits only. And none of those fancy decimals!\"\n");
                Console.ReadLine();
            }

            if (bank >= 100)
            {
                Console.WriteLine("\n\tThe Dealer grins and eye's your coin purse, and then back up to you.\n");
                Console.ReadLine();
                Console.WriteLine("DEALER: \"I didn't know we was dealing with such a High Roller!\"");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\n\tThe Dealer's friendly smile turns to a frown as he eye's your coin purse.\n");

                Console.ReadLine();
                Console.WriteLine("DEALER: \"Well, not everyone's pockets run deep, do they?" +
                    "\n\tLet's see if fortune will favor the brave today!\"");
                Console.ReadLine();
            }

            // START GAME
            Console.Write("DEALER: \"Would you like to hunker down and try your hand at a game of BlackJack?\"\n{0}: ", playerName);
            string answer = Console.ReadLine().ToLower();
            if (answer == "yes" || answer == "yeah" || answer == "y" || answer == "ya"
                || answer == "yessir" || answer == "sure" || answer == "ok" || answer == "okay")
            {
                Console.WriteLine("\nDEALER: \"Great! Have yourself a drink while you're at it.  Free of charge while you're at the table.\"");
                Console.ReadLine();
                Console.Write("Would you like a drink?\n{0}: ", playerName);
                string drink = Console.ReadLine().ToLower();
                if (drink == "yes" || drink == "yeah" || drink == "y" || drink == "ya"
                    || drink == "yessir" || drink == "sure" || drink == "ok" || drink == "okay")
                {
                    Console.WriteLine("\n\tOne of the Lady's of the Hall pours you a whiskey and brings it over." +
                        "\n\tYou tip your hat to her and smile as she walks away.");
                    Console.ReadLine();
                }
                else
                {
                    Console.Write("\nDEALER: \"Ah!  Like to stay sharp do ya?  I respect that." +
                        "\n\tWhen it comes to one's money, a sharper mind will keep you in line.\"");
                    Console.ReadLine();
                }
                    
            }
            else
            {
                Console.WriteLine("\nDEALER: \"Well then.  Thank you for comin' in today, {0}.  We appreciate your business!\" ", playerName);
                Console.WriteLine("DEALER:\"Come on back now, when you're ready!\"");
                Console.Read();
                return;
            }
            Console.WriteLine("\nDEALER: \"Alright now.  Good Luck!\"");
            Console.Read();
            Console.WriteLine("\n======================================================================================================================\n" +
            "======================================================================================================================");
            Console.Read();
            



            Player player = new Player(playerName, bank); // New Player object.  Initalized with name and funds (from Player Constructor)
                player.Id = Guid.NewGuid(); // Global Unique Identifier.  Access players Guid and look up player information.
                using (StreamWriter file = new StreamWriter(@"C:\Users\kevin\Desktop\Logs\log.txt", true)) // "using" clears the memory when done with application
                {
                    file.WriteLine(player.Id);
                }
                Game game = new TwentyOneGame(); // Create New Game.  Polymorphism
                game += player;  // Adding Player to Game
                player.IsActivelyPlaying = true;  // While Player is actively playing, and balance is above 0, play the game
                while (player.IsActivelyPlaying && player.Balance > 0)
                {
                    try
                    {
                        game.Play();
                    }
                    catch (FraudException ex)
                    {
                        Console.WriteLine("DEALER: \"What're you trying to pull here!?\n" +
                            "Best think twice before you decide to ROB ME!\"");
                        Console.ReadLine();
                        Console.WriteLine("\n\tThe Dealer grabs you by the collar and throws you out on the street!\n" +
                            "\tYou pick yourself up and walk home with your head hangin' low.  The Mrs. isn't going to be happy about this one.");
                        Console.ReadLine();
                        Console.WriteLine("\tBetter luck next time, {0}", playerName);
                        UpdateDbWithException(ex);
                        Console.Read();
                        return;
                    }
                    catch (Exception ex)

                    {
                        Console.WriteLine("\n\tHmm...It looks like TumbleWeeds is going through some maintance.\n" +
                            "\tAwfully sorry to be cuttin things short.\n" +
                            "\tWe hope to see you here again very soon!");
                        UpdateDbWithException(ex);
                        Console.Read();
                        return;
                    }
                }

                while (player.IsActivelyPlaying && player.Balance <= 0)
                {
                    Console.WriteLine("\nDEALER: \"Unfortunatly, you do not have anymore money to gamble with...\"");
                    player.IsActivelyPlaying = false;
                }
            
            Console.WriteLine("\nDEALER: \"Alright then.  We appreciate your business {0}!\" ", playerName);
            Console.WriteLine("DEALER:\"Come on back now, you hear!?\"");
            Console.Read();
        }

        // DATABASE HANDLING

        private static void UpdateDbWithException(Exception ex)
        {
            string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = TwentyOneGame; Integrated Security = True;
                                        Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False;
                                        ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

            string queryString = @"INSERT INTO Exceptions (ExceptionType, ExceptionMessage, TimeStamp) VALUES" +
                                "(@ExceptionType, @ExceptionMessage, @TimeStamp)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add("@ExceptionType", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@ExceptionMessage", System.Data.SqlDbType.VarChar);
                command.Parameters.Add("@TimeStamp", System.Data.SqlDbType.DateTime);

                command.Parameters["@ExceptionType"].Value = ex.GetType().ToString();
                command.Parameters["@ExceptionMessage"].Value = ex.Message;
                command.Parameters["@TimeStamp"].Value = DateTime.Now;

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private static List<ExceptionEntity> ReadExceptions()
        {
            string connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = TwentyOneGame; Integrated Security = True;
                                        Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False;
                                        ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

            string queryString = @"Select Id, ExceptionType, ExceptionMessage, TimeStamp From Exceptions";

            List<ExceptionEntity> Exceptions = new List<ExceptionEntity>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ExceptionEntity exception = new ExceptionEntity();
                    exception.Id = Convert.ToInt32(reader["Id"]);
                    exception.ExceptionType = reader["ExceptionType"].ToString();
                    exception.ExceptionMessage = reader["ExceptionMessage"].ToString();
                    exception.TimeStamp = Convert.ToDateTime(reader["TimeStamp"]);
                    Exceptions.Add(exception);
                }
                connection.Close();
            }
            return Exceptions;
        }
    }
}