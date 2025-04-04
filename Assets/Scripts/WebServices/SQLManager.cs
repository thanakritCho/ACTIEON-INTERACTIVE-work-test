using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class SQLManager
{    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public readonly static string SERVER_URL = "https://test-piggy.codedefeat.com/worktest/dev11/";

    public static async Task<(bool success, string jsonString)> RegisterUser(string username, string password)
    {
        string URL = $"{SERVER_URL}/RegisterUser.php";

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest req = new UnityWebRequest(URL, "POST");

        req.SetRequestHeader("Content-Type", "application/json");
        req = UnityWebRequest.Post(URL, form);
        req.SendWebRequest();

        while (!req.isDone) await Task.Delay(100);

        Debug.Log(req.downloadHandler.text);
        if (req.error != null || !string.IsNullOrWhiteSpace(req.error))
        {
            return (false, req.downloadHandler.text);
        }
        return (true, req.downloadHandler.text);
    }

    public static async Task<(bool success, string jsonString)> LoginUser(string username, string password)
    {
        string URL = $"{SERVER_URL}/Login.php";

        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest req = new UnityWebRequest(URL, "POST");

        req.SetRequestHeader("Content-Type", "application/json");
        req = UnityWebRequest.Post(URL, form);
        req.SendWebRequest();

        while (!req.isDone) await Task.Delay(100);

        Debug.Log(req.downloadHandler.text);
        if (req.error != null || !string.IsNullOrWhiteSpace(req.error))
        {
            return (false, req.downloadHandler.text);
        }
        return (true, req.downloadHandler.text);
    }

    public static async Task<(bool success, string jsonString)> GetLobbyElement(string curentUser)
    {
        string URL = $"{SERVER_URL}/GetLobbyElement.php";

        WWWForm form = new WWWForm();
        form.AddField("username", curentUser);
        

        UnityWebRequest req = new UnityWebRequest(URL, "POST");

        req.SetRequestHeader("Content-Type", "application/json");
        req = UnityWebRequest.Post(URL, form);
        req.SendWebRequest();

        while (!req.isDone) await Task.Delay(100);

        Debug.Log(req.downloadHandler.text);
        if (req.error != null || !string.IsNullOrWhiteSpace(req.error))
        {
            return (false, req.downloadHandler.text);
        }
        return (true, req.downloadHandler.text);
    }

    public static async Task<(bool success, string jsonString)> AddDiamond(string curentUser)
    {
        string URL = $"{SERVER_URL}/AddDiamond.php";

        WWWForm form = new WWWForm();
        form.AddField("username", curentUser);


        UnityWebRequest req = new UnityWebRequest(URL, "POST");

        req.SetRequestHeader("Content-Type", "application/json");
        req = UnityWebRequest.Post(URL, form);
        req.SendWebRequest();

        while (!req.isDone) await Task.Delay(100);

        Debug.Log(req.downloadHandler.text);
        if (req.error != null || !string.IsNullOrWhiteSpace(req.error))
        {
            return (false, req.downloadHandler.text);
        }
        return (true, req.downloadHandler.text);
    }
}
