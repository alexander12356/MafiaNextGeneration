using UnityEngine;

namespace MafiaNextGeneration.PersonSystemClasses.PersonBehaviorClasses
{
    public class Citizen : BaseBehavior
    {
        protected override void Start()
        {
            base.Start();
            BehaviorType = "Citizen";
        }

        public override void UpdateBehavior()
        {
            switch (CurrentState)
            {
                case State.Patrol:
                    PatrolUpdate();
                    break;
            }
        }
    }
}