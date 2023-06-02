using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
public class PCA : MonoBehaviour
{
    [SerializeField] private TargetRandomSpawner randomSpawner;
    [SerializeField] private LineRenderer line;

    [SerializeField] private float updateTime = 5f;

    private float timer;
    [SerializeField] private float lineLenth = 10f;
    private IEnumerator Start() {
        WaitForSeconds waitTime = new WaitForSeconds(updateTime);
        while (true) {
            Spawn();
            PCA_line();
            yield return waitTime;
        }
    }

    private void Spawn() {
        randomSpawner.NewSpawn();
    }

   
    private void PCA_line() {

        // m = (1 / n) * ¥Ò(i)
        Vector3 m = Vector3.zero;
        foreach(var targetObj in randomSpawner.targetList) {
            m += targetObj.transform.position;
        }
        m /= randomSpawner.targetList.Count;

        // deviation di = i - m
        List<Vector3> deviationPosList = new List<Vector3>();
        foreach(var targetObj in randomSpawner.targetList) {
            deviationPosList.Add(targetObj.transform.position - m);
        }

        // covariance matrix
        float3x3 covMatrix = float3x3.zero;
        // covMatrix[i, j] = ¥Ò((deviationPosList[k][i] * deviationPosList[k][j]))
        for (int i = 0; i < 3; i++) {
            for(int j = 0; j < 3; j++) {
                for(int k = 0;k < deviationPosList.Count;k++) {
                    covMatrix[i][j] += deviationPosList[k][i] * deviationPosList[k][j];
                }
                covMatrix[i][j] /= deviationPosList.Count - 1;
            }
        }

        // a - b
        // - - -
        // c - d
        float a = covMatrix[0][0];
        float b = covMatrix[0][2];
        float c = covMatrix[2][0];
        float d = covMatrix[2][2];

        float mainDiagonal = a + d;
        float det = a * d - b * c;

        float square = math.sqrt(mainDiagonal * mainDiagonal / 4.0f - det);
        float p1 = mainDiagonal / 2.0f + square;
        float p2 = mainDiagonal / 2.0f - square;

        Vector3 x = Vector3.zero;
        Vector3 z = Vector3.zero;
        if(math.abs(a) < math.EPSILON && math.abs(c) <= math.EPSILON) {
            x = new Vector3(1, 0, 0);
        } else if(math.abs(b) > math.EPSILON) {
            x = new Vector3(b, 0, p1 - a);
        } else if(math.abs(c) > math.EPSILON) {
            x = new Vector3(p1 - d, 0, c);
        }
        x.Normalize();

        List<Vector3> linePos = new List<Vector3>();
        linePos.Add(m - x * lineLenth);
        linePos.Add(m + x * lineLenth);
        line.positionCount = linePos.Count;
     
        for (int i = 0; i < linePos.Count; i++) {
            line.SetPosition(i, linePos[i]);
        }

    }

}
