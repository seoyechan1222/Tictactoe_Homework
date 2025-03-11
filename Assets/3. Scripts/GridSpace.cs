using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    
    private GameController myController;
    
    public Button myButton;
    public TextMeshProUGUI buttonText;
    public string playerSide;

    private void Start()
    {
        myButton = GetComponent<Button>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetSpace()
    {
        buttonText.text = myController.GetPlayerSide();
        myButton.interactable = false;
        myController.EndTurn();
    }

    public void SetController(GameController controller)
    {
        myController = controller;
    }
}