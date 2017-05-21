using System.Collections;

using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses.PersonBehaviorClasses;

namespace MafiaNextGeneration.PersonSystemClasses.PersonClasses
{
    public class Person : MonoBehaviour
    {
        public BaseBehavior BaseBehavior;

        public void UpdatePerson()
        {
            BaseBehavior.UpdateBehavior();
        }

        public void SetBehaviorType(string behaviorType)
        {
            Destroy(BaseBehavior);

            switch (behaviorType)
            {
                case "Mafia":
                    BaseBehavior = gameObject.AddComponent<Mafia>();
                    break;
                case "Policeman":
                    BaseBehavior = gameObject.AddComponent<Policeman>();
                    break;
            }
        }
    }
}