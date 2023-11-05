#nullable enable

using System;
using Core.Conditions.LevelConditions;
using Core.Utils.NullSafety;
using Cysharp.Threading.Tasks;
using Game.Configs.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class FinishView : MonoBehaviour
    {

        [SerializeField] private Text text = null!;
        [SerializeField] private SoFinishTexts finishTexts = null!;
        [SerializeField] private Button restartButton = null!;


        private ILevelConditions levelConditions = null!;

        public void Initialize(ILevelConditions levelConditions)
        {
            text.EnsureNotNull();
            finishTexts.EnsureNotNull();
            restartButton.EnsureNotNull();

            gameObject.SetActive(false);

            this.levelConditions = levelConditions;
        }

        public void Show()
        {
            gameObject.SetActive(true);

            if (levelConditions.IsWin())
            {
                text.text = finishTexts.Win;
                return;
            }

            if (levelConditions.IsLose())
            {
                text.text = finishTexts.Lose;
                return;
            }

            throw new Exception("Level is not finished");
        }

        public async UniTask WaitRestart()
        {
            var ready = false;

            void Callback()
            {
                ready = true;
            }

            restartButton.onClick.AddListener(Callback);
            await UniTask.WaitUntil(() => ready);
            restartButton.onClick.RemoveListener(Callback);
        }
    }
}