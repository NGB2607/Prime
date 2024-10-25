using System;
using System.Collections.Concurrent;
using System.Threading;

namespace SnakeMaze
{
    //1.Lớp này có trách nhiệm đọc và ghi lại mọi lần nhấn phím của người chơi:

    class KeyReader
    {
        //Khai báo một BlockingCollection mới để lưu tất cả các lần nhấn ConsoleKey
        private BlockingCollection<ConsoleKey> keyQueue;
        //Lưu đầu vào cuối cùng dưới dạng chuỗi
        public string Input = "";
        //Khai báo một luồng mới
        private Thread thread1;
        //Khai báo một luồng mới
        private Thread thread2;

        //2.Cấu trúc keyReader:

        public KeyReader()
        {
            thread1 = new Thread(GetInput); //Khởi tạo "thread1"
            thread2 = new Thread(ReadFromQueue); //Khởi tạo "thread2"

            keyQueue = new BlockingCollection<ConsoleKey>(); //Khởi tạo "keyQueue"
            thread1.Start(); //Thống kê "thread1"
            thread2.Start(); //Thống kê "thread2"
        }

        //3.Nhận Input từ người chơi:

        private void GetInput()
        {
            ConsoleKey pressedKey;
            do
            {
                pressedKey = Console.ReadKey(true).Key;
                keyQueue.Add(pressedKey); //Thêm "pressedKey" vào "keyQueue"
            } while (true);
        }

        //4.Đọc đầu vào từ BockingCollection "keyQueue":

        private void ReadFromQueue()
        {
            ConsoleKey currentKey; //Khai báo biến "currentKey"

            while (true)
            { // While "true" Do...
                //Đặt khóa hiện tại bằng với "pressedKey" được lấy từ "keyQueue"
                currentKey = keyQueue.Take();

                if (currentKey == ConsoleKey.Enter) //Hỏi xem "currentKey" có phải là Enter không
                    // If so...
                    Input = "Enter";
                //Hỏi xem "currentKey" là W hay UpArrow
                if (currentKey == ConsoleKey.W || currentKey == ConsoleKey.UpArrow)
                    // If so...
                    Input = "Up";
                //Hỏi xem "currentKey" là A hay LeftArrow
                if (currentKey == ConsoleKey.A || currentKey == ConsoleKey.LeftArrow)
                    // If so...
                    Input = "Left";
                //Hỏi xem "currentKey" là S hay DownArrow
                if (currentKey == ConsoleKey.S || currentKey == ConsoleKey.DownArrow)
                    // If so...
                    Input = "Down";
                //Hỏi xem "currentKey" là D hay RightArrow
                if (currentKey == ConsoleKey.D || currentKey == ConsoleKey.RightArrow)
                    // If so...
                    Input = "Right";
                if (currentKey == ConsoleKey.Q)
                    Input = "Sound";
                if (currentKey == ConsoleKey.H)
                    Input = "History";
                if (currentKey == ConsoleKey.R)
                    Input = "Return";
            }
        }
    }
}
