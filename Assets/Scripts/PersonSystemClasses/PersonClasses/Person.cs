using System.Collections;

using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses.PersonBehaviorClasses;

namespace MafiaNextGeneration.PersonSystemClasses.PersonClasses
{
    public class Person : MonoBehaviour
    {
        public BaseBehavior BaseBehavior;

        private SpriteRenderer m_SpriteRenderer;

        public void Awake()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void UpdatePerson()
        {
            BaseBehavior.UpdateBehavior();

            CheckDistance();
        }

        private void CheckDistance()
        {
            var personList = PersonManager.Instance.PersonList;
        }

        public void SetBehaviorType(string behaviorType)
        {
            Destroy(BaseBehavior);

            switch (behaviorType)
            {
                case "Mafia":
                    m_SpriteRenderer.color = Color.red;
                    BaseBehavior = gameObject.AddComponent<Mafia>();
                    break;
                case "Policeman":
                    m_SpriteRenderer.color = Color.blue;
                    BaseBehavior = gameObject.AddComponent<Policeman>();
                    break;
            }
        }
    }
}