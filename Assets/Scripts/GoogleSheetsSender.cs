using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Response
{
    public string status;
    public string message;
    public string timestamp; // retornado pelo Apps Script
}

public class GoogleSheetsSender : MonoBehaviour
{
    [Header("URL do Web App do Google Apps Script")]
    public string webAppUrl ="https://script.google.com/macros/s/AKfycbx1O0e3MEgnMcRdojYZp81dKwkQdbzwhKe9QJWabTRytyhff1T2kQo-XfhDuCrvYxu-uw/exec";

    /// <summary>
    /// Envia os dados do jogador para o Google Sheets.
    /// Todos os parâmetros são strings.
    /// </summary>
    public void SendPlayerData(
        string sheetName,
        string PlayerID,
        string Age,
        string Sex,
        string MaritalStatus,
        string Residence,
        string TechUse,
        string TechDificulty,
        string Q1, string Q2, string Q3, string Q4, string Q5,
        string Q6, string Q7, string Q8, string Q9, string Q10,
        string Q11, string Q12, string Q13, string Q14, string Q15
    )
    {
        StartCoroutine(PostPlayerData(
            sheetName, PlayerID, Age, Sex, MaritalStatus, Residence, TechUse, TechDificulty,
            Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8, Q9, Q10, Q11, Q12, Q13, Q14, Q15
        ));
    }

    private IEnumerator PostPlayerData(
        string sheetName,
        string PlayerID,
        string Age,
        string Sex,
        string MaritalStatus,
        string Residence,
        string TechUse,
        string TechDificulty,
        string Q1, string Q2, string Q3, string Q4, string Q5,
        string Q6, string Q7, string Q8, string Q9, string Q10,
        string Q11, string Q12, string Q13, string Q14, string Q15
    )
    {
        WWWForm form = new WWWForm();

        // Nome da aba
        form.AddField("sheetName", sheetName);

        // Dados do jogador
        form.AddField("PlayerID", PlayerID);
        form.AddField("Age", Age);
        form.AddField("Sex", Sex);
        form.AddField("MaritalStatus", MaritalStatus);
        form.AddField("Residence", Residence);
        form.AddField("TechUse", TechUse);
        form.AddField("TechDificulty", TechDificulty);

        // Respostas Q1 - Q15
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

        using (UnityWebRequest www = UnityWebRequest.Post(webAppUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erro ao enviar: " + www.error);
                Debug.Log("Resposta raw: " + www.downloadHandler?.text);
            }
            else
            {
                string raw = www.downloadHandler.text;
                Debug.Log("Resposta raw: " + raw);

                // Tenta desserializar JSON
                try
                {
                    Response resp = JsonUtility.FromJson<Response>(raw);
                    if (resp != null && !string.IsNullOrEmpty(resp.status))
                    {
                        Debug.Log($"Status: {resp.status} | Mensagem: {resp.message} | Timestamp: {resp.timestamp}");
                    }
                    else
                    {
                        Debug.LogWarning("Resposta JSON inválida ou sem campos esperados.");
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Erro ao parsear JSON: " + ex.Message);
                }
            }
        }
    }
}
