using UnityEngine;
using UnityEngine.Tilemaps;

namespace MrNibblesML
{
    public class PlayerSpawnPoint : EnterPipeTrigger
    {
        public void SpawnPlayer()
        {
            FindObjectOfType<PlayerPlatformerController>()
                .transform.position = transform.position;

            OnTriggerEnter2D();
        }
    }
}