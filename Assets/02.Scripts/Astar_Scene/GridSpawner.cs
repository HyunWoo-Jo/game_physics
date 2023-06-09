using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    [SerializeField] GameObject gridPrefab;
    public Vector2Int nodeSize;
    [HideInInspector] public List<List<GridData>> nodeMap_list = new List<List<GridData>>();
    private void Awake() {

        transform.position = new Vector3(-(nodeSize.x / 2), -(nodeSize.x + nodeSize.y) / 2 , -(nodeSize.y / 2));

        for (int x = 0; x < nodeSize.x; x++) {
            nodeMap_list.Add(new List<GridData>());
            for(int y= 0; y< nodeSize.y; y++) {
                var obj = Instantiate(gridPrefab);
                obj.transform.SetParent(transform);
                var mat = obj.GetComponent<MeshRenderer>().material;

                obj.transform.localPosition = new Vector3(x, 0, y);
                obj.transform.localEulerAngles = new Vector3(90, 0, 0);      
                int ranValue = Random.Range(0, 3);

                nodeMap_list[x].Add(new GridData { obj = obj, pos = new Vector2Int(x, y) });
                if(ranValue == 0) {
                    mat.color = Color.red;
                    nodeMap_list[x][y].isNotUse = true;
                    obj.GetComponent<MeshCollider>().enabled = false;
                }
               

            }
        } 
    }
}
