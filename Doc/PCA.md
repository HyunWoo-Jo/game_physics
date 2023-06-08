## PCA(Principal Component Analysis)
---
### [link: wikipadia](https://en.wikipedia.org/wiki/Principal_component_analysis)

use to find center vector

<img width="275" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/72eff580-2009-4e08-9b89-3e613317086a">
<img width="167" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/8b16f426-1832-4046-b18f-35dc47307999">
<img width="224" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/586df01d-82ec-47c8-9c64-b466a26c3e69">

---
<img width="448" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/4b3d1a30-1e4c-4b78-9c6a-3558b2a81c53">

```C#
Vector3 m = Vector3.zero;
foreach(var targetObj in randomSpawner.targetList) {
  m += targetObj.transform.position;
}
```
---
<img width="674" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/2cb444fc-620c-43c1-b830-567ea9143f28">

```C#
List<Vector3> deviationPosList = new List<Vector3>();
foreach(var targetObj in randomSpawner.targetList) {
  deviationPosList.Add(targetObj.transform.position - m);
}
```
---

<img width="720" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/38eb924f-a815-487f-a070-7b799bdd8df1">

```C#
float3x3 covMatrix = float3x3.zero;
for (int i = 0; i < 3; i++) {
  for(int j = 0; j < 3; j++) {
    for(int k = 0;k < deviationPosList.Count;k++) {
      covMatrix[i][j] += deviationPosList[k][i] * deviationPosList[k][j];
    }
    covMatrix[i][j] /= deviationPosList.Count - 1;
  }
}
```

<img width="715" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/f200c0e1-6177-4501-aa38-396b699eae84">

```C#
float a = covMatrix[0][0];
float b = covMatrix[0][2];
float c = covMatrix[2][0];
float d = covMatrix[2][2];

float mainDiagonal = a + d;
float det = a * d - b * c;

float square = math.sqrt(mainDiagonal * mainDiagonal / 4.0f - det);
float p1 = mainDiagonal / 2.0f + square;
float p2 = mainDiagonal / 2.0f - square;

Vector3 x = Vector3.zero;
if(math.abs(a) < math.EPSILON && math.abs(c) <= math.EPSILON) {
  x = new Vector3(1, 0, 0);
} else if(math.abs(b) > math.EPSILON) {
  x = new Vector3(b, 0, p1 - a);
} else if(math.abs(c) > math.EPSILON) {
  x = new Vector3(p1 - d, 0, c);
}
x.Normalize();
```
