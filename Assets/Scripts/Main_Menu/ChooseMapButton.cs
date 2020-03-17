using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseMapButton : MonoBehaviour
{
    public GameObject m_chooseVehicleMenu;
    public GameObject m_mapMenu;

    public void ChooseMap()
    {
        if (name == "RustyLair")
            GameData.CurrentScene = 1;
        else if (name == "SandBlood")
            GameData.CurrentScene = 2;

        m_chooseVehicleMenu.SetActive(true);
        m_mapMenu.SetActive(false);
    }
}
