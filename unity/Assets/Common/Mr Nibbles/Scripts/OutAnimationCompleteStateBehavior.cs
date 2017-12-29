using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrNibblesML
{
    public class OutAnimationCompleteStateBehavior : StateMachineBehaviour
    {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            FindObjectOfType<LevelTransitionController>()
                .AnimationMidpointReached();
        }
    }
}