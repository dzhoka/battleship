namespace battleship
{
    public class Shot
    {
        readonly Coordinates coords;
        readonly Result result;

        public Shot(Coordinates coords, Result result)
        {
            this.coords = coords;
            this.result = result;
        }

        public Coordinates GetCoords() { return coords; }
        public Result GetResult() { return result; }
    }
}
