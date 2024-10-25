using SnakeMaze;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Xml.Linq;
using System.Media;


namespace SnakeMaze
{
    //1.Class chịu trách nhiệm cho các tác vụ liên quan đến Snake:

    class Snake
    {
        //Khai báo animation
        private int animation;
        //Khai báo thời gian di chuyển
        private int speedTimer;
        //Khai báo số trứng nhặt được
        public int totalEggs;
        //Khai báo thời gian animation
        private int animationTimer;
        //Khai báo hướng di chuyển
        public Direction direction;
        //Khai báo hướng di chuyển tiếp theo
        public Direction nextDirection;
        //Khai báo tốc độ di chuyển
        private readonly int moveSpeed;
        //Khai báo tốc độ animation
        private readonly int animationSpeed;
        //Khai báo biến Snake Dead (True or False)
        public bool IsDead { get; set; }
        //Khai báo trứng
        public int eggs { get; set; }
        //Khai báo trục X của vị trí Snake
        public int X { get; private set; }
        //Khai báo trục Y của vị trí Snake
        public int Y { get; private set; }
        //Khai báo số mạng của Snake
        public int Health { get; private set; }
        //Khai báo Level hiện tại
        public int NLevel { get; private set; }
        //Khai báo Dictionary để chứa 2 sprites
        private Dictionary<int, string[]> pac;
        //Tạo một Sprite mới
        private Sprite sp = new Sprite();
        //Khai báo sự kiến đặc biệt
        public event Action EatSpecialPoints;
        //Khai báo sự kiện đặc biệt
        public event Action Died;
        SoundPlayer player = new SoundPlayer(@"C:\Users\baong\OneDrive\Desktop\ATSH\Thử\binbin\1.wav");

        //2.Cấu trúc Rắn:

        public Snake(int X, int Y, Direction direction)
        {
            this.X = X; //Thiết lập trục X = tọa độ X đã nhận
            this.Y = Y; //Thiết lập trục Y = tọa độ Y đã nhận
            //Thêm Dictionary để thêm 2 sprites
            pac = new Dictionary<int, string[]>
            {
                [0] = sp.rFrame1,
                [1] = sp.rFrame2
            };
            animation = 0; //Đặt animation = 0
            animationTimer = 0; //Đặt animationTimer = 0
            animationSpeed = 4; //Đặt animationSpeed = 0
            this.direction = direction; //Đặt hướng di chuyển bằng với hướng di chuyển nhận được

            speedTimer = 0; //Đặt speedTimer = 0
            moveSpeed = 1; //Đặt moveSpeed = 1

            Health = 3;  //Đặt Health = 3

            totalEggs = 48; //Đặt totalEggs = 48

            NLevel = 1; //Đặt Level hiện tại = 1
        }

        //3.Hàm vẽ Snake:

        public void Plot()
        {
           if (totalEggs != 0)
            {
                animationTimer++; //Tăng animationTimer lên 1 đơn vị
                                  //Kiểm tra xem "animationTimer" có bằng "animationSpeed" không
                if (animationTimer == animationSpeed)
                {
                    //Nếu đúng
                    animation = animation == 0 ? 1 : 0; //Chuyển đổi animation
                    animationTimer = 0; //Tạo mới animationTimer
                }

                //Chuyển ForegroundColor sang DarkYellow
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                for (int i = 0; i < 3; i++)
                { //Lặp lại 3 lần để hiển thị đúng sprite mong muốn
                    Console.SetCursorPosition(X, Y + i); //Đặt vị trí con trỏ
                    Console.WriteLine(pac[animation][i]); //Vẽ nó lên Console
                }
            }
        }

        //4.hàm xóa vị trí trước đó của Snake
        public void UnPlot()
        {
            for (int i = 0; i < 3; i++)
            { //Lặp lại 3 lần để làm sạch sprite đúng cách
                Console.SetCursorPosition(X, Y + i); //Đặt vị trí con trỏ
                Console.WriteLine("     "); //Viết nó lên Console
            }
        }

        //5.Hàm kiểm tra xem có chuyển động nào có sẵn đến nơi người chơi muốn di chuyển không
        private void FixDirection()
        {
            switch (nextDirection)
            { //Kiểm tra hướng mong muốn tiếp theo
                case Direction.Up: //Nếu Lên
                    //Kiểm tra xem có bức tường nào ở trên không
                    if (!Level.WallCollider[X, Y - 1] && !Level.WallCollider[X + 4, Y - 1])
                    {
                        direction = nextDirection; //Di chuyển tiếp
                        nextDirection = Direction.None; //Đặt hướng di chuyển tiếp theo về None
                    }
                    break;
                case Direction.Down: //Nếu Xuống
                    //Kiểm tra xem có bức tường nào ở dưới không
                    if (!Level.WallCollider[X, Y + 3] && !Level.WallCollider[X + 4, Y + 3])
                    {
                        direction = nextDirection; //Di chuyển tiếp
                        nextDirection = Direction.None; //Đặt hướng di chuyển tiếp theo về None
                    }
                    break;
                case Direction.Left: //Nếu sang Trái
                    //Kiểm tra xem có bức tường nào bên trái không
                    if (!Level.WallCollider[X - 1, Y] && !Level.WallCollider[X - 1, Y + 2])
                    {
                        direction = nextDirection; //Di chuyển tiếp
                        nextDirection = Direction.None; //Đặt hướng di chuyển tiếp theo về None
                    }
                    break;
                case Direction.Right: //Nếu sang Phải
                    //Kiểm tra xem có bức tường nào bên Phải không
                    if (!Level.WallCollider[X + 5, Y] && !Level.WallCollider[X + 5, Y + 2])
                    {
                        direction = nextDirection; //Di chuyển tiếp
                        nextDirection = Direction.None; //Đặt hướng di chuyển tiếp theo về None
                    }
                    break;
            }
        }

