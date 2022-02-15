using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] [Range(10f, 100f)]
    private float cameraSpeed = 25f;

    [SerializeField] [Tooltip("Minimum value for X (Should be smaller than 2nd X restriction)")]
    private float xRestriction01;
    [SerializeField] [Tooltip("Maximum value for X (Should be larger than 1st X restriction)")]
    private float xRestriction02;

    [SerializeField] [Tooltip("Minimum value for Z (Should be smaller than 2nd Z restriction)")]
    private float zRestriction01;
    [SerializeField] [Tooltip("Maximum value for Z (Should be larger than 1st Z restriction)")]
    private float zRestriction02;

    private Vector3 rightTransform;
    private Vector3 leftTransform;
    private Vector3 forwardTransform;
    private Vector3 backTransform;

    private bool canMove = true;

    private void Awake()
    {
        rightTransform = cameraSpeed * Time.deltaTime * Vector3.right;
        leftTransform = cameraSpeed * Time.deltaTime * Vector3.left;
        forwardTransform = cameraSpeed * Time.deltaTime * Vector3.forward;
        backTransform = cameraSpeed * Time.deltaTime * Vector3.back;
    }

    private void Update()
    {
        CheckInput();
        UpdateTransform();
        CameraBoundaries();
    }

    private void CheckInput()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.Translate(rightTransform, Space.World);
            }


            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                transform.Translate(forwardTransform, Space.World);
            }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                transform.Translate(backTransform, Space.World);
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.Translate(leftTransform, Space.World);
            }
        }
    }

    private void CameraBoundaries()
    {
        var pos = transform.position;

        pos.x = Mathf.Clamp(transform.position.x, xRestriction01, xRestriction02);
        pos.z = Mathf.Clamp(transform.position.z, zRestriction01, zRestriction02);

        transform.position = pos;
    }

    public void UpdateSpeed(float newSpeed)
    {
        cameraSpeed = newSpeed;
        UpdateTransform();
    }

    private void UpdateTransform()
    {
        rightTransform = cameraSpeed * Time.deltaTime * Vector3.right;
        leftTransform = cameraSpeed * Time.deltaTime * Vector3.left;
        forwardTransform = cameraSpeed * Time.deltaTime * Vector3.forward;
        backTransform = cameraSpeed * Time.deltaTime * Vector3.back;
    }

    public void ToggleMovement()
    {
        canMove = !canMove;
    }
}
