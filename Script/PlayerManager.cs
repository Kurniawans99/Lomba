using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float playerTimer = 0f;
    public bool onCatch = false;
    [SerializeField] public bool onSaveZone = false;
    [SerializeField] private bool onControl = false;
    [SerializeField] public int team = 0;
    [SerializeField] private int idPlayer = 0;
    public bool gameStarted = false;
    private bool timePlaying = true;
    /*    [SerializeField] private CastleScript castle;*/

    public Vector3 intialSpawnPos;
    private PlayerController playerController;
    private GameManager gameManager;
    private SpawnPoint spawnPoint;

    void Start()
    {
       
        playerController = GetComponent<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        SpawnPoint spawnPoint = FindObjectOfType<SpawnPoint>();
        intialSpawnPos = transform.position;



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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SaveZone")
        {
            SaveZoneManage zoneTeam = other.GetComponent<SaveZoneManage>();

            if (zoneTeam.saveZoneTeam == team) {
                onSaveZone = true;
            }
            

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "SaveZone")
        {
            SaveZoneManage zoneTeam = other.GetComponent<SaveZoneManage>();
            if (zoneTeam.saveZoneTeam == team)
            {
                onSaveZone = false;
            }

        }
    }
        public void OnTouchingOtherPlayer(PlayerManager otherPlayerManager)
    {
        if (playerTimer > otherPlayerManager.playerTimer && !onCatch)
        {
            Debug.Log("terpegang");
            otherPlayerManager.ArrestPlayer(); 
        }
        else if(playerTimer < otherPlayerManager.playerTimer && !onCatch)   
        {
             Debug.Log("dapet org");
            ArrestPlayer();
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

        foreach (SpawnPoint spawnPoint in gameManager.spawnPoints)
        {
            if (spawnPoint.teamSpawn != team)
            {
                // Move the player to the spawn point
                transform.position = spawnPoint.transform.position;
                // Additional logic for being caught -> got stuck on save zone enemy

                gameManager.Catching(team, true);
                break; // Exit the loop once a suitable spawn point is found
            }
        }
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

    public void TouchingCastleAlly()
    {
        playerTimer = 0f;
        // Additional logic for entering the safe zone
    }


}
