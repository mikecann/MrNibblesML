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

        public void Reset()
        {
            IsTriggered = false;
        }

        //private Collider2D _collider;

        //protected override void OnTriggerEnter2D()
        //{
        //    FindObjectOfType<GameController>()
        //        .ChangeToNextLevel();
        //}

        //void Awake()
        //{
        //    _collider = GetComponent<Collider2D>();
        //}

        //public bool IsTriggered(PlayerPlatformerController player)
        //{
        //    return _collider.OverlapPoint(new Vector2(player.transform.position.x, player.transform.position.y));
        //}
    }
}