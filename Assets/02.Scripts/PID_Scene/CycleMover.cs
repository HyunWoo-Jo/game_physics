using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleMover : MonoBehaviour
{

    [SerializeField] private float rotateSpeed = 120;
    [SerializeField] private float speed = 25;

   
    private void Update() {
        transform.position += transform.forward * speed * Time.deltaTime;
        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
    }
}
