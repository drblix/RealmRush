using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] [Range(10f, 100f)]
    private float cameraSpeed = 16f;

    [SerializeField]
    private float maxXRange01 = 12.1f;
    [SerializeField]
    private float maxXRange02 = 104.92f;

    [SerializeField]
    private float maxZRange01 = -44.6f;
    [SerializeField]
    private float maxZRange02 = 27.7f;

    private Vector3 rightTransform;
    private Vector3 leftTransform;
    private Vector3 forwardTransform;
    private Vector3 backTransform;

    private void Awake()
    {
        rightTransform = Vector3.right * Time.deltaTime * cameraSpeed;
        leftTransform = Vector3.left * Time.deltaTime * cameraSpeed;
        forwardTransform = Vector3.forward * Time.deltaTime * cameraSpeed;
        backTransform = Vector3.back * Time.deltaTime * cameraSpeed;
    }

    private void Update()
    {
        CheckInput();
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

    public void UpdateTransforms(float newSpeed)
    {
        cameraSpeed = newSpeed;

        rightTransform = Vector3.right * Time.deltaTime * cameraSpeed;
        leftTransform = Vector3.left * Time.deltaTime * cameraSpeed;
        forwardTransform = Vector3.forward * Time.deltaTime * cameraSpeed;
        backTransform = Vector3.back * Time.deltaTime * cameraSpeed;
    }
}
