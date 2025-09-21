using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Response
{
    public string status;
    public string message;
}

public class GoogleSheetsSender : MonoBehaviour
{
    [Header("https://script.google.com/macros/s/AKfycbx1O0e3MEgnMcRdojYZp81dKwkQdbzwhKe9QJWabTRytyhff1T2kQo-XfhDuCrvYxu-uw/exec")]
    public string webAppUrl;

    public void SendPlayerData(string sheetName, string playerName, int score, float timePlayed)
    {
        StartCoroutine(PostData(sheetName, playerName, score, timePlayed));
    }

    private IEnumerator PostData(string sheetName, string playerName, int score, float timePlayed)
    {
        WWWForm form = new WWWForm();
        form.AddField("sheetName", sheetName);
        form.AddField("playerName", playerName);
        form.AddField("score", score);
        form.AddField("timePlayed", timePlayed.ToString());

        using (UnityWebRequest www = UnityWebRequest.Post(webAppUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao enviar dados: " + www.error);
            }
            else
            {
                // Lê a resposta JSON
                string jsonText = www.downloadHandler.text;
                Response response = JsonUtility.FromJson<Response>(jsonText);
                Debug.Log($"Status: {response.status}, Mensagem: {response.message}");
            }
        }
    }
}
