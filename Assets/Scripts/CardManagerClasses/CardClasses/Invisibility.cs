using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration.CardManagerClasses.CardClasses
{
    public class Invisibility : BaseCard
    {
        public float InvisibilityCoeff;

        public override void StartEffect()
        {
            base.StartEffect();

            PersonManager.Instance.Invisibility += InvisibilityCoeff;
        }

        public override void StopEffect()
        {
            base.StopEffect();

            PersonManager.Instance.Invisibility -= InvisibilityCoeff;
        }
    }
}