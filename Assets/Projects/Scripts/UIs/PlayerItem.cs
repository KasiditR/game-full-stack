using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerItem : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private Button buttonPlayerFirst;
    [SerializeField] private Button buttonChooseColor;
    [SerializeField] private Button[] buttonColors;
    private Color playerColor;
    public void InitializePlayerItem(Player player,int playerIndex,Action<Color,int> onClickPlayFirst)
    {
        buttonPlayerFirst.gameObject.SetActive(true);
        buttonChooseColor.gameObject.SetActive(true);
        //Player Color
        for (int i = 0; i < buttonColors.Length; i++)
        {
            Image buttonImg = buttonColors[i].GetComponent<Image>();
            buttonImg.color = GenerateRandomColor();
            Color color = buttonImg.color;
            buttonColors[i].onClick.AddListener(() =>
            {
                playerColor = color;
                bg.color = color;
                player.SetPlayerColor(color);
            });
        }
        buttonPlayerFirst.onClick.AddListener(() =>
        {
            onClickPlayFirst?.Invoke(playerColor,playerIndex);
        });
    }
    public void PlayerCloseButton()
    {
        buttonPlayerFirst.gameObject.SetActive(false);
        buttonChooseColor.gameObject.SetActive(false);
        foreach (Button button in buttonColors)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }
    private Color GenerateRandomColor()
    {
        float r = UnityEngine.Random.value;
        float g = UnityEngine.Random.value;
        float b = UnityEngine.Random.value;

        return new Color(r, g, b);
    }
}
