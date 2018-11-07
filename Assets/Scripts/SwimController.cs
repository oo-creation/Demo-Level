using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimController : MonoBehaviour
{
    public float MaxSpeed;
    public float WanderRadius;
    public Transform TargetPoint;
    public float AutoDestroyTimer = 30;

    private float _wanderPoint = 0f;
    private float _wanderSpeed = .03f;
    private Rigidbody _rb;
    private float _startY;
    private Vector3 _position;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _startY = transform.position.y;
        Destroy(this, AutoDestroyTimer);
    }

    private void Update()
    {
        // Cache position
        _position = transform.position;

        // Calculate velocity and move
        var velocity = MoveToTarget();
        velocity.y = _startY;
        _rb.MovePosition(velocity);

        // Calculate rotation and turn
        var targetDir = TargetPoint.position - _position;
        var newDir = Vector3.RotateTowards(transform.forward, targetDir, Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    private Vector3 MoveToTarget()
    {
        var targetPos = Vector3.MoveTowards(_position, TargetPoint.position, MaxSpeed * Time.deltaTime);
        return targetPos;
    }

    // Tried to make the fish make swim movements.
    private Vector3 Swim()
    {
        if (_wanderPoint < 0)
            _wanderSpeed += WanderRadius * Time.deltaTime;
        else
            _wanderSpeed -= WanderRadius * Time.deltaTime;

        _wanderPoint += _wanderSpeed;
        return _wanderPoint * transform.right;
    }

    public void HighlightFish(GameObject fishObject)
    {
        if (fishObject == gameObject)
        {
            // Highlight
            Debug.DrawLine(transform.position, transform.position + Vector3.up * 50);
        }
    }
    
    public void RemoveHighlight(GameObject fishObject)
    {
        if (fishObject == gameObject)
        {
            //RemoveHighlight
        }
    }
}