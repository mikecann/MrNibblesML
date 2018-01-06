using UnityEngine;

namespace MrNibblesML
{
    public class ExitLevelTrigger : EnterPipeTrigger
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