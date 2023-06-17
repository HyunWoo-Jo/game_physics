## Ballistics
---
#### [link: wikipadia](https://en.wikipedia.org/wiki/Ballistics)

![Inclinedthrow](https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/f1663231-82ae-4410-b91e-bfd5d12ab6ca)

---

Maximum Height (H)
```
H = (v^2 * sin^2(θ)) / (2g)
```
- H represents the maximum height.
- v is the initial velocity of the projectile.
- θ is the launch angle of the projectile.
- g is the acceleration due to gravity.

---

Maximum Range (R)
```
R = (v^2 * sin(2θ)) / g
```
- R represents the maximum range.
- v is the initial velocity of the projectile.
- θ is the launch angle of the projectile.
- g is the acceleration due to gravity.

---
Fight Time (T)
```
T = (2 * v * sin(θ)) / g
```
- T represents the time of flight.
- v is the initial velocity of the projectile.
- θ is the launch angle of the projectile.
- g is the acceleration due to gravity.

---
The initial velocity of the projectile.(Vi)
```
vi = power / (50 * mass)
```
- power is Rigidybody AddForce magnitude
- 50 is the constant for changing from Unity to vi.
- mass is the mass of an object
---

Code (Ballistics.cs)
```c#
vi = power / (PowerConstant * mass);
estimated_hight = (Mathf.Pow(vi, 2) * Mathf.Pow(Mathf.Sin(polarAngle * Mathf.Deg2Rad), 2)) / (2 * -Physics.gravity.y);
estimated_distance = (Mathf.Pow(vi, 2) * Mathf.Sin(polarAngle * Mathf.Deg2Rad * 2)) / -Physics.gravity.y;
estimated_time = (2 * vi * Mathf.Sin(polarAngle * Mathf.Deg2Rad)) / -Physics.gravity.y;
```
