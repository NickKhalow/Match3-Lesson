#nullable enable

using Core.PieceSpawners;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "Piece Spawner", menuName = "Piece Spawner", order = 0)]
    public class SoPieceSpawner : ScriptableObject, IPieceSpawner
    {
        [SerializeField] private SoPieceSet set = null!;

        public int CreateBasicPiece()
        {
            return set.Pieces[Random.Range(0, set.Pieces.Count)]!.Type;
        }
    }
}