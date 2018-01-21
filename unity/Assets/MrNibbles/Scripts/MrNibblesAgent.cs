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
            _tiles = new TilesController.TileInfo[tilesAroundNibblesW *
                tilesAroundNibblesH]; 
        }

        public override List<float> CollectState()
        {
            var state = new List<float>();

            // Populate the _tiles array with the tiles that surround us
            var bounds = _tilesController.GetBoundsAround(transform, tilesAroundNibblesW, tilesAroundNibblesH);
            _tilesController.GetTilesAround(bounds, _tiles);

            foreach (var tile in _tiles)
            {
                state.Add(tile.position.x);
                state.Add(tile.position.y);
                state.Add(tile.type);
            }

            state.Add(_exitPoint.transform.position.x);
            state.Add(_exitPoint.transform.position.y);

            state.Add(_platformController.transform.position.x);
            state.Add(_platformController.transform.position.y);
            state.Add(_platformController.Velocity.x);
            state.Add(_platformController.Velocity.y);

            return state;
        }

       public override void AgentStep(float[] actions)
        {
            PerformActions(actions);
            UpdateRewards();
            HandleSessionTooLong();
        }

        private void PerformActions(float[] actions)
        {
            var nothing = (int) actions[None] == 1;
            var moveLeft = (int) actions[MoveLeft] == 1;
            var moveRight = (int) actions[MoveRight] == 1;
            var jump = (int) actions[Jump] == 1;

            var isJumping = false;
            var hozMove = 0f;

            if (moveLeft)
                hozMove = -1f;
            if (moveRight)
                hozMove = 1f;
            if (jump)
                isJumping = true;

            _platformController.Tick(hozMove, isJumping);
        }

        private void UpdateRewards()
        {
            if (_exitPoint.IsTriggered)
            {
                Wins++;
                reward = 10;
                done = true;
            }
            else if (_spiders.IsTriggered)
            {
                Deaths++;
                reward = -10;
                done = true;
            }
            else
            {
                reward = -0.01f;
            }
        }

        private void HandleSessionTooLong()
        {
            if (CumulativeReward < -80)
            {
                reward = -10;
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