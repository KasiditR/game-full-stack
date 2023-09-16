using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DiceManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button rollButton;
    [Space]
    [SerializeField] private int diceValue;
    [SerializeField] private Dice dice;
    private Player targetPlayer;
    private bool isSetValue = true;


    private void Start()
    {
        rollButton.onClick.AddListener(() => { DiceRoll(); });
    }
    private void OnDestroy()
    {
        rollButton.onClick.RemoveListener(() => { DiceRoll(); });
    }
    public void OpenButton()
    {
        rollButton.gameObject.SetActive(true);
    }
    public void SetTargetDice(Player player)
    {
        targetPlayer = player;
    }
    private void DiceRoll()
    {
        rollButton.gameObject.SetActive(false);
        isSetValue = false;
        dice.Roll();
    }
    public bool GetIsSetValue()
    {
        return isSetValue;
    }

    public int GetDiceValue()
    {
        return diceValue;
    }

    public void SetDiceValue(int value)
    {
        isSetValue = true;
        diceValue = value;
        targetPlayer.Move(diceValue);
    }
}
