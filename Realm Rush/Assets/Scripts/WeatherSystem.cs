using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject sun;
    [SerializeField]
    private ParticleSystem rain;

    [SerializeField] [Range(-10f, 10f)]
    private float dayNightSpeed = 1f;

    [Header("Rain Config")]

    [SerializeField] [Range(10f, 100f)]
    private float rainCooldown = 60f; 
    [SerializeField] [Range(1f, 10f)]
    private int rainChance = 7;

    private bool isRaining = false;

    private void Start()
    {
        StartCoroutine(RainChance());
    }

    private void Update()
    {
        DayNightCycle();
    }

    private void DayNightCycle()
    {
        sun.transform.Rotate(0f, 0f, dayNightSpeed * Time.deltaTime, Space.World);
    }

    private IEnumerator RainChance()
    {
        while (true)
        {
            Debug.Log("running");
            if (!isRaining)
            {
                int newNum;

                yield return new WaitForSeconds(rainCooldown);
                newNum = Mathf.RoundToInt(Random.Range(1f, 10f));

                Debug.Log(newNum);

                if (newNum <= rainChance)
                {
                    Debug.Log("Starting rain");
                    isRaining = true;
                    StartCoroutine(RainStart());
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator RainStart()
    {
        Light sunLight = sun.GetComponent<Light>();

        while (sunLight.intensity > 0.1f)
        {
            sunLight.intensity -= 0.01f;
            yield return new WaitForEndOfFrame();
        } // Gradually reduces sunlight intensity

        rain.Play();

        yield return new WaitForSeconds(Random.Range(60f, 120f));

        rain.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        while (sunLight.intensity < 0.5f)
        {
            sunLight.intensity += 0.01f;
            yield return new WaitForEndOfFrame();
        } // Gradually increases sunlight intensity

        isRaining = false;
    }


}
