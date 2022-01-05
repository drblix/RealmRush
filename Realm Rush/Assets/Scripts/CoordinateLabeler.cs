using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coords = new Vector2Int();


    private void Awake()
    {
        label = transform.GetComponent<TextMeshPro>();
        DisplayCoordinates();
        UpdateObjectName();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
    }

    private void DisplayCoordinates()
    {
        coords.x = Mathf.RoundToInt(transform.parent.position.x / 10);
        coords.y = Mathf.RoundToInt(transform.parent.position.z / 10);

        label.text = coords.ToString();
    }

    private void UpdateObjectName()
    {
        transform.parent.name = coords.ToString();
    }
}
