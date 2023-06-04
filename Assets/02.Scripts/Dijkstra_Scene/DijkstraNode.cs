using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
[Serializable]
public class DijkstraNodeData {
    [HideInInspector] public int index;
    public DijkstraNode nextNode;
    [HideInInspector] public float cost = 0f;
    [HideInInspector] public LineRenderer line_renderer;
}

public class DijkstraNode : MonoBehaviour
{
    [SerializeField] private int nodeIndex;   
    [SerializeField] private GameObject lineRendererPrefab;
    [SerializeField] private GameObject costTextPrefabs;
    [SerializeField] private List<DijkstraNode> nodeList;
    private Image _image;

    public List<DijkstraNodeData> nodeData = new List<DijkstraNodeData>();
    public List<DijkstraNodeData> nodeData_reversed = new List<DijkstraNodeData>();
    private void Awake() {
        _image = GetComponent<Image>();
    }
    public int NodeIndex {
        get => nodeIndex;
    }


    public void SetImageColor(Color _color) {
        _image.color = _color;
    }
   


    public void Init(int index) {
        nodeIndex = index;
        GetComponentInChildren<TextMeshProUGUI>().text = (nodeIndex + 1).ToString();
    }
     
    public void CreateData() {
        for (int i = 0; i < nodeList.Count; i++) {
            var nextNode = nodeList[i];
            if (nextNode == null) continue;

            // line Init
            var line_Renderer = Instantiate(lineRendererPrefab).GetComponent<LineRenderer>();
            line_Renderer.transform.SetParent(transform);
            line_Renderer.positionCount = 2;
            line_Renderer.SetPosition(0, transform.position);
            line_Renderer.SetPosition(1, nextNode.transform.position);

            // text Init
            float cost = Vector3.Distance(transform.position, nextNode.transform.position) * 0.1f;
            cost = Mathf.Round(cost);
            var text = Instantiate(costTextPrefabs).GetComponent<TextMeshProUGUI>();
            text.text = string.Format("{0:0}", cost);
            var newTextPos = (nextNode.transform.localPosition - transform.localPosition) / 2;
            newTextPos.z = -1;
            text.transform.SetParent(transform);
            text.transform.localPosition = newTextPos;
            text.transform.localScale = Vector3.one;
            text.transform.localEulerAngles = Vector3.zero;

            // data
            DijkstraNodeData data = new DijkstraNodeData();
            data.index = NodeIndex;
            data.nextNode = nextNode;
            data.cost = cost;
            data.line_renderer = line_Renderer;
            nodeData.Add(data);

            DijkstraNodeData data_reverse = new DijkstraNodeData();
            data_reverse.index = nextNode.NodeIndex;
            data_reverse.nextNode = this;
            data_reverse.cost = cost;
            data_reverse.line_renderer = line_Renderer;
            nodeData_reversed.Add(data_reverse);
        }
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < nodeList.Count; i++) {
            if(nodeList[i] == null) continue;
            Gizmos.DrawLine(transform.position, nodeList[i].transform.position);
        }
    }
}
