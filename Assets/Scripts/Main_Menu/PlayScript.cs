using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScript : MonoBehaviour
{
    public GameObject m_mainMenu;
    public GameObject m_mapMenu;

    public void Play()
    {
        m_mapMenu.SetActive(true);
        m_mainMenu.SetActive(false);
    }
}
