using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParseUsersLog : MonoBehaviour {

	public ParseUsersLog instance;
	public GameObject textPrefab;
	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	public RectTransform holderContent;

	void Start(){
		holderContent = GameObject.Find ("Content").GetComponent<RectTransform>();
		textPrefab = Resources.Load<GameObject> ("Prefabs/text");
	}

	public void CreateText(string showingText){
		GameObject textt = Instantiate (textPrefab, holderContent)as GameObject;
		textt.GetComponent<Text> ().text = showingText;
	}
}
