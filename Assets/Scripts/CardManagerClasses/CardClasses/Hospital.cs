using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration.CardManagerClasses.CardClasses
{
    public class Hospital : BaseCard
    {
        public float DeadPersonTime;

        public override void StartEffect()
        {
            base.StartEffect();

            PersonManager.Instance.DeadPersonTime -= DeadPersonTime;
        }

        public override void StopEffect()
        {
            base.StopEffect();
            PersonManager.Instance.DeadPersonTime += DeadPersonTime;

        }
    }
}