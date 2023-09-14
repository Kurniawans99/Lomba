using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float playerTimer = 0f;
    public bool onCatch = false;
    [SerializeField] private bool onSaveZone = false;
    [SerializeField] private bool onControl = false;
    [SerializeField] public int team = 0;
    [SerializeField] private int idPlayer = 0;
    public bool gameStarted = false;
    private bool timePlaying = true;
    /*    [SerializeField] private CastleScript castle;*/

    private PlayerController playerController;
    private GameManager gameManager;

    void Start()
    {
       
        playerController = GetComponent<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        if (gameStarted && timePlaying)
        {
            playerTimer += Time.deltaTime;

            // ... (other logic)
        }
        // Logic for timer, catching, safe zone, etc.
        if (onCatch)
        {
            // Additional logic for being caught
        }

        if (onSaveZone)
        {
            // Additional logic for being in the safe zone
        }

        if (onControl)
        {

            // Additional logic for being controlled 
            //asign to bot to not controll 
            //can be move using keyboard 1 or 2

        }
        // Check if the respective key is pressed
        /*if ((playerManager.idPlayer == 1 && Input.GetKeyDown(KeyCode.E)) ||
            (playerManager.idPlayer == 2 && Input.GetKeyDown(KeyCode.I)))
        {
            TryTouching();
        }*/
    }
   

    public void OnTouchingOtherPlayer(PlayerManager otherPlayerManager)
    {
        if (playerTimer > otherPlayerManager.playerTimer)
        {
            Debug.Log("terpegang");
            //  otherPlayerManager.ArrestPlayer(); 
        }
        else
        {
             Debug.Log("dapet org");
          //  ArrestPlayer();
        }
    }



    /* EVENT PLAYER? SHOULD I MOVE FROM HERE */

        public void SwitchControl(int newPlayerID)
    {
        /*        PlayerManager newPlayer = GameManager.GetPlayerByID(newPlayerID);

                if (newPlayer != null && newPlayer.team == team && newPlayer.onControl)
                {
                    newPlayer.onControl = false;
                    onControl = true;
                    playerController.enabled = true;
                    newPlayer.playerController.enabled = false;
                }*/

        // Logic for switching control between players on the same team
    }
    public void ArrestPlayer()
    {
        onCatch = true;
        timePlaying = false;
        // Additional logic for being caught -> got stuck on save zone enemy

        gameManager.Catching(team, true);
    }


    // Logic for releasing hand from the castle
    public void ReleaseFromArrest()
    {
        onCatch = false;
        timePlaying = true;
        // Additional logic for releasing from the castle enemy
        gameManager.Catching(team, false);
    }

    // Logic for entering the safe zone
    public void EnterSafeZone()
    {
        onSaveZone = true;
        // Additional logic for entering the safe zone
    }

    public void TouchingCastleAlly()
    {
        playerTimer = 0f;
        // Additional logic for entering the safe zone
    }


}
