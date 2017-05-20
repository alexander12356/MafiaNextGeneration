using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Beansjam.PersonSystemClasses.PersonClasses;

namespace Beansjam.PersonSystemClasses
{
    public class PersonManager : MonoBehaviour
    {
        private static PersonManager m_Instance;

        [SerializeField]
        private Person m_SpeciePrefab;

        public float SPECIES_SPAWN_TIME;
        public float SpawnRadius;
        public int PersonCount = 0;

        public float CreatePersonTime = 1;
        public float DeadPersonTime = 1;
        public int InitPersonCount = 1000;

        private List<Person> m_PersonList = new List<Person>();

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
                CreateSpecies();
            }
        }

        public void Update()
        {
            
        }

        private void CreateSpecies()
        {
            var l_Specie = Instantiate(m_SpeciePrefab);
            l_Specie.transform.position = GetRandomFromRect();
        }

        public Vector2 GetRandomFromRect()
        {
            return Random.insideUnitCircle * SpawnRadius;
        }
    }
}