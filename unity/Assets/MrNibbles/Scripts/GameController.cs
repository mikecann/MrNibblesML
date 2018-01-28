using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MrNibblesML
{
    public class GameController : MonoBehaviour
    {
        public Animator animator;
        public int maxLevel = 1;

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
            var range = GetLevels().Take(maxLevel+1).ToArray();
            return range[Random.Range(0, range.Length)];
        }

        private GameObject[] GetLevels()
        {
            var levels = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
                levels.Add(transform.GetChild(i).gameObject);
            return levels.ToArray();
        }

        public GameObject CurrentLevel { get; private set; }
    }
}