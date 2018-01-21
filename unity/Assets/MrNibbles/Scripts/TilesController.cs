using System.Collections;
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

        public BoundsInt GetBoundsAround(Transform transform, int width, int height)
        {
            var centre = pipemap.WorldToCell(transform.position);
            return new BoundsInt(centre.x-width/2, centre.y-height/2, 0, width, height, 1);
        }

        public void GetTilesAround(BoundsInt bounds, TileInfo[] tiles)
        {
            var xi = 0;
            for (var x = bounds.xMin; x < bounds.xMax; x++, xi++)
            {
                var yi = 0;
                for (var y = bounds.yMin; y < bounds.yMax; y++, yi++)
                {
                    var pos = new Vector3Int(x, y, 0);
                    var pipemapTile = pipemap.GetTile(pos);
                    var spidermapTile = spidermap.GetTile(pos);
                    var type = 0;

                    if (pipemapTile)
                        type = 1;
                    else if (spidermapTile)
                        type = 2;

                    var index = xi + (yi * bounds.size.x);
                    if (tiles[index]==null)
                        tiles[index] = new TileInfo();

                    tiles[index].position = new Vector2(pos.x, pos.y);
                    tiles[index].type = type;
                }
            }
        }

        public class TileInfo
        {
            public Vector2 position;
            public int type;
        }
    }
}