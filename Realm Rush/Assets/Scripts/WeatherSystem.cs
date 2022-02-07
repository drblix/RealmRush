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

    [SerializeField] [Range(1f, 100f)] [Tooltip("Cooldown in seconds between rain chance coroutine being called")]
    private float rainCooldown = 60f; 
    [SerializeField] [Range(1f, 10f)]
    private int rainChance = 7;
    [SerializeField] [Range(2, 10)]
    private int lightningChance = 4;

    private bool isRaining = false;

    // Variables


    private void Awake()
    {
        worldTiles = GameObject.FindGameObjectWithTag("WorldTiles");
    }

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
            if (!isRaining)
            {
                int newNum;

                yield return new WaitForSeconds(rainCooldown);
                newNum = Mathf.RoundToInt(Random.Range(1f, 10f));

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

        var emissionModule = rain.emission;
        float rainRate = Random.Range(100f, 200f);
        emissionModule.rateOverTime = rainRate;

        rain.Play();

        float rainLength = Random.Range(60f, 120f);
        
        Debug.Log(rainLength);

        StartCoroutine(LightningHandler());
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
        while (isRaining)
        {
            yield return new WaitForSeconds(Random.Range(20f, 30f));
            int newNum = Mathf.RoundToInt(Random.Range(1, lightningChance));

            if (isRaining)
            {
                if (newNum == (lightningChance - 1))
                {
                    Debug.Log("Lightning hit");
                    StartCoroutine(StrikeLightning());
                }
            }
            else
            {
                break;
            }
        }
    }

    private IEnumerator StrikeLightning()
    {
        Vector3 newPos;
        GameObject newBolt = Instantiate(lightningBolt, new Vector3(0f, 0f, 0f), Quaternion.Euler(new Vector3(-90f, 0f, 0f)));

        int tileChildNum = Mathf.RoundToInt(Random.Range(0, worldTiles.transform.childCount));
        GameObject selectedTile = worldTiles.transform.GetChild(tileChildNum).gameObject;

        while (!selectedTile.GetComponent<Waypoint>().IsPlaceable)
        {
            tileChildNum = Mathf.RoundToInt(Random.Range(0, worldTiles.transform.childCount));
            selectedTile = worldTiles.transform.GetChild(tileChildNum).gameObject;
        }

        newPos = selectedTile.transform.position;
        newBolt.transform.position = newPos;

        LineRenderer lineRenderer = newBolt.GetComponent<LineRenderer>();

        Vector3[] linePositions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(linePositions);

        for (int i = 0; i < linePositions.Length; i++)
        {
            float newX = Random.Range(0f, 6f);
            float newY = Random.Range(0f, 6f);

            linePositions[i] = new Vector3(newX, newY, linePositions[i].z);
            lineRenderer.SetPositions(linePositions);
            yield return new WaitForSeconds(Random.Range(0.01f, 0.08f));
            lineRenderer.enabled = false;
            yield return new WaitForSeconds(Random.Range(0.01f, 0.08f));
            lineRenderer.enabled = true;
        }

        lineRenderer.enabled = false;
        newBolt.transform.Find("Light").gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        Destroy(newBolt);
    }
}
