using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIDObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pidPrefab;
    [SerializeField] private Transform target;
    [SerializeField] private float spawnRate = 0.5f;

    private IEnumerator Start() {
        var waitTime = new WaitForSeconds(spawnRate);
        while (true) {
            yield return waitTime;
            var obj = Instantiate(pidPrefab);
            obj.transform.position = transform.position;
            obj.transform.rotation = Random.rotation;
            var pid = obj.GetComponent<TracePID>();
            pid.SetTarget(target);
            
        }
    }

}
