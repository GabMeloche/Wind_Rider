using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    [SerializeField] private float m_strength;
    [SerializeField] private float m_frequency;
    [SerializeField] private float m_duration;

    [SerializeField] private bool m_randomizeFrequency;
    [SerializeField] private bool m_randomizeDuration;
    [SerializeField] private Vector2 m_minMaxRandomFrequency;
    [SerializeField] private Vector2 m_minMaxRandomDuration;

    private MeshRenderer m_meshRenderer;
    public ParticleSystem m_particles;
    private bool m_isActive = false;

    private float m_timeAfterStart = 0f;

	void Start ()
    {
        if (m_randomizeFrequency)
            m_frequency = Random.Range(m_minMaxRandomFrequency.x, m_minMaxRandomFrequency.y);

        if (m_randomizeDuration)
            m_duration = Random.Range(m_minMaxRandomDuration.x, m_minMaxRandomDuration.y);
    }
	
	void Update ()
    {
        m_timeAfterStart += Time.deltaTime;

        if (m_timeAfterStart >= m_frequency)
        {
            m_isActive = true;
            m_particles.Emit(5);

            if (m_timeAfterStart >= m_frequency + m_duration)
            {
                m_timeAfterStart = 0f;
                m_isActive = false;
            }
        }
	}

    private void OnTriggerStay(Collider p_other)
    {
        if (p_other.gameObject.GetComponentInParent<Rigidbody>().gameObject.CompareTag("Player")
            && m_isActive == true)
        {
            p_other.gameObject.GetComponentInParent<Rigidbody>().AddForce
                (-transform.up * m_strength);
        }
    }
}
