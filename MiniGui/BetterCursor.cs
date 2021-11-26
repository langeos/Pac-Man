using System;
namespace MiniGui
{
    public static class BetterCursor
    {
        /// <summary>
        /// Set cursor one line lower and write centered text.
        /// </summary>
        /// <param name="lenght">Lenght of string</param>
        static public void WriteCenterOneLineLower(string str)
        {
            int lenght = str.Length;
            Console.SetCursorPosition(Console.WindowWidth / 2 - lenght / 2, Console.CursorTop + 1);
            Console.Write(str);
        }

        /// <summary>
        /// For MacOS system, place Cursor in the console corner
        /// </summary>
        static public void HideCursor()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }

        /// <summary>
        /// Center cursor horizontally
        /// </summary>
        static public void CentercursorX()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.CursorTop);
        }

        /// <summary>
        /// Center cursor vertically
        /// </summary>
        static public void CentercursorY()
        {
            Console.SetCursorPosition(Console.CursorLeft, Console.WindowHeight / 2);
        }

        static public void Centercursor()
        {
            CentercursorX();
            CentercursorY();
        }

        /// <summary>
        /// Set cursor to place allowing center written text 
        /// </summary>
        /// <param name="lenght">Lenght of string</param>
        static public void CentercursorX(int lenght)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - lenght / 2, Console.CursorTop);
        }


        /// <summary>
        /// Move cursor vertically up
        /// </summary>
        /// <param name="x">Number of lines</param>
        static public void CursorUp(int x)
        {
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - x);
        }

        /// <summary>
        /// Move cursor vertically down
        /// </summary>
        /// <param name="x">Number of lines</param>
        static public void CursorDown(int x)
        {
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + x);
        }


        /// <summary>
        /// Erase currend area in cursor position
        /// </summary>
        static public void Clear_Current_Area(int x_axis, int y_axis)
        {
            Console.SetCursorPosition(x_axis, y_axis);
            Console.Write(" ");
            if (OperatingSystem.IsMacOS()) HideCursor();
        }
    }
}
