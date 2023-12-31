using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public float playerTimer = 0f;
    public bool onCatch = false;
    [SerializeField] public bool onSaveZone = false;
    [SerializeField] private bool onControl = false;
    [SerializeField] public int team = 0;
    [SerializeField] public int idPlayer = 0;
    public bool gameStarted = false;
    private bool timePlaying = true;
    /*    [SerializeField] private CastleScript castle;*/

    public Vector3 intialSpawnPos;
    private PlayerController playerController;
    private GameManager gameManager;
    private SpawnPoint spawnPoint;
    public BotDummy botDummy;
    private FollowCameraScript cameraQ;
    public bool gotPlayer;

    public GameObject ball;
    void Start()
    {
        botDummy = GetComponent<BotDummy>();

        playerController = GetComponent<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        SpawnPoint spawnPoint = FindObjectOfType<SpawnPoint>();
        intialSpawnPos = transform.position;

        foreach (FollowCameraScript cameraObject in FindObjectsOfType<FollowCameraScript>())
        {
            if (cameraObject.CameraTeam == team)
            {
                cameraQ = cameraObject; // Found the desired camera
                break; // Exit the loop since we found it
            }
        }


    }
    void Update()
    {
        if (gameStarted && timePlaying)
        {
            playerTimer += Time.deltaTime;


           /* float normalizedTime = Mathf.Clamp01(playerTimer / 60f);

            // Interpolate between startColor and endColor
            Color lerpedColor = Color.Lerp(Color.green, Color.red, normalizedTime);

            // Apply the color to the ball's material
            Renderer renderer = ball.GetComponent<Renderer>();
            renderer.material.color = lerpedColor;
            // ... (other logic)*/
        }

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
        if (playerTimer < otherPlayerManager.playerTimer && !onCatch)
        {
            gotPlayer = true;
            Debug.Log("terpegang");
            otherPlayerManager.ArrestPlayer(); 
        }
        else if(playerTimer > otherPlayerManager.playerTimer && !onCatch)   
        {
            gotPlayer= true;
             Debug.Log("dapet org");
            ArrestPlayer();
        }
    }



    /* EVENT PLAYER? SHOULD I MOVE FROM HERE */

        public void SwitchControl()
    {
        cameraQ.SwitchTarget(transform.position, transform);
        playerController.handrise = false;
        playerController.isControlled = true;
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
        if (!playerController.isControlled)
        {
            botDummy.currentState = BotState.BackToBase;
            botDummy.agent.speed = 15f;
        }
    }

    // Logic for entering the safe zone

    public void TouchingCastleAlly()
    {
        playerTimer = 0f;
        // Additional logic for entering the safe zone
    }


}
