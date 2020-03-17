using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private GameObject m_windStrand;
    [SerializeField] private float m_windForce;
    [Range(0.1f, 40f)] public float m_windStrandsRatio = 1f;
    [SerializeField] public Color m_strandsColor = new Color(255,255,255, 25);

    [SerializeField] private Vector3 m_maxStrandsDistanceFromCenter =
        new Vector3(0.45f, 1f, 0.9f);

    [SerializeField] private Vector3 m_minStrandsDistanceFromCenter = 
        new Vector3 (-0.45f, -0.2f, -1f);

    private List<GameObject> m_windStrands = new List<GameObject>();
    private GameObject m_vehicle;
    private float m_velocity;
    private float m_maxWindSpeed = 1000f;

	void Start ()
    {
        m_vehicle = GameObject.FindGameObjectWithTag("Player");
    }

    private void Awake()
    {
        Bounds bounds = GetComponent<Collider>().bounds;

        for (int i = 0; i < bounds.size.magnitude * m_windForce / (m_windStrandsRatio * 3000f); ++i)
        {
            GameObject tmp = Instantiate(m_windStrand);
            ParticleSystem particles = tmp.GetComponent<ParticleSystem>();
            var main = particles.main;
            //var color = particles.startColor;
            var color = new Color(m_strandsColor.r, m_strandsColor.g,
                m_strandsColor.b, m_strandsColor.a);

            main.startColor = color;

            main.simulationSpeed = m_windForce / m_maxWindSpeed;
            tmp.transform.SetParent(transform);
            tmp.transform.localPosition = GetRandomPointInCapsule();
            tmp.transform.eulerAngles = transform.eulerAngles;
            tmp.transform.localScale = Vector3.one;
            m_windStrands.Add(tmp);
        }
    }

    void Update ()
    {
	}

    private void OnTriggerStay(Collider p_other)
    {
        if (p_other.gameObject.CompareTag("Sail"))
        {
            Rigidbody rb = m_vehicle.GetComponent<Rigidbody>();

            if (rb == null)
                return;

            if (rb.velocity.magnitude >= m_vehicle.GetComponent<Vehicle>().GetMaxSpeed())
                return;

            rb.AddForce(m_vehicle.transform.forward * m_windForce *
                p_other.gameObject.GetComponentInChildren<Sail>().GetIntensity());
        }
    }

    private Vector3 GetRandomPointInCapsule()
    {
        float xPos = Random.Range(m_minStrandsDistanceFromCenter.x, 
            m_maxStrandsDistanceFromCenter.x);
        float yPos = Random.Range(m_minStrandsDistanceFromCenter.y, 
            m_maxStrandsDistanceFromCenter.y);
        float zPos = Random.Range(m_minStrandsDistanceFromCenter.z,
            m_maxStrandsDistanceFromCenter.z);

        return new Vector3(xPos, yPos, zPos);
    }

    public Vector3 GetDirection()
    {
        return transform.forward;
    }
}
