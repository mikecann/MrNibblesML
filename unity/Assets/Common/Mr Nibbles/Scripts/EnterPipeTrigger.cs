using UnityEngine;
using UnityEngine.Tilemaps;

namespace MrNibblesML
{
    public class EnterPipeTrigger : MonoBehaviour
    {
        protected virtual void OnTriggerEnter2D()
        {
            var tilemap = GetComponentInParent<Tilemap>();
            var cell = tilemap.WorldToCell(transform.position);

            FindObjectOfType<PlayerPlatformerController>()
                .GetComponent<PlayerInPipeController>()
                .Travel(cell, tilemap);
        }
    }
}