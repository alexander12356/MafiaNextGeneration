using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cards
{
	[System.Serializable]
	public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler {

		public string CardId;
		public int HandIndexPosition;
		public bool afterActive;

		public RectTransform rectTransform;
		Font myFont;

		Transform startParent;
		CardBase cardBase;

		public Card (string cardID){
			this.CardId = cardID;
		}

		void Awake(){
			rectTransform = GetComponent<RectTransform> ();
		}

		void Start(){
			myFont = Resources.Load<Font> ("Font/Slabo27px-Regular");
			startParent = transform;
			cardBase = CardBase.instance;
			Image face = new GameObject ("face").AddComponent<Image> ();
			face.rectTransform.SetParent (rectTransform, false);
			face.sprite = Resources.Load<Sprite> ("Icon/" + CardId);
			face.rectTransform.sizeDelta = new Vector2 (140, 240);
			face.rectTransform.localPosition = Vector3.zero;

			Image frame = new GameObject ("frame").AddComponent<Image> ();
			frame.rectTransform.SetParent (rectTransform, false);
			frame.sprite = Resources.Load<Sprite> ("UI/Images/frame");
			frame.rectTransform.sizeDelta = new Vector2 (155, 255);
			frame.rectTransform.localPosition = Vector3.zero;

			Text cost = new GameObject ("cost").AddComponent<Text> ();
			cost.rectTransform.SetParent (rectTransform, false);
			for (int i = 0; i < cardBase.cardCostList.Count; i++) {
				if (cardBase.cardCostList [i].id == CardId) {
					cost.text = cardBase.cardCostList [i].cost.ToString();
				}
			}
			cost.color = Color.white;
			cost.font = myFont;
			cost.rectTransform.localPosition = new Vector3 (-55, 107, 0);
			cost.alignByGeometry = true;
			cost.resizeTextForBestFit = true;
			cost.resizeTextMaxSize = 100;
			cost.alignment = TextAnchor.MiddleCenter;
			cost.rectTransform.sizeDelta = new Vector2 (75, 75);

			Text description = new GameObject ("description").AddComponent<Text> ();
			description.rectTransform.SetParent (rectTransform, false);
			description.fontSize = 40;
			description.alignment = TextAnchor.MiddleCenter;
			description.rectTransform.sizeDelta = new Vector2 (120, 70);
			description.rectTransform.localPosition = new Vector3 (0, -78, 0);
			description.color = Color.white;
			description.resizeTextForBestFit = true;
			description.alignByGeometry = true;
			description.resizeTextMaxSize = 100;
			description.font = myFont;
			for (int i = 0; i < cardBase.descriptionList.Count; i++) {
				if (cardBase.descriptionList [i].id == CardId) {
					description.text = cardBase.descriptionList [i].description;
				}
			}
		}

		public void OnBeginDrag(PointerEventData eventData){
			GetComponent<CanvasGroup> ().blocksRaycasts = false;
			UIController.instance.selectedCard = rectTransform;
		}

		public void OnEndDrag(PointerEventData eventData){
			if (startParent == transform) {
				UIController.instance.DiscardSelectedCard (rectTransform, HandIndexPosition,true);
			} else {
				
			}
			if (afterActive) {
				rectTransform.localPosition = Vector3.zero;
				rectTransform.localScale = Vector3.one;
				rectTransform.eulerAngles = Vector3.zero;
			}
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
			UIController.instance.selectedCard = null;
		}

		public void OnDrag(PointerEventData eventData){
			transform.position = eventData.position;
			rectTransform.localEulerAngles = Vector3.zero;
		}
	}
}
