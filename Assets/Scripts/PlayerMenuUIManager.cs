using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class PlayerMenuUIManager : MonoBehaviour
{
    public ColorPicker ColorPicker;
    public OpponentPicker OpponentPicker;

    private Color[] gameColors;

    private const string blueTeamName = "TeamBlue";
    private const string greenTeamName = "TeamGreen";
    private const string redTeamName = "TeamRed";
    private const string yellowTeamName = "TeamYellow";

    private const string oneOpponent = "One opponent";
    private const string twoOpponent = "Two opponents";
    private const string threeOpponent = "Three opponents";
        
    public void TeamSelected(Color color)
    {
        MainGameManager.Instance.TeamColor = color;
        
        if (color == gameColors[0])
        {
            MainGameManager.Instance.PlayerTeamNumber = 0;
            MainGameManager.Instance.Team = blueTeamName;
        }
        else if (color == gameColors[1])
        {
            MainGameManager.Instance.PlayerTeamNumber = 1;
            MainGameManager.Instance.Team = greenTeamName;
        }
        else if (color == gameColors[2])
        {
            MainGameManager.Instance.PlayerTeamNumber = 2;
            MainGameManager.Instance.Team = redTeamName;
        }
        else if (color == gameColors[3])
        {
            MainGameManager.Instance.PlayerTeamNumber = 3;
            MainGameManager.Instance.Team = yellowTeamName;
        }
    }

    public void OpponentsSelected(string opponents)
    {
        int opponentOne = GenerateRandomNumberWithExlusion(0, gameColors.Length, new HashSet<int>() { MainGameManager.Instance.PlayerTeamNumber });
        int opponentTwo = GenerateRandomNumberWithExlusion(0, gameColors.Length, new HashSet<int>() { MainGameManager.Instance.PlayerTeamNumber, opponentOne });
        int opponentThree = GenerateRandomNumberWithExlusion(0, gameColors.Length, new HashSet<int>() { MainGameManager.Instance.PlayerTeamNumber, opponentOne, opponentTwo }); ;

        switch (opponents)
        {
            case oneOpponent:
                MainGameManager.Instance.numberofOpponents = 1;
                MainGameManager.Instance.SelectedOpponents[0] = opponentOne;
                MainGameManager.Instance.OpponentOneColor = gameColors[opponentOne];
                break;
            case twoOpponent:
                MainGameManager.Instance.numberofOpponents = 2;
                MainGameManager.Instance.SelectedOpponents[0] = opponentOne;
                MainGameManager.Instance.SelectedOpponents[1] = opponentTwo;
                MainGameManager.Instance.OpponentOneColor = gameColors[opponentOne];
                MainGameManager.Instance.OpponentTwoColor = gameColors[opponentTwo];
                break;
            case threeOpponent:
                MainGameManager.Instance.numberofOpponents = 3;
                MainGameManager.Instance.SelectedOpponents[0] = opponentOne;
                MainGameManager.Instance.SelectedOpponents[1] = opponentTwo;
                MainGameManager.Instance.SelectedOpponents[2] = opponentThree;

                MainGameManager.Instance.OpponentOneColor = gameColors[opponentOne];
                MainGameManager.Instance.OpponentTwoColor = gameColors[opponentTwo];
                MainGameManager.Instance.OpponentThreeColor = gameColors[opponentThree];
                break;
            default:
                break;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += TeamSelected;

        if (MainGameManager.Instance != null)
        {
            gameColors = MainGameManager.Instance.gameColors;
            ColorPicker.SelectColor(MainGameManager.Instance.TeamColor);            
        }

        OpponentPicker.Init();
        OpponentPicker.onOpponentChanged += OpponentsSelected;

    }

    private int GenerateRandomNumberWithExlusion(int minRange, int maxRange, HashSet<int> exclusion)
    {       
        var range = Enumerable.Range(minRange, maxRange).Where(i => !exclusion.Contains(i));

        var rand = new System.Random();
        int index = rand.Next(minRange, maxRange - exclusion.Count);
        return range.ElementAt(index);
    }
}