using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration.CardManagerClasses.CardClasses
{
    public class BugaiCard : BaseCard
    {
        public float BugaiChance;

        public override void StartEffect()
        {
            base.StartEffect();

            PersonManager.Instance.ChangeMutationChance(PersonType.MafiaKillerBugai, BugaiChance);
        }

        public override void StopEffect()
        {
            base.StopEffect();

            PersonManager.Instance.ChangeMutationChance(PersonType.MafiaKillerBugai, -BugaiChance);
        }
    }
}