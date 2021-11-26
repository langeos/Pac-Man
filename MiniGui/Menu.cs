using System;

namespace MiniGui
{
    public class Menu
    {

        private string[] elements = Array.Empty<string>();
        private int longest = 0;

        /// <summary>
        /// Configure menu by giving elements to show.
        /// </summary>
        /// <param name="elementyMenu"></param>
        public void Configure(string[] MenuElement)
        {
            if (MenuElement != null)
            {
                elements = MenuElement;
            }

            for (int i = 0; i < MenuElement.Length; i++)
            {
                if (MenuElement[i] is null)
                {
                    continue;
                }
                if (MenuElement[i].Length > longest)
                {
                    longest = MenuElement[i].Length;
                }

            }

        }

        /// <summary>
        /// Open and show entire menu (erasing your screen) and returning number of selected option.
        /// </summary>
        /// <returns></returns>
        public int Open()
        {
            Console.CursorVisible = false;
            Console.Clear();
            int selected = 0;
            ConsoleKeyInfo key;

            do
            {
                //Center cursor
                BetterCursor.CentercursorX(longest);
                Console.SetCursorPosition(Console.CursorLeft, Console.WindowHeight / 2 - elements.Length / 2);

                //Color rows especially currently selected one.
                for (int i = 0; i < elements.Length; i++)
                {
                    BetterCursor.CentercursorX(longest);
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    }

                    //Justify labels
                    Console.WriteLine(elements[i].PadRight(longest));

                }
                BetterCursor.HideCursor();

                //Read key from user to change selected option.
                key = Console.ReadKey(true);

                //Ifs with protection to change selected option.
                if (key.Key == ConsoleKey.UpArrow && selected != 0)
                {
                    selected -= 1;
                }
                else if (key.Key == ConsoleKey.UpArrow && selected == 0)
                {
                    selected = elements.Length - 1;
                }
                else if (key.Key == ConsoleKey.DownArrow && selected != elements.Length - 1)
                {
                    selected += 1;
                }
                else if (key.Key == ConsoleKey.DownArrow && selected == elements.Length - 1)
                {
                    selected = 0;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    selected = -1;
                }

                Console.ResetColor();
            } while (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Escape);

            
            Console.ResetColor();
            Console.CursorVisible = true;
            return selected;
        }


        /// <summary>
        /// Makes heading at the top of console window.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static public void Heading(string str)
        {
            Console.Clear();
            string title = ($"----------------{str}----------------");
            BetterCursor.CentercursorX(title.Length);
            Console.WriteLine(title);
            if (OperatingSystem.IsMacOS()) BetterCursor.HideCursor();
        }

        /// <summary>
        /// Makes heading at given row.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="vertical"></param>
        static public void Heading(string str, int vertical)
        {
            Console.Clear();
            Console.SetCursorPosition(Console.CursorLeft, vertical);
            string title = ($"----------------{str}----------------");
            BetterCursor.CentercursorX(title.Length);
            Console.WriteLine(title);
            if (OperatingSystem.IsMacOS()) BetterCursor.HideCursor();

        }

        

    }
}
