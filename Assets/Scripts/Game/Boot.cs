#nullable enable
using System.Collections;
using System.Linq;
using Core.Boards;
using Core.Conditions.LevelConditions.Factory;
using Core.Features.CollectedPieces;
using Core.Features.MoveCounts;
using Core.Levels;
using Core.ResolvePipe.SpecialAbilities.Map.Factory;
using Core.Utils.NullSafety;
using Cysharp.Threading.Tasks;
using Game.Configs;
using Game.Views;
using UnityEngine;

namespace Game
{
    public class Boot : MonoBehaviour
    {

        [Header("Dependencies")]
        [SerializeField] private BoardRenderer boardRenderer;
        [SerializeField] private MovesView movesView;
        [SerializeField] private FinishView finishView;
        [SerializeField] private ObjectiveView objectiveView;

        [Header("Config")]
        [SerializeField] private SoPieceSpawner pieceSpawner;
        [SerializeField] private SoPieceSpawner rocketSpawner;
        [SerializeField] private SoSpecials specials;
        [SerializeField] private SoConditions conditions;

        private void Awake()
        {
            boardRenderer.EnsureNotNull();
            movesView.EnsureNotNull();
            finishView.EnsureNotNull();

            pieceSpawner.EnsureNotNull();
            rocketSpawner.EnsureNotNull();
            specials.EnsureNotNull();
            conditions.EnsureNotNull();
        }

        private async void Start()
        {
            while (this)
            {
                Initialize();
                await PlayCycle();
            }
        }

        private async UniTask PlayCycle()
        {
            await boardRenderer.Play();
            finishView.Show();
            await finishView.WaitRestart();
        }

        private void Initialize()
        {
            //should be extracted to config
            int[,] boardDefinition =
            {
                { 3, 3, 1, 2, 3, 3 },
                { 2, 2, 1, 2, 3, 3 },
                { 1, 1, 0, 0, 2, 2 },
                { 2, 2, 0, 0, 1, 1 },
                { 1, 1, 2, 2, 1, 1 },
                { 1, 1, 2, 2, 1, 1 },
            };

            var movesCount = new MemoryMovesCount();
            var collectedPieces = new MemoryCollectedPiecesCount();
            var conditionFactory = new LevelConditionsFactory(collectedPieces, movesCount);
            var levelConditions = conditionFactory.CreateLevelConditions(conditions, conditions);

            var abilityFactory = new AbilityPieceMapFactory();
            var abilityMap = abilityFactory.AbilityPieceMap(specials.ToList());
            var board = new Board(
                boardDefinition,
                pieceSpawner.EnsureNotNull(),
                rocketSpawner.EnsureNotNull(),
                abilityMap
            );
            ILevel level = new Level(board, levelConditions);
            level = new MovesCountLevel(level, movesCount);
            level = new CollectedPiecesCountLevel(level, collectedPieces);

            boardRenderer.Initialize(level);
            finishView.Initialize(levelConditions);
            movesView.Initialize(movesCount, conditions.MovesReachedAmount);
            objectiveView.Initialize(collectedPieces, conditions.CollectedAmount);
        }
    }
}