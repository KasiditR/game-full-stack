using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [SerializeField] private Dice dice;
    [SerializeField] private DiceManager diceManager;
    private Rigidbody diceRb;
    private void Awake()
    {
        diceRb = dice.GetComponent<Rigidbody>();
    }
    private void OnTriggerStay(Collider other) 
    {
        if (!diceRb.IsSleeping() || diceManager.GetIsSetValue() == true)
        {
            return;
        }
        int diceValue = 0;
        switch (other.name)
        {
            case "1":
                Debug.Log("3");
                diceValue = 3;
                break;
            case "2":
                Debug.Log("6");
                diceValue = 6;
                break;
            case "3":
                Debug.Log("1");
                diceValue = 1;
                break;
            case "4":
                Debug.Log("5");
                diceValue = 5;
                break;
            case "5":
                Debug.Log("4");
                diceValue = 3;
                break;
            case "6":
                Debug.Log("2");
                diceValue = 2;
                break;
            default:
                break;
        }

        diceManager.SetDiceValue(diceValue);
    }
}
