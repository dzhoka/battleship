namespace battleship
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            IConsole console = new GameConsole();
            Game game = new Game(8, console);
            game.Play();
        }
    }
}
