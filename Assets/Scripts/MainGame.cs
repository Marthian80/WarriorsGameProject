using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public GameObject PlayerPiece;
    public GameObject StartGameSpace;
    public GameObject[] TeamStartPlaces = new GameObject[4];
    public Button BoardViewButton;
    public Button PlayerViewButton;

    public GameObject PlayerCamera;
    public GameObject MainCamera;

    private const float gamePiecePositionY = 2.25f;

    private GamePiece gamePieceScript;
    private GameObject[] playerBoardGamePieces = new GameObject[4];
    private List<GameObject[]> opponents = new List<GameObject[]>();
    private GameObject[] opponentOneBoardGamePieces = new GameObject[4];
    private GameObject[] opponentTwoBoardGamePieces = new GameObject[4];
    private GameObject[] opponentThreeBoardGamePieces = new GameObject[4];

    //variables initialized for testing purposes
    private int playerTeamNumber = 0;
    private int opponentOneTeamNumber = 1;
    private int opponentTwoTeamNumber = 2;
    private int opponentThreeTeamNumber = 3;

    private const int cameraGameBoardNumber = 0;
    private const int cameraPlayerViewNumber = 1;

    // Start is called before the first frame update
    void Start()
    {       
        if (MainGameManager.Instance != null)
        {
            playerTeamNumber = MainGameManager.Instance.PlayerTeamNumber;
            opponentOneTeamNumber = MainGameManager.Instance.OpponentNumberOneTeam;
            opponentTwoTeamNumber = MainGameManager.Instance.OpponentNumberTwoTeam;
            opponentThreeTeamNumber = MainGameManager.Instance.OpponentNumberThreeTeam;
        }

        SetupTeamPieces();

        FocusCameraOnPlayerTeam();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * Menu Button methods
     */

    public void ViewBoard()
    {
        ChangeCamera(cameraGameBoardNumber);
    }

    public void ViewPlayer()
    {
        ChangeCamera(cameraPlayerViewNumber);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    /*
     * Private methods
     */

    private void ChangeCamera(int cameraNumber)
    {
        MainCamera.SetActive(cameraNumber == cameraGameBoardNumber);
        PlayerCamera.SetActive(cameraNumber == cameraPlayerViewNumber);

        BoardViewButton.gameObject.SetActive(cameraNumber == cameraPlayerViewNumber);
        PlayerViewButton.gameObject.SetActive(cameraNumber == cameraGameBoardNumber);
    }

    private void SetupTeamPieces()
    {
        Transform startPositionTeam = null;

        //Create default team 0 for testing
        if (MainGameManager.Instance == null)
        {            
            startPositionTeam = TeamStartPlaces[0].transform.GetComponentInChildren<Transform>();
        }        
        else
        {
            //Get correct position starting location
            startPositionTeam = TeamStartPlaces[MainGameManager.Instance.PlayerTeamNumber].transform.GetComponentInChildren<Transform>();        
        }
        playerBoardGamePieces = CreateBoardGamePieces(startPositionTeam, playerTeamNumber);

        //Create default one opponent (team 2) for testing 
        if (MainGameManager.Instance == null)
        {
            opponents.Add(CreateBoardGamePieces(TeamStartPlaces[2].transform.GetComponentInChildren<Transform>(), 2));
        }
        else
        {
            for(int i = 0; i < MainGameManager.Instance.numberofOpponents; i++)
            {
                var teamNumber = MainGameManager.Instance.SelectedOpponents[i];
                opponents.Add(CreateBoardGamePieces(TeamStartPlaces[teamNumber].transform.GetComponentInChildren<Transform>(), teamNumber));
            }
        }
    }

    private void FocusCameraOnPlayerTeam()
    {
        Vector3 posPlayerTeam;
        
        if (MainGameManager.Instance == null)
        {
            posPlayerTeam = TeamStartPlaces[0].transform.GetComponentInChildren<Transform>().position;
        }
        else
        {
            posPlayerTeam = TeamStartPlaces[MainGameManager.Instance.PlayerTeamNumber].transform.GetComponentInChildren<Transform>().position;
        }
        posPlayerTeam.y += 14f;
        posPlayerTeam.z -= 10f;

        PlayerCamera.GetComponent<PlayerCamera>().Move(posPlayerTeam);

        MainCamera.SetActive(false);
        PlayerCamera.SetActive(true);
    }

    private GameObject[] CreateBoardGamePieces(Transform startPositionTeam, int teamNumber)
    {        
        var gamePieces = new GameObject[4];

        for (int i = 0; i < startPositionTeam.childCount; i++)
        {
            var test = startPositionTeam.GetChild(i).gameObject;
            var posX = test.transform.parent.localPosition.x + test.transform.localPosition.x;
            var posY = gamePiecePositionY;
            var posZ = test.transform.parent.localPosition.z + test.transform.localPosition.z;
            var piece = Instantiate(PlayerPiece, new Vector3(posX, posY, posZ), PlayerPiece.transform.rotation);

            if (MainGameManager.Instance != null)
            {
                piece.GetComponent<GamePiece>().SetColor(MainGameManager.Instance.gameColors[teamNumber]);
            }
            else
            {
                piece.GetComponent<GamePiece>().SetColor(new Color(Random.Range(0,255), Random.Range(0,255), Random.Range(0, 255), 255));
            }            

            gamePieces[i] = piece;
        }

        return gamePieces;
    }
}