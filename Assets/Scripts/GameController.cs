using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses;

namespace MafiaNextGeneration
{
    public class GameController : MonoBehaviour
    {
        private static GameController m_Instance;
        public static GameController Instance;

        public PersonManager PersonManagerPrefab;

        public WorldData worldData;

        public void Awake()
        {
            Instance = this;
        }

        public void GoingToWorld(int id)
        {
            Instantiate(PersonManagerPrefab);
            if (worldData == null)
            {
                PersonManager.Instance.StartNewGame(id);
            }
            else
            {
                PersonManager.Instance.LoadData(id, worldData);
            }
        }
        
        public void Save()
        {
            worldData = PersonManager.Instance.SaveData();
        }
    }
}