using System.Collections;
using System.Collections.Generic;
using MrNibblesML;
using UnityEngine;

namespace MrNibbles
{
    public class MrNbiblesAcademy : Academy
    {
        public GameController gameController;

        public override void AcademyReset()
        {
            gameController.maxLevel = (int)resetParameters["max_level"];
        }

      
    }
}