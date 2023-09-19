using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    private int Round = 1;
    public List<PlayerManager> players = new List<PlayerManager>();
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    private int[] teamPoints;
    private int[] arrestedPlayersTeam;
    private int totalPlayerTeam = 4;
    public Transform[] points;





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




    }

    // Update is called once per frame
    void Update()
    {

        
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
            IncrementTeamPoints(wutTeam);
            Debug.Log(Round);
            if(Round >= 3 && teamPoints[0] != teamPoints[1])
            {
                //UI Game End -> back home -> playagain
                if(teamPoints[0] > teamPoints[1])
                {
                    Debug.Log("BLUE"); //wins
                }else
                {
                    Debug.Log("Red");//wins
                }
            }
            

            foreach (PlayerManager player in players)
            {
                player.gameStarted = false;
                player.TouchingCastleAlly();

            }
            
            StartNewRound();
        }



    }
    private void IncrementTeamPoints(int team)
    {
        
        teamPoints[team]++;
        //UI Win 
        Debug.Log(teamPoints[team]);
    }

    private void StartNewRound()
    {
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
    }

    private void respawn()
    {
        foreach (PlayerManager playerManager in players)
        {
            playerManager.transform.position = playerManager.intialSpawnPos;
            playerManager.onCatch = false;
            playerManager.gameStarted = true;
            // Other logic for respawning the player

        }
        arrestedPlayersTeam[0] = 0;
        arrestedPlayersTeam[1] = 0;
        loadRound = false;
    }
    public void GamePause() { }
    public void GameResume() { }
    public void GameBackHome() { }

    public void GameExit() { }
}
