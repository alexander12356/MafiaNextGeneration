using System;
using UnityEngine;

namespace MafiaNextGeneration.PersonSystemClasses.PersonBehaviorClasses
{
    public class Policeman : BaseBehavior
    {
        private Transform m_TargetTransform;

        protected override void Start()
        {
            base.Start();
            BehaviorType = "Policeman";
        }

        public override void UpdateBehavior()
        {
            switch (CurrentState)
            {
                case State.Patrol:
                    PatrolUpdate();
                    break;
                case State.Hunting:
                    HuntingUpdate();
                    break;
            }
        }

        private void HuntingUpdate()
        {
            transform.position = Vector2.MoveTowards(transform.position, m_TargetTransform.position, Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<BaseBehavior>().BehaviorType == "Mafia")
            {
                SetState(State.Hunting);
                m_TargetTransform = collision.transform;
            }
        }
    }
}