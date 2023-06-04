using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GirdNode {
    public GameObject obj;
    public Vector2Int pos;
    public Vector2Int prePos;
    public float H;
    public float G;
    public float F;
}
public class NodeSpawner : MonoBehaviour
{
    [SerializeField] GameObject nodePrefab;
    public Vector2Int nodeSize;
    [HideInInspector] public List<List<GirdNode>> nodeMap_list = new List<List<GirdNode>>();
    [HideInInspector] public Vector2Int startIndex;
    private void Awake() {
        for (int x = 0; x < nodeSize.x; x++) {
            nodeMap_list.Add(new List<GirdNode>());
            for(int y= 0; y< nodeSize.y; y++) {
                var obj = Instantiate(nodePrefab);
                obj.transform.SetParent(transform);
                var mat = obj.GetComponent<MeshRenderer>().material;

                obj.transform.localPosition = new Vector3(x * 2, 0, y * 2);
                obj.transform.localEulerAngles = new Vector3(90, 0, 0);      
                int ranValue = Random.Range(0, 6);

                nodeMap_list[x].Add(new GirdNode { obj = obj, pos = new Vector2Int(x, y) });
                if (x == nodeSize.x / 2 && y == nodeSize.y / 2) {
                    mat.color = Color.green;
                    startIndex = new Vector2Int(x, y);
                }
                if(ranValue == 0 && startIndex.x != x  && startIndex.y != y) {
                    mat.color = Color.red;
                    nodeMap_list[x][y].obj = null;
                    obj.GetComponent<MeshCollider>().enabled = false;
                }
               

            }
        } 
    }
}
