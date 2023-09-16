using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private TMP_Text playerNameText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Image playerTurn;

    public void InitializePlayerHP(Color color, string playerNameText, int hp)
    {
        this.bg.color = color;
        this.playerNameText.text = playerNameText;
        SetHpText(hp);
    }
    public void SetHpText(int hpValue)
    {
        this.hpText.text = $"Hp : {hpValue}";
    }
    public void PlayerInTurn()
    {
        playerTurn.gameObject.SetActive(true);
        playerTurn.rectTransform.sizeDelta = Vector2.zero;
        playerTurn.rectTransform.DOSizeDelta(new Vector2(450,200),0.5f,false).SetEase(Ease.Linear);
    }
    public void PlayerOffTurn()
    {
        playerTurn.gameObject.SetActive(false);
    }
    
}
