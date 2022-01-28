using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject sun;

    [SerializeField] [Range(-10f, 10f)]
    private float dayNightSpeed = 1f;

    private void Update()
    {
        DayNightCycle();
    }

    private void DayNightCycle()
    {
        sun.transform.Rotate(0f, 0f, dayNightSpeed * Time.deltaTime, Space.World);
    }
}
