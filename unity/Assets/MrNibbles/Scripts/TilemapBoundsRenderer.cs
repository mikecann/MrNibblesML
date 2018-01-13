using MrNibblesML;
using UnityEngine;

namespace MrNibbles
{
    [RequireComponent(typeof(TilesController))]
    [ExecuteInEditMode]
    public class TilemapBoundsRenderer : MonoBehaviour
    {
#if UNITY_EDITOR
        private static Texture2D gizmoBg;
        private static MrNibblesAgent agent;
        void OnDrawGizmos()
        {
            if (gizmoBg == null)
                gizmoBg = new Texture2D(128, 128);

            if (agent == null)
                agent = GameObject.FindObjectOfType<MrNibblesAgent>();

            if (agent == null)
                return;

            var tiles = GetComponent<TilesController>().GetTiles(agent.tileBoundsToIncludeInState);

            var style = new GUIStyle();
            style.normal.textColor = new Color(0.1f, 0.1f, 0.1f, 1f);
            style.normal.background = gizmoBg;

            foreach (var tile in tiles)
                UnityEditor.Handles.Label(new Vector3(tile.position.x + 0.5f, tile.position.y + 0.5f, 0), ""+tile.type, style);
        }
#endif
    }
}