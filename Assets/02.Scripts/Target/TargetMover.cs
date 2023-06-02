using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMover : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private Vector3 addPos;
    private Vector3 startPos;
    private void Start() {
        startPos = transform.position;
    }
    private float time;
    private void Update() {
        time += Time.deltaTime;
        transform.position = startPos + addPos * Mathf.Sin(time * speed);
    }
}
