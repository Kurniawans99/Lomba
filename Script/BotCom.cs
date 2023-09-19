using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class team
{
    public int idPlayerOnAttackBase, idPlayerOnDeffend, idPlayerOnAttackEnemy, idPlayerOnAvoid, idPlayerOnRelease;
    //theres no zero
}

public class BotCom : MonoBehaviour
{
    private GameManager gameManager;
    private int OnBaseBlue, OnBaseRed;
    team blueTeam = new team();
    team redTeam = new team();
    private PlayerController playerController;
    private BotDummy botDummy;
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        GameObject Blue = GameObject.Find("Blue");
        GameObject Red = GameObject.Find("Red");
        SaveZoneManage saveZoneBlue = Blue.GetComponentInChildren<SaveZoneManage>();
        SaveZoneManage saveZoneRed = Red.GetComponentInChildren<SaveZoneManage>();

        

        gameManager = FindObjectOfType<GameManager>();
        //initialBotStrategy();

    }

    void initialBotStrategy()
    {
        blueTeam.idPlayerOnAttackEnemy = 2;
        blueTeam.idPlayerOnDeffend = 3;
        blueTeam.idPlayerOnAttackBase = 4;

        redTeam.idPlayerOnAttackEnemy = 2;
        redTeam.idPlayerOnDeffend = 3;
        redTeam.idPlayerOnAttackBase = 4;

    }

    void switchCharTeam(int currentId,int newIdPlayer, int team)
    {

        // change what team to current id 
        // newIdPlayer.botDummy is must be stopped from AINav
        // current id will play the old navmesh
    }
    
    void StateAI(PlayerManager player,BotState state,int numTeam,Transform target)
    {
        player.botDummy.currentState = state;
        player.botDummy.target = target;
        // if player == idPlayer(state) && !onCatch
        //do the target & excute change it
    }

    void FixedUpdate()
    {
        OnBaseBlue = 0;
        OnBaseRed = 0;
        foreach (PlayerManager playerManager in gameManager.players)
           {
            
            if (playerManager.team==0 && playerManager.onSaveZone) {
                OnBaseBlue++;
            }
            if (playerManager.team==1 && playerManager.onSaveZone)
            {
                OnBaseRed++;
            }
                
        }
        foreach (PlayerManager playerManager in gameManager.players)
        {
            if (!playerManager.botDummy.onAvoidingPlayer)
            {
                if (OnBaseBlue == 0)
                {
                    if (redTeam.idPlayerOnAttackBase == playerManager.idPlayer && playerManager.team == 1)
                    {
                        StateAI(playerManager, BotState.AttackBase, playerManager.team, GetTargetPos(2, playerManager));
                    }
                    //ally defend
                    //enmy atack
                }
                else if (OnBaseBlue >= 2)
                {
                    // attack blue base said red
                    if (redTeam.idPlayerOnAttackBase == playerManager.idPlayer && playerManager.team == 1)
                    {
                        StateAI(playerManager, BotState.AttackBase, playerManager.team, playerManager.transform);
                    }
                }

                if (OnBaseRed == 0)
                {
                    if (blueTeam.idPlayerOnAttackBase == playerManager.idPlayer && playerManager.team == 0)
                    {
                        StateAI(playerManager, BotState.AttackBase, playerManager.team, GetTargetPos(2, playerManager));
                    }
                }
                else if (OnBaseRed >= 2)
                {
                    if (blueTeam.idPlayerOnAttackBase == playerManager.idPlayer && playerManager.team == 0)
                    {
                        StateAI(playerManager, BotState.AttackBase, 0, playerManager.transform);
                    }
                }
            }
                

                //check player Time attack Player --> wanna change to raycast?
                /*
                if (!playerManager.onSaveZone && !playerManager.onCatch)
                {
                    if (playerManager.playerTimer < 3f)
                    {

                        //player new
                        //beware!!
                    }
                    else if (playerManager.playerTimer < 10f)
                    {
                        //old player
                        // get that On target
                    }

                }
                */
            
            

        }

  
    }


    private Transform GetTargetPos(int who, PlayerManager players)
    {
        /* 1. Enemy Base
         2. Ally Base
         3. Enmy human
        4. idle
        */
        if (who == 1)
        {
            if (playerController.checkTeamPlayer() == 0)
            {
                GameObject targetObject = GameObject.Find("CastleRed");
                return targetObject.transform;
            }
            else
            {
                GameObject targetObject = GameObject.Find("CastleBlue");
                return targetObject.transform;

            }


        }
        else if (who == 2)
        {
            if (players.team == 1)
            {
                GameObject targetObject = GameObject.Find("CastleRed");
                return targetObject.transform;
            }
            else if(players.team == 0)
            {
                GameObject targetObject = GameObject.Find("CastleBlue");
                return targetObject.transform;
            }

        } else if (who == 3)
        {
            //find pos player with idTarget
        }
        else if (who == 4)
        {
            //find pos self with idTarget trasnfrom.position
        }
        return transform;
    }

    public void OnRealeaseFromArrest()
    {
        // switch StateMent -> run to defend base
    }
}
