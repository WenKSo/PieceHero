using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;
    public float speed = 70f;
    private Vector3 axis;

    void Awake()
    {
        axis = new Vector3(xAngle, yAngle, zAngle);
    }

    void Update()
    {
        transform.Rotate(axis, speed * Time.deltaTime);
    }
}
