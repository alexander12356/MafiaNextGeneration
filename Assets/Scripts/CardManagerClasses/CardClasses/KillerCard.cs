using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration.CardManagerClasses.CardClasses
{
    public class KillerCard : BaseCard
    {
        public float KillerBornChance;

        public override void StartEffect()
        {
            base.StartEffect();

            PersonManager.Instance.MafiaKillerMutationChance += KillerBornChance;
        }

        public override void StopEffect()
        {
            base.StopEffect();

            PersonManager.Instance.MafiaKillerMutationChance -= KillerBornChance;
        }
    }
}