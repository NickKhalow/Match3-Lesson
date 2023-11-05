using Core.PieceSpawners;

namespace Tests.UnitTests.Editor
{
    public class PieceSpawnerFake : IPieceSpawner {
        
        private readonly int value;
        
        public PieceSpawnerFake(int value) {
            this.value = value;
        }

        public int CreateBasicPiece() {
            return value;
        }
    }
}