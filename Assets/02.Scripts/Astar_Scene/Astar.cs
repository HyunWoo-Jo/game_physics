using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PriorityQueue<T> {
    private List<Tuple<T, float>> elements = new List<Tuple<T, float>>();

    public int Count => elements.Count;

    public void Enqueue(T item, float priority) {
        elements.Add(Tuple.Create(item, priority));
    }

    public T Dequeue() {
        if (elements.Count == 0) {
            throw new InvalidOperationException("PriorityQueue is empty");
        }

        int bestIndex = 0;
        for (int i = 1; i < elements.Count; i++) {
            if (elements[i].Item2 < elements[bestIndex].Item2) {
                bestIndex = i;
            }
        }

        T bestItem = elements[bestIndex].Item1;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }

    public bool Contains(T item) {
        for (int i = 0; i < elements.Count; i++) {
            if (EqualityComparer<T>.Default.Equals(elements[i].Item1, item)) {
                return true;
            }
        }
        return false;
    }

}

public class Astar : MonoBehaviour
{
    public NodeSpawner nodeSpawner;

    private List<Vector2Int> path = new List<Vector2Int>();

    private bool isFind = false;
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.CompareTag("NODE")) {
                    Vector2Int targetPos = new Vector2Int((int)hit.transform.localPosition.x / 2, (int)hit.transform.localPosition.z / 2);
                    PaintPath(Color.white);
                    FindAsta(nodeSpawner.startIndex, targetPos);
                    PaintPath(Color.yellow);
                }
            }
        }
    }

    private void PaintPath(Color color) {
        for (int i = 0; i < path.Count; i++) {
            nodeSpawner.nodeMap_list[path[i].x][path[i].y].obj.GetComponent<MeshRenderer>().material.color = color;
        }
    }

    private void FindAsta(Vector2Int startPos, Vector2Int targetPos) {

        PriorityQueue<Node> openSet = new PriorityQueue<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        Node startNode = nodeSpawner.nodeMap_list[startPos.x][startPos.y];
        Node targetNode = nodeSpawner.nodeMap_list[targetPos.x][targetPos.y];

        startNode.G = 0;
        startNode.H = Vector2Int.Distance(startNode.pos, targetNode.pos);
        startNode.F = startNode.H;

        openSet.Enqueue(startNode, startNode.H);

        isFind = false;
        path.Clear();
        while (openSet.Count > 0) {
            Node currentNode = openSet.Dequeue();
            closedSet.Add(currentNode);
            if(currentNode == targetNode) {
                path.Add(targetPos);
                isFind = true;
                break;
            }
            foreach(Node neighborNode in FindNeigborNodes(currentNode)) {
                if (closedSet.Contains(neighborNode)) continue;
                if (neighborNode.obj == null) continue;
                float G = currentNode.G + Vector2Int.Distance(currentNode.pos, neighborNode.pos);
                if(G < neighborNode.G || !openSet.Contains(neighborNode)) {
                    neighborNode.G = G;
                    neighborNode.H = Vector2Int.Distance(neighborNode.pos, targetPos);
                    neighborNode.F = neighborNode.G + neighborNode.H;
                    neighborNode.prePos = currentNode.pos;

                    if (!openSet.Contains(neighborNode)) {
                        openSet.Enqueue(neighborNode, neighborNode.H);
                    }
                }
            }
        }
        
       

        Vector2Int findPos = targetPos;
        
        while (isFind) {
            Node node = nodeSpawner.nodeMap_list[findPos.x][findPos.y];
            findPos = node.prePos;
            path.Add(findPos);
            if (findPos == startPos) break;
        }

    }

    private List<Node> FindNeigborNodes(Node node) {

        List<Node> neigbor_list = new List<Node>();

        //up
        if (node.pos.y + 1 < nodeSpawner.nodeSize.y) {
            Node neNode = nodeSpawner.nodeMap_list[node.pos.x][node.pos.y + 1];
            neigbor_list.Add(nodeSpawner.nodeMap_list[node.pos.x][node.pos.y + 1]);
        }
        // right
        if (node.pos.x + 1 < nodeSpawner.nodeSize.x) {
            neigbor_list.Add(nodeSpawner.nodeMap_list[node.pos.x + 1][node.pos.y]);
        }
        // down
        if (node.pos.y - 1 >= 0) {
            neigbor_list.Add(nodeSpawner.nodeMap_list[node.pos.x][node.pos.y - 1]);
        }
        // left
        if (node.pos.x - 1 >= 0) {
            neigbor_list.Add(nodeSpawner.nodeMap_list[node.pos.x - 1][node.pos.y]);
        }

        return neigbor_list;
    }
    
}
