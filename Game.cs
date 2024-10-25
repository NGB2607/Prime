using System;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using NAudio.Wave;


namespace SnakeMaze
//Tạo vòng lặo cho game và thực hiện các tác nhân trò chơi
{
    class Game
    {
        //1.Khai báo
        public Stopwatch sw;      
        //Khai báo một cấp độ mới
        private Level level;
        //Khai báo Snake mới
        private Snake snake;
        //Khai báo nút lệnh thực hiện 
        private KeyReader kR;
        public string playerName;
        static Dictionary<string, TimeSpan> playerRecords = new Dictionary<string, TimeSpan>();
       

        //2.Hàm cấu trúc Game:

        public Game(KeyReader kr)
        {
            Console.Clear();
           
           
            //Khởi tạo Level mới
            level = new Level();
           
           
            //Khởi tạo rắc với các thuộc tính đã cho
            snake = new Snake(68, 21, Direction.Right); 
            kR = kr;
            //Gọi hàm lặp Game
            GameLoop();
        }

        //3.Hàm lặp Game:

        public void GameLoop()
        {
            while (true)
            {
                Console.SetCursorPosition(60, 32);
                Console.WriteLine("Nhập tên người chơi:");
                playerName = Console.ReadLine();
                sw = new Stopwatch();
                sw.Start();
                while (snake.Health != 0)
                { //Chạy vòng lặp cho đến khi Snake hết mạng
                    double elapsedTime = sw.Elapsed.TotalSeconds;
                    int time = Convert.ToInt32(elapsedTime);

                    Console.SetCursorPosition(0, 31);
                    snake.Plot(); //"Plot" Vẽ rắn 
                                  //Gọi hàm Update của Level
                    level.Update(time, snake.Health, snake.NLevel);

                    //Đọc Input của người chơi cho hướng đi của Snake
                    ReadInput();

                    Thread.Sleep(25); //Tạm dừng khoảng thời gian 25 mili giây

                    snake.UnPlot(); //"UnPlot" Xóa hình Snake

                    snake.Update(); //Gọi hàm Update cho Snake



                    if (snake.WinCondition())
                    { //Nếu Snake thắng
                        sw.Stop();
                        TimeSpan playTime = sw.Elapsed; // Lấy thời gian đã trôi qua
                        Console.Clear();
                        Console.SetCursorPosition(63, 12);
                        Console.Write("No quá đi cảm ơn bạn!!!");
                        SaveRecord(playerName, playTime); // Lưu kỷ lục
                        SaveRecordsToTextFile();
                        ReadRecordsFromFile();
                        DisplayRecords();
                    }
                }
                Console.Clear(); //Xóa màn hình Console
            }
        }


        //4.Hàm đọc Input người chơi để di chuyển Snake:

        private void ReadInput()
        {
            if (kR.Input == "Return")
            {
                Console.Clear();
                Menu menu = new Menu();
                Console.SetCursorPosition(60, 32);
                Console.WriteLine("Nhập tên người chơi:");
                Console.WriteLine("Ấn R + Enter để quay lại Menu");
                playerName = Console.ReadLine();
            }
            switch (kR.Input)
            { //Đọc dữ liệu đầu vào
                case "Up": //Nếu như đi Lên thì xem coi Snake có đang thuộc trường hợp None hoặc Xuống không 
                    if (snake.direction == Direction.None || snake.direction == Direction.Down)
                        snake.direction = Direction.Up; //Nếu như vậy thì chuyển nó Lên
                    snake.nextDirection = Direction.Up; //Ấn mũi tên lên để di chuyển Lên 
                    break;
                case "Right": //Nếu như đi Phải thì xem coi Snake có đang thuộc trường hợp None hoặc Trái không
                    if (snake.direction == Direction.None || snake.direction == Direction.Left)
                        snake.direction = Direction.Right; //Nếu như vậy thì chuyển nó sang phải
                    snake.nextDirection = Direction.Right; //Ấn mũi tên sang phải để di chuyển sang Phải 
                    break;
                case "Left": //Nếu như đi Trái thì xem coi Snake có đang thuộc trường hợp None hoặc Phải không
                    if (snake.direction == Direction.None || snake.direction == Direction.Right)
                        snake.direction = Direction.Left; //Nếu như vậy thì chuyển nó sang trái
                    snake.nextDirection = Direction.Left; //Ấn mũi tên sang trái để di chuyển sang Trái 
                    break;
                case "Down": //Nếu như đi Xuống thì xem coi Snake có đang thuộc trường hợp None hoặc Lên không
                    if (snake.direction == Direction.None || snake.direction == Direction.Up)
                        snake.direction = Direction.Down; //Nếu như vậy thì chuyển nó Xuống
                    snake.nextDirection = Direction.Down; //Ấn mũi tên xuống để di chuyển Xuống 
                    break;

            }
        }
        // Hàm lưu kỷ lục của từng người chơi
        static void SaveRecord(string playerName, TimeSpan playTime)
        {
            if (playerRecords.ContainsKey(playerName))
            {
                // Nếu người chơi đã có kỷ lục trước đó, so sánh và lưu thời gian tốt nhất
                if (playTime < playerRecords[playerName])
                {
                    playerRecords[playerName] = playTime;
                }
            }
            else
            {
                // Nếu người chơi chưa có kỷ lục, lưu thời gian
                playerRecords.Add(playerName, playTime);
            }
        }

        // Hàm lưu tất cả kỷ lục vào file text
        static void SaveRecordsToTextFile()
        {
            string filePath = @"C:\Users\baong\OneDrive\Desktop\ATSH\Thử\binbin\bin\Release\KL.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var record in playerRecords)
                {
                    writer.WriteLine($"{record.Key}: {record.Value.TotalSeconds} giây");
                }
            }
        }
        // Hàm đọc kỷ lục từ file
        static void ReadRecordsFromFile()
        {
            string filePath = @"C:\Users\baong\OneDrive\Desktop\ATSH\Thử\binbin\bin\Release\KL.txt";

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length == 2)
                        {
                            string playerName = parts[0].Trim();
                            if (TimeSpan.TryParse(parts[1].Trim().Split(' ')[0], out TimeSpan playTime))
                            {
                                playerRecords[playerName] = playTime; // Lưu vào dictionary
                            }
                        }
                    }
                }
                Console.SetCursorPosition(62, 13);
                Console.WriteLine("Kỷ lục đã được lưu vào file.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File không tồn tại. Chưa có kỷ lục nào được lưu.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đọc file: {ex.Message}");
            }
        }
        // Hàm hiển thị kỷ lục
        public static void DisplayRecords()
        {
            if (playerRecords.Count == 0)
            {
                Console.WriteLine("Chưa có kỷ lục nào.");
            }
            else
            {
                Console.SetCursorPosition(63, 22);
                Console.WriteLine("Danh sách kỷ lục:");
                foreach (var record in playerRecords)
                {
                    Console.SetCursorPosition(58, 24);
                    Console.WriteLine($"Người chơi: {record.Key}, Thời gian chơi: {record.Value.TotalSeconds} giây");
                }
            }
        }
    }
}