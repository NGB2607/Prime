using System;
using System.Security.Policy;

namespace SnakeMaze
{
    //1.Lớp này có trách nhiệm hiển thị cấp độ:

    class Level
    {
        //Tạo một LevelLoader mới
        LevelLoader loader = new LevelLoader();
        //Tạo một Sprite mới
        Sprite sprite = new Sprite();
        //Tạo một chuỗi mới và đặt giá trị của nó bằng với "loader.LevelSprite"
        private string[] LevelSprite => loader.LevelSprite;
        //Tạo một chuỗi mới và đặt giá trị của nó bằng "loader.LevelEggs"
        private string[] EggsSprite => loader.LevelEggs;
        
        //Bốn biến tĩnh cần được truy cập từ các lớp khác
        //Level x size
        public static readonly int x = 141;
        //brief Level y size
        public static readonly int y = 31;
        //Giữ vị trí của tất cả các máy va chạm tường
        public static bool[,] WallCollider { get; private set; } = new bool[x, y];
        //Giữ vị trí của tất cả các điểm va chạm
        public static char[,] EggsCollider { get; private set; } = new char[x, y];
        //Đếm các khung hình cần bỏ qua khi hiển thị các Trứng
        private int otherFrame = 0;

        //2.Cấu trúc Level:

        public Level()
        {
            GetCollider(); //Gọi phương thức "GetCollider"
            RenderLevel(); //Gọi phương thức "RenderLevel"
        }

        //3.Lấy vị trí của tất cả các bộ va chạm và lưu chúng vào mảng:

        public void GetCollider()
        {
            //Lặp lại sprite cấp độ để lấy vị trí của mọi bức tường
            for (int i = 0; i < LevelSprite.Length; i++)
            {
                for (int u = 0; u < LevelSprite[i].Length; u++)
                {
                    if (LevelSprite[i][u] != ' ')
                    {
                        //Lưu từng vị trí tường vào mảng "WallCollider"
                        WallCollider[u, i] = true;
                    }
                }
            }

            //Lặp lại sprite cấp độ để lấy vị trí của mọi Trứng
            for (int i = 0; i < LevelSprite.Length; i++)
            {
                if (EggsSprite[i] != null)
                {
                    for (int u = 0; u < EggsSprite[i].Length; u++)
                    {
                        if (EggsSprite[i][u] != ' ')
                        {
                            //Lưu từng vị trí điểm vào mảng "EggsCollider"
                            EggsCollider[u, i] = EggsSprite[i][u] == '▄' ? '▄' : '█';
                        }
                    }
                }
            }
        }

        //4.Hàm Update của Level:

        public void Update(int eggs, int lives, int level)
        {
            RenderUi(eggs, lives, level); //Gọi phương thức "RenderUi" cung cấp 3 thuộc tính
            //Bỏ qua 9 khung hình hiển thị trứng hiển thị lại vào lần thứ 10
            if (otherFrame == 0)
            {
                otherFrame += 10;
                RenderEggs(); //Gọi phương thức "RenderEggs"
            }
            otherFrame--;
        }

        //5.Hàm Render UI:

