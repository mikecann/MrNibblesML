using UnityEngine;

namespace MrNibblesML
{
    public class LevelTransitionController : MonoBehaviour
    {
        public Animator animator;
        private PlayerPlatformerController _player;

        public void BeginTransition()
        {
            _player = FindObjectOfType<PlayerPlatformerController>();
            _player.gameObject.SetActive(false);

            animator.SetTrigger("transition");
        }

        public void AnimationMidpointReached()
        {
            DisableLastLevel();

            var nextLevel = GetNextLevel();
            nextLevel.SetActive(true);
            RespawnPlayer(nextLevel);
        }

        private void RespawnPlayer(GameObject nextLevel)
        {
            _player.gameObject.SetActive(true);

            nextLevel.GetComponentInChildren<PlayerSpawnPoint>()
                .SpawnPlayer();
        }

        private void DisableLastLevel()
        {
            for (var i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(false);
        }

        private GameObject GetNextLevel()
        {
            return transform.GetChild(Random.Range(0, transform.childCount - 1))
                .gameObject;
        }
    }
}