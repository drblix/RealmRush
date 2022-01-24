using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private TowerScript towerScript;
    [SerializeField]
    private ParticleSystem selectionEffect;

    [SerializeField]
    private bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    private bool hasTower = false;


    // Mouse functions
    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerScript.CreateTower(towerScript, transform.position);

            isPlaceable = !isPlaced;
            hasTower = true;
        }
    }

    private void OnMouseOver()
    {
        if (isPlaceable && !selectionEffect.isPlaying)
        {
            Debug.Log("playing");
            selectionEffect.Play();
        }
        else
        {
            selectionEffect.Stop();
        }
    }

    private void OnMouseExit()
    {
        selectionEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
    


    public void ToggleTile(bool toggle)
    {
        if (!hasTower)
        {
            isPlaceable = toggle;
        }
    }

    public void GameOver()
    {
        isPlaceable = false;
    }
}
