using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [Header("Lobby")]
    [SerializeField] GameObject uiManager;
    [SerializeField] TMP_Text diamondCount;
    [SerializeField] Slider heartSlider;

    [Header("Message Box")]
    [SerializeField] GameObject msgBox;
    [SerializeField] TMP_Text msgBoxText = null;
    string currentUser;
    string logMessage;
    void OnEnable()
    {
        currentUser = uiManager.GetComponent<LoginManager>().GetCurrentUser();
        ObtainHeartAndDiamond();
    }

    async void ObtainHeartAndDiamond()
    {
        logMessage = null;
        if (currentUser != null)
        {
            (bool success, string jsonString) = await SQLManager.GetLobbyElement(currentUser);
            if (success)
            {
                if(JsonUtility.FromJson<JsonForm>(jsonString).isComplete)
                {
                    diamondCount.text = JsonUtility.FromJson<JsonForm>(jsonString).diamond.ToString();
                    heartSlider.value = JsonUtility.FromJson<JsonForm>(jsonString).heart;
                }
                else
                {
                    logMessage += JsonUtility.FromJson<JsonForm>(jsonString).message;
                    msgBoxText.text = logMessage;
                    msgBox.SetActive(true);
                }

            }
            else
            {
                logMessage += "Failed to connect to the server";
                msgBoxText.text = logMessage;
                msgBox.SetActive(true);
            }
        }
        else
        {
            logMessage += "Error: Unknown User";
            msgBoxText.text = logMessage;
            msgBox.SetActive(true);
        }

    }

    public async void OnAddDiamondButton()
    {
        logMessage = null;
        if (currentUser != null)
        {
            (bool success, string jsonString) = await SQLManager.AddDiamond(currentUser);
            if (success)
            {
                if (JsonUtility.FromJson<JsonForm>(jsonString).isComplete)
                {
                    diamondCount.text = JsonUtility.FromJson<JsonForm>(jsonString).diamond.ToString();
                }
                logMessage += JsonUtility.FromJson<JsonForm>(jsonString).message;
                msgBoxText.text = logMessage;
                msgBox.SetActive(true);
                
            }
            else
            {
                logMessage += "Failed to connect to the server";
                msgBoxText.text = logMessage;
                msgBox.SetActive(true);
            }
        }
        else
        {
            logMessage += "Error: Unknown User";
            msgBoxText.text = logMessage;
            msgBox.SetActive(true);
        }
    }

}
