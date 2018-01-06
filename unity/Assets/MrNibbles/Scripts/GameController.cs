using System;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MrNibblesML
{
    public class GameController : MonoBehaviour
    {
        public Animator animator;
        public GameObject[] levels;

        private PlayerPlatformerController _player;

        void Awake()
        {
            _player = FindObjectOfType<PlayerPlatformerController>();

            CurrentLevel = GetActiveLevel();
            SpawnPlayer(CurrentLevel);
        }

        public void RestartLevel()
        {
            SpawnPlayer(CurrentLevel);
        }

        public void ChangeToNextLevel()
        {
            CurrentLevel.SetActive(false);
            CurrentLevel = GetNextLevel();
            CurrentLevel.SetActive(true);
            SpawnPlayer(CurrentLevel);
        }

        private void SpawnPlayer(GameObject level)
        {
            var spawnPoint = level.GetComponentInChildren<PlayerSpawnPoint>();
            _player.transform.position = spawnPoint.transform.position;
            _player.Reset();
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
            return levels[Random.Range(0, levels.Length)];
        }

        public GameObject CurrentLevel { get; private set; }
    }
}