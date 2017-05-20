using System.Collections.Generic;

using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses.PersonClasses;
using MafiaNextGeneration.PersonSystemClasses.PersonBehaviorClasses;

namespace MafiaNextGeneration.PersonSystemClasses
{
    public class PersonManager : MonoBehaviour
    {
        private static PersonManager m_Instance;

        [SerializeField]
        private Person m_PersonPrefab = null;
        
        public float SpawnRadius;
        public int InitPersonCount = 1000;

        [Header("Population")]
        public float CreatePersonTime = 1;
        public float DeadPersonTime = 1;

        [Header("Mutation")]
        public float MutationFrequency;
        public float PolicemanMutationChance;
        public float MafiaMutationChance;

        [Header("Inforimation")]
        public int PersonCount = 0;

        private float m_CreatePersonTimer;
        private float m_DeadPersonTimer;
        private float m_MutationFrequencyTimer;

        private List<Person> m_PersonList = new List<Person>();

        public List<Person> PersonList
        {
            get
            {
                return m_PersonList;
            }
        }

        public static PersonManager Instance
        {
            get
            {
                return m_Instance;
            }
        }

        public void Awake()
        {
            m_Instance = this;
        }

        public void Start()
        {
            InitPersons(InitPersonCount);
        }

        private void InitPersons(int p_Count)
        {
            for (int i = 0; i < p_Count; i++)
            {
                var person = CreatePerson();
                person.transform.position = GetRandomPos();
                m_PersonList.Add(person);
            }
        }

        public void Update()
        {
            m_CreatePersonTimer += Time.deltaTime;
            m_DeadPersonTimer += Time.deltaTime;
            m_MutationFrequencyTimer += Time.deltaTime;

            if (m_CreatePersonTimer > CreatePersonTime)
            {
                GiveBirth();
                m_CreatePersonTimer = 0.0f;
            }

            if (m_DeadPersonTimer > DeadPersonTime)
            {
                DeadPerson();
                m_DeadPersonTimer = 0.0f;
            }

            if (m_MutationFrequencyTimer > MutationFrequency)
            {
                CheckMutations();
            }

            for (int i = 0; i < m_PersonList.Count - 10; i++)
            {
                m_PersonList[i].UpdatePerson();
            }

            PersonCount = m_PersonList.Count;
        }

        private Person CreatePerson()
        {
            var person = Instantiate(m_PersonPrefab, transform);

            var citizenBeahvior = person.gameObject.AddComponent<Citizen>();
            person.BaseBehavior = citizenBeahvior;
            
            return person;
        }

        private void DeadPerson()
        {
            var l_Index = Random.Range(0, m_PersonList.Count - 1);
            Destroy(m_PersonList[l_Index].gameObject);
            m_PersonList.RemoveAt(l_Index);
        }

        public Vector2 GetRandomPos()
        {
            return Random.insideUnitCircle * SpawnRadius;
        }

        private void GiveBirth()
        {
            var person = CreatePerson();

            var motherIndex = Random.Range(0, m_PersonList.Count);
            person.transform.position = m_PersonList[motherIndex].transform.position;

            m_PersonList.Add(person);
        }

        private void CheckMutations()
        {
            float mafiaChance = Random.Range(0.0f, 100.0f);
            float policemanChance = Random.Range(0.0f, 100.0f);

            if (mafiaChance < MafiaMutationChance)
            {
                var randomIndex = Random.Range(0, m_PersonList.Count - 1);

                m_PersonList[randomIndex].SetBehaviorType("Mafia");
            }

            if (policemanChance < MafiaMutationChance)
            {
                var randomIndex = Random.Range(0, m_PersonList.Count - 1);

                m_PersonList[randomIndex].SetBehaviorType("Policeman");
            }
        }
    }
}