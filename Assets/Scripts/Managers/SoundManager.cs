using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Range(0f, 1f)] public float m_SailIntensityMin = 0.95f;
    private GameObject m_player;
    private AudioSource m_sailFlap;
    private Sail[] m_sails;

    private bool[] m_sailFull;

	void Start ()
    {
        m_player = GameObject.FindWithTag("Player");

        if (m_player == null)
            Debug.LogError("SoundManager could not find object with Player tag");

        m_sails = m_player.GetComponentsInChildren<Sail>();
        m_sailFull = new bool[m_sails.Length];

        if (m_sails == null)
            Debug.LogError("SoundManager could not find Sail script within Player object");

        m_sailFlap = m_player.GetComponent<AudioSource>();

        if (m_sailFlap == null)
            Debug.LogError("SoundManager could not find AudioSource SailFlap within Player object");
    }
	
	void Update ()
    {
		for (int i = 0; i < m_sails.Length; ++i)
        {

            if (m_sails[i].GetIntensity() >= m_SailIntensityMin)
            {
                if (!m_sailFlap.isPlaying && m_sailFull[i] == false)
                {
                    m_sailFlap.Play();
                }

                m_sailFull[i] = true;
            }
            else
                m_sailFull[i] = false;
        }
	}
}
