using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    public static bool buttonsConnected = false; //check if the hardware buttons are connected

    public static int greenButton; // Green button ID
    public static int redButton; // Red button ID

    // Timmers

    public static float drawnTimer = 15f;
    public static float winnerCanvasTimer = 15f;

    // 2.Gamer Variables

    public static string PlayerID = "0"; 
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
                                                           //GlobalVariables.timeToClick[i, 3] = YesNo; // Color Button 0 = Red; 1 = Green
    // TAM Variables
    public static string[,] TAMQuest = new string[14, 2]
{
    { "Jogar este bingo ajuda-me a exercitar a mente.", "0" },
    { "Sinto que o jogo me mantém ativo(a) e concentrado(a).", "0" },
    { "O bingo digital é útil para o meu bem-estar.", "0" },
    { "Jogar este bingo ajuda-me a passar o tempo de forma agradável.", "0" },
    { "Foi fácil aprender a jogar este bingo.", "0" },
    { "Os botões e o ecrã são fáceis de perceber e de usar.", "0" },
    { "É fácil controlar o jogo e responder quando aparece um número.", "0" },
    { "Consigo ver bem as letras, os números e os botões.", "0" },
    { "Gosto de jogar este bingo digital.", "0" },
    { "Sinto-me bem e relaxado(a) quando jogo.", "0" },
    { "Jogar este bingo é uma experiência agradável para mim.", "0" },
    { "Gostaria de jogar este bingo outra vez.", "0" },
    { "Jogaria mais vezes se estivesse disponível.", "0" },
    { "Recomendaria este jogo a outras pessoas da minha idade.", "0" }
};


    
    public static bool pausa = false;

    public static string linha = "";
    public static string bingo = "";



}
