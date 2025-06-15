using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    public static bool buttonsConnected = true; //check if the hardware buttons are connected

    public static int greenButton; // Green button ID
    public static int redButton; // Red button ID

    // Gamer_1 Variables
    public static int age = 74; // Player Age
    public static string sex = "Feminino"; // Player Sex: Masculino, Feminino

    // Gamer_2 Variables
    public static string estadoCivil = "Casado(a)"; // Player Estado Civil: Solteiro(a), Casado(a), Viúvo(a), Divorciado(a)
    public static string residence = "Casa, sozinho(a)"; // Player Residence: "Casa, sozinho(a)", "Casa, com outros familiares", "Residência assistida"


    public static string tech_use = "Sim"; //Player uses tech like Smartphones or Tablets (Sim e Não)
    public static string tech_dificulty = "Sim"; //The player has difficulties accessing technologies. (Sim e Não)


}
