using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration.CardManagerClasses.CardClasses
{
    public class LegalBuisnessCard : BaseCard
    {
        public float createPersonTime;
        public float MoneyIncreaseValue;

        public override void StartEffect()
        {
            base.StartEffect();

            PersonManager.Instance.CreatePersonTime -= createPersonTime;
            MoneyManager.Instance.MoneyIncreaseValue += MoneyIncreaseValue;
        }

        public override void StopEffect()
        {
            base.StopEffect();

            PersonManager.Instance.CreatePersonTime += createPersonTime;
            MoneyManager.Instance.MoneyIncreaseValue -= MoneyIncreaseValue;
        }
    }
}