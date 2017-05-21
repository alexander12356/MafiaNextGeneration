using System.Collections.Generic;

using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses.PersonClasses;
using MafiaNextGeneration.PersonSystemClasses.PersonBehaviorClasses;

namespace MafiaNextGeneration.PersonSystemClasses
{
    [System.Serializable]
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

    [System.Serializable]
    public struct PersonBuffChance
    {
        public BuffType BuffType;
        public float Chance;

        public PersonBuffChance(BuffType buffType, float chance)
        {
            BuffType = buffType;
            Chance = chance;
        }
    }

    [System.Serializable]
    public struct BuffData
    {
        public BuffType BuffId;
        public int Count;

        public BuffData(BuffType buffId, int count)
        {
            BuffId = buffId;
            Count = count;
        }
    }

    public enum PersonType
    {
        Policeman = -2,
        Citizen = -3,
        Mafia = 3,
        MafiaKiller = 4,
        MafiaKillerBugai = 5,
        MafiaKillerAgility = -1,
        MafiaKillerIntelect = 2
    }

    public enum BuffType
    {
        Invisibility = 1,
        Robber = 6,
        Spy = 7,
        TrueSight = 8,
        BountyHunter = 9
    }

    public class PersonManager : MonoBehaviour
    {
        public PersonManager PersonManagePrefab;
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
        public List<PersonBuffChance> PersonBuffChance = new List<PersonBuffChance>();

        [Header("Citizen property")]
        public float CitizenDeathrate;

        [Header("Mafia property")]
        public float MafiaArrestChance;

        [Header("Policeman property")]
        public float PolicemanKilledChance;
        public float DiminishingMafia;

        [Header("Global property")]
        public float Invisibility;

        [Header("Inforimation")]
        public int PersonCount = 0;
        public int CitizenCount = 0;
        public int MafiaCount = 0;
        public int PolicemanCount = 0;
        public int MafiaKillerCount = 0;
        public List<BuffData> BuffList = new List<BuffData>();

        public bool IsGameStarted = false;

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
            if (m_Instance == null)
            {
                m_Instance = this;
            }
            else
            {
                m_Instance = null;
                Destroy(gameObject);
                Instantiate(m_Instance.PersonManagePrefab);
            }
        }

        public void LeaveWorld()
        {
            GameController.Instance.Save();
            Destroy(gameObject);
            m_Instance = null;
            enabled = false;
        }

        public void StartNewGame(int id)
        {
            SetInitPos(id);

            InitPersons(InitPersonCount);
        }

