using System;
using System.Collections.Generic;
using System.Linq;
using MrNibblesML;
using UnityEngine;

namespace MrNibbles
{
    public class MrNibblesAgent : Agent
    {
        public const int None = 0;
        public const int MoveLeft = 1;
        public const int MoveRight = 2;
        public const int Jump = 3;
        public const int JumpAndMoveRight = 4;
        public const int JumpAndMoveLeft = 5;

        public int tilesAroundNibblesW = 5;
        public int tilesAroundNibblesH = 5;

        private PlayerPlatformerController _platformController;
        private GameController _game;
        private TilesController.TileInfo[] _tiles;
        private ExitLevelTrigger _exitPoint;
        private SpiderMap _spiders;
        private TilesController _tilesController;

        void Awake()
        {
            _game = FindObjectOfType<GameController>();
            _platformController = GetComponent<PlayerPlatformerController>();
            _tilesController = _game.CurrentLevel.GetComponentInChildren<TilesController>();
            _tiles = new TilesController.TileInfo[tilesAroundNibblesW *
                tilesAroundNibblesH]; 
        }

        public override List<float> CollectState()
        {
            var state = new List<float>();

            CollectSurroundingTiles();

            foreach (var tile in _tiles)
            {
                // Providing positions relative to the player
                state.Add(_platformController.transform.position.x - tile.position.x);
                state.Add(_platformController.transform.position.y - tile.position.y);
                state.Add(tile.type);
            }

            // Provide positions relative to the player
            state.Add(_platformController.transform.position.x - _exitPoint.transform.position.x);
            state.Add(_platformController.transform.position.y - _exitPoint.transform.position.y);

            state.Add(_platformController.transform.position.x);
            state.Add(_platformController.transform.position.y);
            state.Add(_platformController.Velocity.x);
            state.Add(_platformController.Velocity.y);
            state.Add(_platformController.IsGrounded ? 1 : 0);

            return state;
        }

        private void CollectSurroundingTiles()
        {
            var bounds = _tilesController.GetBoundsAround(transform, tilesAroundNibblesW, tilesAroundNibblesH);
            _tilesController.GetTilesAround(bounds, _tiles);
        }

        public override void AgentStep(float[] actions)
        {
            PerformActions(actions);
            UpdateRewards();
            HandleSessionTooLong();
        }

        private void PerformActions(float[] actions)
        {
            var action = actions[0];
            var isJumping = false;
            var hozMove = 0f;

            if (action == MoveLeft)
                hozMove = -1f;
            else if (action == MoveRight)
                hozMove = 1f;
            else if (action == Jump)
                isJumping = true;
            else if (action == JumpAndMoveRight)
            {
                isJumping = true;
                hozMove = 1f;
            }
            else if (action == JumpAndMoveLeft)
            {
                isJumping = true;
                hozMove = -1f;
            }

            _platformController.Tick(hozMove, isJumping);
        }

        private void UpdateRewards()
        {
            if (_exitPoint.IsTriggered)
            {
                Wins++;
                reward = 5;
                done = true;
            }
            else if (_spiders.IsTriggered)
            {
                Deaths++;
                reward = -5;
                done = true;
            }
            else
            {
                reward = -0.01f;
            }
        }

        private void HandleSessionTooLong()
        {
            if (CumulativeReward < -100)
            {
                reward = -1;
                Deaths++;
                done = true;
            }
        }

        public override void AgentReset()
        {
            _game.ChangeToNextLevel();
            _tilesController = _game.CurrentLevel.GetComponentInChildren<TilesController>();
            _exitPoint = _game.CurrentLevel.GetComponentInChildren<ExitLevelTrigger>();
            _spiders = FindObjectOfType<SpiderMap>();
        }

        public int Deaths { get; set; }
        public int Wins { get; set; }
    }
}