using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] 
    List<Waypoint> path = new List<Waypoint>();

    [SerializeField]
    [Range(0.1f, 4f)]
    private float speed = 1f;

    private void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }


    private void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            path.Add(child.GetComponent<Waypoint>());
        }
    }

    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endingPos = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endingPos);

            while (travelPercent < 1f)
            {
                transform.position = Vector3.Lerp(startPos, endingPos, travelPercent);
                travelPercent += Time.deltaTime * speed;
                yield return new WaitForEndOfFrame();
            }
        }

        gameObject.SetActive(false);
        ReturnToStart();
    }
}
