using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration.CardManagerClasses.CardClasses
{
    public class TrueSight : BaseCard
    {
        public float Chance;

        public override void StartEffect()
        {
            base.StartEffect();

            PersonManager.Instance.ChangeBuffChance(BuffType.Invisibility, Chance);
        }

        public override void StopEffect()
        {
            base.StopEffect();

            PersonManager.Instance.ChangeBuffChance(BuffType.Invisibility, -Chance);
        }
    }
}
