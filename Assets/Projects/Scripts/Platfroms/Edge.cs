using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : BasePlatform
{
    [SerializeField] private SpriteRenderer[] dots;
    [SerializeField] private List<Player> players;
    private Color tmpColorDot;
    private Color tmpColorEdge;
    private int index;
    public override void InitializePlatForm()
    {
        base.InitializePlatForm();
        tmpColorEdge = initColor;
        tmpColorDot = dots[0].color;
    }
    public override void HandleLandingPlatform(Player player)
    {
        if (players.Count >= 3 && !players.Contains(player))
        {
            player.DecreaseHealth(3);
            foreach (var item in players)
            {
                Debug.Log(item.gameObject.name);
            }
            players.Clear();
            this.SetColorPlatform(tmpColorEdge);
            foreach (SpriteRenderer dot in dots)
            {
                dot.color = tmpColorDot;
            }
        }
        Action accept = () =>
        {
            AddColorInEdge(player);
            GameManager.Instance?.PlayNextTurn();
        };
        Action cancel = () =>
        {
            GameManager.Instance?.PlayNextTurn();
        };
        GameManager.Instance?.OpenUIEdgePanel("Decrease Health for stretch the area", accept, cancel);
        this.HandleSetPositionPlayerInPlatform(player);
    }
    
    public void AddColorInEdge(Player player)
    {
        if (players.Count >= dots.Length)
        {
            return;
        }
        //
        player.DecreaseHealth(1);
        players.Add(player);
        dots[index].color = player.GetPlayerColor();
        index = (index + 1) % dots.Length;
        //
        if (IsPlayTakePlatForm())
        {
            SetColorPlatform(player.GetPlayerColor());
        }
    }
    private bool IsPlayTakePlatForm()
    {
        if (players.Count != 3)
        {
            return false;
        }
        Player firstPlayer = players[0];
        foreach (Player player in players)
        {
            if (player != firstPlayer)
            {
                return false;
            }
        }
        return true;
    }
}
