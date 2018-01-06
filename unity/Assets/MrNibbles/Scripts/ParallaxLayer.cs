using UnityEngine;

namespace MrNibblesML
{
    public class ParallaxLayer : MonoBehaviour
    {
        public float magnitude = 0.5f;

        private Vector3 _last;

        void Start()
        {
            _last = Camera.main.transform.position;
        }

        void Update()
        {
            var now = Camera.main.transform.position;
            var delta = now - _last;
            transform.position = transform.position - (delta * magnitude);
            _last = now;
        }
    }
}