        public void RenderUi(int eggs, int lives, int level)
        {
            //Đặt ForegroundColor thành DarkYellow
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            //Tạo một chuỗi mới và truyền vào đó số trứng
            string score = Convert.ToString(eggs);
            //Lặp lại trong khi độ dài điểm khác với 7
            while (score.Length != 7)
            {
                score = "0" + score; //Thêm 0 vào bên trái ở mỗi vòng lặp
            }
            Console.SetCursorPosition(67, 15);
                 //Đặt vị trí con trỏ
            Console.Write("Your Record"); //Viết vào bảng điều khiển
            //Lặp lại chuỗi Điểm để hiển thị phiên bản "cao hơn" của chuỗi đó trên màn hình
            for (int i = 0, u = 0; i < score.Length; i++, u += 2)
            {
                for (int a = 0; a < 3; a++)
                {
                    //Thay đổi vị trí con trỏ khi cần thiết
                    Console.SetCursorPosition(66 + u, 17 + a);
                    //Kiểm tra số lượng cần thiết để ghim
                    switch (score[i])
                    {
                        case '0':
                            Console.Write(sprite.zero[a]); //Ghi nó vào bảng điều khiển
                            break;
                        case '1':
                            Console.Write(sprite.one[a]); //Ghi nó vào bảng điều khiển
                            break;
                        case '2':
                            Console.Write(sprite.two[a]); //Ghi nó vào bảng điều khiển
                            break;
                        case '3':
                            Console.Write(sprite.tree[a]); //Ghi nó vào bảng điều khiển
                            break;
                        case '4':
                            Console.Write(sprite.four[a]); //Ghi nó vào bảng điều khiển
                            break;
                        case '5':
                            Console.Write(sprite.five[a]); //Ghi nó vào bảng điều khiển
                            break;
                        case '6':
                            Console.Write(sprite.six[a]); //Ghi nó vào bảng điều khiển
                            break;
                        case '7':
                            Console.Write(sprite.seven[a]); //Ghi nó vào bảng điều khiển
                            break;
                        case '8':
                            Console.Write(sprite.eight[a]); //Ghi nó vào bảng điều khiển
                            break;
                        case '9':
                            Console.Write(sprite.nine[a]); //Ghi nó vào bảng điều khiển
                            break;
                    }
                }
            }
        }

        //5.Kiểm tra một số để hiển thị phiên bản "cao hơn" của nó
        private void CheckNumber(int temp, int i)
        {
            switch (temp)
            { //Kiểm tra xem đó là số nào
                //Tùy thuộc vào số lượng hiển thị một phiên bản tốt hơn của nó
                case 0:
                    Console.Write(sprite.zero[i]); //Ghi nó vào bảng điều khiển
                    break;
                case 1:
                    Console.Write(sprite.one[i]); //Ghi nó vào bảng điều khiển
                    break;
                case 2:
                    Console.Write(sprite.two[i]); //Ghi nó vào bảng điều khiển
                    break;
                case 3:
                    Console.Write(sprite.tree[i]); //Ghi nó vào bảng điều khiển
                    break;
                case 4:
                    Console.Write(sprite.four[i]); //Ghi nó vào bảng điều khiển
                    break;
                case 5:
                    Console.Write(sprite.five[i]); //Ghi nó vào bảng điều khiển
                    break;
                case 6:
                    Console.Write(sprite.six[i]); //Ghi nó vào bảng điều khiển
                    break;
                case 7:
                    Console.Write(sprite.seven[i]); //Ghi nó vào bảng điều khiển
                    break;
                case 8:
                    Console.Write(sprite.eight[i]); //Ghi nó vào bảng điều khiển
                    break;
                case 9:
                    Console.Write(sprite.nine[i]); //Ghi nó vào bảng điều khiển
                    break;
            }
        }

        //6.Hiển thị toàn bộ trứng:

        public void RenderEggs()
        {
            for (int i = 0; i < LevelSprite.Length; i++)
            {
                if (EggsSprite[i] != null)
                {
                    for (int u = 0; u < EggsSprite[i].Length; u++)
                    {
                        if (EggsCollider[u, i] != default(char))
                        {
                            if (EggsCollider[u, i] == '█')
                            {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.SetCursorPosition(u, i);
                                Console.Write(EggsCollider[u, i]);
                            }
                        }
                    }
                }
            }
        }

        public void RenderLevel()
        {
            //Tường cấp độ hiển thị
            Console.SetCursorPosition(0, 0);
            //Lặp lại số lần cần thiết để sprite hiển thị đúng
            for (int i = 0; i < LevelSprite.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(LevelSprite[i]);
            }
        }
    }
}
