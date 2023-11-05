namespace Core.ValueObjects
{

    public class Piece : IPiece
    {

        public int Type { get; } //as immutable for safety

        public Piece(int type)
        {
            Type = type;
        }

        public override string ToString()
        {
            return string.Format("(type:{0})", Type);
        }

    }

}