#nullable enable

using Core.Boards;
using Core.ValueObjects;
using UnityEngine;

namespace Game.Views
{

    public class ViewCamera : MonoBehaviour
    {
        [SerializeField] private Camera cam = null!;

        public void CenterTo(IReadOnlyBoard board)
        {
            //some magic centring, should be refactored
            cam.transform.position = new Vector3((board.Width - 1) * 0.5f, -(board.Height - 1) * 0.5f);
        }

        public BoardPos ScreenPosToLogicPos(float x, float y)
        {
            var worldPos = cam.ScreenToWorldPoint(new Vector3(x, y, -cam.transform.position.z));
            var boardSpace = transform.InverseTransformPoint(worldPos);

            return new BoardPos(
                Mathf.RoundToInt(boardSpace.x),
                -Mathf.RoundToInt(boardSpace.y)
            );
        }
    }
}