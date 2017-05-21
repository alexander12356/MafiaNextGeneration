using System;
using UnityEngine;

namespace MafiaNextGeneration.PersonSystemClasses.PersonBehaviorClasses
{
    public class Mafia : BaseBehavior
    {
        private Vector2 m_MovingTarget;
        private string m_SubclassId;

        protected override void Start()
        {
            base.Start();
            BehaviorType = "Mafia";
            m_SpriteRenderer.color = Color.red;
        }

        public override void UpdateBehavior()
        {
            switch (CurrentState)
            {
                case State.Patrol:
                    PatrolUpdate();
                    break;
                case State.RunAway:
                    RunUpdate();
                    break;
            }
        }

        private void RunUpdate()
        {
            throw new NotImplementedException();
        }

        public void SetSubclass(string id)
        {
            m_SubclassId = id;

            // TODO change visualization
            //Visualization(this, id);

            m_SpriteRenderer.color = new Color(255.0f / 255.0f, 121 / 255.0f, 0 / 255.0f);
        }
    }
}