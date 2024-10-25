using System;
using System.Threading;
using NAudio.Wave;

namespace SnakeMaze
{
    class Program
    {
        static AudioFileReader backgroundMusicReader;
        static WaveOutEvent backgroundMusicPlayer;
        static bool isMuted = false;
        static float originalVolume = 0.6f;  // Lưu trữ âm lượng ban đầu

        static void Main(string[] args)
        {
            // Tạo và phát nhạc nền theo vòng lặp
            string backgroundMusicPath = @"C:\Users\baong\OneDrive\Desktop\ATSH\Thử\binbin\2.wav";
            PlayBackgroundMusicLoop(backgroundMusicPath);
            // Duy trì chương trình đang chạy
            while (true)
            {

                Thread.Sleep(100);  // Chờ 100ms để tránh tiêu tốn tài nguyên CPU
            } 
        }
        public static void PlayBackgroundMusicLoop(string backgroundMusicPath)
        {
            Thread backgroundMusicThread = new Thread(() =>
            {
                
             
                    while (true)  // Lặp vô hạn
                    {
                        // Tạo đối tượng để đọc file nhạc nền
                        backgroundMusicReader = new AudioFileReader(backgroundMusicPath);

                        // Tạo đối tượng để phát nhạc nền
                        backgroundMusicPlayer = new WaveOutEvent();
                        backgroundMusicPlayer.Init(backgroundMusicReader);

                        // Đặt âm lượng cho nhạc nền
                        backgroundMusicReader.Volume = 0.6f;

                        // Phát nhạc nền
                        backgroundMusicPlayer.Play();

                        // Chờ đến khi nhạc nền phát xong
                        while (backgroundMusicPlayer.PlaybackState == PlaybackState.Playing)
                        {
                            Thread.Sleep(500); // Kiểm tra sau mỗi 500ms
                        }

                        // Giải phóng tài nguyên khi nhạc nền phát xong
                        backgroundMusicReader.Dispose();
                        backgroundMusicPlayer.Dispose();
                    }
                
             
            });

            // Khởi động luồng phát nhạc nền
            backgroundMusicThread.IsBackground = true;
            backgroundMusicThread.Start();
            Console.CursorVisible = false; //Ẩn con trỏ
            Console.SetWindowSize(142, 32); //Thiết lập khung màn hình cho Console
            Menu menu = new Menu(); //Tạo Menu mới
        }
        public static void ToggleSound()
        {
            Console.Clear();
            // Đảo ngược trạng thái âm thanh
            Program.isMuted = !Program.isMuted;

            if (Program.isMuted)
            {
                Program.backgroundMusicPlayer.Volume = 0.0f;  // Tắt âm lượng
            }
            else
            {
                Program.backgroundMusicPlayer.Volume = Program.originalVolume;  // Khôi phục âm lượng ban đầu
            }
            Console.ReadKey(true); // Đợi người dùng nhấn phím bất kỳ để quay lại menu
            Console.Clear();
            Menu menu = new Menu();
        }

    }
}


