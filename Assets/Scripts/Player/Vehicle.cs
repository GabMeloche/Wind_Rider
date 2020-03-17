using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Vehicle : MonoBehaviour
{
    private WheelCollider[] m_wheelColliders;
    private float[] m_dampers;
    private float[] m_stiffnesses;

    [Header("Player Input")]
    [SerializeField] private float m_steerIntensity = 2.0f;
    [SerializeField] private float m_maxSteerAngle = 50f;
    private PlayerInput m_playerInput;

    [Header("Physics")]
    [SerializeField] private float m_maxSpeed = 30.0f;
    [SerializeField] private float m_bodyMass = 500f;
    [SerializeField] private float m_bodyAirResistance = 0.1f;
    [SerializeField] private Vector3 m_centerOfMassOffset;

    [Header("Camera")]
    [SerializeField] private float m_fieldOfView = 60f;
    [SerializeField] private float m_maxFieldOfView = 90f;
    [SerializeField] private float m_FOVChangeIntensity = 1.4f;
    [Range(0.1f, 10.0f)] public float m_mouseSensitivity;
    [Range(0.1f, 100.0f)] public float m_zoomSensitivity;
    private CameraScript m_cameraScript;

    [Header("Sail")]
    [SerializeField] private float m_maxSailRotation = 90f;
    [SerializeField] private float m_sailRotationSpeed = 1.0f;
    private Sail[] m_sails;

    [Header("Wheels")]
    [SerializeField] private float m_suspensionDistance;
    [SerializeField] private float m_suspensionTargetPosition = 0.5f;
    [SerializeField] private float m_roadAdherence = 1f;
    [Range(0.01f, 10f)] public float m_suspensionLooseness = 1f;


    void Start ()
    {
        //Wheels setup
        m_wheelColliders = GetComponentsInChildren<WheelCollider>();
        int numberOfWheels = m_wheelColliders.Length;
        m_dampers = new float[numberOfWheels];
        m_stiffnesses = new float[numberOfWheels];

        for (int i = 0; i < numberOfWheels; ++i)
        {
            //spring
            m_wheelColliders[i].suspensionDistance = m_suspensionDistance; // * m_suspensionBounciness;
            JointSpring spring = new JointSpring();
            spring.spring = m_wheelColliders[i].suspensionSpring.spring;

            spring.damper = m_wheelColliders[i].suspensionSpring.damper / m_suspensionLooseness;

            spring.targetPosition = m_suspensionTargetPosition;
            m_wheelColliders[i].suspensionSpring = spring;


            m_dampers[i] = m_wheelColliders[i].wheelDampingRate;
            m_stiffnesses[i] = m_roadAdherence;
        }

        //rigidbody
        Vector3 originalCOM = GetComponent<Rigidbody>().centerOfMass;
        GetComponent<Rigidbody>().centerOfMass = m_centerOfMassOffset + GetComponent<Rigidbody>().centerOfMass;

        GetComponent<Rigidbody>().mass = m_bodyMass;
        GetComponent<Rigidbody>().drag = m_bodyAirResistance;

        //Camera
        m_cameraScript = GetComponentInChildren<CameraScript>();

        if (!m_cameraScript)
            Debug.LogError("Vehicle Script could not find CameraScript within vehicle's children");

        m_cameraScript.Set(m_fieldOfView, m_maxFieldOfView, m_FOVChangeIntensity, m_mouseSensitivity, m_zoomSensitivity);

        //Sail
        m_sails = GetComponentsInChildren<Sail>();

        if (m_sails == null)
            Debug.LogError("Vehicle Script could not find any objects with Sail Script");

        foreach (Sail sail in m_sails)
        {
            sail.Set(m_maxSailRotation, m_sailRotationSpeed);
        }

        //Player Input
        m_playerInput = GetComponent<PlayerInput>();

        if (!m_playerInput)
            Debug.LogError("Vehicle Script could not find Player Input Script attached to vehicle");

        m_playerInput.Set(m_steerIntensity, m_maxSteerAngle);
    }

    private void OnCollisionStay(Collision p_other)
    {

        for (int i = 0; i < m_wheelColliders.Length; ++i)
        {

            if (p_other.gameObject.CompareTag("Grass"))
            {
                Debug.Log("Grass");
                m_wheelColliders[i].wheelDampingRate = m_dampers[i];

                WheelFrictionCurve tmpForward = m_wheelColliders[i].forwardFriction;
                tmpForward.stiffness = 1f;

                WheelFrictionCurve tmpSideways = m_wheelColliders[i].sidewaysFriction;
                tmpSideways.stiffness = 1f;

                m_wheelColliders[i].forwardFriction = tmpForward;
                m_wheelColliders[i].sidewaysFriction = tmpSideways;
            }
            else if (p_other.gameObject.CompareTag("Ice"))
            {
                Debug.Log("Ice");
                m_wheelColliders[i].wheelDampingRate =
                    m_dampers[i] / 100f;

                WheelFrictionCurve tmpForward = m_wheelColliders[i].forwardFriction;
                tmpForward.stiffness = 0.1f;

                WheelFrictionCurve tmpSideways = m_wheelColliders[i].sidewaysFriction;
                tmpSideways.stiffness = 0.1f;

                m_wheelColliders[i].forwardFriction = tmpForward;
                m_wheelColliders[i].sidewaysFriction = tmpSideways;

                //Debug.Log(m_wheelColliders[i].forwardFriction.stiffness);

            }
            else if (p_other.gameObject.CompareTag("Asphalt"))
            {
                m_wheelColliders[i].wheelDampingRate =
                    m_dampers[i] * 0.8f;

                WheelFrictionCurve tmpForward = m_wheelColliders[i].forwardFriction;
                tmpForward.stiffness = 1f;

                WheelFrictionCurve tmpSideways = m_wheelColliders[i].sidewaysFriction;
                tmpSideways.stiffness = 1f;

                m_wheelColliders[i].forwardFriction = tmpForward;
                m_wheelColliders[i].sidewaysFriction = tmpSideways;
            }
            else if (p_other.gameObject.CompareTag("Sand"))
            {

            }
            else
            {
                //m_wheelColliders[i].wheelDampingRate = m_dampers[i];
            }
        }
    }

    public void Brake()
    {
        foreach (WheelCollider wheel in m_wheelColliders)
        {
            if (wheel.tag != "Front Wheel")
                wheel.brakeTorque = 400;
        }
    }

    public void ReleaseBrake()
    {
        foreach (WheelCollider wheel in m_wheelColliders)
        {
            if (wheel.tag != "Front Wheel")
                wheel.brakeTorque = 0;

            wheel.motorTorque += 1;
        }
    }

    public float GetMaxSpeed()
    {
        return m_maxSpeed;
    }

    public void RepositionVehicle()
    {
        if (transform.rotation.eulerAngles.z != 0f)
        {
            transform.Rotate(0f, 0f, -transform.rotation.eulerAngles.z);
            GetComponent<Rigidbody>().angularDrag = 0f;
        }
    }

}
