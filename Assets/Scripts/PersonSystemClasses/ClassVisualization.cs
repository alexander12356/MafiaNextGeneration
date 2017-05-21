using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MafiaNextGeneration.PersonSystemClasses;

public static class ClassVisualization {

	public static void ConformNewClassView(GameObject unit, PersonType type) {
		unit.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Class/" + type.ToString());
        unit.GetComponent<SpriteRenderer>().sortingOrder = (int)type;

    }

    public static void ConformNewSubclassView(GameObject unit, PersonType type)
    {
        GameObject buff = new GameObject();
        buff.transform.parent = unit.transform;
        buff.transform.localPosition = Vector3.zero;
        buff.transform.localScale = Vector3.one;
        buff.AddComponent<SpriteRenderer>();
        buff.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Subclass/" + type.ToString());
        buff.GetComponent<SpriteRenderer>().sortingOrder = (int)type;
    }

    public static void ConformNewClassView(GameObject unit, BuffType type) {
		GameObject buff = new GameObject();
		buff.transform.parent = unit.transform;
        buff.transform.localPosition = Vector3.zero;
        buff.transform.localScale = Vector3.one;
        buff.AddComponent<SpriteRenderer>();
		buff.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Buff/" + type.ToString());
		buff.GetComponent<SpriteRenderer>().sortingOrder = (int)type;
	}
}
