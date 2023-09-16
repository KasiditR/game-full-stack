using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : BasePlatform
{
    [SerializeField] private List<Transform> routes;
    private Player ownPlatform;
    public void InitializeCorner(Player player)
    {
        ownPlatform = player;
        initColor = ownPlatform.GetPlayerColor();
        //
        base.InitializePlatForm();
    }
    public List<Transform> GetRouteFromCorner()
    {
        return routes;
    }
    public override void HandleLandingPlatform(Player player)
    {
        if (player == ownPlatform)
        {
            ownPlatform.IncreaseHealth(3);
        }
        else
        {
            player.IncreaseHealth(1);
        }
        this.HandleSetPositionPlayerInPlatform(player);
        GameManager.Instance?.PlayNextTurn();
    }
    
}
