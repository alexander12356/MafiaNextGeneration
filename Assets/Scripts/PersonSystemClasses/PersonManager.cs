using System.Collections.Generic;

using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses.PersonClasses;
using MafiaNextGeneration.PersonSystemClasses.PersonBehaviorClasses;

namespace MafiaNextGeneration.PersonSystemClasses
{
    public struct PersonListStruct
    {
        public PersonType Id;
        public List<Person> PersonList;

        public PersonListStruct(PersonType id, List<Person> personList)
        {
            Id = id;
            PersonList = personList;
        }
    }

    [System.Serializable]
    public struct PersonMutationChance
    {
        public PersonType FromId;
        public PersonType TargetId;
        public float Chance;
    }

    public enum PersonType
    {
        Citizen,
        Mafia,
        MafiaKiller,
        MafiaKillerBugai,
        MafiaKillerAgility,
        MafiaKillerIntelect,
        Policeman
    }

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
        public List<PersonMutationChance> PersonMutationChance = new List<PersonMutationChance>();

        [Header("Citizen property")]
        public float CitizenDeathrate;

        [Header("Mafia property")]
        public float MafiaArrestChance;

        [Header("Policeman property")]
        public float PolicemanKilledChance;
        public float DiminishingMafia;

        [Header("Inforimation")]
        public int PersonCount = 0;
        public int CitizenCount = 0;
        public int MafiaCount = 0;
        public int PolicemanCount = 0;
        public int MafiaKillerCount = 0;

        private float m_CreatePersonTimer;
        private float m_DeadPersonTimer;
        private float m_MutationFrequencyTimer;
        
        private List<PersonListStruct> m_PersonList = new List<PersonListStruct>();

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

        private void AddNewPerson(PersonType personType, Person person)
        {
            for (int i = 0; i < m_PersonList.Count; i++)
            {
                if (m_PersonList[i].Id == personType)
                {
                    m_PersonList[i].PersonList.Add(person);
                    return;
                }
            }

            PersonListStruct personListStruct;
            personListStruct.Id = personType;
            personListStruct.PersonList = new List<Person>();
            personListStruct.PersonList.Add(person);

            m_PersonList.Add(personListStruct);
        }

        private Person GetPerson(PersonType type, int index)
        {
            for (int i = 0; i < m_PersonList.Count; i++)
            {
                if (m_PersonList[i].Id == type)
                {
                    return m_PersonList[i].PersonList[index];
                }
            }

            return null;
        }

        private Person GetRandomPerson(PersonType type)
        {
            for (int i = 0; i < m_PersonList.Count; i++)
            {
                if (m_PersonList[i].Id == type)
                {
                    int index = Random.Range(0, m_PersonList.Count);

                    return m_PersonList[i].PersonList[index];
                }
            }

            return null;
        }

        private PersonListStruct GetPersonList(PersonType type)
        {
            for (int i = 0; i < m_PersonList.Count; i++)
            {
                if (m_PersonList[i].Id == type)
                {
                    return m_PersonList[i];
                }
            }

            var personList = new PersonListStruct(type, new List<Person>());
            m_PersonList.Add(personList);
            return personList;
        }

        private void InitPersons(int p_Count)
        {
            for (int i = 0; i < p_Count; i++)
            {
                var person = CreatePerson();
                person.transform.position = GetRandomPos();
                AddNewPerson(PersonType.Citizen, person);
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

            PersonCount = GetPersonList(PersonType.Citizen).PersonList.Count + GetPersonList(PersonType.Mafia).PersonList.Count + GetPersonList(PersonType.Policeman).PersonList.Count + GetPersonList(PersonType.MafiaKiller).PersonList.Count;
            CitizenCount = GetPersonList(PersonType.Citizen).PersonList.Count;
            MafiaCount = GetPersonList(PersonType.Mafia).PersonList.Count;
            PolicemanCount = GetPersonList(PersonType.Policeman).PersonList.Count;
            MafiaKillerCount = GetPersonList(PersonType.MafiaKiller).PersonList.Count;
        }

        private void CheckMafiaArrest()
        {
            var arrestChance = Random.Range(0, 100.0f);
            if (arrestChance < MafiaArrestChance)
            {
                RemoveFromList(GetPersonList(PersonType.Mafia).PersonList, Random.Range(0, GetPersonList(PersonType.Mafia).PersonList.Count));
            }
        }

        private void CheckPolicemanKilled()
        {
            var policemanKilledChance = Random.Range(0, 100.0f);
            if (policemanKilledChance < PolicemanKilledChance)
            {
                RemoveFromList(GetPersonList(PersonType.Policeman).PersonList, Random.Range(0, GetPersonList(PersonType.Policeman).PersonList.Count));
            }
        }

        private void DeadPerson()
        {
            int indexForDeath = Random.Range(0, PersonCount);

            for (int i = 0; i < m_PersonList.Count; i++)
            {
                if (indexForDeath < m_PersonList[i].PersonList.Count)
                {
                    DestroyObject(m_PersonList[i].PersonList[indexForDeath].gameObject);
                    m_PersonList[i].PersonList.RemoveAt(indexForDeath);
                    return;
                }
                else
                {
                    indexForDeath -= m_PersonList[i].PersonList.Count - 1;
                }
            }
        }

        private void RemoveFromList(List<Person> personList, int deathIndex)
        {
            Destroy(personList[deathIndex].gameObject);
            personList.RemoveAt(deathIndex);
        }

        private void GiveBirth()
        {
            var person = CreatePerson();

            var motherIndex = Random.Range(0, GetPersonList(PersonType.Citizen).PersonList.Count);
            person.transform.position = GetPersonList(PersonType.Citizen).PersonList[motherIndex].transform.position;

            AddNewPerson(PersonType.Citizen, person);
        }

        private void PersonUpdate()
        {
            for (int i = 0; i < m_PersonList.Count; i++)
            {
                for (int j = 0; j < m_PersonList[i].PersonList.Count; j++)
                {
                    m_PersonList[i].PersonList[j].UpdatePerson();
                }
            }
        }

        private void CheckMutations()
        {
            for (int i = 0; i < PersonMutationChance.Count; i++)
            {
                var mutationChance = Random.Range(0, 100.0f);
                
                if (mutationChance < PersonMutationChance[i].Chance)
                {
                    var fromId = PersonMutationChance[i].FromId;
                    var targetId = PersonMutationChance[i].TargetId;
                    var randomIndex = Random.Range(0, GetPersonList(fromId).PersonList.Count);

                    GetPersonList(fromId).PersonList[randomIndex].SetBehaviorType(targetId);
                    AddNewPerson(targetId, GetPerson(fromId, randomIndex));
                    GetPersonList(fromId).PersonList.RemoveAt(randomIndex);
                }
            }
        }

        public void ChangeMutationChance(PersonType personType, float value)
        {
            for (int i = 0; i < PersonMutationChance.Count; i++)
            {
                if (PersonMutationChance[i].TargetId == personType)
                {
                    var mutationChance = PersonMutationChance[i];
                    mutationChance.Chance += value;
                    PersonMutationChance[i] = mutationChance;
                }
            }
        }
    }
}