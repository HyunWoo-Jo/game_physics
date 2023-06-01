using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracePID : MonoBehaviour
{

    private Transform target;

    public void SetTarget(Transform tr) {
        target = tr;
    }
    Vector3 derivative_Et;
    Vector3 cur_error;
    Vector3 pre_error;
    float kp = 3f;
    float ki = 21f;
    float kd = 2f;
    float speed = 0.5f;
    Vector3 mv;
    readonly float timeRate = 0.01f;
    float timer;
    Vector3 newAddPos;
    private IEnumerator Start() {

        GetComponent<Rigidbody>().AddForce(transform.forward * 2000f);
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

            if (cur_error.magnitude <= 2f) {
                Destroy(gameObject);
                break;
            }

            derivative_Et += cur_error * timer;
            var _de = (cur_error - pre_error) / timer;
            mv = kp * cur_error + ki * derivative_Et + kd * _de;
            pre_error = cur_error;

            newAddPos = Vector3.ClampMagnitude(mv, speed);
            transform.position += newAddPos;

            timer = 0;
            yield return null;
            }
    }



    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("TARGET")) {
            Destroy(this.gameObject);
        }
    }
}
