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

        private PlayerPlatformerController _platformController;
        private GameController _game;
        private GameObject _currentLevel;
        private IEnumerable<LevelTilemap.LevelTile> _tiles;
        private GameObject _exitPoint;

        void Awake()
        {
            _platformController = GetComponent<PlayerPlatformerController>();
            _game = FindObjectOfType<GameController>();
        }

        public override List<float> CollectState()
        {
            if (_currentLevel != _game.CurrentLevel)
                UpdateStateConstants();

            var state = new List<float>();
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
            state.Add(_platformController.IsGrounded ? 1 : 0);
            state.Add(_platformController.enabled ? 1 : 0);

            return state;
        }

        private void UpdateStateConstants()
        {
            _tiles = _game.CurrentLevel.GetComponentInChildren<LevelTilemap>()
                .GetTiles(new BoundsInt(-25,-25,0,50,50,1));

            _exitPoint = _game.CurrentLevel.GetComponentInChildren<ExitLevelTrigger>()
                .gameObject;

            _currentLevel = _game.CurrentLevel;
        }

        public override void AgentStep(float[] actions)
        {
            var nothing = (int) actions[None] == 1;
            var moveLeft = (int) actions[MoveLeft] == 1;
            var moveRight = (int) actions[MoveRight] == 1;
            var jump = (int)actions[Jump] == 1;

            MrNibblesInput.Jump = false;
            MrNibblesInput.HorizontalAxis = 0;

            if (moveLeft)
                MrNibblesInput.HorizontalAxis = -1f;
            if (moveRight)
                MrNibblesInput.HorizontalAxis = 1f;
            if (jump)
                MrNibblesInput.Jump = true;
        }

        public override void AgentReset()
        {
            base.AgentReset();
        }
    }
}