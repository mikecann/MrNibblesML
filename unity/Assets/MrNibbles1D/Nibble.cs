using UnityEngine;

namespace MrNibbles1D
{
    public class Nibble : MonoBehaviour
    {
        public Transform minPosition;
        public Transform maxPosition;

        public void Respawn()
        {
            var randX = Random.Range(minPosition.position.x, maxPosition.transform.position.x);
            transform.position = new Vector3(randX, transform.position.y, transform.position.z);
        }
    }
}