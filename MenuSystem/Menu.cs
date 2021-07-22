using System;
using System.Collections.Generic;
using System.Linq;

namespace MenuSystem
{
    public enum MenuLevel
    {
        Level0,
        Level1,
        Level2Plus
    }
    public class Menu
    {
        private Dictionary<string, MenuItem> MenuItems { get; set; } = new Dictionary<string, MenuItem>();

        private readonly MenuLevel _menuLevel;

        private readonly string[] _reservedActions = new[] {"x", "m", "r"};
        public Menu(MenuLevel level)
        {
            _menuLevel = level;
        }

        public void AddMenuItem(MenuItem item)
        {
            if (item.UserChoice.Trim() == "")
            {
                throw new ArgumentException("UserChoice cannot be empty");
            }

            if (MenuItems.ContainsKey(item.UserChoice) || _reservedActions.Any(rChoice => rChoice == item.UserChoice))
            {
                throw new ArgumentException("Sorry, but this userChoice key is already in use");
            }

            MenuItems.Add(item.UserChoice, item);
        }

        public string RunMenu()
        {
            var userChoice = "";
            var menuSize = MenuItems.Count;
            var menuPos = 0;
            do
            {
                Console.WriteLine("");

                var incrementer = 0;
                foreach (var menuItem in MenuItems)
                {
                    Console.Write(menuItem.Value);
                    //Console.Write($"{menuItem.Value.UserChoice}) {menuItem.Value.Label}");
                    if (menuPos == incrementer)
                    {
                        WriteWithColor(" *", "yellow", true);
                    }
                    else
                    {
                        Console.WriteLine("");
                    }

                    incrementer++;
                }

                Console.WriteLine("-------- NAVIGATION --------");
                switch (_menuLevel)
                {
                    case MenuLevel.Level0:
                        WriteWithColor("X)", "yellow");
                        Console.WriteLine("Exit");
                        break;
                    case MenuLevel.Level1:
                        WriteWithColor("M)", "yellow");
                        Console.WriteLine("Return to Main");
                        WriteWithColor("X)", "yellow");
                        Console.WriteLine("Exit");
                        break;

                    case MenuLevel.Level2Plus:
                        WriteWithColor("R)", "yellow");
                        Console.WriteLine("Return to Previous");
                        WriteWithColor("M)", "yellow");
                        Console.WriteLine("Return to Main Menu");
                        WriteWithColor("X)", "yellow");
                        Console.WriteLine("Exit");
                        break;
                    default:
                        throw new Exception("Unknown menu level");
                }

                Console.Write(">");

                var inputKey = Console.ReadKey();

                switch (inputKey.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (menuPos != menuSize-1)
                        {
                            menuPos++;
                        }
                        else
                        {
                            menuPos = 0;
                        }
                        //Console.Clear();
                        break;
                    
                    case ConsoleKey.UpArrow:
                        if (menuPos != 0)
                        {
                            menuPos--;
                        }
                        else
                        {
                            menuPos = menuSize-1;
                        }
                        //Console.Clear();
                        break;
                    
                    case ConsoleKey.Enter:
                        userChoice = MenuItems.ElementAt(menuPos).Key;
                        break;
                    
                    default:
                        userChoice = inputKey.KeyChar.ToString();
                        Console.WriteLine("");
                        break;
                }

                //userChoice = Console.ReadLine()?.ToLower().Trim() ?? "";

                if (!_reservedActions.Contains(userChoice))
                {
                    if(MenuItems.TryGetValue(userChoice, out var userMenuItem))
                    {
                        //Console.Clear();
                        userChoice = userMenuItem.MethodToExecute();
                    }
                    else
                    {
                        if(userChoice != "")
                        {
                            //Console.Clear();
                            WriteWithColor("I don't have this option", "red", true);
                        }
                    }
                }

                if (userChoice == "redraw" && _menuLevel == MenuLevel.Level1)
                {
                    break;
                }
                
                if (userChoice == "x")
                {
                    if (_menuLevel == MenuLevel.Level0)
                    {
                        Console.WriteLine("Closing ......");
                    }
                    break;
                }

                if (_menuLevel != MenuLevel.Level0 && userChoice == "m")
                {
                    //Console.Clear();
                    break;
                }

                if (_menuLevel == MenuLevel.Level2Plus && userChoice == "r")
                {
                    //Console.Clear();
                    break;
                }
            } while (true);

            return userChoice;
        }

        public static void WriteWithColor(string text, string color, bool newLine = false)
        {
            color = color.ToLower();
            if(Enum.TryParse(char.ToUpper(color[0]) + color.Substring(1), out ConsoleColor cColor))
            {
                Console.ForegroundColor = cColor;
                if(!newLine)
                {
                    Console.Write($"{text} ");
                }
                else
                {
                    Console.WriteLine($"{text} ");
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                if(!newLine)
                {
                    Console.Write($"{text} ");
                }
                else
                {
                    Console.WriteLine($"{text} ");
                }
            }
        }
    }
}