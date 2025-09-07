using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    public static bool buttonsConnected = true; //check if the hardware buttons are connected

    public static int greenButton; // Green button ID
    public static int redButton; // Red button ID

    // Timmers

    public static float drawnTimer = 15f;
    public static float winnerCanvasTimer = 15f;

    // 2.Gamer Variables
    public static int age = 74; // Player Age
    public static string sex = "Feminino"; // Player Sex: Masculino, Feminino

    // 3.Gamer Variables
    public static string estadoCivil = "Casado(a)"; // Player Estado Civil: Solteiro(a), Casado(a), Viúvo(a), Divorciado(a)
    public static string residence = "Casa, sozinho(a)"; // Player Residence: "Casa, sozinho(a)", "Casa, com outros familiares", "Residência assistida"

    // 4.Gamer Variables
    public static string tech_use = "Sim"; //Player uses tech like Smartphones or Tablets (Sim e Não)
    public static string tech_dificulty = "Sim"; //The player has difficulties accessing technologies. (Sim e Não)

    // 5.Game Variables
    public static float timeToStart = 0;
    public static float lastButton = 0;
    public static float[,] timeToClick = new float[40, 4]; // Button ID; drawnNumberText; drawnTimerRemaining; 0=Red 1=Green
                                                           //GlobalVariables.timeToClick[i, 0] = GlobalVariables.lastButton; // Button ID
                                                           //GlobalVariables.timeToClick[i, 1] = int.Parse(drawnNumberText.text); // Bingo Number at the present time
                                                           //GlobalVariables.timeToClick[i, 2] = drawnTimerRemaining; // Time remaining
                                                           //GlobalVariables.timeToClick[i, 3] = YesNo; // Color Buton 0 = Red; 1 = Green
    // TAM Variables
    public static string[,] TAMQuest = new string[15, 2]
{
    { "Este jogo ajuda a mexer-me mais.", "0" },
    { "Sinto-me melhor depois de jogar.", "0" },
    { "Acho que este jogo é bom para a minha saúde.", "0" },
    { "Jogar ajuda-me a manter a mente ativa.", "0" },
    { "É fácil usar os botões para jogar.", "0" },
    { "Percebo bem o que tenho de fazer durante o jogo.", "0" },
    { "Consigo jogar sozinho(a), sem ajuda.", "0" },
    { "O jogo é simples e claro.", "0" },
    { "Gosto de jogar este bingo.", "0" },
    { "O jogo é divertido.", "0" },
    { "Gosto de jogar com outras pessoas.", "0" },
    { "Quero jogar mais vezes.", "0" },
    { "Estou satisfeito(a) com este jogo.", "0" },
    { "Recomendaria este jogo a outras pessoas.", "0" },
    { "Gostava que houvesse mais jogos como este.", "0" }
};


    
    public static bool pausa = false;

    public static string linha = "";
    public static string bingo = "";



}
