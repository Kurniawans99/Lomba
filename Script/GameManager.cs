using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    private int Round = 0;
    public List<PlayerManager> players = new List<PlayerManager>();
    private int[] teamPoints;
    private int[] arrestedPlayersTeam;
    private int totalPlayerTeam = 4;

    
       


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
            StartNewRound();
        }
       
    }
    private void IncrementTeamPoints(int team)
    {
        
        teamPoints[team]++;
        Debug.Log(teamPoints[team]);
    }

    private void StartNewRound()
    {
        // Reset player positions, timers, or any other relevant game state
        foreach (PlayerManager player in players)
        {
            player.gameStarted = false;
         
        }
        // reSpawn();
        // GameBegin(); // wait until 3sec
        loadRound = false;
    }

    void UpdatePointsDisplay()
    {
       // pointsDisplay.text = "Team 1: " + teamPoints[0] + " | Team 2: " + teamPoints[1];
    }

    public void GamePause() { }
    public void GameResume() { }
    public void GameBackHome() { }

    public void GameExit() { }
}
