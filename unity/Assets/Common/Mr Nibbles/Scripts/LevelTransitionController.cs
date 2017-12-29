using System;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MrNibblesML
{
    public class LevelTransitionController : MonoBehaviour
    {
        public Animator animator;
        public CinemachineConfiner virtualCameraConfiner;

        private PlayerPlatformerController _player;

        void Awake()
        {
            _player = FindObjectOfType<PlayerPlatformerController>();

            var level = GetActiveLevel();
            SetCameraBounds(level);
            SpawnPlayer(level);
        }

        public void BeginTransition()
        {
            _player.gameObject.SetActive(false);
            animator.SetTrigger("transition");
        }

        public void AnimationMidpointReached()
        {
            DisableLastLevel();

            var nextLevel = GetNextLevel();
            nextLevel.SetActive(true);

            SetCameraBounds(nextLevel);

            SpawnPlayer(nextLevel);
        }

        private void SetCameraBounds(GameObject level)
        {
            var bounds = level.GetComponentInChildren<CameraBounds>();
            virtualCameraConfiner.m_BoundingShape2D =
                bounds.GetComponent<PolygonCollider2D>();
        }

        private void SpawnPlayer(GameObject level)
        {
            _player.gameObject.SetActive(true);

            level.GetComponentInChildren<PlayerSpawnPoint>()
                .SpawnPlayer();
        }

        private void DisableLastLevel()
        {
            GetActiveLevel().SetActive(false);
        }

        private GameObject GetActiveLevel()
        {
            for (var i = 0; i < transform.childCount; i++)
                if (transform.GetChild(i).gameObject.activeSelf)
                    return transform.GetChild(i).gameObject;

            throw new Exception("No currently active level");
        }

        private GameObject GetNextLevel()
        {
            return transform.GetChild(Random.Range(0, transform.childCount))
                .gameObject;
        }
    }
}