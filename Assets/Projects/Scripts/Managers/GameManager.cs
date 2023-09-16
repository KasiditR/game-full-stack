using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<Player> Players { get => players; }
    public Color[] PlayerColors { get => playerColors;}

    [SerializeField] private DiceManager diceManager;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private PanelAcceptCancel addEdgePanel;
    [SerializeField] private PanelAcceptCancel restartPanel;
    [SerializeField] private ChoosePlyer choosePlyer;
    [SerializeField] private int currentPlayerIndex = 0;
    [SerializeField] private List<Player> players = new List<Player>();
    [SerializeField] private List<Corner> corners = new List<Corner>();
    [SerializeField] private BasePlatform[] allPlatform;
    [SerializeField] private PlayerHP[] playerHPs;
    [SerializeField] private Color[] playerColors = new Color[MAX_PLAYER]; 
    private string[] playerName = new string[] { "PlayerOne", "PlayerTwo", "PlayerThree", "PlayerFour" };
    public const int MAX_PLAYER = 4;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
        InitializeGame();
    }
    private void Start()
    {
        choosePlyer.StartGame();
    }
    public void SetPlayerStart(int index)
    {
        currentPlayerIndex = index;
        PlayNextTurn();
    }
    public void OpenUIEdgePanel(string info, Action onAccept, Action onCancel)
    {
        addEdgePanel.OpenEdgePanel(info, onAccept, onCancel);
    }
    public void OpenUIRestartPanel(string info, Action onAccept, Action onCancel)
    {
        restartPanel.OpenEdgePanel(info, onAccept, onCancel);
    }
    private void InitializeGame()
    {
        foreach (BasePlatform platform in allPlatform)
        {
            platform.InitializePlatForm();
        }
        CreatePlayer();
    }
    public void CreatePlayer()
    {
        for (int i = 0; i < MAX_PLAYER; i++)
        {
            //Init
            Player player = Instantiate(playerPrefab).GetComponent<Player>();
            // player.PlayerColor = playerColors[i];
            player.Initialize(corners[i], playerHPs[i], playerName[i]);
            player.onPlayerDie += OnPlayerDie;

            //Set Position
            player.transform.position = corners[i].route.transform.position;
            player.transform.rotation = corners[i].route.transform.rotation;

            players.Add(player);
        }
    }
    public void InitCorner()
    {
        for (int i = 0; i < MAX_PLAYER; i++)
        {
            corners[i].InitializeCorner(players[i]);
        }
    }
    public void PlayNextTurn()
    {
        Player currentPlayer = players[currentPlayerIndex];
        currentPlayer.PlayerHP.PlayerInTurn();
        for (int i = 0; i < players.Count; i++)
        {
            if (currentPlayerIndex != i)
            {
                players[i].PlayerHP.PlayerOffTurn();
            }
        }
        //Set Dice Manager
        diceManager.SetTargetDice(currentPlayer);
        diceManager.OpenButton();
        //
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
    }
    private void OnPlayerDie(Player player)
    {
        players.Remove(player);
        if (players.Count == 1)
        {
            GameOver(player);
            return;
        }
        Destroy(player.gameObject);
    }
    public void GameOver(Player player)
    {
        OpenUIRestartPanel($"{player.name} Win!!!!", () =>
        {
            Destroy(player.gameObject);
            players.Clear();
            InitializeGame();
            choosePlyer.StartGame();
        }, () => Application.Quit());
    }
}
