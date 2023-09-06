using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;

public class uiMain : MonoBehaviour
{
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button bPlay = root.Q<Button>("ButtonPlay");
        Button bTutorial = root.Q<Button>("ButtonTutorial");
        Button bOption = root.Q<Button>("ButtonOption");
        Button bcredit = root.Q<Button>("ButtonCredit");
        Button bExit = root.Q<Button>("ButtonExit");

        bPlay.clicked += () => {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        };
    }
}
