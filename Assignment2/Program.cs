using System;
using System.Drawing;
using System.IO;

namespace Assignment2
{
    internal class Program
    {
        static string filePath = "VideoGames.txt";
        static void Main(string[] args)
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
            
            // Display all games using ToString()
            Console.WriteLine("Video Game Inventory:\n");
            for (int i = 0; i < games.Length; i++)
            {
                if (games[i] != null)
                {
                    Console.WriteLine($"{i + 1}. {games[i].ToString()}");
                }
            }

            Console.ReadKey();

        }
    }

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
}