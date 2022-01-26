using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private TowerScript towerScript;

    [Header("Tile Properties")]
    [SerializeField]
    private GameObject selectionBox;
    [SerializeField]
    private ParticleSystem selectionEffect;
    [SerializeField]
    private ParticleSystem placeEffect;

    [SerializeField]
    private bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    private bool hasTower = false;
    private bool isObstacleTile;

    private void Awake()
    {
        if (!isPlaceable)
        {
            isObstacleTile = true;
        }
        else
        {
            isObstacleTile = false;
        }
    }


    // Mouse functions
    private void OnMouseDown()
    {
        PlaceTower();
    }

    private void OnMouseOver()
    {
        if (isPlaceable && !hasTower && !selectionEffect.isPlaying)
        {
            selectionEffect.Play();
            selectionBox.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        selectionEffect.Stop();
        selectionBox.SetActive(false);
    }

    // Other functions
    private void PlaceTower()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerScript.CreateTower(towerScript, transform.position);

            if (!isPlaced) { return; }

            isPlaceable = !isPlaced;
            hasTower = true;

            placeEffect.Play();
            selectionEffect.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
            selectionBox.SetActive(false);
        }
    }

    public void ToggleTile(bool toggle)
    {
        if (!hasTower && !isObstacleTile)
        {
            isPlaceable = toggle;
        }
    }

    public void GameOver()
    {
        isPlaceable = false;
    }
}
