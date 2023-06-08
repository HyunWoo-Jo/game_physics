# PID(proportional–integral–derivative controller)
---
### [link: wikipedia](https://en.wikipedia.org/wiki/PID_controller)

![PID_Compensation_Animated](https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/a8975b62-3d6d-4a63-84fb-36be33fea7d6)

#### Use Object Movement

<img width="452" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/c93faa03-e3fd-4db1-a60e-b94c9d533e5d">

---
<img width="238" alt="image" src="https://github.com/HyunWoo-Jo/game_physics_math/assets/73084993/7fd453c3-abf5-4335-87d4-4095cfc0494e">


```C#
public void PID() {
  cur_error = target.position - transform.position;
  sum_Et += cur_error * timer;
  var _de = (cur_error - pre_error) / timer;
  mv = (kp * cur_error) + (ki * sum_Et) + (kd * _de);
  pre_error = cur_error;
}
```
