using System;
using UnityEngine;

namespace MafiaNextGeneration.PersonSystemClasses.PersonBehaviorClasses
{
    public class Mafia : BaseBehavior
    {
        private PersonType m_SubclassId;
        private string m_BuffType;
        private string m_CharaceristicId;

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

        public void SetSubclass(PersonType id)
        {
            m_SubclassId = id;

            switch (id)
            {
                case PersonType.MafiaKiller:
                    m_SpriteRenderer.color = new Color(255.0f / 255.0f, 121 / 255.0f, 0 / 255.0f);
                    break;
                case PersonType.MafiaKillerBugai:
                    m_SpriteRenderer.color = new Color(255.0f / 255.0f, 255 / 255.0f, 0 / 255.0f);
                    break;
                case PersonType.MafiaKillerAgility:
                    //m_SpriteRenderer.color = new Color(255.0f / 255.0f, 255 / 255.0f, 0 / 255.0f);
                    break;
            }
        }

        public void SetBuff(string buffId)
        {

        }
    }
}