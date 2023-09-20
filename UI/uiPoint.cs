using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class uiPoint : MonoBehaviour
{
    VisualElement root;
    
    Label totalRound;
    Label pointB;
    Label pointR;
    VisualElement popwin;
    Label titleWin;
    Label subtitleWin;
    Button bResume;
    Button bExit;
    Button bHome;

    int round = 0;
    int bluePoint = 0;
    int redPoint = 0;
    private void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
         totalRound = root.Q<Label>("totalRound");
         pointB = root.Q<Label>("blueTeamPoint");
        pointR = root.Q<Label>("redTeamPoint");

        popwin = root.Q<VisualElement>("popwin");
        titleWin = root.Q<Label>("Title");
        subtitleWin = root.Q<Label>("subTitle");
        bResume = root.Q<Button>("bResume");
        bExit = root.Q<Button>("bExit");
        bHome = root.Q<Button>("bHome");

    }
    private void Start()

    {
        popwin.style.display = DisplayStyle.None;
        GameManager.OnStageClear += GameManager_OnStageClear;
        GameManager.OnPointUpdate += GameManager_OnPointUpdate;
        GameManager.OnPause += GameManager_OnPause;
        GameManager.OnUnPause += GameManager_OnUnPause;
        GameManager.OnWin += GameManager_OnWin;
        titleWin.text = "PAUSED";
        bResume.clicked += () =>
        {
            GameManager.Instance.GamePaused();

        };
    }

    private void GameManager_OnWin(object sender, GameManager.OnWinEventArgs e)
    {
        titleWin.text = e.teamWin;
        bResume.style.display = DisplayStyle.None;

        GameManager.Instance.GamePaused();

    }

    private void GameManager_OnUnPause(object sender, EventArgs e)
    {
        popwin.style.display = DisplayStyle.None;

    }

    private void GameManager_OnPause(object sender, EventArgs e)
    {
        popwin.style.display = DisplayStyle.Flex;
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
