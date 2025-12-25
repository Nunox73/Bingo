using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text;

public class RuntimeConsoleTMP : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI output;

    [Header("Settings")]
    public int maxLines = 30;
    public bool visible = true;
    public KeyCode toggleKey = KeyCode.F12;

    private readonly Queue<string> lines = new Queue<string>();
    private readonly StringBuilder sb = new StringBuilder(4096);

    void Awake()
    {
        if (output == null)
            output = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            visible = !visible;
            if (output != null) output.gameObject.SetActive(visible);
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (string.IsNullOrWhiteSpace(logString))
        return;

        logString = logString.Trim();

        // Mantém curto e legível
        string msg = $"[{type}] {logString}";

        lines.Enqueue(msg);
        while (lines.Count > maxLines)
            lines.Dequeue();

        if (output == null) return;

        sb.Clear();
        foreach (var l in lines)
            sb.AppendLine(l);

        // Atualiza o TMP (no main thread é ok; este callback vem do Unity)
        output.text = sb.ToString();
    }
}
