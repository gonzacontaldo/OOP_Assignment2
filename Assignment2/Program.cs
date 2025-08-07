using System;
using System.Drawing;
using System.IO;
using static System.Console;

namespace Assignment2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //load the stream
            ReadStream.Stream();
            //load menu
            Menu mainMenu = new Menu();
        }
    }

    /*===========================================Main================================================================*/


    internal class ReadStream
    {
        static string filePath = "VideoGames.txt";
        public static void Stream()
        {
            string[] lines;
            StreamReader myFileContent = new StreamReader(filePath);

            lines = myFileContent.ReadToEnd().Split('\n');

            myFileContent.Close();

            Game[] games = new Game[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue; // Skip empty lines

                // Remove \r if file is from Windows and split by comma
                string[] gameProperties = lines[i].Trim().Split(',');

                // Create a Game object for each line
                games[i] = new Game(
                    int.Parse(gameProperties[0]),
                    gameProperties[1],
                    decimal.Parse(gameProperties[2]),
                    float.Parse(gameProperties[3]),
                    int.Parse(gameProperties[4])
                );
            }
        }
    }
    /*=========================================== Stream  ================================================================*/

    internal class Game
    {
        private int itemNumber;
        private string itemName;
        private decimal itemPrice;
        private float userRating;
        private int itemStock;

        public Game() { }

        public Game(int number, string name, decimal price, float rating, int stock)
        {
            itemNumber = number;
            itemName = name;
            itemPrice = price;
            userRating = rating;
            itemStock = stock;
        }

        // Getters
        public int GetItemNumber() { return itemNumber; }
        public string GetItemName() { return itemName; }
        public decimal GetPrice() { return itemPrice; }
        public float GetUserRating() { return userRating; }
        public int GetQuantity() { return itemStock; }

        // Setters
        public void SetItemNumber(int number) { itemNumber = number; }
        public void SetItemName(string name) { itemName = name; }
        public void SetItemPrice(decimal price) { itemPrice = price; }
        public void SetUserRating(float rating) { userRating = rating; }
        public void SetItemStock(int stock) { itemStock = stock; }

        // ToString method for formatted output
        public override string ToString()
        {
            return $"{itemNumber} | {itemName} | ${itemPrice:F2} | Rating: {userRating}/5 | Stock: {itemStock}";
        }

    }

    /*===========================================Game================================================================*/

    internal class Menu
    {
        private int SelectedIndex;
        private string[] Options;
        private string Prompt;
        private string[] searchOptions;

        public Menu(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
        }
        public Menu()
        {
            SelectedIndex = 0;
            Prompt =
                  "===============================\n"
                + "        Game Shop Panel\n"
                + "===============================\n\n"
                + "Use UP/DOWN arrows to navigate\n"
                + "Press [ENTER] to select\n";

            Options = ["Add Products", "Search Products", "Shop Analytics", "About", "Exit"];

            while (true)
            {
                int selectedIndex = Display();
                Clear();
                switch (selectedIndex)
                {
                    case 0:
                        HandleData.addGame();
                        WriteLine("Press [ESC] to go back...");
                        WaitForEsc();
                        break;
                    case 1:
                        //HandleData.searchByID();
                        //HandleData.searchByPrice();
                        SearchMenu();
                        break;
                    case 2:
                        HandleData.Analytics();
                        WriteLine("Press [ESC] to go back...");
                        WaitForEsc();
                        break;
                    case 3:
                        string aboutPage =
                             "                              About                              " 
                            +"\n=============================================================\n"
                            + "Version     : 1.0.0\n"
                            + "Developed by: Beray Erdogan, Berhan Erdogan, Gonzalo Contaldo\n"
                            + "Date        : August 2025\n"
                            + "\n"
                            + "This project was created as part of\n"
                            + "a OOP assignment to manage\n"
                            + "a video game store inventory.\n"
                            + "\n"
                            + "Controls:\n"
                            + "- UP / DOWN  : Navigate Menu\n"
                            + "- ENTER      : Select Option\n"
                            + "- ESC        : Go Back\n"
                            + "\n"
                            + "Thank you for using our system!\n"
                            + "==============================================================";
                        WriteLine(aboutPage);
                        WriteLine("Press [ESC] to go back...");
                        WaitForEsc();
                        break;
                    case 4:
                        return;
                }
            }
        }

        public void SearchMenu()
        {
            SelectedIndex = 0;
            Clear();
            Menu searchMenu = new Menu(
                  "===============================\n"
                + "          Search Menu\n"
                + "===============================\n\n" +
                  "Use UP/DOWN arrows to navigate\n" +
                  "Press [ENTER] to select\n",
                new string[] { "Search by ID", "Search by Price","Back"}
            );
            while (true)
            {
                int selectedSearchIndex = searchMenu.Display();

                switch (selectedSearchIndex)
                {
                    case 0:
                        Clear();
                        HandleData.searchByID();
                        WriteLine("Press [ESC] to go back...");
                        WaitForEsc();
                        break;
                    case 1:
                        Clear();
                        HandleData.searchByPrice();
                        WriteLine("Press [ESC] to go back...");
                        WaitForEsc();
                        break;
                    case 2:
                        return;
                }

            }
        }
        private void DisplayOptions()
        {
            WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                if (SelectedIndex == i)
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                    WriteLine($"{i + 1}. >>{currentOption}<<");
                    ResetColor();
                }
                else
                {
                    WriteLine($"{i + 1}. {currentOption}");
                }
            }
        }
        public int Display()
        {
            ConsoleKeyInfo pressedKey;
            do
            {
                Clear();
                DisplayOptions();
                pressedKey = ReadKey(true);
                if (pressedKey.Key == ConsoleKey.UpArrow)
                {
                    SelectedIndex -= 1;
                }
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                {
                    SelectedIndex += 1;
                }


                if (SelectedIndex > Options.Length - 1)
                {
                    SelectedIndex = 0;
                }
                else if (SelectedIndex < 0)
                {
                    SelectedIndex = Options.Length - 1;
                }
            } while (pressedKey.Key != ConsoleKey.Enter);
            return SelectedIndex;

        }
        public static bool WaitForEsc()
        {
            ConsoleKeyInfo esc;
            do
            {
                esc = Console.ReadKey(true);
            } while (esc.Key != ConsoleKey.Escape);
            return true;
        }
    }

    /*===========================================Menu================================================================*/

    internal class HandleData
    {
        public static List<Game> CreateGameList()
        {
            List<Game> gameList = new List<Game>();

            try
            {
                using StreamReader reader = new StreamReader("VideoGames.txt");
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] gameProperties = line.Trim().Split(',');
                    int id = int.Parse(gameProperties[0].Trim());
                    string name = gameProperties[1].Trim();
                    decimal price = decimal.Parse(gameProperties[2].Trim());
                    float rating = float.Parse(gameProperties[3].Trim());
                    int stock = int.Parse(gameProperties[4].Trim());

                    Game game = new Game(id, name, price, rating, stock);
                    gameList.Add(game);
                }
            }
            catch (Exception exception)
            {
                WriteLine($"Error: {exception.Message}");
            }

            return gameList;
        }

        public static Game? addGame()
        {
            //user enters following data manually

            const string AddItemHeader =
            "===============================\n"
            + "          Add Item\n"
            + "===============================\n\n";
            string newItemName;
            decimal newItemPrice;
            float newItemRating;
            int newItemStock;
        
            WriteLine(AddItemHeader);
            while (true)
            {
                WriteLine("Please enter the item name:");
                newItemName = ReadLine();
                if (string.IsNullOrWhiteSpace(newItemName))
                {
                    WriteLine("Invalid name. Try again.\n");
                }
                else
                {
                    break;
                }
            }
            Clear();

            WriteLine(AddItemHeader);
            while (true)
            {
                WriteLine("Please enter the item price: ");
                decimal.TryParse(ReadLine(), out newItemPrice);
                if (newItemPrice <= 0)
                {
                    WriteLine("Invalid price. Try Again.\n\n");
                }
                else
                {
                    break;
                }  
            }
            Clear();


            WriteLine(AddItemHeader);
            while (true)
            {
                WriteLine("Please enter the rating (1 to 10): ");
                float.TryParse(ReadLine(), out newItemRating);
                if (newItemRating <1 || newItemRating > 10)
                {
                    WriteLine("Invalid rating. Try Again.\n\n");
                }
                else
                {
                    break;
                }  
            }
            Clear();

            WriteLine(AddItemHeader);
            while (true)
            {
                WriteLine("Please enter item stock: ");
                int.TryParse(ReadLine(), out newItemStock);
                if (newItemStock == 0)
                {
                    WriteLine("Invalid stock. Try Again.\n\n");
                }
                else
                {
                    break;
                }  
            }
            Clear();

                
                
        

    
                /*
            WriteLine("Please enter the item stock: ");
            newItemStock = Convert.ToInt32(ReadLine());
            Clear();
            */

            Menu addItemMenu = new Menu("===============================\n"
                                    + "          Add Item\n"
                                    + "===============================\n\n"
                                    +"Use UP/DOWN arrows to navigate\n" 
                                    +"Press [ENTER] to select\n\n"
                                    + "Do you want to add this item ?\n\n"
                                    + $"Name: {newItemName}\nPrice: {newItemPrice}$\nRating {newItemRating}/10\nStock: {newItemStock}\n"
                                    , ["Yes", "No"]);
            int itemMenuSelectedIndex = addItemMenu.Display();
            if (itemMenuSelectedIndex == 1)
            {
                return null;
            }
        
        
            string fileName = "VideoGames.txt";
            //newItemID automatically assigned
            int lineCount = 0;
            using StreamReader st = new StreamReader("VideoGames.txt");
            while (st.ReadLine() != null)
            {
                lineCount++;
            }
            int newItemID = lineCount + 1001;

            try
            {
                using StreamWriter writer = new StreamWriter(fileName, true);
                writer.WriteLine($"{newItemID}, {newItemName}, {newItemPrice}, {newItemRating}, {newItemStock}");

            }
            catch (Exception exception)
            {
                WriteLine($"Error: {exception.Message}");
                return null;
            }
            Clear();
            WriteLine("           New Item            ");
            WriteLine("===============================");
            WriteLine($"{newItemName} is added to stock");
            WriteLine($"Amount: {newItemStock}");
            WriteLine("===============================");
            return new Game(newItemID, newItemName, newItemPrice, newItemRating, newItemStock);
        }

        public static Game? searchByID()
        {
            WriteLine("===============================\n"
                     + "          Search Menu\n"
                    + "===============================\n\n");
            int searchItemID;
            while (true)
            {
                WriteLine("Please enter the 4 digit item ID: ");
                int.TryParse(ReadLine(), out searchItemID);
                if (searchItemID < 1000)
                {
                    WriteLine("Invalid ID. Try Again.\n\n");
                }
                else
                {
                    break;
                }  
            }

            List<Game> gameListForSearch = CreateGameList();
            foreach (Game game in gameListForSearch)
            {
                if (game.GetItemNumber() == searchItemID)
                {
                    Clear();
                    WriteLine("        Search Results      ");
                    WriteLine("===============================");
                    WriteLine($"{game.GetItemName()}");
                    WriteLine($"Price: {game.GetPrice()}");
                    WriteLine($"Rating: {game.GetUserRating()}");
                    WriteLine($"In Stock: {game.GetQuantity()}");
                    WriteLine("===============================");

                    return game;
                }
            }
            // If no such game
            WriteLine("Game not found.");
            return null;
        }

        public static void searchByPrice()
        {
            WriteLine("===============================\n"
                + "          Search Menu\n"
                + "===============================\n\n");
            decimal searchMaxPrice;
            while (true)
            {
                WriteLine("Please enter max price: ");
                decimal.TryParse(ReadLine(), out searchMaxPrice);
                if (searchMaxPrice <= 0)
                {
                    WriteLine("Invalid price. Try Again.\n\n");
                }
                else
                {
                    break;
                }  
            }
            Clear();
            List<Game> gameListForSearch = CreateGameList();
            Clear();
            WriteLine("        Search Results      ");
            WriteLine("===============================");
            foreach (Game game in gameListForSearch)
            {
                if (game.GetPrice() <= searchMaxPrice)
                {
                    WriteLine(game.GetItemName());
                    WriteLine($"Price: {game.GetPrice()}");
                    WriteLine($"Rating: {game.GetUserRating()}");
                    WriteLine($"In Stock: {game.GetQuantity()}");
                    WriteLine("===============================");
                }
            }
        }

        public static void Analytics()
        {
            List<Game> gameList = CreateGameList();
            gameList.Sort((g1, g2) => g1.GetPrice().CompareTo(g2.GetPrice()));
            decimal priceTotal = 0;
            foreach (Game game in gameList)
            {
                priceTotal += game.GetPrice();
            }
            Clear();
            WriteLine("         Shop Analytics Dashboard");
            WriteLine("==============================================");
            WriteLine($"Total Games in Inventory : {gameList.Count}");
            WriteLine($"Our Price Range          : ${gameList[0].GetPrice():F2} - ${gameList[gameList.Count - 1].GetPrice():F2}");
            WriteLine($"Average Game Price       : ${priceTotal / gameList.Count:F2}");
            WriteLine($"Highest Priced Game      : {gameList[gameList.Count - 1].GetItemName()} (${gameList[gameList.Count - 1].GetPrice():F2})");
            WriteLine($"Lowest Priced Game       : {gameList[0].GetItemName()} (${gameList[0].GetPrice():F2})");
            WriteLine("\nThank you for choosing our store!");
            WriteLine("==============================================");
            
        }
    }
}
/*===========================================Handle Data==========================================================*/