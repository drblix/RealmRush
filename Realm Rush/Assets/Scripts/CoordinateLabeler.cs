using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField]
    private Color defaultColor = Color.white;
    [SerializeField]
    private Color blockedColor = Color.grey;

    private TextMeshPro label;
    private Vector2Int coords = new Vector2Int();
    private Waypoint waypoint;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        waypoint = GetComponentInParent<Waypoint>();
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
        ColorCoordinates();
        ToggleLabels();
    }

    private void ColorCoordinates()
    {
        if (waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.isActiveAndEnabled;
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
