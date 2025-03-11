using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Player
{
    public Image panel;
    public TextMeshProUGUI text;
}

[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{
    public Player playerX;
    public Player playerO;

    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;

    public GameObject restartButton;
    
    public TextMeshProUGUI[] buttonList;
    private string playerSide;
    private int moveCount;

    private void Awake()
    {
        playerSide = "X";
        SetPlayerColors(playerX, playerO);
        restartButton.SetActive(false);
        moveCount = 0;
        gameOverPanel.SetActive(false);
        SetControllerOnButtons();
    }

    void SetControllerOnButtons()
    {
        foreach (var button in buttonList)
        {
            button.GetComponentInParent<GridSpace>().SetController(this);
        }
    }
    
    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        moveCount++;
        SetPlayerColors(playerX, playerO);

        if (CheckForWinner())
        {
            GameOver(playerSide);
        }
        else if (moveCount >= 9)
        {
            GameOver("무승부");
        }
        else
        {
            ChangeSides();
            if (playerSide == "O")
            {
                ComputerTurn();
            }
        }
    }

    bool CheckForWinner()
    {
     
        int[,] winConditions = new int[,]
        {
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },
            { 0, 4, 8 },
            { 2, 4, 6 }
        };

        for (int i = 0; i < winConditions.GetLength(0); i++)
        {
            if (buttonList[winConditions[i, 0]].text == playerSide &&
                buttonList[winConditions[i, 1]].text == playerSide &&
                buttonList[winConditions[i, 2]].text == playerSide)
            {
                return true;
            }
        }
        return false;
    }

    void GameOver(string winningPlayer)
    {
        foreach (var button in buttonList)
        {
            button.GetComponentInParent<Button>().interactable = false;
        }

        SetGameOverText(winningPlayer == "무승부" ? "비겼습니다!" : $"{playerSide}가 이겼습니다!");
        restartButton.SetActive(true);
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
        SetPlayerColors(playerSide == "X" ? playerX : playerO, playerSide == "X" ? playerO : playerX);
    }

    void SetGameOverText(string myText)
    {
        gameOverText.text = myText;
        gameOverPanel.SetActive(true);
    }

    public void ReStartGame()
    {
        playerSide = "X";
        moveCount = 0;
        gameOverPanel.SetActive(false);
        
        foreach (var button in buttonList)
        {
            button.text = "";
        }   
        
        SetPlayerColors(playerX, playerO);
        SetBoardInteractable(true); 
        restartButton.SetActive(false);
    }

    void SetBoardInteractable(bool toggle)
    {
        foreach (var button in buttonList)
        {
            button.GetComponentInParent<Button>().interactable = toggle;
        }   
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;

        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    void ComputerTurn()
    {
        bool foundEmptySpot = false;

        while (!foundEmptySpot)
        {
            int randomNumber = Random.Range(0, 9);

            if (buttonList[randomNumber].GetComponentInParent<Button>().IsInteractable())
            {
                buttonList[randomNumber].GetComponentInParent<Button>().onClick.Invoke();
                foundEmptySpot = true;
            }
        }
    }
}
