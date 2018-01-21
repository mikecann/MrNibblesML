using MrNibblesML;
using UnityEngine;

namespace MrNibbles
{
    [ExecuteInEditMode]
    public class TilemapBoundsRenderer : MonoBehaviour
    {
        public MrNibblesAgent agent;

#if UNITY_EDITOR
        private static Texture2D gizmoBg;
        private static TilesController.TileInfo[] tiles;
        private static TilesController tilesController;
        void OnDrawGizmos()
        {
            if (gizmoBg == null)
                gizmoBg = new Texture2D(128, 128);

            if (agent == null)
                return;

            if (tilesController == null || !tilesController.gameObject.activeSelf)
                tilesController = FindObjectOfType<TilesController>();

            if (tilesController == null)
                return;

            tiles = CreateNewTilesArray();

            var bounds = tilesController.GetBoundsAround(agent.transform, agent.tilesAroundNibblesW, 
                agent.tilesAroundNibblesH);
            tilesController.GetTilesAround(bounds, tiles);

            var style = new GUIStyle();
            style.normal.textColor = new Color(0.1f, 0.1f, 0.1f, 1f);
            style.normal.background = gizmoBg;

            foreach (var tile in tiles)
                UnityEditor.Handles.Label(new Vector3(tile.position.x + 0.5f, tile.position.y + 0.5f, 0), ""+tile.type, style);
        }

        private TilesController.TileInfo[] CreateNewTilesArray()
        {
            return new TilesController.TileInfo[agent.tilesAroundNibblesW * agent.tilesAroundNibblesH];
        }
#endif
    }
}