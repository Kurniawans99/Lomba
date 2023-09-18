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
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        GameObject Blue = GameObject.Find("Blue");
        GameObject Red = GameObject.Find("Red");
        SaveZoneManage saveZoneBlue = Blue.GetComponentInChildren<SaveZoneManage>();
        SaveZoneManage saveZoneRed = Red.GetComponentInChildren<SaveZoneManage>();

        

        gameManager = FindObjectOfType<GameManager>();
        initialBotStrategy();

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

    void switchCharTeam(int team,int idPlayer)
    {
        // change what team to current id 
    }
    
    void StateAI(int state,int numTeam,int target)
    {
        // if player == idPlayer(state) && !onCatch
        //do the target & excute change it
    }
    void FixedUpdate()
    {
        if (OnBaseBlue == 0)
        {
            //ally defend
            //enmy atack
        }
        else if (OnBaseBlue >= 2)
        {
            //Red idle dont attack
        }

        if (OnBaseRed == 0)
        {
            //ally defend
            // enmy atack
        }
        else if (OnBaseRed >= 2)
        {
            //Blue idle dont attack
        }

        foreach (PlayerManager playerManager in gameManager.players)
        {
            //check player Time
           if(!playerManager.onSaveZone && !playerManager.onCatch) { 
                if(playerManager.playerTimer < 3f)
                {
                    //player new
                    //beware!!
                }else if(playerManager.playerTimer < 10f)
                {
                    //old player
                    // get that On target
                }
            
            }

            


        }

  
    }

    private void getTargetPos(int who,int idTarget)
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
                target = targetObject.transform;
            }
            else
            {
                GameObject targetObject = GameObject.Find("CastleBlue");
                target = targetObject.transform;

            }


        }
        else if (who == 2)
        {
            if (playerController.checkTeamPlayer() == 1)
            {
                GameObject targetObject = GameObject.Find("CastleRed");
                target = targetObject.transform;
            }
            else
            {
                GameObject targetObject = GameObject.Find("CastleBlue");
                target = targetObject.transform;
            }

        } else if (who == 3)
        {
            //find pos player with idTarget
        }
        else if (who == 4)
        {
            //find pos self with idTarget trasnfrom.position
        }

    }
}
