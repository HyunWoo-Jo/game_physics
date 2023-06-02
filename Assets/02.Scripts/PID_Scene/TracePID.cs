using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracePID : MonoBehaviour {

    private Transform target;
    private Rigidbody _rigidbody;

    public void SetTarget(Transform tr) {
        target = tr;
    }
    Vector3 derivative_Et;
    Vector3 cur_error;
    Vector3 pre_error;
    float kp = 4f;
    float ki = 2f;
    float kd = 0.01f;
    float speed = 30f;
    Vector3 mv;
    readonly float timeRate = 0.01f;
    float timer;
    private IEnumerator Start() {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.forward * 2000f);
        yield return new WaitForSeconds(0.5f);

        while (true) {
            timer += Time.deltaTime;
            if (timer < timeRate) {
                yield return null;
                continue;
            }

            // PID
            // MV(t) = Kp * e(t) + Ki * Differential(e(t)) + kd * de / dt

            cur_error = target.position - transform.position;

            if(Vector3.Distance(transform.position, target.position) < 3f) {
                Destroy(gameObject);
                break;
            }

            derivative_Et += cur_error * timer;
            var _de = (cur_error - pre_error) / timer;
            mv = kp * cur_error + ki * derivative_Et + kd * _de;
            pre_error = cur_error;

            _rigidbody.AddForce(mv.normalized * speed);

            timer = 0;
            yield return null;
        }
    }
}
  
