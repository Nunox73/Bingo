using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Response
{
    public string status;
    public string message;
    public string timestamp;
}

public class GoogleSheetsSender : MonoBehaviour
{
    [Header("Google Web App URL")]
    public string webAppUrl = "https://script.google.com/macros/s/AKfycbx1O0e3MEgnMcRdojYZp81dKwkQdbzwhKe9QJWabTRytyhff1T2kQo-XfhDuCrvYxu-uw/exec";

    /// <summary>
    /// Envia dados para a aba GameData
    /// </summary>
    public void SendGameData(
        string PlayerID,
        string ButtonID,
        string DrawnNumber,
        string TimeRemaining,
        string ButtonColor,
        string Score)
    {
        WWWForm form = new WWWForm();

        // Identifica a ação no Google Apps Script
        form.AddField("action", "SaveGameData");

        // Campos enviados
        form.AddField("PlayerID", PlayerID);
        form.AddField("ButtonID", ButtonID);
        form.AddField("DrawnNumber", DrawnNumber);
        form.AddField("TimeRemaining", TimeRemaining);
        form.AddField("ButtonColor", ButtonColor);
        form.AddField("Score", Score);

        StartCoroutine(PostForm(form));
    }

    /// <summary>
    /// Envia dados para a aba PlayerData
    /// </summary>
    public void SendPlayerData(
        string PlayerID,
        string Age,
        string Sex,
        string MaritalStatus,
        string Residence,
        string TechUse,
        string TechDificulty,
        string Q1, string Q2, string Q3, string Q4, string Q5,
        string Q6, string Q7, string Q8, string Q9, string Q10,
        string Q11, string Q12, string Q13, string Q14, string Q15)
    {
        WWWForm form = new WWWForm();

        // Identifica a ação no Google Apps Script
        form.AddField("action", "SavePlayerData");

        // Dados básicos
        form.AddField("PlayerID", PlayerID);
        form.AddField("Age", Age);
        form.AddField("Sex", Sex);
        form.AddField("MaritalStatus", MaritalStatus);
        form.AddField("Residence", Residence);
        form.AddField("TechUse", TechUse);
        form.AddField("TechDificulty", TechDificulty);

        // Questões Q1 a Q15
        form.AddField("Q1", Q1);
        form.AddField("Q2", Q2);
        form.AddField("Q3", Q3);
        form.AddField("Q4", Q4);
        form.AddField("Q5", Q5);
        form.AddField("Q6", Q6);
        form.AddField("Q7", Q7);
        form.AddField("Q8", Q8);
        form.AddField("Q9", Q9);
        form.AddField("Q10", Q10);
        form.AddField("Q11", Q11);
        form.AddField("Q12", Q12);
        form.AddField("Q13", Q13);
        form.AddField("Q14", Q14);
        form.AddField("Q15", Q15);

        StartCoroutine(PostForm(form));
    }

    /// <summary>
    /// Faz o POST para o Google Apps Script e processa a resposta JSON
    /// </summary>
    private IEnumerator PostForm(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(webAppUrl, form))
        {
            yield return www.SendWebRequest();

            string raw = www.downloadHandler.text;
            Debug.Log("Resposta raw do Google Sheets: " + raw);

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao enviar: " + www.error);
            }
            else
            {
                try
                {
                    Response resp = JsonUtility.FromJson<Response>(raw);
                    Debug.Log($"Status: {resp.status} | Mensagem: {resp.message} | Timestamp: {resp.timestamp}");
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Erro ao parsear JSON: " + ex.Message);
                    Debug.LogError("Resposta completa recebida: " + raw);
                }
            }
        }
    }
}
