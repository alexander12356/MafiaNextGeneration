using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration.CardManagerClasses.CardClasses
{
    public class HuntPoliceman : BaseCard
    {
        public float KillChance;

        public override void StartEffect()
        {
            base.StartEffect();

            PersonManager.Instance.PolicemanKilledChance += KillChance;
        }

        public override void StopEffect()
        {
            base.StopEffect();

            PersonManager.Instance.PolicemanKilledChance -= KillChance;
        }
    }
}