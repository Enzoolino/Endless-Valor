using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingLightBall : MonoBehaviour
{
    [SerializeField] private Transform followedPosition;
    [SerializeField] private float speed = 1.0f;
    
    
    void Update()
    {
        if (transform.position != followedPosition.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, followedPosition.position, Time.deltaTime * speed);
        }
    }
}
