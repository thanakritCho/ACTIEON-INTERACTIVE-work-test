using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoginManager : MonoBehaviour
{
    [Header("UI Gameobjects")]
    [SerializeField] GameObject loginPage;
    [SerializeField] GameObject registerPage;

    [Header("Register InputField")]
    [SerializeField] TMP_InputField reg_Username;
    [SerializeField] TMP_InputField reg_Password;
    [SerializeField] TMP_InputField reg_ConfirmPassword;

    [Header("Login InputField")]
    [SerializeField] TMP_InputField login_Username;
    [SerializeField] TMP_InputField login_Password;

    [Header("Message Box")]
    [SerializeField] GameObject msgBox;
    [SerializeField] TMP_Text msgBoxText = null;

    [Header("Lobby")]
    [SerializeField] GameObject lobby;

    private string currentUser;
    string logMessage;

    public void OnGoToSignUpButton()
    {
        login_Username.text = "";
        login_Password.text = "";
        loginPage.SetActive(false);
        registerPage.SetActive(true);
    }

    public void OnGoBackButton()
    {
        reg_Username.text = "";
        reg_Password.text = "";
        reg_ConfirmPassword.text = "";
        loginPage.SetActive(true);
        registerPage.SetActive(false);
    }

    public async void OnRegisterButton()
    {
        //Register
        logMessage = null;
        if (ValidiateRegister())
        {
            (bool success, string jsonString) = await SQLManager.RegisterUser(reg_Username.text, reg_Password.text);
            if (success)
            {
                logMessage += JsonUtility.FromJson<JsonForm>(jsonString).message;
                if (JsonUtility.FromJson<JsonForm>(jsonString).isComplete)
                {
                    reg_Username.text = "";
                    reg_Password.text = "";
                    reg_ConfirmPassword.text = "";
                    loginPage.SetActive(true);
                    registerPage.SetActive(false);
                }   
            }
            else
            {
                logMessage += "Failed to connect to the server";
            }
        }
        msgBoxText.text = logMessage;
        msgBox.SetActive(true);



    }
    private bool ValidiateRegister()
    {

        if (string.IsNullOrWhiteSpace(reg_Username.text))
        {
            logMessage += "Invalid username.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(reg_Password.text))
        {
            logMessage += "Invalid password.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(reg_ConfirmPassword.text))
        {
            logMessage += "Invalid confirm password.";
            return false;
        }

        if (!reg_Password.text.Equals(reg_ConfirmPassword.text))
        {
            logMessage += "Passwords do not match";
            return false;
        }

        return true;
    }

    public async void OnLoginButton()
    {
        //Login
        logMessage = null;
        if (ValidiateLogin())
        {
            (bool success, string jsonString) = await SQLManager.LoginUser(login_Username.text, login_Password.text);
            if (success)
            {
                if(JsonUtility.FromJson<JsonForm>(jsonString).isComplete)
                {
                    logMessage += JsonUtility.FromJson<JsonForm>(jsonString).message;
                    currentUser = JsonUtility.FromJson<JsonForm>(jsonString).username;
                    lobby.SetActive(true);
                    loginPage.SetActive(false);
                }
                else
                {
                    logMessage += JsonUtility.FromJson<JsonForm>(jsonString).message;
                }
            }
            else
            {
                logMessage += "Failed to connect to the server";
            }
        }
        msgBoxText.text = logMessage;
        msgBox.SetActive(true);

    }

    private bool ValidiateLogin()
    {

        if (string.IsNullOrWhiteSpace(login_Username.text))
        {
            logMessage += "Invalid username.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(login_Password.text))
        {
            logMessage += "Invalid password.";
            return false;
        }

        return true;
    }

    public string GetCurrentUser()
    {
        return currentUser;
    }
}