        private void SetInitPos(int id)
        {
            switch (id)
            {
                case 1:
                    transform.position = new Vector3(-10.0f, -10.0f, 0.0f);
                    break;
                case 2:
                    transform.position = new Vector3(-10.0f, 10.0f, 0.0f);
                    break;
                case 3:
                    transform.position = new Vector3(10.0f, 10.0f, 0.0f);
                    break;
                case 4:
                    transform.position = new Vector3(10.0f, -10.0f, 0.0f);
                    break;
            }
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
                person.transform.localPosition = GetRandomPos();
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
                CheckBuffs();
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
            if (GetPersonList(PersonType.Citizen).PersonList.Count == 0)
            {
                return;
            }

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

                    if (GetPersonList(fromId).PersonList.Count == 0)
                    {
                        return;
                    }

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

        private void CheckBuffs()
        {
            for (int i = 0; i < PersonBuffChance.Count; i++)
            {
                var buffChance = Random.Range(0.0f, 100.0f);
                if (PersonBuffChance[i].Chance < buffChance)
                {
                    var randomIndex = Random.Range(0, GetPersonList(PersonType.Mafia).PersonList.Count);
                    if (!GetPerson(PersonType.Mafia, randomIndex).IsHaveBuff(PersonBuffChance[i].BuffType))
                    {
                        GetPersonList(PersonType.Mafia).PersonList[randomIndex].SetBuff(PersonBuffChance[i].BuffType);
                        AddBuff(PersonBuffChance[i].BuffType);
                    }
                }
            }
        }

        public void ChangeBuffChance(BuffType buffId, float value)
        {
            for (int i = 0; i < PersonBuffChance.Count; i++)
            {
                if (PersonBuffChance[i].BuffType == buffId)
                {
                    var personBuff = PersonBuffChance[i];
                    personBuff.Chance += value;
                    PersonBuffChance[i] = personBuff;
                    return;
                }
            }

            var newPersonBuffChance = new PersonBuffChance(buffId, value);
            PersonBuffChance.Add(newPersonBuffChance);
        }

        private void AddBuff(BuffType buffType)
        {
            for (int i = 0; i < BuffList.Count; i++)
            {
                if (BuffList[i].BuffId == buffType)
                {
                    var buffData = BuffList[i];
                    buffData.Count++;
                    BuffList[i] = buffData;
                    break;
                }
            }

            BuffData newBuffData = new BuffData(buffType, 1);
            BuffList.Add(newBuffData);
        }

        public void RemoveBuff(BuffType buffType)
        {
            for (int i = 0; i < BuffList.Count; i++)
            {
                if (BuffList[i].BuffId == buffType)
                {
                    var buffData = BuffList[i];
                    buffData.Count--;
                    BuffList[i] = buffData;
                    break;
                }
            }
        }

        public WorldData SaveData()
        {
            var worldData = new WorldData();
            worldData.CreatePersonTime = CreatePersonTime;
            worldData.DeadPersonTime = DeadPersonTime;
            worldData.MutationFrequency = MutationFrequency;
            worldData.PersonMutationChance = PersonMutationChance;
            worldData.PersonBuffChance = PersonBuffChance;
            worldData.CitizenDeathrate = CitizenDeathrate;
            worldData.MafiaArrestChance = MafiaArrestChance;
            worldData.PolicemanKilledChance = PolicemanKilledChance;
            worldData.DiminishingMafia = DiminishingMafia;
            worldData.Invisibility = Invisibility;
            worldData.PersonCount = PersonCount;
            worldData.CitizenCount = CitizenCount;
            worldData.MafiaCount = MafiaCount;
            worldData.PolicemanCount = PolicemanCount;
            worldData.MafiaKillerCount = MafiaKillerCount;
            worldData.BuffList = BuffList;

            var personDataList = new List<PersonDataList>();
            for (int i = 0; i < m_PersonList.Count; i++)
            {
                PersonDataList dataList;
                dataList.Type = m_PersonList[i].Id;
                dataList.DataList = new List<PersonData>();
                for (int j = 0; j < m_PersonList[i].PersonList.Count; j++)
                {
                    PersonData data;
                    data.BuffList = m_PersonList[i].PersonList[j].BuffList;
                    dataList.DataList.Add(data);
                }
                personDataList.Add(dataList);
            }
            worldData.PersonDataList = personDataList;

            return worldData;
        }

        public void LoadData(int id, WorldData worldData)
        {
            CreatePersonTime = worldData.CreatePersonTime;
            DeadPersonTime = worldData.DeadPersonTime;
            MutationFrequency = worldData.MutationFrequency;
            PersonMutationChance = worldData.PersonMutationChance;
            PersonBuffChance = worldData.PersonBuffChance;
            CitizenDeathrate = worldData.CitizenDeathrate;
            MafiaArrestChance = worldData.MafiaArrestChance;
            PolicemanKilledChance = worldData.PolicemanKilledChance;
            DiminishingMafia = worldData.DiminishingMafia;
            Invisibility = worldData.Invisibility;
            PersonCount = worldData.PersonCount;
            CitizenCount = worldData.CitizenCount;
            MafiaCount = worldData.MafiaCount;
            PolicemanCount = worldData.PolicemanCount;
            MafiaKillerCount = worldData.MafiaKillerCount;
            BuffList = worldData.BuffList;

            SetInitPos(id);

            for (int i = 0; i < worldData.PersonDataList.Count; i++)
            {
                for (int j = 0; j < worldData.PersonDataList[i].DataList.Count; j++)
                {
                    var person = CreatePerson();
                    person.transform.localPosition = GetRandomPos();
                    AddNewPerson(worldData.PersonDataList[i].Type, person);

                    person.SetBehaviorType((worldData.PersonDataList[i].Type));

                    for (int k = 0; k < worldData.PersonDataList[i].DataList[j].BuffList.Count; k++)
                    {
                        person.SetBuff(worldData.PersonDataList[i].DataList[j].BuffList[k]);
                    }
                }
            }
        }
    }
}