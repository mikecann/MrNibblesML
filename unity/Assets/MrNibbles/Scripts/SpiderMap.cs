using UnityEngine;

namespace MrNibblesML
{
    public class SpiderMap : MonoBehaviour
    {
        public bool IsTriggered { get; private set; }

        void OnTriggerEnter2D()
        {
            IsTriggered = true;
        }

        void OnEnable()
        {
            IsTriggered = false;
        }
    }
}