using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : MonoBehaviour
{
    public GameObject m_mainMenu;
    public GameObject m_creditsMenu;

    public void Credits()
    {
        m_creditsMenu.SetActive(true);
        m_mainMenu.SetActive(false);
    }
}
