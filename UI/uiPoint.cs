using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class uiPoint : MonoBehaviour
{
    VisualElement root;
    Label totalRound;
    Label pointB;
    Label pointR;
    int round = 0;
    int bluePoint = 0;
    int redPoint = 0;
    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
         totalRound = root.Q<Label>("totalRound");
         pointB = root.Q<Label>("blueTeamPoint");
         pointR = root.Q<Label>("redTeamPoint");
    }
    private void Start()
   
    {
        GameManager.OnStageClear += GameManager_OnStageClear;
        GameManager.OnPointUpdate += GameManager_OnPointUpdate;
    }

    private void GameManager_OnStageClear(object sender, EventArgs e)
    {
        round++;
        updateUITotalRound(round);
    }

    void updateUITotalRound(int total)
    {
        totalRound.text =  total.ToString();
    }

    // Start is called before the first frame update
   
    private void GameManager_OnPointUpdate(object sender, GameManager.OnPointUpdateEventArgs e)
    {
        if(e.point == 0)
        {
            bluePoint++;
            pointB.text = bluePoint.ToString(); 
        }if(e.point == 1)
        {
            redPoint++;
            pointR.text = redPoint.ToString(); 
        }
    }
    
}
