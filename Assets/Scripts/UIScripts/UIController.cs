using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cards;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public static UIController instance;
	public event Action<string> OnCardAdded;
	public event Action<string> OnCardDiscarded;

	[HideInInspector]
	public Vector2[] cardPositions = new Vector2[]{
		new Vector2(-300, 100),
		new Vector2(-200, 150),
		new Vector2(-100, 180),
		new Vector2(0, 180),
		new Vector2(100, 150),
		new Vector2(300, 100)
	};
	[HideInInspector]
	public Vector3[] rotations = new Vector3[]{
		new Vector3(0,0,30),
		new Vector3(0,0,20),
		new Vector3(0,0,10),
		new Vector3(0,0,-10),
		new Vector3(0,0,-20),
		new Vector3(0,0,-30)
	};

	public RectTransform selectedCard;
	public List<Card> handCards = new List<Card>();
	public GameObject cardPrefab;
	public RectTransform cardHolder;

	public RectTransform trashHolder;

	public List<Vector3> ActiveZonesPositions = new List<Vector3> ();

	void Awake(){
		if (instance == null) {
			instance = this;
		}
		cardPrefab = Resources.Load<GameObject> ("Prefabs/Card");
		cardHolder = GameObject.Find ("Content").GetComponent<RectTransform> ();
		trashHolder = GameObject.Find ("Trash").GetComponent<RectTransform> ();
	}

	void Start(){
		SpawnCards (Cards.CardBase.instance.GetRandomIds (6));
	}


	void Update(){
		if (selectedCard != null) {
			selectedCard.SetAsLastSibling ();
			selectedCard.localScale = Vector3.one * 1.2f;
		}
	}

	public void DiscardSelectedCard(RectTransform discardCard, int index, bool isAfterDrag = false){
		if (isAfterDrag) {
			//discardCard.SetParent (cardHolder);
			discardCard.localPosition = cardPositions [index];
			discardCard.localEulerAngles = rotations [index];
		}
		discardCard.SetSiblingIndex (index);
		discardCard.localScale = Vector3.one;
		selectedCard = null;
	}

	public void DecreaseCardHandIndex(Card deletedCard){
		for (int i = 0; i < handCards.Count; i++) {
			if (handCards [i].HandIndexPosition > deletedCard.HandIndexPosition && handCards[i] != null) {
				handCards [i].HandIndexPosition--;
				handCards [i].rectTransform.SetSiblingIndex (handCards [i].HandIndexPosition);
				handCards [i].rectTransform.localPosition = cardPositions [handCards [i].HandIndexPosition];
				handCards [i].rectTransform.localEulerAngles = rotations [handCards [i].HandIndexPosition];
			}
		}
		handCards.Remove (deletedCard);
		SpawnCards (CardBase.instance.GetRandomIds (1), true);
	}

	public void SpawnCards(List<string> ids, bool crd = false){
		if (crd) {
			GameObject deltas = Instantiate (cardPrefab) as GameObject;
			RectTransform rtsfm = deltas.GetComponent<RectTransform> ();
			rtsfm.SetParent (cardHolder, false);
			rtsfm.localPosition = cardPositions [5];
			rtsfm.localRotation = Quaternion.Euler (rotations[5]);
			Card card = deltas.GetComponent<Card> ();
			if (card != null) {
				handCards.Add (card);
				card.HandIndexPosition = 5;
				card.CardId = ids [ids.Count - 1];
			}
			return;
		}
		for (int i = 0; i < cardPositions.Length; i++) {
			GameObject delta = Instantiate (cardPrefab) as GameObject;
			RectTransform rectTransform = delta.GetComponent<RectTransform> ();
			rectTransform.SetParent (cardHolder, false);
			rectTransform.localPosition = cardPositions [i];
			rectTransform.localRotation = Quaternion.Euler (rotations [i]);

			Card card = delta.GetComponent<Card> ();
			if (card != null) {
				card.HandIndexPosition = i;
				card.CardId = ids [i];
				handCards.Add (card);
			}
		}
	}

	public void CardDiscard(string cardId){
		if (OnCardDiscarded != null) {
			OnCardDiscarded.Invoke (cardId);
		}
	}

	public void CardAdd(string cardId){
		if (OnCardAdded != null) {
			OnCardAdded.Invoke (cardId);
		}
	}

}
