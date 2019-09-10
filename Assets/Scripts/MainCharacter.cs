using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    private Vector3 m_camRot;
    private Transform m_camTransform;
    private Transform m_transform;
    private Rigidbody rBody;

    public float m_movSpeed = 10;
    public float m_rotateSpeed = 1;

    private void Start()
    {
        m_camRot.x = 35; // Fixed angle when initiate
        m_camTransform = Camera.main.transform;

        m_transform = GetComponent<Transform>();
        rBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Control();

        /* Bad code to force igonre "Bouncing" */
        rBody.velocity = new Vector3(0, 0, 0);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    void Control()
    {

        if (Input.GetMouseButton(1))
        {
            float rh = Input.GetAxis("Mouse X");
            float rv = Input.GetAxis("Mouse Y");

            m_camRot.x -= rv * m_rotateSpeed;
            m_camRot.y += rh * m_rotateSpeed;
        }

        m_camTransform.eulerAngles = m_camRot;

        float xm = 0, zm = 0;

        if (Input.GetKey(KeyCode.W))
        {
            zm += m_movSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            zm -= m_movSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            xm -= m_movSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xm += m_movSpeed * Time.deltaTime;
        }
        m_transform.Translate(new Vector3(xm, 0, zm), Space.Self);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            rBody.velocity = Vector3.zero;
        }
    }

}