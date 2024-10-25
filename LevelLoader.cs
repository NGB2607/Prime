using System.IO;

namespace SnakeMaze
{
    //1.Lớp này chịu trách nhiệm tải cấp độ bằng các tệp đã cho:

    class LevelLoader
    {
        //Tạo một chuỗi rỗng mới
        private readonly string path = @"C:\Users\baong\OneDrive\Desktop\ATSH\Thử\binbin\bin\Debug\";
        //Tạo một chuỗi mới với tên tệp
        private readonly string level = "Map.txt";
        //Tạo một chuỗi mới với tên tệp
        private readonly string eggs = "Eggs.txt";
        //Tạo một mảng chuỗi mới sẽ lưu thông tin về cấp độ Walls
        public string[] LevelSprite { get; private set; } = new string[31];
        //Tạo một mảng chuỗi mới sẽ lưu thông tin LevelEggs
        public string[] LevelEggs { get; private set; } = new string[31];

        //2.LevelLoader Constructor thực thi một số phương thức cần thiết khi được gọi:

        public LevelLoader()
        {

            LoadLevel(level); //Tải cấp độ bằng cách sử dụng chuỗi cấp độ làm đường dẫn
            LoadLevel(eggs); //Tải cấp độ bằng cách sử dụng chuỗi điểm làm đường dẫn
        }

        //3.Tải nội dung vào mảng chuỗi tùy thuộc vào tệp nào được truyền:

        private void LoadLevel(string file)
        {
            //Khởi tạo StreamReader mới để đọc từ tệp mong muốn
            using (StreamReader sr = new StreamReader(path + file))
            {
                string line; //Tạo một chuỗi mới
                //Hỏi xem ngôi nhà đầu tiên trên mảng LevelSprite có phải là null không
                if (LevelSprite[0] == null)
                {
                    //Nếu vậy, thực hiện vòng lặp for để truyền toàn bộ thông tin từ tệp...
                    for (int i = 0; (line = sr.ReadLine()) != null; i++)
                    {
                        LevelSprite[i] = line; //...vào một ngôi nhà cụ thể của mảng
                    }
                }
                else
                {
                    //Nếu không, thực hiện thao tác tương tự nhưng lưu lại toàn bộ thông tin...
                    for (int i = 0; (line = sr.ReadLine()) != null; i++)
                    {
                        LevelEggs[i] = line; //...vào mảng LevelPoints
                    }
                }
            }
        }
    }
}
