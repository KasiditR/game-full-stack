using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChoosePlyer : MonoBehaviour
{
    [SerializeField] private Image bg;
    [SerializeField] private RectTransform circle;
    [SerializeField] private PlayerItem[] playerItems;
    public void StartGame()
    {
        InitGame();
    }

    private void InitGame()
    {
        for (int i = 0; i < GameManager.Instance?.Players.Count; i++)
        {
            int playerIndex = i;
            playerItems[i].InitializePlayerItem(GameManager.Instance?.Players[i],playerIndex,OnClickPlayFirst);
        }
    }

    private void OnClickPlayFirst(Color color, int playerIndex)
    {
        SetButtonDisable(playerIndex);
        bg.DOColor(color, 0.2f).SetEase(Ease.Linear);
        ChooseThisPlayer(playerIndex);
        circle.DOSizeDelta(Vector2.zero, 0.8f, false).SetEase(Ease.Linear).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }

    private void SetButtonDisable(int index)
    {
        for (int i = 0; i < playerItems.Length; i++)
        {
            if (index != i)
            {
                playerItems[i].PlayerCloseButton();
            }
        }
    }
    public void ChooseThisPlayer(int index)
    {
        GameManager.Instance?.SetPlayerStart(index);
    }
}
