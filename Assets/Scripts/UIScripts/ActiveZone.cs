using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Cards;
using UnityEngine.UI;

public class ActiveZone : MonoBehaviour, IDropHandler {
	UIController uiController;
	RectTransform rectTransform;
	RectTransform card;
	RectTransform nextCard;

	void Start(){
		rectTransform = GetComponent<RectTransform> ();
		uiController = UIController.instance;
		uiController.ActiveZonesPositions.Add (rectTransform.position);
	}

	public void OnDrop(PointerEventData eventData){
		if(uiController.selectedCard != null){
			Card crd = uiController.selectedCard.GetComponent<Card> ();
			if (card != null && card.GetComponent<Card>().afterActive) {
				uiController.CardDiscard (card.GetComponent<Card> ().CardId);
				Destroy (card.gameObject);
			}
			uiController.selectedCard.SetParent (transform);
			card = uiController.selectedCard;
			crd.afterActive = true;
			uiController.CardAdd (crd.CardId);
			uiController.DecreaseCardHandIndex (crd);
		}
	}

	public void OnPointerDown(PointerEventData evetnData){
		/*if (uiController.selectedCard != null) {
			Disapear ();

			Card crd = uiController.selectedCard.GetComponent<Card> ();
			crd.cantSelected = true;
			uiController.handCards.Remove (crd);

			if (card != null) {
				nextCard = uiController.selectedCard;
				StartRemoveCard ();
			} else {
				card = uiController.selectedCard;
			}
			uiController.DecreaseCardHandIndex (crd);
			uiController.selectedCard = null;
		} else {
			StartRemoveCard ();
		}*/
	}

	void Disapear(){
		uiController.selectedCard.SetParent (rectTransform, false);
		uiController.selectedCard.localPosition = Vector2.zero;
		uiController.selectedCard.localScale = Vector3.one;
		uiController.selectedCard.localEulerAngles = Vector3.zero;
		//uiController.AfterSelectCard (true);
		uiController.selectedCard.GetComponent<Image> ().raycastTarget = false;
	}
}
