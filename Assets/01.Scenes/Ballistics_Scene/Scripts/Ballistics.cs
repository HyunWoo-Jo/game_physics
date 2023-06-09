using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Ballistics : MonoBehaviour
{
    [SerializeField] private Transform firePoint_tr;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private GameObject lineRendererPrefabs;
    [SerializeField] private Ballistics_TextUI text_ui;
    private Transform bulletObj;
    private LineRenderer lineRenderer;

    private float estimated_hight = 0;
    private float estimated_time = 0;
    private float estimated_distance = 0;
    private float vi;
    private float power = 1000;
    private float mass = 1;

    private float angleX = 45;
    private float flight_time = 0;

    private float hight = 0;
    private float distance = 0;
    private float maxHight = 0;
    private float maxDistance = 0;

    private readonly float PowerConstant = 50;

    private void Awake() {
        lineRenderer = Instantiate(lineRendererPrefabs).GetComponent<LineRenderer>();
        lineRenderer.positionCount = 3;
        text_ui.mass_listener += SetMass;
        text_ui.power_listener += SetPower;
        text_ui.degree_listener += SetAngleX;
    }
    private void Update() {
        GetBulletInformation();

        vi = power / (PowerConstant * mass);

        float polarAngle = UnityToPolarAngle(angleX);

        // H = (Vi^2 * sin^2(еш)) / (2 * g)
        estimated_hight = (Mathf.Pow(vi, 2) * Mathf.Pow(Mathf.Sin(polarAngle * Mathf.Deg2Rad), 2)) / (2 * -Physics.gravity.y);
        // R = (Vi^2 * sin(2еш)) / g
        estimated_distance = (Mathf.Pow(vi, 2) * Mathf.Sin(polarAngle * Mathf.Deg2Rad * 2)) / -Physics.gravity.y;
        // T = (2 * Vi * sin(еш)) / g
        estimated_time = (2 * vi * Mathf.Sin(polarAngle * Mathf.Deg2Rad)) / -Physics.gravity.y;

        text_ui.SetDistanceText(distance, maxDistance);
        text_ui.SetHightText(hight, maxHight);
        text_ui.SetTimeText(flight_time, estimated_time);

        DrawLine();
    }

    public void SetMass(string value) {
        if (float.TryParse(value, out var result))
            SetMass(result);
    }
    public void SetMass(float value) {
        mass = value;
    }
    
    public void SetAngleX(string value) {
        if(float.TryParse(value, out var result))
            SetAngleX(result);
    }
    public void SetAngleX(float value) {

        var angle = transform.eulerAngles;
        angle.x = PolarToUnityAngle(value);
        angleX = angle.x;
        transform.eulerAngles = angle;
    }

    public void SetPower(string value) {
        if (float.TryParse(value, out var result))
            power = result;
    }


    public void Fire() {
        maxHight = 0;
        maxDistance = 0;
        flight_time = 0;
        distance = 0;
        hight = 0;
      
        GameObject obj = Instantiate(bulletPrefabs);
        obj.transform.position = firePoint_tr.position;
        obj.transform.rotation = firePoint_tr.rotation;
        Rigidbody _rigideBody = obj.GetComponent<Rigidbody>();
        bulletObj = obj.transform;
        _rigideBody.mass = mass;
        _rigideBody.AddForce(obj.transform.forward * power);
    }

    private void GetBulletInformation() {
        if(bulletObj != null) {
            hight = bulletObj.transform.position.y - firePoint_tr.position.y;
            if (maxHight < hight) maxHight = hight;
            if (firePoint_tr.position.y < bulletObj.transform.position.y)
                distance = bulletObj.transform.position.x - firePoint_tr.position.x;
            if (maxDistance < distance) maxDistance = distance;
            if (firePoint_tr.position.y < bulletObj.transform.position.y) 
                flight_time += Time.deltaTime;
        }
    }

    private void DrawLine() {
        lineRenderer.SetPosition(0, firePoint_tr.position + new Vector3(0, estimated_hight, 0));
        lineRenderer.SetPosition(1, firePoint_tr.position + new Vector3(estimated_distance, estimated_hight, 0));
        lineRenderer.SetPosition(2, firePoint_tr.position + new Vector3(estimated_distance, 0, 0));

        var dPos = firePoint_tr.position + new Vector3(estimated_distance / 2, estimated_hight + 3, 0);
        text_ui.SetLineDistance(dPos, estimated_distance);
        var hPos = firePoint_tr.position + new Vector3(estimated_distance + 7, estimated_hight / 2 , 0);
        text_ui.SetLineHight(hPos, estimated_hight);
    }

    //polar coordinate system, cartesian coordinate
    private float PolarToUnityAngle(float angle) {
        return -1 * angle + 90;
    }

    private float UnityToPolarAngle(float angle) {
   
        return -1 * angle + 90;
    }
}
