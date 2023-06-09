using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Ballistics_TextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_time;
    [SerializeField] private TextMeshProUGUI text_distance;
    [SerializeField] private TextMeshProUGUI text_hight;

    [SerializeField] private TMP_InputField text_mass;
    [SerializeField] private TMP_InputField text_power;
    [SerializeField] private TMP_InputField text_degree;


    [SerializeField] private Transform worldCanvasH;
    [SerializeField] private Transform worldCanvasD;
    [SerializeField] private TextMeshProUGUI text_worldHight;
    [SerializeField] private TextMeshProUGUI text_worldDistance;
    [SerializeField] private TextMeshProUGUI text_worldDegree;


    public Action<string> mass_listener;
    public Action<string> degree_listener;
    public Action<string> power_listener;


    private void Awake() {
        SetTimeText(1.342f, 0.33343f);
    }

    public void SetTimeText(float time, float maxTime) {
        text_time.text = textForm(time, maxTime);
    }

    public void SetDistanceText(float distance, float maxDistance) {
        text_distance.text = textForm(distance, maxDistance);
    }
    
    public void SetHightText(float hight, float maxHight) {
        text_hight.text = textForm(hight, maxHight);
    }

    private string textForm(float first, float second) {
        return string.Format("{0:0.00} / {1:0.00}", first, second);
    }

    public void OnChangeMass() {
        mass_listener?.Invoke(text_mass.text);
    }

    public void OnChangePower() {
        power_listener?.Invoke(text_power.text);
    }

    public void OnChangeDegree() {
        degree_listener?.Invoke(text_degree.text);
        WorldTextDegree();
    }


    public void SetLineHight(Vector3 pos, float text) {
        worldCanvasH.transform.position = pos;
        text_worldHight.text = text.ToString();
    }

    public void SetLineDistance(Vector3 pos, float text) {
        worldCanvasD.transform.position = pos;
        text_worldDistance.text = text.ToString();
    }

    public void WorldTextDegree() {
        text_worldDegree.text = text_degree.text;
    }
}
