using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindBarScript : MonoBehaviour
{
    private GameObject m_player;
    public Image m_image;

	void Start ()
    {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_image = GetComponent<Image>();
	}
	
	void Update ()
    {
        m_image.fillAmount = Mathf.Abs(m_player.GetComponentInChildren<Sail>().GetIntensity());
	}
}
