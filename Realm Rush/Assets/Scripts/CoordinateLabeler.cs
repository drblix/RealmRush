using UnityEngine;
using TMPro;

[ExecuteAlways] 
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField]
    private Color defaultColor = Color.white;
    [SerializeField]
    private Color blockedColor = Color.grey;

    [SerializeField]
    private TextMeshPro label;

    private Vector2Int coords = new Vector2Int();
    private Waypoint waypoint;

    private void Awake()
    {
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
        SetLabelColor();
        ToggleLabels();
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


    // Debug methods
    private void SetLabelColor()
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

}
