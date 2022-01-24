using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    [SerializeField] [Range(10f, 100f)]
    private float cameraSpeed = 16f;

    [SerializeField]
    private float maxXRange01;
    [SerializeField]
    private float maxXRange02;

    [SerializeField]
    private float maxZRange01;
    [SerializeField]
    private float maxZRange02;

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
        rightTransform = Vector3.right * Time.deltaTime * cameraSpeed;
        leftTransform = Vector3.left * Time.deltaTime * cameraSpeed;
        forwardTransform = Vector3.forward * Time.deltaTime * cameraSpeed;
        backTransform = Vector3.back * Time.deltaTime * cameraSpeed;

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                maxXRange01 = levelXRestrictions[0];
                maxXRange02 = levelXRestrictions[1];
                maxZRange01 = levelZRestrictions[0];
                maxZRange02 = levelZRestrictions[1];
                break;

            default:
                Debug.LogError("Camera restrictions unassigned!");
                break;
        }
    }

    private void Update()
    {
        CheckInput();
        UpdateTransform();
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.RightArrow) && (transform.position.x < maxXRange02))
        {
            transform.Translate(rightTransform, Space.World);
        }

        if (Input.GetKey(KeyCode.UpArrow) && (transform.position.z < maxZRange02))
        {
            transform.Translate(forwardTransform, Space.World);
        }

        if (Input.GetKey(KeyCode.DownArrow) && (transform.position.z > maxZRange01))
        {
            transform.Translate(backTransform, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && (transform.position.x > maxXRange01))
        {
            transform.Translate(leftTransform, Space.World);
        }
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
