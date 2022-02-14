using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float moveSpeedMult = 20;
    Vector3 moveTarget;

    // Start is called before the first frame update
    void Start()
    {
        moveTarget = transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            move += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right;
        }

        move.Normalize();
        moveTarget += move * moveSpeedMult * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, moveTarget, Time.deltaTime * 5.0f);
    }
}
