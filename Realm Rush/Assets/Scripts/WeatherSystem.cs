using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject sun;
    [SerializeField]
    private GameObject lightningBolt;
    [SerializeField]
    private ParticleSystem rain;

    private GameObject worldTiles;

    [SerializeField] [Range(-10f, 10f)]
    private float dayNightSpeed = 1f;

    [Header("Rain Config")]

    [SerializeField] [Range(1f, 100f)]
    private float rainCooldown = 60f; 
    [SerializeField] [Range(1f, 10f)]
    private int rainChance = 7;

    private bool isRaining = false;

    // Variables


    private void Awake()
    {
        worldTiles = GameObject.FindGameObjectWithTag("WorldTiles");
    }

    private void Start()
    {
        Debug.Log(worldTiles.transform.GetChild(Mathf.RoundToInt(Random.Range(0, worldTiles.transform.childCount))));

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

                if (newNum > rainChance)
                {
                    Debug.Log("Starting rain");
                    isRaining = true;
                    StartCoroutine(RainStart());
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator RainStart()
    {
        Light sunLight = sun.GetComponent<Light>();

        while (sunLight.intensity > 0.1f)
        {
            sunLight.intensity -= 0.001f;
            yield return new WaitForEndOfFrame();
        } // Gradually reduces sunlight intensity

        rain.Play();

        float rainLength = Random.Range(60f, 120f);
        Debug.Log(rainLength);
        yield return new WaitForSeconds(rainLength);

        rain.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        while (sunLight.intensity < 0.5f)
        {
            sunLight.intensity += 0.001f;
            yield return new WaitForEndOfFrame();
        } // Gradually increases sunlight intensity

        isRaining = false;
    }

    private IEnumerator LightningHandler()
    {
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator StrikeLightning()
    {
        Vector3 newPos;
        GameObject newBolt = Instantiate(lightningBolt, new Vector3(0f, 0f, 0f), Quaternion.identity);

        int tileChildNum = Mathf.RoundToInt(Random.Range(0, worldTiles.transform.childCount));
        GameObject selectedTile = worldTiles.transform.GetChild(tileChildNum).gameObject;

        if (selectedTile.GetComponent<Waypoint>().IsPlaceable)
        {
            newPos = selectedTile.transform.position;
            newBolt.transform.position = newPos;
        }

        yield return new WaitForEndOfFrame();
    }
}
