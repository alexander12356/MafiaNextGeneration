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

            PersonManager.Instance.MafiaKillerBugaiMutationChance += BugaiChance;
        }

        public override void StopEffect()
        {
            base.StopEffect();

            PersonManager.Instance.MafiaKillerBugaiMutationChance -= BugaiChance;
        }
    }
}