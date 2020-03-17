using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float m_mouseSensitivity;
    private float m_zoomSensitivity;

    private float m_fieldOfView;
    private float m_maxFieldOfView;
    private float m_FOVChangeIntensity;
    private float m_FOVRatio = 1f;
    private float m_maxSpeed;

    private Rigidbody m_vehicle;
    private Camera m_camera;

    private Vector3 m_zoomVelocity = Vector3.zero;

	void Start ()
    {
        m_camera = GetComponentInChildren<Camera>();
        m_vehicle = GetComponentInParent<Rigidbody>();
        m_maxSpeed = m_vehicle.gameObject.GetComponent<Vehicle>().GetMaxSpeed();
	}
	
	void Update ()
    {
    }

    private void LateUpdate()
    {
        if (m_camera.fieldOfView >= m_maxFieldOfView)
            return;

        m_FOVRatio = 1f + (m_vehicle.velocity.magnitude / m_maxSpeed)
            / m_FOVChangeIntensity;

        if (m_FOVRatio <= 2f)
            m_camera.fieldOfView = m_fieldOfView * m_FOVRatio;

            OrbitCamera();
            Zoom();
    }

    public void Set(float p_fieldOfView, float p_maxFieldOfView, float p_FOVChangeIntensity, float p_mouseSensitivity, float p_zoomSensitivity)
    {
        m_fieldOfView = p_fieldOfView;
        m_maxFieldOfView = p_maxFieldOfView;
        m_FOVChangeIntensity = p_FOVChangeIntensity;
        m_mouseSensitivity = p_mouseSensitivity;
        m_zoomSensitivity = p_zoomSensitivity;
    }

    public void OrbitCamera()
    {
        Vector3 target = m_vehicle.transform.position;
        float yRotate = Input.GetAxis("Mouse X") * m_mouseSensitivity;
        float xRotate = Input.GetAxis("Mouse Y") * m_mouseSensitivity;

        /*if (Physics.Raycast(transform.position, -Vector3.up, 1f))
            xRotate = 0f;*/

        OrbitCamera(target, yRotate, xRotate);
    }

    public void OrbitCamera(Vector3 p_target, float p_yRotate, float p_xRotate)
    {
        Vector3 angles = transform.eulerAngles;
        angles.z = 0;
        transform.eulerAngles = angles;

        transform.RotateAround(p_target, Vector3.up, p_yRotate);
        transform.RotateAround(p_target, Vector3.left, p_xRotate);

        transform.LookAt(p_target);
    }

    public void Zoom()
    {
        Vector3 newPos = transform.position + transform.forward * Input.GetAxis("Mouse ScrollWheel") * m_zoomSensitivity;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref m_zoomVelocity, 0.5f);
    }
}
