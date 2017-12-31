using UnityEngine;
using UnityEngine.Tilemaps;

namespace MrNibblesML
{
    public class ChangeLevelTrigger : MonoBehaviour
    {
        void OnTriggerEnter2D()
        {
            FindObjectOfType<GameController>()
                .BeginTransition();
        }
    }
}