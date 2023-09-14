using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    private int Round = 0;
    public List<PlayerManager> players = new List<PlayerManager>();
    private int[] teamPoints = new int[2];
    public int[] arrestedPlayersTeam;
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

    }

    // Update is called once per frame
    void Update()
    {
        
/*        if (arrestedPlayersTeam[0] == totalPlayerTeam)
        {
            IncrementTeamPoints(0);
            StartNewRound();
            UpdatePointsDisplay();
        }
        else if (arrestedPlayersTeam[1] == totalPlayerTeam)
        {
            IncrementTeamPoints(1);
            StartNewRound();
            UpdatePointsDisplay();
        }*/
    }


    public void IncrementTeamPoints(int team)
    {
        teamPoints[team - 1]++; // Assuming team IDs start from 1
    }

    public void StartNewRound()
    {
        // Reset player positions, timers, or any other relevant game state
    }

    void UpdatePointsDisplay()
    {
       // pointsDisplay.text = "Team 1: " + teamPoints[0] + " | Team 2: " + teamPoints[1];
    }
}