        //6.Hàm Update:
        public void Update()
        {
            CheckPointsCollision(); //Gọi hàm CheckPointsCollision
            Move(); //Gọi hàm Move
        }

        //7.Hàm kiểm tra xem có va chạm với bất kỳ điểm nào không:
        private void CheckPointsCollision()
        {
            //Lặp qua Points Collider để kiểm tra xem chúng ta có va chạm với một trong số chúng không
            for (int i = 0; i < Level.y; i++)
            {
                for (int u = 0; u < Level.x; u++)
                {
                    if (Level.EggsCollider[u, i] != default(char))
                    {
                        if (((X + 1 == u || X + 3 == u) && Y + 1 == i) ||
                            ((Y == i || Y + 2 == i) && X + 2 == u))
                        {
                            player.Load();
                            player.Play();
                            eggs += 1; //Tăng trứng
                            totalEggs--; //Giảm số trứng còn lại
                            //Xóa điểm "ate" khỏi mảng
                            Level.EggsCollider[u, i] = default(char);
                            
                        }
                    }
                }
            }
        }

        //8.Hàm di chuyển
        private void Move()
        {
            speedTimer++; //Tăng speedTimer lên 1 đơn vị 
            FixDirection(); //Gọi hàm FixDirection
            CheckToroidal(); //Gọi hàm CheckToroidal

            //Kiểm tra xem speedTimer có bằng moveSpeed ​​không
            if (speedTimer == moveSpeed)
            {
                //Nếu đúng
                speedTimer = 0; //Tạo mới speedTimer

                switch (direction)
                { //Kiểm tra hướng hiện tại
                    case Direction.Up: //Nếu Lên
                        //Kiểm tra xem chúng ta có va chạm với bức tường không
                        if (!Level.WallCollider[X, Y - 1] && !Level.WallCollider[X + 4, Y - 1])
                        {
                            //Nếu có
                            Y--; //Giảm Y 1 đơn vị
                            //Chuyển đổi sprite trên từ điển sang sprite di chuyển Lên
                            pac[0] = sp.uFrame1;
                            pac[1] = sp.uFrame2;
                        }
                        else
                            //Nếu không
                            direction = Direction.None; //Đổi hướng thành None
                        break;
                    case Direction.Down: //Nếu Xuống
                        //Kiểm tra xem chúng ta có va chạm với bức tường không
                        if (!Level.WallCollider[X, Y + 3] && !Level.WallCollider[X + 4, Y + 3])
                        {
                            Y++; //Tăng y lên 1 đơn vị
                            //Chuyển đổi sprite trên từ điển sang sprite di chuyển Xuống
                            pac[0] = sp.dFrame1;
                            pac[1] = sp.dFrame2;
                        }
                        else
                            //Nếu không
                            direction = Direction.None; //Đổi hướng thành None
                        break;
                    case Direction.Left: //Nếu di chuyển sang Trái.
                        //Kiểm tra xem chúng ta có va chạm với bức tường không
                        if (!Level.WallCollider[X - 1, Y] && !Level.WallCollider[X - 1, Y + 2])
                        {
                            X--; //Giảm X đi 1 đơn vị
                            //Chuyển đổi sprite trên từ điển sang sprite di chuyển sang Trái
                            pac[0] = sp.lFrame1;
                            pac[1] = sp.lFrame2;
                        }
                        else
                            // Else...
                            direction = Direction.None; //Đổi hướng thành None
                        break;
                    case Direction.Right: //Nếu sang Phải
                        //Kiểm tra xem chúng ta có va chạm với bức tường không
                        if (!Level.WallCollider[X + 5, Y] && !Level.WallCollider[X + 5, Y + 2])
                        {
                            X++; //Tăng X lên 1 đơn vị
                            //Chuyển đổi sprite trên từ điển sang sprite di chuyển sang Phải
                            pac[0] = sp.rFrame1;
                            pac[1] = sp.rFrame2;
                        }
                        else
                            // Else...
                            direction = Direction.None; //Đổi hướng thành None
                        break;

                }
            }
        }

        //9.Hàm kiểm tra vị trí của người chơi để có thể di chuyển hình xuyến
        private void CheckToroidal()
        {
            //Kiểm tra xem có chuyển động hình xuyến không
            if (X == 16 && Y == 17 ||
                X == 120 && Y == 17)
            {
                //Nếu đúng
                X = direction == Direction.Right ? 16 : 120; //Dịch chuyển đến phía đối diện
            }
        }


        //11.Hàm điều kiện thắng
        public bool WinCondition()
        {
            if (totalEggs == 0)
            {
            
                return true; //Trả về True
            }
            return false; //Trả về False
        }
    }
}
