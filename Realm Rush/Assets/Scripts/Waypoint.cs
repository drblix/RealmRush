using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private TowerScript towerScript;
    [SerializeField]
    private ParticleSystem selectionEffect;

    private Vector3 tilePosition;

    [SerializeField]
    private bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    private void Awake()
    {
        tilePosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerScript.CreateTower(towerScript, transform.position);

            isPlaceable = !isPlaced;
        }
    }
    

    public void GameOver()
    {
        isPlaceable = false;
    }
}
