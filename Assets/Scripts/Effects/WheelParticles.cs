using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticles : MonoBehaviour
{
    [SerializeField] public GameObject m_particlePrefab;

    private WheelCollider[] m_wheels;
    private Rigidbody m_rigidbody;
    private List<ParticleSystem> m_particleSystems = new List<ParticleSystem>();

    private bool m_reverse;

	void Start ()
    {
        m_wheels = GetComponentsInChildren<WheelCollider>();

        if (m_wheels == null)
            Debug.LogError("WheelParticles script could not find any wheel colliders");

        m_rigidbody = GetComponent<Rigidbody>();

        if (m_rigidbody == null)
            Debug.LogError("WheelParticles script could not find rigidbody on char");

        foreach (WheelCollider wheel in m_wheels)
        {
            GameObject tmp = Instantiate(m_particlePrefab, wheel.transform);
            tmp.transform.position = wheel.transform.position;
            tmp.transform.rotation = wheel.transform.rotation;
            tmp.transform.Rotate(new Vector3(0f, -90f, 45f));
            tmp.transform.SetParent(wheel.transform);
            m_particleSystems.Add(tmp.GetComponent<ParticleSystem>());

        }

    }
	
	void Update ()
    {

		for (int i = 0; i < m_particleSystems.Count; ++i)
        {
            var tmp = m_particleSystems[i].emission;
            var shape = m_particleSystems[i].shape;
            var main = m_particleSystems[i].main;

            if (Physics.Raycast(m_wheels[i].transform.position, -transform.up, 0.5f))
            {
                if (Vector3.Dot(m_rigidbody.velocity.normalized, transform.forward) < 0f)
                    main.startSpeed = 0.1f;
                else
                    main.startSpeed = m_rigidbody.velocity.magnitude * 3f;

                shape.radiusSpread = m_rigidbody.velocity.magnitude / 10f;
                tmp.rateOverTimeMultiplier = m_rigidbody.velocity.magnitude;
            }
            else
            {
                main.startSpeed = 0f;
                shape.radiusSpread = 0f;
                tmp.rateOverTimeMultiplier = 0f;
            }
        }
	}
}
