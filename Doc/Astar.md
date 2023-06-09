## Astar
---
#### [link: wikipadia](https://en.wikipedia.org/wiki/A*_search_algorithm)

Path Find Algorithm

![Weighted_A_star_with_eps_5](https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/27dfd4b7-e866-417d-9447-bdab8fcdb4b5)

---

use the PriorityQueue


find 4 direction from Start Grid

<img width="327" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/aead6ac0-e286-4f57-a763-dad81ab2a2a1">

If can move, push it in the priority queue. the priority is Huristic

Do not push if you have already searched.

There are various ways to save heuristics.

H = distance(pos, targetPos);

And register the pre grid.

<img width="422" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/c0779faa-a722-40c0-b03f-749ade75415e">

```C#
foreach (GridData neighborNode in FindNeigborNodes(currentNode)) {
  if (closedSet[neighborNode.pos.x, neighborNode.pos.y]) continue;
  if (neighborNode.isNotUse) continue;
  neighborNode.H = Vector2Int.Distance(neighborNode.pos, targetPos);
  neighborNode.prePos = currentNode.pos;
  PaintNode(Color.yellow, neighborNode.pos);
  openSet.Enqueue(neighborNode, neighborNode.H);
}
```

finded grid are registered in close set.

```C#
closedSet[currentNode.pos.x, currentNode.pos.y] = true;
```

Take out the grid with the lowest priority value of F and repeat the process again

<img width="482" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/8c90b89d-7ecc-494e-935f-a82e81853bc6">

Stop when grid reaches target

---

Pull out the registered pregrid until the start position is reached.

<img width="482" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/22e6cdc6-22ad-4146-8ca9-fbf461e1225d">

```C#
Vector2Int findPos = targetPos;        
while (isFind) {
  GridData node = gridSpawner.nodeMap_list[findPos.x][findPos.y];
  findPos = node.prePos;
  path.Add(findPos);
  if (findPos == startPos) break;
}
```
