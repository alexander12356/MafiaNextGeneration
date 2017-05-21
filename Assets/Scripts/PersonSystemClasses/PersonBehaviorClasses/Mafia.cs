using System;
using UnityEngine;

namespace MafiaNextGeneration.PersonSystemClasses.PersonBehaviorClasses
{
    public class Mafia : BaseBehavior
    {
        private PersonType m_SubclassId;
        private BuffType m_BuffType;

        protected override void Start()
        {
            base.Start();
            BehaviorType = "Mafia";
            ClassVisualization.ConformNewClassView(gameObject, PersonType.Mafia);
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
                    ClassVisualization.ConformNewSubclassView(gameObject, PersonType.MafiaKiller);
                    break;
                case PersonType.MafiaKillerBugai:
                    ClassVisualization.ConformNewSubclassView(gameObject, PersonType.MafiaKillerBugai);
                    break;
                case PersonType.MafiaKillerAgility:
                    ClassVisualization.ConformNewSubclassView(gameObject, PersonType.MafiaKillerAgility);
                    break;
                case PersonType.MafiaKillerIntelect:
                    ClassVisualization.ConformNewSubclassView(gameObject, PersonType.MafiaKillerIntelect);
                    break;
            }
        }

        public void SetBuff(string buffId)
        {

        }
    }
}