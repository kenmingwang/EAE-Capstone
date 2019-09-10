using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    private Vector3 m_camRot;
    private Transform m_camTransform;//摄像机Transform
    private Transform m_transform;//摄像机父物体Transform
    private Rigidbody rBody;
    public float m_movSpeed = 10;//移动系数
    public float m_rotateSpeed = 1;//旋转系数
    private void Start()
    {


        m_camRot.x = 35;
        m_camTransform = Camera.main.transform;

        m_transform = GetComponent<Transform>();
        rBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Control();
        rBody.velocity = new Vector3(0, 0, 0);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
    void Control()
    {

        if (Input.GetMouseButton(1))
        {
            //获取鼠标移动距离
            float rh = Input.GetAxis("Mouse X");
            float rv = Input.GetAxis("Mouse Y");

            // 旋转摄像机
            m_camRot.x -= rv * m_rotateSpeed;
            m_camRot.y += rh * m_rotateSpeed;
        }


        m_camTransform.eulerAngles = m_camRot;

        // 使主角的面向方向与摄像机一致


        // 定义3个值控制移动
        float xm = 0, ym = 0, zm = 0;

        //按键盘W向上移动
        if (Input.GetKey(KeyCode.W))
        {
            zm += m_movSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))//按键盘S向下移动
        {
            zm -= m_movSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))//按键盘A向左移动
        {
            xm -= m_movSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))//按键盘D向右移动
        {
            xm += m_movSpeed * Time.deltaTime;
        }

        m_transform.Translate(new Vector3(xm, 0, zm), Space.Self);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))  // or if(gameObject.CompareTag("YourWallTag"))
        {
            rBody.velocity = Vector3.zero;
        }
    }

}