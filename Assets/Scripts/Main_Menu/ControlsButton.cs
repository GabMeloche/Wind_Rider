using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsButton : MonoBehaviour
{
    public GameObject m_mainMenu;
    public GameObject m_controlsMenu;

    public void Controls()
    {
        m_controlsMenu.SetActive(true);
        m_mainMenu.SetActive(false);
    }
}
