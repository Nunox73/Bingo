using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleSheetsSender : MonoBehaviour
{
    [Header("Google Script URL")]
    public string googleScriptURL = "https://script.google.com/macros/s/AKfycbzcb8AXcXTpWGoIjPq0vuua0CykDp-c_nPUy6mUKRpqM5VaAY_7VcYuYpSUWyhhXeaZ6A/exec";

    // Chamar esta função para enviar dados
    public void SendPlayerData(string playerName, int score, float timePlayed)
    {
        StartCoroutine(PostToGoogle(playerName, score, timePlayed));
    }

    private IEnumerator PostToGoogle(string playerName, int score, float timePlayed)
    {
        // Criar o JSON para enviar
        string jsonData = JsonUtility.ToJson(new PlayerData(playerName, score, timePlayed));

        // Configurar o pedido POST
        UnityWebRequest request = new UnityWebRequest(googleScriptURL, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Esperar a resposta
        yield return request.SendWebRequest();

        // Validar resultado
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Data sent successfully: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error sending data: " + request.error);
        }
    }

    // Classe auxiliar para converter dados em JSON
    [System.Serializable]
    public class PlayerData
    {
        public string PlayerName;
        public int Score;
        public float TimePlayed;

        public PlayerData(string playerName, int score, float timePlayed)
        {
            PlayerName = playerName;
            Score = score;
            TimePlayed = timePlayed;
        }
    }
}

