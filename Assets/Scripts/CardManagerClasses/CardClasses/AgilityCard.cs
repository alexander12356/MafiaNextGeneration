﻿using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration.CardManagerClasses.CardClasses
{
    public class AgilityCard : BaseCard
    {
        public float Chance;

        public override void StartEffect()
        {
            base.StartEffect();
            PersonManager.Instance.ChangeMutationChance(PersonType.MafiaKillerAgility, Chance);
        }

        public override void StopEffect()
        {
            base.StopEffect();
            PersonManager.Instance.ChangeMutationChance(PersonType.MafiaKillerAgility, -Chance);
        }
    }
}