using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MrNibbles1D
{
    public class MrNibblesAgent : Agent
    {
        private const int MoveLeft = 0;
        private const int MoveRight = 1;

        public int NibblesCollected { get; private set; }
        public int Deaths { get; private set; }

        public Transform minimumBounds;
        public Transform maximumBounds;
        public Nibble nibble;

        public float speed = 0.05f;

        private Vector3 _startingPostition;
        private float _lastAction;

        void Awake()
        {
            _startingPostition = transform.position;
            _lastAction = 0;
        }

        public override List<float> CollectState()
        {
            return new List<float> { transform.position.x, nibble.transform.position.x };
        }

        public override void AgentStep(float[] action)
        {
            if (action.Is(MoveLeft))
                Move(-speed);
            else if (action.Is(MoveRight))
                Move(speed);

            if (OutOfBounds())
                AgentFailed();
            else if (HaveReachedTarget())
                AgentSucceed();
            else
            {
                reward -= 0.01f;
                if (_lastAction != action[0])
                    reward -= 0.5f;

                _lastAction = action[0];
            }
                
        }

        private bool HaveReachedTarget()
        {
            return Mathf.Abs(nibble.transform.position.x - transform.position.x) <= speed;
        }

        private void Move(float delta)
        {
            transform.Translate(delta, 0, 0);
        }

        private void AgentFailed()
        {
            reward = -10f;
            done = true;
            Deaths++;
        }

        private void AgentSucceed()
        {
            reward = 5;
            done = true;
            NibblesCollected++;
        }

        private bool OutOfBounds()
        {
            return transform.position.x <= minimumBounds.position.x || 
                transform.position.x >= maximumBounds.position.x;
        }

        public override void AgentReset()
        {
            nibble.Respawn();
            transform.position = _startingPostition;
        }
    }
}

