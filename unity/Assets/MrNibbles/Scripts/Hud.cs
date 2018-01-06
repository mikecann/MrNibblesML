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
            str += $"Deaths: {_agent.Deaths}\n";
            str += $"Wins: {_agent.Wins}\n";

            text.text = str;
        }
    }
}