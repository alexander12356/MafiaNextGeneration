using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trash : MonoBehaviour,IDropHandler {
	UIController uiController;

	void Start(){
		uiController = UIController.instance;
	}

	public void OnDrop(PointerEventData eventData){
		if(uiController.selectedCard != null){
			uiController.selectedCard.SetParent (transform);
			Cards.Card crd = uiController.selectedCard.GetComponent<Cards.Card> ();
			if (crd.afterActive) {
				TrashObject ();
			} else {
				TrashObject ();
				uiController.DecreaseCardHandIndex (crd);
			}
		}
	}

	void TrashObject(){
		Destroy (transform.GetChild (0).gameObject);
	}
}
