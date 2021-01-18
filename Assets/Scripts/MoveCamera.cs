using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private float moveSpeed = 7;

    [SerializeField]
    GameObject GameObject;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.localEulerAngles = Vector3.zero;
    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        GameObject.transform.localEulerAngles = new Vector3(0, -Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg + 90, 0);

        if (direction.x != 0 ||
            direction.z != 0)
        {
            transform.position += VelocityCalculation(moveSpeed);
            GameObject.transform.position += VelocityCalculation(moveSpeed);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }

        ScrollCamera();
    }

    void ScrollCamera()
    {
        Vector3 angle = new Vector3(Input.GetAxis("Mouse Y") * -4.0f, Input.GetAxis("Mouse X") * 4.0f, 0);

        transform.localEulerAngles += angle;

        //Debug.Log(angle);
        //transform.RotateAround(transform.position, Vector3.up, angle.x);
        //transform.RotateAround(transform.position, Vector3.right, angle.y);
    }

    Vector3 VelocityCalculation(float speed)
    {
        Vector3 velocity;

        velocity.x = speed * Mathf.Sin(Mathf.Deg2Rad * (GameObject.transform.localEulerAngles.y + transform.localEulerAngles.y)) * Time.deltaTime;
        velocity.z = speed * Mathf.Cos(Mathf.Deg2Rad * (GameObject.transform.localEulerAngles.y + transform.localEulerAngles.y)) * Time.deltaTime;
        velocity.y = 0;

        return velocity;
    }
}
