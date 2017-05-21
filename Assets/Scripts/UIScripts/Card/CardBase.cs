using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards{
	public class CardBase : MonoBehaviour {

		public static CardBase instance;
		List<string> returnIds = new List<string>();

		void Awake(){
			if (instance == null) {
				instance = this;
			} else {
				return;
			}
		}

		public List<string> GetRandomIds(int count){
			for (int i = 0; i < count; i++) {
				//returnIds.Add(Random.Range (0, 31).ToString ());
				returnIds.Add(CardList [Random.Range(0, CardList.Count)]);
			}
			return returnIds;
		}

		public List<string> CardList = new List<string>();
		[System.Serializable]
		public struct CardCostData {
			public int cost;
			public string id;
		}
		public List<CardCostData> cardCostList = new List<CardCostData> ();

		[System.Serializable]
		public struct Description{
			public string id;
			public string description;
		}
		public List<Description> descriptionList = new List<Description>();
	}
}
