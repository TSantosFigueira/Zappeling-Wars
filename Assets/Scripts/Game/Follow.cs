using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;

    // Use this for initialization

    void Start()
    {
        //targetPos = transform.position;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;
            Vector3 targetDirection = (target.transform.position - posNoZ);
            interpVelocity = targetDirection.magnitude * 5f;
            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").gameObject;
            targetPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
        }
    }

}


    //public Transform target;
    //public float smooth = 5.0f;

    //void Update()
    //{
    //    if(!target)
    //        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    //    else
    //    {
    //        transform.position = Vector3.Lerp (transform.position, target.position, Time.deltaTime * smooth);
    //    }
        
    //}


