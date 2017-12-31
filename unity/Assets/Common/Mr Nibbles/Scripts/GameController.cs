using System;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MrNibblesML
{
    public class GameController : MonoBehaviour
    {
        public Animator animator;
        public CinemachineConfiner virtualCameraConfiner;

        private PlayerPlatformerController _player;

        void Awake()
        {
            _player = FindObjectOfType<PlayerPlatformerController>();

            CurrentLevel = GetActiveLevel();
            SetCameraBounds(CurrentLevel);
            SpawnPlayer(CurrentLevel);
        }

        public void BeginTransition()
        {
            _player.gameObject.SetActive(false);
            animator.SetTrigger("transition");
        }

        public void AnimationMidpointReached()
        {
            DisableLastLevel();

            CurrentLevel = GetNextLevel();
            CurrentLevel.SetActive(true);
            SetCameraBounds(CurrentLevel);
            SpawnPlayer(CurrentLevel);
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

        public GameObject CurrentLevel { get; private set; }
    }
}