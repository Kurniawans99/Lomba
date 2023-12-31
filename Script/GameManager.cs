using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static event EventHandler<OnWinEventArgs> OnWin;
    public class OnWinEventArgs : EventArgs
    {
        public string teamWin;
    }
    public static event EventHandler OnStageClear; 
    public static event EventHandler OnPause; 
    public static event EventHandler OnUnPause; 
    public static event EventHandler<OnPointUpdateEventArgs> OnPointUpdate;
    [SerializeField] private GameInput1 gameInput1;
    public class OnPointUpdateEventArgs
    {
       public int point;
    }
    private int Round = 0;
    public List<PlayerManager> players = new List<PlayerManager>();
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    private int[] teamPoints;
    private int[] arrestedPlayersTeam;
    private int totalPlayerTeam = 4;
    public Transform[] points;

    public int currentIdA = 1;
    public int currentIdB = 1;

    public uiPoint uipoint;
    private bool isPaused = false;
    private bool gameDone = false;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (PlayerManager player in players)
        {
            player.gameStarted = true;
        }
        arrestedPlayersTeam = new int[2];
        arrestedPlayersTeam[0] = 0;
        arrestedPlayersTeam[1] = 0;
        teamPoints = new int[2];
        teamPoints[0] = 0;
        teamPoints[1] = 0;

        uipoint = FindObjectOfType<uiPoint>();
        gameInput1.OnPause += GameInput1_OnPause;


    }

    private void GameInput1_OnPause(object sender, EventArgs e)
    {
        GamePaused();
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    public void GamePaused()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            OnPause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;
            OnUnPause?.Invoke(this, EventArgs.Empty);

        }
    }
    public void Catching(int teamNumber, bool onCatch)
    {
        if (onCatch)
        {
            arrestedPlayersTeam[(int)teamNumber]++ ;
        }else if(onCatch == false) {

            arrestedPlayersTeam[(int)teamNumber]--;
        }

        if (arrestedPlayersTeam[teamNumber] == totalPlayerTeam)
        {
            IncrementTeamPoints(teamNumber);
            StartNewRound();
            UpdatePointsDisplay();
        }
        else if (arrestedPlayersTeam[teamNumber] == totalPlayerTeam)
        {
            IncrementTeamPoints(teamNumber);
            StartNewRound();
            UpdatePointsDisplay();
        }
    }

    private bool loadRound = false;
    public void WinRound(int wutTeam)
    {
        if (!loadRound)
        {
            loadRound = true;
            teamPoints[wutTeam]++;
            IncrementTeamPoints(wutTeam);
            Debug.Log(Round);
            if(Round >= 3 && teamPoints[0] != teamPoints[1])
            {
                //UI Game End -> back home -> playagain
                if(teamPoints[0] > teamPoints[1])
                {
                    gameDone = true;
                    OnWin?.Invoke(this, new OnWinEventArgs
                    {
                        teamWin = "Blue Win"
                    });
                }
                else 
                {
                    gameDone = true;

                    OnWin?.Invoke(this, new OnWinEventArgs
                    {
                        teamWin = "Red Win"
                    });
                }
               
            }
                    foreach (PlayerManager player in players)
                    {
                        player.gameStarted = false;
                        player.TouchingCastleAlly();

                    }


                if(gameDone == false)
                {

                  StartNewRound();
                }


        }



    }
    private void IncrementTeamPoints(int team)
    {
        
        
        OnPointUpdate?.Invoke(this, new OnPointUpdateEventArgs
        {
            point = team
        }) ;
        //iuupdate()
        Debug.Log(teamPoints[team]);
    }

    private void StartNewRound()
    {
        UpdatePointsDisplay();
        arrestedPlayersTeam[0] = 0;
        arrestedPlayersTeam[1] = 0;

        currentIdA = 0;
        currentIdB = 0;

        
        // Reset player positions, timers, or any other relevant game state
        respawn();
        Round++;
        // update UI
       // new WaitForSeconds(3);
        // 
        // GameBegin(); // wait until 3sec
        
        
    }

    void UpdatePointsDisplay()
    {
        // pointsDisplay.text = "Team 1: " + teamPoints[0] + " | Team 2: " + teamPoints[1];
        // updateUITotal()
        //Round,  teamPoints[0] (blue),  teamPoints[1] (red tearm)
        OnStageClear?.Invoke(this, EventArgs.Empty);
    }

    private void respawn()
    {
        foreach (PlayerManager playerManager in players)
        {
            playerManager.transform.position = playerManager.intialSpawnPos;
            playerManager.onCatch = false;
            playerManager.gameStarted = true;
            playerManager.botDummy.readyFight = false;
            playerManager.botDummy.currentState= BotState.BackToBase;
            playerManager.botDummy.agent.speed = 15f;

        }
       
        loadRound = false;
    }
    public void GameResume() { }
    public void GameBackHome() { }

    public void GameExit() { }
}
