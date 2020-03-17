using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float m_steerIntensity;
    private float m_maxSteerAngle;
    private GameObject m_sail;
    private WheelCollider[] m_wheels;
    private Vector3 m_lastCheckpointPos;
    private Quaternion m_lastCheckpointRot;
    private List<Transform> m_wheelMeshes = new List<Transform>();

    void Start ()
    {
        m_wheels = GetComponentsInChildren<WheelCollider>();
        m_lastCheckpointPos = transform.position;
        m_lastCheckpointRot = transform.rotation;

        m_sail = GameObject.FindGameObjectWithTag("Sail");

        if (m_sail == null)
            Debug.LogError("GameObject with Sail tag not found");

        else
        {
            for (int i = 0; i < m_wheels.Length; ++i)
            {
                m_wheelMeshes.Add(m_wheels[i].gameObject.transform);
            }
        }
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene(0);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_sail.GetComponent<Sail>().RotateSail(1f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_sail.GetComponent<Sail>().RotateSail(-1f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Vehicle>().Brake();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<Vehicle>().ReleaseBrake();
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetAxis("Fire2") > 0f)
        {
            transform.SetPositionAndRotation(
                m_lastCheckpointPos,
                m_lastCheckpointRot
                );

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.C) || Input.GetAxis("Fire1") > 0f)
        {
            GetComponent<Vehicle>().RepositionVehicle();
        }

        if (Input.GetAxis("R-Horizontal") != 0f)
        {
            m_sail.GetComponent<Sail>().RotateSail(-Input.GetAxis("R-Horizontal"));
        }

        if (Input.GetAxis("Horizontal") >= 0.05f || Input.GetAxis("Horizontal") <= -0.05f)
        {
            SteerWheels(Input.GetAxis("Horizontal"));
        }
        else
        {
            ResetWheelSteer();
        }
    }

    //true == go left ; false == go right
    private void SteerWheels(float p_intensity)
    {
        float steerStrength = m_steerIntensity * p_intensity;

        foreach (WheelCollider child in m_wheels)
        {
            if (child.CompareTag("Front Wheel"))
            {
                float currSteering = child.steerAngle;

                if (currSteering >= m_maxSteerAngle || currSteering <= -m_maxSteerAngle)
                    return;

                child.steerAngle += steerStrength;
            }
        }
    }

    private void ResetWheelSteer()
    {
        foreach (WheelCollider child in m_wheels)
        {
            if (child.steerAngle != 0f)
            {
                child.steerAngle = 0f;
            }
        }
    }

    public void SetLastCheckpoint(Transform p_transform)
    {
        m_lastCheckpointPos = p_transform.position;
        m_lastCheckpointRot = p_transform.rotation;
    }

    public void Set(float p_steerIntensity, float p_maxSteerAngle)
    {
        m_steerIntensity = p_steerIntensity;
        m_maxSteerAngle = p_maxSteerAngle;
    }
}
