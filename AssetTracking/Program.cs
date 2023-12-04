using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        List<Product> prod = new List<Product>();
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Enter 1 to add a product, 2 to display the list of products, 3 to search for a product, or 4 to exit.");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine("Enter product category (computer or mobile phone) and press enter.");
                    string category = Console.ReadLine();
                    if (category.Equals(""))
                    {
                        Console.WriteLine("You must enter a product category. Enter a category and press enter");
                        break;
                    }
                    Console.WriteLine("Enter brand name and press enter.");
                    string brand = Console.ReadLine();
                    if (brand.Equals(""))
                    {
                        Console.WriteLine("You must enter a brand name. Enter a brand and press enter");
                        break;
                    }
                    Console.WriteLine("Enter model name and press enter.");
                    string model = Console.ReadLine();
                    if (model.Equals(""))
                    {
                        Console.WriteLine("You must enter a model name. Enter a model name and press enter");
                        break;
                    }
                    Console.WriteLine("Enter an office location (London, Miami or Rio) and press enter.");
                    string location = Console.ReadLine();
                    if (location.Equals(""))
                    {
                        Console.WriteLine("You must enter an office location. Enter a location and press enter");
                        break;
                    }
                    Console.WriteLine("Enter the purchase date in the format dd/MM/yy and press enter.");
                    string date = Console.ReadLine();
                    if (date.Equals(""))
                    {
                        Console.WriteLine("You must enter a date. Enter a date in the format dd/MM/yy and press enter.");
                        break;
                    }
                    else
                    {
                        DateTime parsedDate;
                        if (!DateTime.TryParseExact(date, "dd/MM/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                        {
                            Console.WriteLine("Invalid date format. Enter a date in the format dd/MM/yy and press enter.");
                            break;
                        }

                        Console.WriteLine("Enter price and press enter.");
                        string wrongInput = Console.ReadLine();
                        int price;
                        if (int.TryParse(wrongInput, out price))
                        {
                            Console.WriteLine("Price has been entered");
                            prod.Add(new Product(category, brand, model, location, parsedDate, price));
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer.");
                            break;
                        }

                    }
                case "2":

                    List<Product> sortedList = prod.OrderBy(prod => prod.Location).ThenBy(prod => prod.ParsedDate).ToList();
                    Console.WriteLine("");
                    Console.WriteLine("Category".PadRight(20) + "Brand".PadRight(20) + "Model".PadRight(20) + "Location".PadRight(20) + "Date".PadRight(20) + "Price");
                    foreach (Product product in sortedList)
                    {
                        ConsoleColor origColor = Console.ForegroundColor;
                        int timeAfterPurchase = (DateTime.Now.Year - product.ParsedDate.Year) * 12 + (DateTime.Now.Month - product.ParsedDate.Month);
                        if (timeAfterPurchase > 33)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if (timeAfterPurchase >= 30 && timeAfterPurchase <= 33)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        Console.WriteLine(product.Category.PadRight(20) + product.Brand.PadRight(20) + product.Model.PadRight(20) + product.Location.PadRight(20) + product.ParsedDate.ToString("dd/MM/yy").PadRight(20) + product.Price);
                        Console.ForegroundColor = origColor;
                    }
                    Console.WriteLine("");
                    int totalPrice = prod.Sum(prod => prod.Price);
                    Console.WriteLine("The total price for your products is:   " + totalPrice);
                    break;
                case "3":
                    Console.WriteLine("Enter product name to search for and press enter.");
                    string searchName = Console.ReadLine();
                    List<Product> searchResults = prod.Where(prod => prod.Brand.ToLower().Contains(searchName.ToLower())).ToList();
                    if (searchResults.Count > 0)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Category".PadRight(20) + "Brand".PadRight(20) + "Model".PadRight(20) + "Location".PadRight(20) + "Date" + "Price");
                        Console.ForegroundColor = ConsoleColor.Green;

                        foreach (Product product in searchResults)
                        { Console.WriteLine(product.Category.PadRight(20) + product.Brand.PadRight(20) + product.Model.PadRight(20) + product.Location.PadRight(20) + product.ParsedDate + product.Price); }
                        Console.WriteLine("");
                        Console.ResetColor();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("No products found with that name.");
                        break;
                    }
                 case "4":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }
}

class Product
    {
        public Product(string category, string brand, string model, string location, DateTime parsedDate, int price)
        {
            Category = category;
            Brand = brand;
            Model = model;
            Location = location;
            ParsedDate = parsedDate;
            Price = price;
        }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Location { get; set; }
        public DateTime ParsedDate { get; set; }
        public int Price { get; set; }
    }
