using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    private static string m_vehicleClicked;
    private static int m_currentScene;

    public static string VehicleClicked
    {
        get { return m_vehicleClicked; }
        set { m_vehicleClicked = value; }
    }

    public static int CurrentScene
    {
        get { return m_currentScene; }
        set { m_currentScene = value; }
    }
}
