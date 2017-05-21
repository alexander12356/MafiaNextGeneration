using System;
using System.Collections.Generic;

using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses.PersonBehaviorClasses;

namespace MafiaNextGeneration.PersonSystemClasses.PersonClasses
{
    public class Person : MonoBehaviour
    {
        public BaseBehavior BaseBehavior;
        public List<BuffType> BuffList = new List<BuffType>();

        public void UpdatePerson()
        {
            BaseBehavior.UpdateBehavior();
        }

        public void SetBehaviorType(PersonType type)
        {
            switch(type)
            {
                case PersonType.Mafia:
                    BaseBehavior = gameObject.AddComponent<Mafia>();
                    break;
                case PersonType.Policeman:
                    BaseBehavior = gameObject.AddComponent<Policeman>();
                    break;
                case PersonType.MafiaKiller:
                    if (!(BaseBehavior is Mafia))
                    {
                        BaseBehavior = gameObject.AddComponent<Mafia>();
                    }
                    (BaseBehavior as Mafia).SetSubclass(type);
                    break;
                case PersonType.MafiaKillerBugai:
                    if (!(BaseBehavior is Mafia))
                    {
                        BaseBehavior = gameObject.AddComponent<Mafia>();
                    }
                    (BaseBehavior as Mafia).SetSubclass(type);
                    break;
                case PersonType.MafiaKillerAgility:
                    if (!(BaseBehavior is Mafia))
                    {
                        BaseBehavior = gameObject.AddComponent<Mafia>();
                    }
                    (BaseBehavior as Mafia).SetSubclass(type);
                    break;
                case PersonType.MafiaKillerIntelect:
                    if (!(BaseBehavior is Mafia))
                    {
                        BaseBehavior = gameObject.AddComponent<Mafia>();
                    }
                    (BaseBehavior as Mafia).SetSubclass(type);
                    break;
            }
        }

        public void SetBuff(BuffType buffId)
        {
            BuffList.Add(buffId);
            ClassVisualization.ConformNewClassView(gameObject, buffId);
        }

        private void OnDestroy()
        {
            for (int i = 0; i < BuffList.Count; i++)
            {
                PersonManager.Instance.RemoveBuff(BuffList[i]);
            }
        }

        public bool IsHaveBuff(BuffType buffType)
        {
            for (int i = 0; i < BuffList.Count; i++)
            {
                if (BuffList[i] == buffType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}