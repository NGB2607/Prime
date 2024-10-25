namespace SnakeMaze
{
    class Sprite
    {
        //PreMade SnakeMaze Sprites
        public readonly string[] grString = new string[5]
        {
            "╔══════╗                               ▄███████▄  ",
            "║  ══  ║╔═════╗╔═════╗╔═╗ ╔═╗╔═════╗   ▀▀    ▀██  ",
            "║  ╔═══╗║  ═  ║║  ╔╗ ║║ ║ ║ ║║  ═  ║        ▄██▀  ",
            "║  ╚═  ║║  ═══╝║  ╚╝ ║║ ╚═╝ ║║  ╔══╝      ▄██▀   ",
            "╚══════╝╚══╝╚═╝╚═════╝╚═════╝╚══╝      ▄███████▀ "
        };
        public readonly string[] pmString = new string[5]
        {
           "╔═════╗",
           "║  ╔══╝╔═╗ ╔═╗ ╔═════╗╔═╗ ╔═╗╔═════╗    ╔═╗  ╔═╗╔═════╗╔═════╗╔═════╗",
           "║  ╚══╗║ ╚╗╣ ║ ║  ═  ║║ ╚╝ / ║  ═  ║    ║ ╚╗╔╝ ║║  ═  ║╚══  / ║  ═  ║",
          @"╔═══  ║║ ╠╚╗ ║ ║ ╔═╗ ║║ ╔╗ \ ║  ═══╝    ║ ╠╚╝╣ ║║ ╔═╗ ║ / ═══╗║  ═══╝",
           "╚═════╝╚═╝ ╚═╝ ╚═╝ ╚═╝╚═╝ ╚═╝╚═════╝    ╚═╝  ╚═╝╚═╝ ╚═╝╚═════╝╚═════╝"
        };

        public readonly string[] playString = new string[4]
        {
            "╔══════╗╔══╗  ╔═════╗╔╗   ╔╗",
           @"║   ═  ║║  ║  ║  ═  ║╚╗\ /╔╝",
            "║  ╔═══╝║  ╚═╗║ ╔═╗ ║ ╚╗ ╔╝",
            "╚══╝    ╚════╝╚═╝ ╚═╝  ╚═╝",
        };

        public readonly string[] quitString = new string[5]
        {
            "╔═════╗╔═╗ ╔═╗╔══╗╔═════╗",
            "║ ╔═╗ ║║ ║ ║ ║║  ║╚═╗ ╔═╝",
            "║ ╚═╝ ║║ ╚═╝ ║║  ║  ║ ║",
            "╚═══╗ ║╚═════╝╚══╝  ╚═╝",
            "    ╚═╝"
        };

        public readonly string[] sString1 = new string[4]
        {
            "▄@███▄   ▄█0██0▄  ▄██0██▄",
            "||   0█  ██   ██  0█   █0",
            "     █0  █0   0█  ██   0█",
            "     ▀█0██▀   ▀0██0▀   ▀0▄"
        };
        public readonly string[] SString2 = new string[4]
        {
            "  ▄0██0▄   ▄█0██0▄  ▄██0█@▄",
            "  █0   0█  ██   ██  0█   ||",
            "  0█   █0  █0   0█  ██",
            " ▄▀0    ▀█0██▀  ▀0██0▀"
        };
        public readonly string[] GString = new string[]
         { "  ________________",
          @" //              \\",
           "||  GIVE ME EGGS! ||",
           "||                ||",
          @" \\______________//"
         };
        public readonly string[] mcString1 = new string[]
        {
            "______________",
            "| |    _____ |",
            "| |__  |__ | |",
            "| ___|   | | |",
            "| |______| | |",
            "|___________@|"
        };
        public readonly string[] mcString2 = new string[]
        {
            "______________",
            "| |_________ |",
            "|_________ | |",
            "|________| | |",
            "|  ________| |",
            "|___________@|"
        };
        public readonly string[] feString = new string[]
        {"_____"};
        public readonly string[] selectionString = new string[2]
        {
            "▄▄▄█▄",
            "▀▀▀█▀"
        };
        //Tạo một mảng chuỗi chỉ đọc mới chứa những gì chúng ta cần
        //Vẽ trên khung hình hoạt hình đầu tiên của kẻ gác
        public readonly string[] rFrame1 = new string[3]
        {
            "   @_",
            "▀▀▀▀-",
            ""
        };
        //Tạo một mảng chuỗi chỉ đọc mới chứa những gì chúng ta cần
        //Vẽ trên khung hình hoạt hình đầu tiên của kẻ gác
        public readonly string[] rFrame2 = new string[3]
        {
            "   @/",
           @"▀▀▀▀\",
            ""
        };
        //Tạo một mảng chuỗi chỉ đọc mới chứa những gì chúng ta cần
        //Vẽ trên khung hình hoạt hình đầu tiên của kẻ gác
        public readonly string[] lFrame1 = new string[3]
        {
            "_@",
            "-▀▀▀▀",
            ""
        };
        //Tạo một mảng chuỗi chỉ đọc mới chứa những gì chúng ta cần
        //Vẽ trên khung hình hoạt hình đầu tiên của kẻ gác
        public readonly string[] lFrame2 = new string[3]
        {
           @"\@",
            "/▀▀▀▀",
            ""
        };
        //Tạo một mảng chuỗi chỉ đọc mới chứa những gì chúng ta cần
        //Vẽ trên khung hình hoạt hình đầu tiên của kẻ gác
        public readonly string[] uFrame1 = new string[3]
        {
            "@||",
            " █",
            " █"
        };
        //Tạo một mảng chuỗi chỉ đọc mới chứa những gì chúng ta cần
        //Vẽ trên khung hình hoạt hình đầu tiên của kẻ gác
        public readonly string[] uFrame2 = new string[3]
        {
           @"@\/ ",
            " █",
            " █"
        };
        //Tạo một mảng chuỗi chỉ đọc mới chứa những gì chúng ta cần
        //Vẽ trên khung hình hoạt hình đầu tiên của kẻ gác
        public readonly string[] dFrame1 = new string[3]
        {
            " █",
            " █",
            "@||"
        };
        //Tạo một mảng chuỗi chỉ đọc mới chứa những gì chúng ta cần
        //Vẽ trên khung hình hoạt hình đầu tiên của kẻ gác
        public readonly string[] dFrame2 = new string[3]
        {
            " █",
            " █",
           @"@/\"
        };
        //Tạo một mảng chuỗi chỉ đọc mới chứa những gì chúng ta cần
        //Vẽ trên khung hình hoạt hình đầu tiên của con số
        public readonly string[] zero = new string[3]
        {
            "╔╗",
            "║║",
            "╚╝"
        };
        public readonly string[] one = new string[3]
        {
            " ╗",
            " ║",
            " ╩"
        };
        public readonly string[] two = new string[3]
        {
            "╔╗",
            "╔╝",
            "╚╝"
        };
        public readonly string[] tree = new string[3]
        {
            "╔╗",
            " ╣",
            "╚╝"
        };
        public readonly string[] four = new string[3]
        {
            "╗╗",
            "╚╣",
            " ╩"
        };
        public readonly string[] five = new string[3]
        {
            "╔╗",
            "╚╗",
            "╚╝"
        };
        public readonly string[] six = new string[3]
        {
            "╔╗",
            "╠╗",
            "╚╝"
        };
        public readonly string[] seven = new string[3]
        {
            "╔╗",
            " ║",
            " ╩"
        };
        public readonly string[] eight = new string[3]
        {
            "╔╗",
            "╠╣",
            "╚╝"
        };
        public readonly string[] nine = new string[3]
        {
            "╔╗",
            "╚╣",
            " ╩"
        };
    }
}
