using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraNodeMap : MonoBehaviour
{
    [SerializeField] public Transform nodeParent;
    private DijkstraNode[] dijkstraNodes;
    public readonly Dictionary<int, List<DijkstraNodeData>> nodeMap_dic = new Dictionary<int, List<DijkstraNodeData>>();

    public int Count {
        get => dijkstraNodes.Length;
    }
    private void Awake() {
        Init();
        CreateMap();
    }

    private void Init() {
        dijkstraNodes = nodeParent.GetComponentsInChildren<DijkstraNode>();
    }

    private void CreateMap() {
        dijkstraNodes = nodeParent.GetComponentsInChildren<DijkstraNode>();

        for (int index = 0; index < dijkstraNodes.Length; index++) {
            dijkstraNodes[index].Init(index);
        }
        for (int index = 0; index < dijkstraNodes.Length; index++) {
            dijkstraNodes[index].CreateData();
            // Add node connected Data
            if (!nodeMap_dic.ContainsKey(index)) {
                nodeMap_dic.Add(index, new List<DijkstraNodeData>());
            }
            foreach (var nodeData in dijkstraNodes[index].nodeData) {
                if (!nodeMap_dic[index].Contains(nodeData)) {
                    nodeMap_dic[index].Add(nodeData);
                }
            }
            // Add node reversed connected Data
            foreach (var nodeData_reversed in dijkstraNodes[index].nodeData_reversed) {
                if (!nodeMap_dic.ContainsKey(nodeData_reversed.index)){
                    nodeMap_dic.Add(nodeData_reversed.index, new List<DijkstraNodeData>());
                }
                if (!nodeMap_dic[nodeData_reversed.index].Contains(nodeData_reversed)) {
                    nodeMap_dic[nodeData_reversed.index].Add(nodeData_reversed);
                }
            }
        }
    }
}
