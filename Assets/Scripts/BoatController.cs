using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
	public Transform TargetPoint;
	public GameObject BoatConstructor;
	public float MaxSpeed;
	public float FishDetectionRadius;
	
	[HideInInspector] public UnityEvent BoatEntered;
	[HideInInspector] public UnityEvent BoatLeft;

	private bool _latchedOn;
	private GameObject _fishAttachedTo;
	private bool _characterInBoat;
	private float _startY;
	private Rigidbody _rb;
	private BoatConstructor _boatConstructor;

	private void Start()
	{
		_startY = transform.position.y;
		_rb = GetComponent<Rigidbody>();
		_boatConstructor = BoatConstructor.GetComponent(typeof(BoatConstructor)) as BoatConstructor;
		
		BoatEntered = new UnityEvent();
		BoatEntered.AddListener(EnteredBoat);
		BoatLeft = new UnityEvent();
		BoatLeft.AddListener(LeftBoat);
	}

	private void Update()
	{
		var nearestFish = GetNearestFish();
		
		if (_boatConstructor.BoatComplete && _characterInBoat)
		{
			if (!_latchedOn)
				FloatAway();
			else
				FollowShark();
		}
		
	}

	private GameObject GetNearestFish()
	{
		var colliders = Physics.OverlapSphere(transform.position, FishDetectionRadius);
		GameObject nearestShark = null;
		float closestDistance = float.MaxValue;

		foreach (var coll in colliders)
		{
			var dist = Vector3.Distance(transform.position, coll.gameObject.transform.position);

			if (dist < closestDistance && coll.gameObject != _fishAttachedTo)
			{
				nearestShark = coll.gameObject;
				closestDistance = dist;
			}
		}

		return nearestShark;
	}

	private void FollowShark()
	{
		var position = transform.position;

		var rigidbodyOfFish = _fishAttachedTo.GetComponent<Rigidbody>();
		var velocity = rigidbodyOfFish.velocity;
	}

	private void EnteredBoat()
	{
		_characterInBoat = true;
	}

	private void LeftBoat()
	{
		_characterInBoat = false;
	}

	private void FloatAway()
	{
		var position = transform.position; 
		
		var velocity = Vector3.MoveTowards(position, TargetPoint.position, MaxSpeed * Time.deltaTime);;
		velocity.y = _startY;
		_rb.MovePosition(velocity);	
		
		var targetDir = TargetPoint.position - position;
		var newDir = Vector3.RotateTowards(transform.forward, targetDir, Time.deltaTime, 0.0f);
		transform.rotation = Quaternion.LookRotation(newDir);
	}

	private void OnTriggerEnter(Collider other)
	{
		
	}
}
