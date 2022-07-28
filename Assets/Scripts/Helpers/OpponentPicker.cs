using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentPicker : MonoBehaviour
{
    public string[] AvailableOpponents;
    public Button OpponentButtonPrefab;

    public string SelectedOpponents { get; private set; }
    public System.Action<string> onOpponentChanged;

    List<Button> m_OpponentButtons = new List<Button>();

    // Start is called before the first frame update
    public void Init()
    {

        foreach (string text in AvailableOpponents)
        {
            var newButton = Instantiate(OpponentButtonPrefab, transform);
            newButton.GetComponentInChildren<Text>().text = text;

            newButton.onClick.AddListener(() =>
            {
                SelectedOpponents = text;
                foreach (var button in m_OpponentButtons)
                {
                    button.interactable = true;
                }

                newButton.interactable = false;

                onOpponentChanged.Invoke(SelectedOpponents);
            });

            m_OpponentButtons.Add(newButton);
        }
    }
}
