using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedForceUI : MonoBehaviour
{
    private Rigidbody m_vehicle;
    private Text m_speed;
    private Text m_time;
    
	void Start ()
    {
        m_vehicle = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        m_speed = GetComponentsInChildren<Text>()[0];
        m_time = GetComponentsInChildren<Text>()[1];

        Cursor.visible = false;
    }
	
	void Update ()
    {
        m_speed.text = "Speed: " + m_vehicle.velocity.magnitude;
        m_time.text = "Time: " + Time.realtimeSinceStartup;

    }
}
