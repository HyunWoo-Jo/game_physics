## Astar
---
#### [link: wikipadia](https://en.wikipedia.org/wiki/A*_search_algorithm)

Path Find Algorithm

![Weighted_A_star_with_eps_5](https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/27dfd4b7-e866-417d-9447-bdab8fcdb4b5)

---

use the PriorityQueue


find 4 direction from Start Grid

<img width="327" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/aead6ac0-e286-4f57-a763-dad81ab2a2a1">

If can move, push it in the priority queue. the priority is F

Do not push if you have already searched.

F = G + H

F = moveCost + distance(pos, targetPos);

And register the pre grid.

<img width="665" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/7788a1c4-6d6f-4584-b146-96ab258e246e">


```C#
foreach(GridData neighborNode in FindNeigborNodes(currentNode)) {
  if (closedSet[neighborNode.pos.x, neighborNode.pos.y]) continue;
  if (neighborNode.isNotUse) continue;
  float G = currentNode.G + Vector2Int.Distance(currentNode.pos, neighborNode.pos);
  if(G < neighborNode.G || !openSet.Contains(neighborNode)) {
    neighborNode.G = G;
    neighborNode.H = Vector2Int.Distance(neighborNode.pos, targetPos);
    neighborNode.F = neighborNode.G + neighborNode.H;
    neighborNode.prePos = currentNode.pos;
    if (!openSet.Contains(neighborNode)) {
      openSet.Enqueue(neighborNode, neighborNode.F);
    }
  }
}
```

finded grid are registered in close set.

```C#
closedSet[currentNode.pos.x, currentNode.pos.y] = true;
```

Take out the grid with the lowest priority value of F and repeat the process again

<img width="436" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/2f49b7ef-b8bf-406b-9ac6-04cb72dfad27">

---

<img width="419" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/66bba0fa-1b1b-41e3-8d6c-e1ed18ebe801">

---

<img width="512" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/17ba7805-97b7-49e1-8cd6-f5a5c4c25096">

Stop when grid reaches target

---

Pull out the registered pregrid until the start position is reached.

<img width="512" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/a4474cbf-9607-432b-88c2-67226a43690a">

```C#
Vector2Int findPos = targetPos;        
while (isFind) {
  GridData node = gridSpawner.nodeMap_list[findPos.x][findPos.y];
  findPos = node.prePos;
  path.Add(findPos);
  if (findPos == startPos) break;
}
```
