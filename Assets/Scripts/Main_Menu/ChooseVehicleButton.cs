using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseVehicleButton : MonoBehaviour
{
    public GameObject m_loadText;
    public GameObject m_returnButton;

    public void LoadScene()
    {
        GameData.VehicleClicked = name;
        m_loadText.SetActive(true);
        m_returnButton.SetActive(false);
        SceneManager.LoadSceneAsync(GameData.CurrentScene);
    }
}
