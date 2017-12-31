using MrNibblesML;
using UnityEngine;
using UnityEngine.UI;

namespace MrNibbles
{
    public class Hud : MonoBehaviour
    {
        public Text text;

        private MrNibblesAgent _agent;
        private GameController _game;

        void Awake()
        {
            _agent = FindObjectOfType<MrNibblesAgent>();
            _game = FindObjectOfType<GameController>();
        }

        public void Update()
        {
            var str = $"Cumulative Reward: {_agent.CumulativeReward}\n";
            str += $"Deaths: {_game.Deaths}\n";
            str += $"Wins: {_game.Wins}\n";

            text.text = str;
        }
    }
}