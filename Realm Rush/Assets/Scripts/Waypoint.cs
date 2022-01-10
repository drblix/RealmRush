using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _ballistaTower;
    [SerializeField]
    private bool isPlaceable;

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            Instantiate(_ballistaTower, transform.position, Quaternion.identity);
            isPlaceable = false;
        }
        else
        {
            Debug.Log("Not placeable");
        }
    }
}
