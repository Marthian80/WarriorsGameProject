using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance { get; private set; }

    public Color[] gameColors = new Color[4];
    public int[] SelectedOpponents = new int[3];

    public Color TeamColor;
    public Color OpponentOneColor;
    public Color OpponentTwoColor;
    public Color OpponentThreeColor;
    public int PlayerTeamNumber;
    public int numberofOpponents;

    public int OpponentNumberOneTeam;
    public int OpponentNumberTwoTeam;
    public int OpponentNumberThreeTeam;

    public string Team;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
