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

    private void Start()
    {
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endingPos = waypoint.transform.position;
            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                transform.position = Vector3.Lerp(startPos, endingPos, travelPercent);
                travelPercent += Time.deltaTime * speed;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
