using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MrNibblesML
{
    public class TilesController : MonoBehaviour
    {
        public Tilemap pipemap;
        public Tilemap spidermap;

        public IEnumerable<TileInfo> GetTiles(BoundsInt bounds)
        {
            var state = new List<TileInfo>();

            for (int ix = 0; ix < bounds.size.x; ix++)
            {
                for (int iy = 0; iy < bounds.size.y; iy++)
                {
                    var pos = new Vector3Int(ix + bounds.xMin, iy+ bounds.yMin, 0);
                    var pipemapTile = pipemap.GetTile(pos);
                    var spidermapTile = spidermap.GetTile(pos);
                    var type = 0;

                    if (pipemapTile)
                        type = 1;
                    else if (spidermapTile)
                        type = 2;

                    state.Add(new TileInfo
                    {
                        position = new Vector2(ix,iy),
                        type = type
                    });
                }
            }

            return state;
        }

        public class TileInfo
        {
            public Vector2 position;
            public int type;
        }
    }
}