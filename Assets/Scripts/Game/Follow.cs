using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    private GameObject target;
    public Vector3 offset;
    Vector3 targetPos;

    void Update()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;
            Vector3 targetDirection = (target.transform.position - posNoZ);
            interpVelocity = targetDirection.magnitude * 5f;
            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
            if (targetPos.x < 20)
            {
                transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.20f);
            }   
                //transform.position = Vector3.Lerp(transform.position, targetPos, 0.20f);
        }
        else
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length > 0)
                target = GameObject.FindGameObjectsWithTag("Player")[GameObject.FindGameObjectsWithTag("Player").Length - 1].gameObject;
            else
                target = GameObject.FindGameObjectsWithTag("Player")[0].gameObject;
        }
    }
}


