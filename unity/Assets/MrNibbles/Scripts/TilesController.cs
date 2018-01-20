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

            for (var ix = bounds.xMin; ix < bounds.xMax; ix++)
            {
                for (var iy = bounds.yMin; iy < bounds.yMax; iy++)
                {
                    var pos = new Vector3Int(ix, iy, 0);
                    var pipemapTile = pipemap.GetTile(pos);
                    var spidermapTile = spidermap.GetTile(pos);
                    var type = 0;

                    if (pipemapTile)
                        type = 1;
                    else if (spidermapTile)
                        type = 2;

                    state.Add(new TileInfo
                    {
                        position = new Vector2(pos.x, pos.y),
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