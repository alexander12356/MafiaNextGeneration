using System;
using System.Collections;

using UnityEngine;

using MafiaNextGeneration.PersonSystemClasses;

public class MoneyManager : MonoBehaviour
{
    private static MoneyManager m_Instance;
	public static MoneyManager Instance{
		get{ 
			return m_Instance;
		}
	}

    public int CurrentMoney = 500;
    public float MoneyIncreaseFrequency = 5.0f;
    public float MoneyIncreaseValue = 1.0f;

    private void Awake()
    {
        m_Instance = this;
    }

    private void Start ()
    {
        StartCoroutine(MoneyIncrease());
	}

    private IEnumerator MoneyIncrease()
    {
        while (true)
        {
            yield return new WaitForSeconds(MoneyIncreaseFrequency);

            if (PersonManager.Instance)
            {
                CurrentMoney += Convert.ToInt32(PersonManager.Instance.PersonCount * MoneyIncreaseValue);
            }
        }
    }
}
