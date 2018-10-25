using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimController : MonoBehaviour
{
	public float MaxSpeed;
	public float WanderRadius;
	public Transform TargetPoint;

	private float _wanderPoint = 0f;
	private float _wanderSpeed = .05f;

	private void Update()
	{
		var velocity = MoveToTarget() + Wander();
		velocity.y = transform.position.y;
		transform.position = velocity;
	}

	private Vector3 MoveToTarget()
	{
		var targetPos = Vector3.MoveTowards(transform.position, TargetPoint.position, MaxSpeed * Time.deltaTime);
		return targetPos;
	}

	private Vector3 Wander()
	{
		if (_wanderPoint < 0)
			_wanderSpeed += WanderRadius * Time.deltaTime;
		else
			_wanderSpeed -= WanderRadius * Time.deltaTime;

		_wanderPoint += _wanderSpeed;
		return _wanderPoint * transform.right;
	}
}
