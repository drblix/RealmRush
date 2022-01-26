using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    [SerializeField] [Range(10f, 100f)]
    private float cameraSpeed = 25f;

    private Vector3 rightTransform;
    private Vector3 leftTransform;
    private Vector3 forwardTransform;
    private Vector3 backTransform;

    private readonly float[] levelXRestrictions = new float[]
    {
        // Level1
        12.1f,
        104.92f,
    };
    private readonly float[] levelZRestrictions = new float[]
    {
        // Level1
        -44.6f,
        27.7f,
    };

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

    private void CameraBoundaries()
    {
        var pos = transform.position;

        pos.x = Mathf.Clamp(transform.position.x, levelXRestrictions[0], levelXRestrictions[1]);
        pos.z = Mathf.Clamp(transform.position.z, levelZRestrictions[0], levelZRestrictions[1]);

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
}
