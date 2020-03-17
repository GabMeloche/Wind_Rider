using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : MonoBehaviour
{
    public GameObject m_mainMenu;
    public GameObject m_playMenu;

    private void Start()
    {
    }

    public void Return()
    {
        m_playMenu.SetActive(false);
        m_mainMenu.SetActive(true);
    }
}
