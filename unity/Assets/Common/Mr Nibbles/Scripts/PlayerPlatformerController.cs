using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrNibblesML
{
    public class PlayerPlatformerController : PhysicsObject
    {
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;

        private SpriteRenderer spriteRenderer;
        private Animator animator;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void ComputeVelocity()
        {
            Vector2 move = Vector2.zero;

            move.x = MrNibblesInput.HorizontalAxis;

            if (MrNibblesInput.Jump && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }
            else if (!MrNibblesInput.Jump)
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }

            if (move.x < -0.01f)
            {
                if (spriteRenderer.flipX == true)
                {
                    spriteRenderer.flipX = false;
                }
            }
            else if (move.x > 0.01f)
            {
                if (spriteRenderer.flipX == false)
                {
                    spriteRenderer.flipX = true;
                }
            }

            animator.SetBool("grounded", grounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }
    }
}