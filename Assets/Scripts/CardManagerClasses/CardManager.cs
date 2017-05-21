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
        public Dictionary<string, BaseCard> CardsList = new Dictionary<string, BaseCard>();

        [Header("Card prefabs")]
        [SerializeField]
        public List<CardStruct> CardPrefabList;

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
            Destroy(CardsList[cardId].gameObject);
            CardsList[cardId].StopEffect();
            CardsList.Remove(cardId);
        }

        [ContextMenu("Use killer card")]
        private void UseKillCard()
        {
            UseCard("KillerCard");
        }

        [ContextMenu("Unuse killer card")]
        private void UnuseKillCard()
        {
            UnuseCard("KillerCard");
        }

        [ContextMenu("Use Invisibility card")]
        private void UseInvisibilityCard()
        {
            UseCard("Invisibility");
        }

        [ContextMenu("Unuse Invisibility card")]
        private void UnuseInvisibilityCard()
        {
            UnuseCard("Invisibility");
        }
    }
}