using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _ballistaTower;

    [SerializeField]
    private bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            Instantiate(_ballistaTower, transform.position, Quaternion.identity);
            isPlaceable = false;
        }
    }
}
