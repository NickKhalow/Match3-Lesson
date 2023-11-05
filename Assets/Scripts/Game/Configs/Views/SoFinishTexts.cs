#nullable enable

using UnityEngine;

namespace Game.Configs.Views
{
    [CreateAssetMenu(fileName = "Finish Texts", menuName = "Finish Texts", order = 0)]
    public class SoFinishTexts : ScriptableObject
    {
        [SerializeField] private string lose = string.Empty;
        [SerializeField] private string win = string.Empty;

        public string Lose => lose;
        public string Win => win;
    }
}