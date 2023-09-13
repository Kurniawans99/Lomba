using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float playerTimer = 0f;
    [SerializeField] private bool onCatch = false;
    [SerializeField] private bool onSaveZone = false;
    [SerializeField] private bool onControl = false;
    [SerializeField] private int team = 0;
    [SerializeField] private int idPlayer = 0;
    private bool gameStarted = false;

    /*    [SerializeField] private CastleScript castle;*/

    private PlayerController playerController;
/*    private GameManager gameManager;*/

    void Start()
    {
       
        playerController = GetComponent<PlayerController>();
   /*     gameManager = FindObjectOfType<GameManager>();*/
    }
    void Update()
    {
        if (gameStarted)
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
        }
        // Check if the respective key is pressed
        /*if ((playerManager.idPlayer == 1 && Input.GetKeyDown(KeyCode.E)) ||
            (playerManager.idPlayer == 2 && Input.GetKeyDown(KeyCode.I)))
        {
            TryTouching();
        }*/
    }

    private void TryTouching()
    {
        // Raycast to detect if the player is touching another player
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f))
        {
            PlayerManager otherPlayerManager = hit.collider.GetComponent<PlayerManager>();

            if (otherPlayerManager != null && otherPlayerManager.team != this.team)
            {
                // Notify the PlayerManager that this player is touching another player
                otherPlayerManager.OnTouchingOtherPlayer(this);
            }
        }

    }

    public void OnTouchingOtherPlayer(PlayerManager otherPlayerManager)
    {
        if (playerTimer > otherPlayerManager.playerTimer)
        {
            otherPlayerManager.ArrestPlayer();
        }
        else
        {
            ArrestPlayer();
        }
    }
    public void ArrestPlayer()
    {
        onCatch = true;
        playerTimer = 0f;
        // Additional logic for being caught -> got stuck on save zone enemy
    }
    // Logic for releasing hand from the castle
    public void ReleaseFromCastle()
    {
        onCatch = false;
        playerTimer = 0f;
        // Additional logic for releasing from the castle
    }

    // Logic for entering the safe zone
    public void EnterSafeZone()
    {
        onSaveZone = true;
        // Additional logic for entering the safe zone
    }

    // Logic for switching control between players on the same team
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
    }
}
