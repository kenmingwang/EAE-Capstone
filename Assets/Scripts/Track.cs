using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Just really bad code here to test stuff out.. */
public class Track : MonoBehaviour
{
    float moveSpeed;
    float CurDist;
    GameObject MainObject;
    Rigidbody rBody;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();

        moveSpeed = 0.1f;
        MainObject = GameObject.Find("MainObject");
        Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), MainObject.GetComponent<CapsuleCollider>());
        Vector3 MainPos = MainObject.transform.position;
        Vector3 CurPOs = transform.position;
        CurDist = Vector3.Distance(MainPos, CurPOs);
    }

    // Update is called once per frame
    void Update()
    {    
        Vector3 MainPos = MainObject.transform.position;
        Vector3 CurPOs = transform.position;

        float dist = Vector3.Distance(MainPos, CurPOs);

        if (dist < CurDist && dist != 0 )
        {
            transform.LookAt(MainObject.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            transform.position += transform.forward * moveSpeed;
            MainPos = MainObject.transform.position;
            CurPOs = transform.position;
            dist = Vector3.Distance(MainPos, CurPOs);
            CurDist = dist;
            rBody.velocity = new Vector3(0,0,0);
            return;
        }

        if (dist > CurDist && dist != 0)
        {
            transform.LookAt(MainObject.transform);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            transform.position += transform.forward * (moveSpeed/2);
            MainPos = MainObject.transform.position;
            CurPOs = transform.position;
            dist = Vector3.Distance(MainPos, CurPOs);
            CurDist = dist;
            rBody.velocity = new Vector3(0, 0, 0);
            return;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.name == "MainObject")
        {
            rBody.velocity = Vector3.zero;
        }
    }
}
