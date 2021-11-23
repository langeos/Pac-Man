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
                CentercursorX(longest);
                Console.SetCursorPosition(Console.CursorLeft, Console.BufferHeight / 2 - elements.Length / 2);

                //Color rows especially currently selected one.
                for (int i = 0; i < elements.Length; i++)
                {
                    CentercursorX(longest);
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
                HideCursor();

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
        /// Center cursor horizontally
        /// </summary>
        static public void CentercursorX()
        {
            Console.SetCursorPosition(Console.BufferWidth / 2, Console.CursorTop);
        }

        /// <summary>
        /// Center cursor vertically
        /// </summary>
        static public void CentercursorY()
        {
            Console.SetCursorPosition(Console.CursorLeft, Console.WindowHeight / 2);
        }

        /// <summary>
        /// Set cursor to place allowing center written text 
        /// </summary>
        /// <param name="lenght">Lenght of string</param>
        static public void CentercursorX(int lenght)
        {
            Console.SetCursorPosition(Console.BufferWidth / 2 - lenght / 2, Console.CursorTop);
        }


        /// <summary>
        /// Erase currend area in cursor position
        /// </summary>
        static public void Clear_Current_Area(int x_axis,int y_axis)
        {
            Console.SetCursorPosition(x_axis, y_axis);
            Console.Write(" ");
            if (OperatingSystem.IsMacOS()) HideCursor();
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
            CentercursorX(title.Length);
            Console.WriteLine(title);
            if (OperatingSystem.IsMacOS()) HideCursor();
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
            CentercursorX(title.Length);
            Console.WriteLine(title);
            if (OperatingSystem.IsMacOS()) HideCursor();

        }

        /// <summary>
        /// Set cursor one line lower and write centered text.
        /// </summary>
        /// <param name="lenght">Lenght of string</param>
        static public void WriteCenterOneLineLower(string str)
        {
            int lenght = str.Length;
            Console.SetCursorPosition(Console.BufferWidth / 2 - lenght / 2, Console.CursorTop+1);
            Console.Write(str);
        }

        /// <summary>
        /// For MacOS system, place Cursor in the console corner
        /// </summary>
        static public void HideCursor()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }

    }
}
