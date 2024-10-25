using SnakeMaze;
using System;
using System.IO;
using System.Threading;


namespace SnakeMaze
{
    /// <summary>
    /// This Class is responsible for Displaying the menu and letting the player sellect an option
    /// </summary>
    class Menu
    {
        /** \brief Creates a new empty string */
        private readonly string path = @"C:\Users\baong\OneDrive\Desktop\ATSH\Thử\binbin\bin\Debug\";
        /** \brief Creates a new string with the file name */
        private readonly string menu = "Menu.txt";
        /** \brief Saves the Y position for the Selection sprite */
        private int selectionY;
        /** \brief Knows if have the play button selected */
        private bool playSelected;
        /** \brief Knows if have the play button selected */
        public string[] MenuSprite { get; private set; } = new string[49];
        /** \brief Declares a new "KeyReader" */
        KeyReader kR;
        /** \brief Declares a new "Sprite" */
        Sprite sp;

        /// <summary>
        /// Menu constructor
        /// </summary>
        public Menu()
        {

            sp = new Sprite(); // Iniciates the "Sprite" "sp"
            kR = new KeyReader(); // Iniciates the "KeyReader" "kR"

            selectionY = 11; // Set the value of "selectionY" to 11
            playSelected = true; // Set the value of "playSelected" to true
            LoadMenu(); // Calls the LoadMenu method
            RenderMenu(); // Calls the RenderMenu method

        }

        /// <summary>
        /// Reads the user input to select on of the options from the menu
        /// </summary>
        private void GetInput()
        {
            do
            {
                switch (kR.Input)
                {
                    case "Down": // Di chuyển xuống
                        if (playSelected)
                        {
                            playSelected = false;
                            selectionY = 18;
                            Console.Clear();
                            RenderMenu();
                        }
                        break;
                    case "Up": // Di chuyển lên
                        if (!playSelected)
                        {
                            playSelected = true;
                            selectionY = 11;
                            Console.Clear();
                            RenderMenu();
                        }
                        break;
                    case "Enter": // Bắt đầu trò chơi hoặc thoát
                        if (playSelected)
                        {
                            Game game = new Game(kR); // Bắt đầu game
                        }
                        else
                        {
                            Environment.Exit(0); // Thoát chương trình
                        }
                        break;
                    case "Sound": // Phím Q để bật/tắt âm thanh
                        Program.ToggleSound(); // Gọi phương thức để bật/tắt âm thanh
                        Console.WriteLine("Ấn phím bất kỳ để trở về");
                        break;
                    case "History": // Phím H để xem lịch sử
                        Console.SetCursorPosition(65, 32);
                        Game.DisplayRecords(); // Gọi phương thức để hiển thị lịch sử
                        Console.WriteLine("Ấn phím bất kỳ để trở về");
                        break;
                }
            } while (true); // Vòng lặp vô hạn để liên tục nhận đầu vào
        }


        /// <summary>
        /// Loads the menu from a specific file
        /// </summary>
        private void LoadMenu()
        {
            // Iniciates a new StreamReader to read from the wanted file
            using (StreamReader sr = new StreamReader(path + menu))
            {
                string line; // Creates a new string
                // Executes a for loop to pass all the information from the file...
                for (int i = 0; (line = sr.ReadLine()) != null; i++)
                {
                    MenuSprite[i] = line; // ...into a specific house of the array
                }
            }
        }

        /// <summary>
        /// Renders the Menu
        /// </summary>
        public void RenderMenu()
        {

            // Display Level Borders
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < MenuSprite.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(MenuSprite[i]);
            }

            Console.SetCursorPosition(45, 31);
            Console.WriteLine("Ấn Q để tắt âm và H để xem lịch sử và Enter");
            // Display Snake Logo
            for (int i = 0; i < sp.grString.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(45, 2 + i);
                Console.Write(sp.grString[i]);
            }
            for (int i = 0; i < sp.pmString.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.SetCursorPosition(33, 24 + i);
                Console.Write(sp.pmString[i]);
            }

            // Display Play
            for (int i = 0; i < sp.playString.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(55, 10 + i);
                Console.Write(sp.playString[i]);
            }

            // Display Quit
            for (int i = 0; i < sp.quitString.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(55, 16 + i);
                Console.Write(sp.quitString[i]);
            }

            for (int i = 0; i < sp.sString1.Length; i++)
            {
                Console.SetCursorPosition(110, 17 + i);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(sp.sString1[i]);
            }
            for (int i = 0; i < sp.SString2.Length; i++)
            {
                Console.SetCursorPosition(3, 17 + i);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(sp.SString2[i]);
            }
            for (int i = 0; i < sp.GString.Length; i++)
            {
                Console.SetCursorPosition(110, 11 + i);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(sp.GString[i]);
            }
            for (int i = 0; i < sp.GString.Length; i++)
            {
                Console.SetCursorPosition(6, 11 + i);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(sp.GString[i]);
            }
            for (int i = 0; i < sp.mcString1.Length; i++)
            {
                Console.SetCursorPosition(115, 23 + i);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(sp.mcString1[i]);
            }
            for (int i = 0; i < sp.mcString2.Length; i++)
            {
                Console.SetCursorPosition(10, 23 + i);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(sp.mcString2[i]);
            }
            // Display Selection Square
            for (int i = 0; i < sp.selectionString.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(45, selectionY + i);
                Console.Write(sp.selectionString[i]);
            }
            
            // Ask for input
            GetInput();
        }
    }
}
