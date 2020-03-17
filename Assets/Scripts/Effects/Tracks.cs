using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracks : MonoBehaviour
{
    [SerializeField] private GameObject m_trackDecalPrefab;
    [SerializeField] private int m_maxConcurrentDecals = 500;

    private int m_delay = 1;
    private int m_delayPos = 0;
    private WheelCollider[] m_wheels;
    private Vector3[] m_wheelTransformRatio;
    private Vector3[] m_wheelScales;
    private Queue<GameObject> m_decalsInPool;
    private Queue<GameObject> m_decalsActiveInWorld;

    private Rigidbody m_rigidbody;

    private void Awake()
    {
        InitializeDecals();
    }

    private void InitializeDecals()
    {
        m_decalsInPool = new Queue<GameObject>();
        m_decalsActiveInWorld = new Queue<GameObject>();
        m_wheels = GetComponentsInChildren<WheelCollider>();
        m_wheelTransformRatio = new Vector3[m_wheels.Length];
        m_wheelScales = new Vector3[m_wheels.Length];
        m_rigidbody = GetComponent<Rigidbody>();


        for (int i = 0; i < m_maxConcurrentDecals; ++i)
        {
            InstantiateDecal();
        }

        for (int i = 0; i < m_wheels.Length; ++i)
        {
            Vector3 ratio = m_wheels[i].GetComponentInChildren<Renderer>().bounds.size;
            Vector3 trackSize = m_trackDecalPrefab.GetComponent<SpriteRenderer>().bounds.size;
            ratio.x *= trackSize.x;
            ratio.y /= trackSize.y;
            ratio.z /= trackSize.z;

            m_wheelScales[i] = m_trackDecalPrefab.transform.localScale;
            m_wheelScales[i].x *= ratio.x;
            m_wheelScales[i].y *= ratio.y;
            m_wheelScales[i].z *= ratio.z;

            m_wheelTransformRatio[i] = ratio;
        }
    }

    private void InstantiateDecal()
    {
        var spawned = Instantiate(m_trackDecalPrefab);
        spawned.transform.SetParent(transform);

        m_decalsInPool.Enqueue(spawned);
        spawned.SetActive(false);
    }

    public GameObject SpawnDecal(RaycastHit p_hit)
    {
        GameObject decal = GetNextAvailableDecal();

        if (decal != null)
        {
            decal.transform.position = p_hit.point;
            decal.transform.eulerAngles = p_hit.normal + new Vector3(90f, 90f, 0f) + transform.eulerAngles;
            decal.SetActive(true);
            decal.transform.SetParent(p_hit.collider.transform);
            m_decalsActiveInWorld.Enqueue(decal);

            return decal;
        }

        return null;
    }

    private GameObject GetNextAvailableDecal()
    {
        if (m_decalsInPool.Count > 0)
            return m_decalsInPool.Dequeue();

        var oldestActiveDecal = m_decalsActiveInWorld.Dequeue();
        return oldestActiveDecal;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        for (int i = 0; i < m_wheels.Length; ++i)
        {
            if (Physics.Raycast(m_wheels[i].transform.position, -transform.up, out hit, 1f)
                    && m_rigidbody.velocity.magnitude > 1f)
            {
                GameObject decal = SpawnDecal(hit);

                decal.transform.localScale = m_wheelScales[i];
            }
        }
    }

}
