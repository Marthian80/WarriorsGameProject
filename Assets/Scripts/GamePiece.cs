using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{    
    // Start is called before the first frame update
    void Start()
    {
        //if (MainGameManager.Instance != null)
        //{
        //    SetColor(MainGameManager.Instance.TeamColor);
        //    //SetOpponetsColors();
        //}        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetOpponetsColors()
    {
        for (int i = 0; i < MainGameManager.Instance.numberofOpponents; i++)
        {
            SetColor(MainGameManager.Instance.gameColors[MainGameManager.Instance.SelectedOpponents[i]]);
        }
    }

    public void SetColor(Color color)
    {
        foreach (var rend in gameObject.GetComponentsInChildren<Renderer>())
        {
            rend.material.color = color;
        }
    }
}
