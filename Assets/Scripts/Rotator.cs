using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Vector3 rotateAmount = new Vector3(50f, 90, 130);
    void Update()
    {
        transform.Rotate(rotateAmount * Time.deltaTime);
    }
}