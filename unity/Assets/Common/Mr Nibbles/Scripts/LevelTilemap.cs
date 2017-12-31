using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MrNibblesML
{
    public class LevelTilemap : MonoBehaviour
    {
        public TileBase pipe;
        public TileBase spider;

        private Tilemap _tilemap;

        void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
        }

        public IEnumerable<LevelTile> GetTiles(BoundsInt bounds)
        {
            var state = new List<LevelTile>();

            for (int ix = 0; ix < bounds.size.x; ix++)
            {
                for (int iy = 0; iy < bounds.size.y; iy++)
                {
                    var pos = new Vector3Int(ix + bounds.xMin, iy+ bounds.yMin, 0);
                    var tile = _tilemap.GetTile(pos);
                    var type = 0;

                    if (tile == pipe)
                        type = 1;
                    else if (tile == spider)
                        type = 2;

                    state.Add(new LevelTile
                    {
                        position = new Vector2(ix,iy),
                        type = type
                    });
                }
            }

            return state;
        }

        public class LevelTile
        {
            public Vector2 position;
            public int type;
        }
    }
}