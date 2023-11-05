namespace Core.ValueObjects
{

    //rename to BoardPose or BoardPosition to be more clear
    public struct BoardPos
    {
        public int X { get; } //as immutable for safety
        public int Y { get; }

        public BoardPos(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return "BoardPos{"
                   + $"x={X}"
                   + $", y={Y}"
                   + "}";
        }
    }
}