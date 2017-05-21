using System;
using System.Collections;

using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses;

public class MoneyManager : MonoBehaviour
{
    public int CurrentMoney = 500;
    public float MoneyIncreaseFrequency = 5.0f;
    public float MoneyIncreaseValue = 1.0f;

    private void Start ()
    {
        StartCoroutine(MoneyIncrease());
	}

    private IEnumerator MoneyIncrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(MoneyIncreaseFrequency);

            CurrentMoney += Convert.ToInt32(PersonManager.Instance.PersonCount * MoneyIncreaseValue);
        }
    }
}
