using UnityEngine;

namespace MrNibblesML
{
    public class SpiderMap : MonoBehaviour
    {
        void OnTriggerEnter2D()
        {
            var game = FindObjectOfType<GameController>();
            game.RestartLevel();
        }
    }
}