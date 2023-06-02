using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [HideInInspector] public List<GameObject> targetList = new List<GameObject>();

    [SerializeField] private Vector3 randomRange;
    [SerializeField] private int spawnCount;
    [SerializeField] private bool isAutoSpawn;

    private void Awake() {
        if (isAutoSpawn) {
            NewSpawn();
        }
    }
    public void NewSpawn() {
        while(targetList.Count > 0) {
            Destroy(targetList[targetList.Count - 1]);
            targetList.RemoveAt(targetList.Count - 1);
        }
        for (int i = 0; i < spawnCount; i++) {
            var obj = Instantiate(targetPrefab);
            obj.transform.position = transform.position + new Vector3(Random.Range(-randomRange.x, randomRange.x), 0, Random.Range(-randomRange.z, randomRange.z));
            targetList.Add(obj);
        }
    }
}
