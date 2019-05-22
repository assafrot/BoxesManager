using System;
using System.Collections.Generic;

namespace Program
{
    class Program
    {
        private static int index = 0;

        static void Main(string[] args)
        {
            UI ui = new UI();
            Console.Clear();
            List<string> menuItems = new List<string>() {
                "Update Suplly",
                "Check Suplly",
                "Get Box For Gift",
                "Remove Unused Boxes",
                "Logs",
                "Exit"
            };

            Console.CursorVisible = false;
            while (true)
            {
                string selectedMenuItem = drawMenu(menuItems);
                if (selectedMenuItem == "Update Suplly")
                {
                    Console.Clear();
                    Console.CursorVisible = true;
                    ui.UpdateSuplly();
                    GoBack();
                }
                if (selectedMenuItem == "Check Suplly")
                {
                    Console.Clear();
                    Console.CursorVisible = true;
                    ui.CheckSuplly();
                    GoBack();
                }
                if (selectedMenuItem == "Get Box For Gift")
                {
                    Console.Clear();
                    Console.CursorVisible = true;
                    ui.GetGiftBox();
                    GoBack();
                }
                if (selectedMenuItem == "Remove Unused Boxes")
                {
                    ui.RemoveUnused();
                    Console.Clear();
                }
                if (selectedMenuItem == "Logs")
                {
                    Console.Clear();
                    Console.CursorVisible = true;
                    ui.ReadLog();
                    GoBack();
                }
                else if (selectedMenuItem == "Exit")
                {
                    Environment.Exit(0);
                }
            }
        }

        private static void GoBack()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Go back");
            Console.ReadLine();
            Console.CursorVisible = false;
            Console.ResetColor();
            Console.Clear();


        }

        private static string drawMenu(List<string> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (i == index)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine(items[i]);
                }
                else
                {
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
            }

            ConsoleKeyInfo ckey = Console.ReadKey();

            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index == items.Count - 1)
                {
                    index = 0;
                }
                else { index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                    index = items.Count - 1;
                }
                else { index--; }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return items[index];
            }
            else
            {
                return "";
            }

            Console.Clear();
            return "";
        }
    }
}
