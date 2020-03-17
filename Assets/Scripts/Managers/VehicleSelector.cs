using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSelector : MonoBehaviour
{
    private void Start()
    {
        string chosenVehicle = GameData.VehicleClicked;

        switch (chosenVehicle)
        {
            case "MadMaxChar":
                GameObject.Find("WoodChar").SetActive(false);
                GameObject.Find("AstonMartin").SetActive(false);
                break;

            case "WoodChar":
                GameObject.Find("MadMaxChar").SetActive(false);
                GameObject.Find("AstonMartin").SetActive(false);
                break;

            case "AstonMartin":
                GameObject.Find("MadMaxChar").SetActive(false);
                GameObject.Find("WoodChar").SetActive(false);
                break;

            default:
                Debug.Log("Could not find chosen vehicle: " + chosenVehicle);
                break;
        }
    }

}
