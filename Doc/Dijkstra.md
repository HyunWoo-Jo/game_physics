## Dijkstra
---
#### [link: wikipadia](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm)

path find

![Dijkstra_Animation](https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/05a47222-97d7-40b1-8a48-08e2f0de0655)

---

### Find Conneted Node

Register by calculating the cost of a connected node

<img width="198" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/76e13d5f-d46d-4be7-a145-a6205c1a13c2">

<img width="373" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/f3050dc9-aedb-4e5b-b2d7-9bbd29b3f723">

Register in priority queue if calculation results are missing or below registered cost

<img width="374" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/32c1b670-4aa2-4023-8f91-efc9ac380b42">


```C#
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
```

Pulling out the highest priority node

<img width="211" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/de7b6662-fd81-4219-9b1a-af802d8c8926">

<img width="627" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/7b0fb9ab-3e52-4010-b18a-d901512c5604">

<img width="206" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/003c6b7e-dd6a-4233-8b4b-c9424340ad5e">

<img width="377" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/01dfa293-a4a3-45dc-a936-c30fc43a76bb">

Repeat until you reach the target

```C#
while(scheduler_prique.Count != 0) {
  var node = scheduler_prique.Dequeue(out float cost);
  if (node.nextNode.NodeIndex == targetIndex) break;
  AddConnectNode(scheduler_prique, node.nextNode.NodeIndex, cost);
}
```
