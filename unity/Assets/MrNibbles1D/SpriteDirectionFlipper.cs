using UnityEngine;

namespace MrNibbles1D
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteDirectionFlipper : MonoBehaviour
    {
        private float _lastPosition;
        private SpriteRenderer _renderer;

        void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _lastPosition = transform.position.x;
        }

        void FixedUpdate()
        {
            var delta = transform.position.x - _lastPosition;
            _renderer.flipX = delta > 0;
            _lastPosition = transform.position.x;
        }
    }
}