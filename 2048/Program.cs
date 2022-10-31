using System.Reflection;
using System.Xml.Schema;

namespace _2048
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        public static void Start()
        {
            var model = new Model(4);

            while (true)
            {
                Show(model);

                switch(Console.ReadKey(false).Key)
                {
                    case ConsoleKey.UpArrow:
                        model.Up();
                        model.IsGameOver();
                        break;
                    case ConsoleKey.DownArrow:
                        model.Down();
                        model.IsGameOver();
                        break;
                    case ConsoleKey.RightArrow:
                        model.Right();
                        model.IsGameOver();
                        break;
                    case ConsoleKey.LeftArrow:
                        model.Left();
                        model.IsGameOver();
                        break;
                }
            }
        }

        public static void Show(Model model)
        {
            Console.Clear();

            for (int y = 0; y < model.size; y++)
            {
                for (int x = 0; x < model.size; x++)
                {
                    Console.SetCursorPosition(x * 5 + 5, y * 2 + 2);

                    int number = model.GetMap(x, y);
                    Console.WriteLine(number == 0 ? '.' : number.ToString() + " ");
                }
            }

            if (model.isGameOver)
            {
                Console.WriteLine("Game Over");
            }
            else
            {
                Console.WriteLine("Still play");
            }
        }
    }
}