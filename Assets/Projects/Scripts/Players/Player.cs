using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private PlayerHP playerHP;
    private string playerName;
    public int health;
    private Color playerColor = Color.white;
    public MeshRenderer playerRenderer;
    private List<Transform> routes;
    private bool isMoving;
    private int routePosition;
    public Action<Player> onPlayerDie;
    private Corner corner;
    public PlayerHP PlayerHP { get => playerHP; }

    public void SetPlayerColor(Color value)
    {
        playerColor = value;
        playerRenderer.material.SetColor("_Color", playerColor);
        this.playerHP.InitializePlayerHP(GetPlayerColor(), this.playerName, health);
    }

    public Corner Corner { get => corner;}

    public void Initialize(Corner corner, PlayerHP playerHP, string playerName)
    {
        this.corner = corner;
        this.gameObject.name = playerName;
        this.playerName = playerName;
        this.routes = corner.GetRouteFromCorner();
        this.playerHP = playerHP;
    }
    private void OnDestroy()
    {
        onPlayerDie = null;
    }
    public void Move(int steps)
    {
        StartCoroutine(CoroutineMove(steps));
    }
    private IEnumerator CoroutineMove(int steps)
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;
        while (steps > 0)
        {
            routePosition = (routePosition + 1) % routes.Count;

            Vector3 nextPos = routes[routePosition].position;
            if ((transform.position - nextPos).sqrMagnitude > 0.0001f)
            {
                transform.DOLookAt(nextPos, 0.2f, AxisConstraint.Y);
            }
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }
            steps--;
        }

        isMoving = false;

        BasePlatform platform = routes[routePosition].GetComponent<BasePlatform>();
        platform.HandleLandingPlatform(this);

        yield return null;
    }

    private bool MoveToNextNode(Vector3 goal)
    {
        transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
        return transform.position != goal;
    }

    public Color GetPlayerColor()
    {
        return playerColor;
    }
    public void IncreaseHealth(int increaseValue)
    {
        health += increaseValue;
        playerHP.SetHpText(health);
    }
    public void DecreaseHealth(int decreaseValue)
    {
        health -= decreaseValue;
        playerHP.SetHpText(health);
        if (health > 0)
        {
            return;
        }
        onPlayerDie?.Invoke(this);
    }
}
