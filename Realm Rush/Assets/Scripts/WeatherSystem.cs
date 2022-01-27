using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject sun;

    [SerializeField] [Range(-1f, 1f)]
    private float dayNightSpeed = 0.01f;

    private void Update()
    {
        DayNightCycle();
    }

    private void DayNightCycle()
    {
        sun.transform.Rotate(0f, 0f, dayNightSpeed, Space.World);
    }
}
