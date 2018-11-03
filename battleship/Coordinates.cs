namespace battleship
{
    public struct Coordinates
    {
        public readonly int x;
        public readonly int y;

        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public string Image()
        {
            char l = (char)(Game.FirstLetter + x);
            char n = (char)(Game.FirstNumber + y);
            return $"{l}{n}";
        }
    }
}
