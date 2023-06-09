using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AstarFinder : MonoBehaviour {
    public GridSpawner gridSpawner;

    private List<Vector2Int> path = new List<Vector2Int>();

    private bool isFind = false;

    private Vector2Int prePos = new Vector2Int(-1, -1);

    [SerializeField] private List<Vector2Int> painting_list = new List<Vector2Int>();

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.CompareTag("GRID")) {
                    Vector2Int targetPos = new Vector2Int((int)hit.transform.localPosition.x, (int)hit.transform.localPosition.z);
                    if (prePos == new Vector2Int(-1, -1)) {
                        ResetPaint();
                        prePos = targetPos;
                        PaintNode(Color.green, targetPos);
                    } else if (prePos != targetPos) {
                        FindAsta(prePos, targetPos);
                        PaintPath(Color.green);
                        prePos = new Vector2Int(-1, -1);
                    }
                }
            }
        }
    }
    private void ResetPaint() {
        foreach (var pos in painting_list) {
            gridSpawner.nodeMap_list[pos.x][pos.y].obj.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        painting_list.Clear();
    }
    private void PaintNode(Color color, Vector2Int pos) {
        painting_list.Add(pos);
        gridSpawner.nodeMap_list[pos.x][pos.y].obj.GetComponent<MeshRenderer>().material.color = color;
    }

    private void PaintPath(Color color) {
        for (int i = 0; i < path.Count; i++) {
            PaintNode(color, path[i]);
        }
    }

    private void FindAsta(Vector2Int startPos, Vector2Int targetPos) {

        PriorityQueue<GridData> openSet = new PriorityQueue<GridData>();
        bool[,] closedSet = closedSet = new bool[gridSpawner.nodeSize.x, gridSpawner.nodeSize.y];

        GridData startNode = gridSpawner.nodeMap_list[startPos.x][startPos.y];
        GridData targetNode = gridSpawner.nodeMap_list[targetPos.x][targetPos.y];

        startNode.G = 0;
        startNode.H = Vector2Int.Distance(startNode.pos, targetNode.pos);
        startNode.F = startNode.H;

        openSet.Enqueue(startNode, startNode.H);

        isFind = false;
        path.Clear();
        while (openSet.Count > 0) {
            GridData currentNode = openSet.Dequeue();
            closedSet[currentNode.pos.x, currentNode.pos.y] = true;
            if (currentNode == targetNode) {
                path.Add(targetPos);
                isFind = true;
                break;
            }

            foreach (GridData neighborNode in FindNeigborNodes(currentNode)) {
                if (closedSet[neighborNode.pos.x, neighborNode.pos.y]) continue;
                if (neighborNode.isNotUse) continue;
                neighborNode.H = Vector2Int.Distance(neighborNode.pos, targetPos);
                neighborNode.prePos = currentNode.pos;
                PaintNode(Color.yellow, neighborNode.pos);
                closedSet[neighborNode.pos.x, neighborNode.pos.y] = true;
                openSet.Enqueue(neighborNode, neighborNode.H);
            }
        }

        Vector2Int findPos = targetPos;

        while (isFind) {
            GridData node = gridSpawner.nodeMap_list[findPos.x][findPos.y];
            findPos = node.prePos;
            path.Add(findPos);
            if (findPos == startPos) break;
        }

    }


    private List<GridData> FindNeigborNodes(GridData node) {
        List<GridData> neigbor_list = new List<GridData>();
        //up
        if (node.pos.y + 1 < gridSpawner.nodeSize.y) {
            neigbor_list.Add(gridSpawner.nodeMap_list[node.pos.x][node.pos.y + 1]);
        }
        // right
        if (node.pos.x + 1 < gridSpawner.nodeSize.x) {
            neigbor_list.Add(gridSpawner.nodeMap_list[node.pos.x + 1][node.pos.y]);
        }
        // down
        if (node.pos.y - 1 >= 0) {
            neigbor_list.Add(gridSpawner.nodeMap_list[node.pos.x][node.pos.y - 1]);
        }
        // left
        if (node.pos.x - 1 >= 0) {
            neigbor_list.Add(gridSpawner.nodeMap_list[node.pos.x - 1][node.pos.y]);
        }
        return neigbor_list;
    }

}
