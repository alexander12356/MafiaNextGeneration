using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Cards;
using UnityEngine.UI;

public class ActiveZone : MonoBehaviour, IDropHandler {
	UIController uiController;
	RectTransform rectTransform;
	RectTransform card;
	[SerializeField]
	RectTransform trash;
	RectTransform nextCard;

	void Start(){
		rectTransform = GetComponent<RectTransform> ();
		uiController = UIController.instance;
		trash = uiController.trashHolder;
		uiController.ActiveZonesPositions.Add (rectTransform.position);
	}

	public void OnDrop(PointerEventData eventData){
		if(uiController.selectedCard != null){
			if (card != null) {
				Destroy (card.gameObject);
			}
			uiController.selectedCard.SetParent (transform);
			card = uiController.selectedCard;
			Card crd = uiController.selectedCard.GetComponent<Card> ();
			crd.afterActive = true;
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
		uiController.selectedCard.GetComponent<Button> ().onClick.AddListener (() => StartRemoveCard ());
	}

	void StartRemoveCard(){
		StartCoroutine (MoveToTrash ());
	}

	IEnumerator MoveToTrash(){
		float t = 0;
		while (t < 5) {
			t += Time.deltaTime;
			card.position = Vector3.MoveTowards (card.position, trash.position, Time.deltaTime * 100);
			if ((card.position - trash.position).magnitude < 1) {
				Destroy (card.gameObject);
				card = nextCard;
				nextCard = null;
				yield break;
			}
			yield return null;
		}
	}
}
