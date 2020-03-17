using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfMap : MonoBehaviour
{
    private GameObject m_player;

    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");

        if (m_player == null)
            Debug.LogError("EndOfMap(s) could not find GameObject with Player tag");
    }

    private void OnTriggerEnter(Collider p_other)
    {
        if (p_other.gameObject.GetComponentInParent<Rigidbody>().gameObject.CompareTag("Player"))
        {
            ++GameData.CurrentScene;
            SceneManager.LoadScene(GameData.CurrentScene);
        }
    }

}
