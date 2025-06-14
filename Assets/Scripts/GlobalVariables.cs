using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    public static bool buttonsConnected = true; //check if the hardware buttons are connected

    public static int greenButton; // Green button ID
    public static int redButton; // Red button ID

    // Gamer Variables
    public static int age; // Player Age
    public static string sex; // Player Sex: Masculino, Feminino
    public static string estadoCivil; // Player Estado Civil: Solteiro(a), Casado(a), Viúvo(a), Divorciado(a)
    public static string residence; // Player Residence: "Casa, sozinho(a)", "Casa, com outros familiares", "Residência assistida"

    public static bool tech_use; //Player uses tech like Smartphones or Tablets
    public static bool tech_dificulty; //The player has difficulties accessing technologies.


}
