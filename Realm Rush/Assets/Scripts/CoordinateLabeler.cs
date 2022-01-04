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
    }

    private void Update()
    {
        label = transform.GetComponent<TextMeshPro>();

        if (!Application.isPlaying)
        {
            DisplayCoordinates();
        }
    }

    private void DisplayCoordinates()
    {
        coords.x = Mathf.RoundToInt(transform.parent.position.x);
        coords.y = Mathf.RoundToInt(transform.parent.position.z);

        label.text = coords.ToString();
    }
}
