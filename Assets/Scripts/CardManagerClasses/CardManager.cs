using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MafiaNextGeneration.CardManagerClasses.CardClasses;

namespace MafiaNextGeneration.CardManagerClasses
{
    [System.Serializable]
    public struct CardStruct
    {
        public string CardID;
        public BaseCard CardPrefab;
    }

    public class CardManager : MonoBehaviour
    {
        private static CardManager m_Instance;

        public Dictionary<string, BaseCard> CardsList = new Dictionary<string, BaseCard>();

        [Header("Card prefabs")]
        [SerializeField]
        public List<CardStruct> CardPrefabList;

        public static CardManager Instance
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
            UIController.instance.OnCardAdded += UseCard;
            UIController.instance.OnCardDiscarded += UnuseCard;
        }

        public void UseCard(string cardId)
        {
            for (int i = 0; i < CardPrefabList.Count; i++)
            {
                if (CardPrefabList[i].CardID == cardId)
                {
                    var CurrentCard = Instantiate(CardPrefabList[i].CardPrefab, transform);
                    CurrentCard.StartEffect();
                    CardsList.Add(cardId, CurrentCard);
                }
            }
        }

        public void UnuseCard(string cardId)
        {
            if (!CardsList.ContainsKey(cardId))
            {
                return;
            }

            Destroy(CardsList[cardId].gameObject);
            CardsList[cardId].StopEffect();
            CardsList.Remove(cardId);
        }

        public List<string> GetRandomCards(int count)
        {
            List<string> returnIds = new List<string>();
            for (int i = 0; i < count; i++)
            {
                returnIds.Add(CardPrefabList[Random.Range(0, CardPrefabList.Count - 1)].CardID);
            }
            return returnIds;
        }
    }
}