using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DijkstraFinder : MonoBehaviour
{
    [SerializeField] private DijkstraNodeMap map;
    [SerializeField] private List<KeyValuePair<DijkstraNodeData,float>> dp;
    public List<DijkstraNodeData> path = new List<DijkstraNodeData>();
    private DijkstraNode preNode;
    private DijkstraNode clickNode;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.CompareTag("NODE")) {
                    clickNode = hit.collider.GetComponent<DijkstraNode>();
                    if (preNode == null) {
                        clickNode.SetImageColor(Color.green);
                        PaintingPath(Color.yellow);
                        preNode = clickNode;
                    } else {   
                        FindDijkstra(preNode.NodeIndex, clickNode.NodeIndex);
                        PaintingPath(Color.green);
                        preNode.SetImageColor(Color.white);
                        preNode = null;
                    }             
                }
            }
        }
    }

    public void PaintingPath(Color color) {
        foreach(var data in path) {
            data.line_renderer.material.color = color;
        }
    }

    public void FindDijkstra(int startIndex, int targetIndex) {
        // Init Data
        path.Clear();
        var nodeDataList = map.nodeMap_dic[startIndex];
        PriorityQueue<DijkstraNodeData> scheduler_prique = new PriorityQueue<DijkstraNodeData>();
        dp = new List<KeyValuePair<DijkstraNodeData, float>>(map.Count);
        for (int i = 0; i < map.Count; i++) {
            dp.Add(new KeyValuePair<DijkstraNodeData, float>(null , float.MaxValue));
        }
        // add start(connectedNode);
        foreach (var nodeData in nodeDataList) { 
            dp[nodeData.nextNode.NodeIndex] = new KeyValuePair<DijkstraNodeData, float>(nodeData , nodeData.cost);
            scheduler_prique.Enqueue(nodeData, nodeData.cost);
        }
        while(scheduler_prique.Count != 0) {
            var node = scheduler_prique.Dequeue(out float cost);
            if (node.nextNode.NodeIndex == targetIndex) break;
            AddConnectNode(scheduler_prique, node.nextNode.NodeIndex, cost);
        }
        if (dp[targetIndex].Key != null) {
            var nodeData = dp[targetIndex].Key;
            path.Add(nodeData);
            while (nodeData.index != startIndex) { 
                nodeData = dp[nodeData.index].Key;
                path.Add(nodeData);   
            }
        }
    }

    public void AddConnectNode(PriorityQueue<DijkstraNodeData> scheduler_prique,int index, float cost) {
        if(dp[index].Value > cost) {
            dp[index] = new KeyValuePair<DijkstraNodeData, float>(dp[index].Key, cost);
        }
        var nodeDataList = map.nodeMap_dic[index];
        foreach (var nodeData in nodeDataList) {
            if (dp[nodeData.nextNode.NodeIndex].Value > nodeData.cost + cost) {
                dp[nodeData.nextNode.NodeIndex] = new KeyValuePair<DijkstraNodeData, float>(nodeData, nodeData.cost + cost);
                scheduler_prique.Enqueue(nodeData, nodeData.cost + cost);
            }
        }
    }

}
