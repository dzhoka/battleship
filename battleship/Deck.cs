namespace battleship
{
    public class Deck
    {
        readonly Coordinates coordinates;
        State state;

        public Deck(Coordinates coordinates)
        {
            this.coordinates = coordinates;
            state = State.afloat;
        }

        public Coordinates GetCoordinates()
        {
            return coordinates;
        }

        public State GetState()
        {
            return state;
        }

        public void SetState(State state)
        {
            this.state = state;
        }
    }
}
