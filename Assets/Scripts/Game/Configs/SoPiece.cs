using Core.ValueObjects;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "Piece", menuName = "Piece", order = 0)]
    public class SoPiece : ScriptableObject, IPiece
    {
        [SerializeField] private int type;
        [SerializeField] private Sprite sprite;
        [SerializeField] private GameObject prefabView;

        public int Type => type;

        public Sprite Icon => sprite;

        public GameObject PrefabView => prefabView;
    }
}