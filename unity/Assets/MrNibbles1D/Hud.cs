using UnityEngine;
using UnityEngine.UI;

namespace MrNibbles1D
{
    public class Hud : MonoBehaviour
    {
        public MrNibblesAgent mrNibbles;
        public Text text;

        void FixedUpdate()
        {
            text.text = $"{mrNibbles.NibblesCollected} Nibbles Collected\n";
            text.text += $"{mrNibbles.Deaths} Deaths";
        }
    }
}