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

        [Header("Citizen property")]
        public float CitizenDeathrate;

        [Header("Mafia property")]
        public float MafiaArrestChance;
        public float MafiaKillerMutationChance;

        [Header("Policeman property")]
        public float PolicemanKilledChance;
        public float DiminishingMafia;

        [Header("Inforimation")]
        public int PersonCount = 0;
        public int CitizenCount = 0;
        public int MafiaCount = 0;
        public int PolicemanCount = 0;

        private float m_CreatePersonTimer;
        private float m_DeadPersonTimer;
        private float m_MutationFrequencyTimer;

        private List<Person> m_CitizenList = new List<Person>();
        private List<Person> m_MafiaList = new List<Person>();
        private List<Person> m_PolicemanList = new List<Person>();

        public List<Person> PersonList
        {
            get
            {
                return m_CitizenList;
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
                m_CitizenList.Add(person);
            }
            PersonCount = p_Count;
        }

        private Person CreatePerson()
        {
            var person = Instantiate(m_PersonPrefab, transform);

            var citizenBeahvior = person.gameObject.AddComponent<Citizen>();
            person.BaseBehavior = citizenBeahvior;

            return person;
        }

        public Vector2 GetRandomPos()
        {
            return Random.insideUnitCircle * SpawnRadius;
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
                CheckPolicemanKilled();
                CheckMafiaArrest();
                m_DeadPersonTimer = 0.0f;
            }

            if (m_MutationFrequencyTimer > MutationFrequency)
            {
                CheckMutations();
            }

            PersonUpdate();

            PersonCount = m_CitizenList.Count + m_MafiaList.Count + m_PolicemanList.Count;
            CitizenCount = m_CitizenList.Count;
            MafiaCount = m_MafiaList.Count;
            PolicemanCount = m_PolicemanList.Count;
        }

        private void CheckMafiaArrest()
        {
            var arrestChance = Random.Range(0, 100.0f);
            if (arrestChance < MafiaArrestChance)
            {
                RemoveFromList(m_MafiaList, Random.Range(0, m_MafiaList.Count));
            }
        }

        private void CheckPolicemanKilled()
        {
            var policemanKilledChance = Random.Range(0, 100.0f);
            if (policemanKilledChance < PolicemanKilledChance)
            {
                RemoveFromList(m_PolicemanList, Random.Range(0, m_PolicemanList.Count));
            }
        }

        private void DeadPerson()
        {
            int deathIndex = Random.Range(0, PersonCount);

            if (deathIndex < m_CitizenList.Count)
            {
                RemoveFromList(m_CitizenList, deathIndex);
                return;
            }

            deathIndex -= m_CitizenList.Count - 1;

            if (deathIndex < m_MafiaList.Count)
            {
                RemoveFromList(m_MafiaList, deathIndex);
                return;
            }

            deathIndex -= m_MafiaList.Count - 1;

            RemoveFromList(m_PolicemanList, deathIndex);
        }

        private void RemoveFromList(List<Person> personList, int deathIndex)
        {
            Destroy(personList[deathIndex].gameObject);
            personList.RemoveAt(deathIndex);
        }

        private void GiveBirth()
        {
            var person = CreatePerson();

            var motherIndex = Random.Range(0, m_CitizenList.Count);
            person.transform.position = m_CitizenList[motherIndex].transform.position;

            m_CitizenList.Add(person);
        }

        private void PersonUpdate()
        {
            for (int i = 0; i < m_CitizenList.Count; i++)
            {
                m_CitizenList[i].UpdatePerson();
            }
            for (int i = 0; i < m_MafiaList.Count; i++)
            {
                m_MafiaList[i].UpdatePerson();
            }
            for (int i = 0; i < m_PolicemanList.Count; i++)
            {
                m_PolicemanList[i].UpdatePerson();
            }
        }

        private void CheckMutations()
        {
            CheckMafiaMutation();
            CheckPolicemanMutation();

            CheckMafiaKillerMutation();
        }

        private void CheckMafiaMutation()
        {
            float mafiaChance = Random.Range(0.0f, 100.0f);

            if (mafiaChance < MafiaMutationChance)
            {
                var randomIndex = Random.Range(0, m_CitizenList.Count - 1);

                m_CitizenList[randomIndex].SetBehaviorType("Mafia");
                m_MafiaList.Add(m_CitizenList[randomIndex]);
                m_CitizenList.RemoveAt(randomIndex);
            }
        }

        private void CheckPolicemanMutation()
        {
            float policemanChance = Random.Range(0.0f, 100.0f);

            if (policemanChance < PolicemanMutationChance)
            {
                var randomIndex = Random.Range(0, m_CitizenList.Count - 1);

                m_CitizenList[randomIndex].SetBehaviorType("Policeman");
                m_PolicemanList.Add(m_CitizenList[randomIndex]);
                m_CitizenList.RemoveAt(randomIndex);

                MafiaArrestChance += DiminishingMafia;
            }
        }

        private void CheckMafiaKillerMutation()
        {
            var mutationChance = Random.Range(0.0f, 100.0f);

            if (mutationChance < MafiaKillerMutationChance)
            {
                var index = Random.Range(0, m_MafiaList.Count);
                (m_MafiaList[index].BaseBehavior as Mafia).SetSubclass("Killer");
            }
        }
    }
}