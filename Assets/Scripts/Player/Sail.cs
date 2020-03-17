using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sail : MonoBehaviour
{
    private Transform m_sailOriginPos;
    private Vector3 m_windDirection;
    private Vector3 m_sailNormal;
    private Transform m_vehicleTransform;
    private float m_pushIntensity;
    private float m_sailRotationY = 0f;

    private float m_maxSailRotation = 90f;
    private float m_rotationSpeed = 1.0f;

    void Start()
    {
        m_sailNormal = transform.forward;
        m_vehicleTransform = GetComponentsInParent<Transform>()[1];
    }

    void FixedUpdate()
    {
        Vector3 tmpEulerAngles = m_vehicleTransform.localEulerAngles;
        m_sailNormal = -transform.right;

        transform.localPosition = new Vector3(0f, 0.0f, 0.0f);
        transform.localEulerAngles = new Vector3(
            tmpEulerAngles.x,
            m_sailRotationY,
            tmpEulerAngles.z);

        m_pushIntensity = Mathf.Abs(Vector3.Dot(m_sailNormal, m_windDirection));

        if (0 > Vector3.Dot(m_vehicleTransform.forward, m_windDirection))
            m_pushIntensity *= -1;
    }

    private void OnTriggerStay(Collider p_other)
    {
        if (p_other.gameObject.CompareTag("Wind"))
        {
            m_windDirection = p_other.gameObject.GetComponent<Wind>().GetDirection();
        }
    }

    //true == go left ; false == go right
    public void RotateSail(float p_intensity)
    {
        if (m_sailRotationY + p_intensity >= m_maxSailRotation ||
            m_sailRotationY + p_intensity <= -m_maxSailRotation)
        {
            return;
        }

        m_sailRotationY += m_rotationSpeed * p_intensity;
    }

    public float GetIntensity()
    {
        return m_pushIntensity;
    }

    public Vector3 GetWindDirection()
    {
        return m_windDirection;
    }

    public void Set(float p_maxSailRot, float p_rotSpeed)
    {
        m_maxSailRotation = p_maxSailRot;
        m_rotationSpeed = p_rotSpeed;
    }

}
