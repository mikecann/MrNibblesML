using System.Collections;
using System.Collections.Generic;
using MrNibblesML;
using UnityEngine;

namespace MrNibbles
{
    public class MrNbiblesAcademy : Academy
    {
        public GameController gameController;
        public MrNibblesAgent agent;

        public override void AcademyReset()
        {
            var newLevel = (int) resetParameters["max_level"];
            if (newLevel > gameController.maxLevel)
            {
                agent.Wins = 0;
                agent.Deaths = 0;
            }

            gameController.maxLevel = newLevel;
        }
    }
}