using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private TowerScript towerScript;

    [SerializeField]
    private bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }


    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerScript.CreateTower(towerScript, transform.position);

            isPlaceable = !isPlaced;
        }
    }
}
