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
	public GameObject RopeAttachment;
	
	[HideInInspector] public UnityEvent BoatEntered;
	[HideInInspector] public UnityEvent BoatLeft;
	[HideInInspector] public AttachToFishEvent HighlightFishToAttach;
	[HideInInspector] public DetachFromFishEvent RemoveHighLight;

	private bool _latchedOn;
	private GameObject _fishAttachedTo;
	private bool _characterInBoat;
	private float _startY;
	private Rigidbody _rb;
	private BoatConstructor _boatConstructor;
	private Cable_Procedural_Simple _CableProceduralSimple;

	private void Start()
	{
		_startY = transform.position.y;
		_rb = GetComponent<Rigidbody>();
		_boatConstructor = BoatConstructor.GetComponent(typeof(BoatConstructor)) as BoatConstructor;
		_CableProceduralSimple = RopeAttachment.GetComponent(typeof(Cable_Procedural_Simple)) as Cable_Procedural_Simple;
		
		BoatEntered = new UnityEvent();
		BoatEntered.AddListener(EnteredBoat);
		BoatLeft = new UnityEvent();
		BoatLeft.AddListener(LeftBoat);
		HighlightFishToAttach = new AttachToFishEvent();
		RemoveHighLight = new DetachFromFishEvent();
	}

	private void Update()
	{
		var nearestFish = GetNearestFish();
		if (nearestFish != null)
		{
			HighlightFishToAttach.Invoke(nearestFish);
		}

		if (Input.GetKeyDown(KeyCode.F) && nearestFish != null)
		{
			_fishAttachedTo = nearestFish;
			_latchedOn = true;
			Debug.Log("Attached to Fish!");
		}
		else if (Input.GetKeyDown(KeyCode.F))
		{
			_latchedOn = false;
			_fishAttachedTo = null;
		}
		
		if (_boatConstructor.BoatComplete && _characterInBoat)
		{
			if (!_latchedOn)
			{
				FloatAway();
				_CableProceduralSimple.endPointTransform = Vector3.zero;
			}
			else
			{
				FollowFish();
				_CableProceduralSimple.endPointTransform = _fishAttachedTo.transform.position + Vector3.up * 6;
			}
		}
		
	}

	private GameObject GetNearestFish()
	{
		var colliders = Physics.OverlapSphere(transform.position, FishDetectionRadius);
		GameObject nearestShark = null;
		float closestDistance = float.MaxValue;

		foreach (var coll in colliders)
		{
			if (coll.gameObject.CompareTag("Fish"))
			{
				var dist = Vector3.Distance(transform.position, coll.gameObject.transform.position);

				if (dist < closestDistance && coll.gameObject != _fishAttachedTo)
				{
					nearestShark = coll.gameObject;
					closestDistance = dist;
				}
			}
		}

		return nearestShark;
	}

	private void FollowFish()
	{
		Debug.Log("Follow fish");
		
		var velocity = Vector3.MoveTowards(transform.position, _fishAttachedTo.transform.position, MaxSpeed * Time.deltaTime);;
		velocity.y = _startY;

		var rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_fishAttachedTo.transform.position - transform.position), Time.deltaTime);
		rotation.x = 0f;
		rotation.z = 0f;
		transform.rotation = rotation;
		_rb.MovePosition(velocity);
	}

	private void FloatAway()
	{
		var position = transform.position; 
		
		var velocity = Vector3.MoveTowards(position, TargetPoint.position, MaxSpeed * Time.deltaTime);;
		velocity.y = _startY;
		
		var targetDir = TargetPoint.position - position;
		var newDir = Vector3.RotateTowards(transform.forward, targetDir, Time.deltaTime, 0.0f);
		
		transform.rotation = Quaternion.LookRotation(newDir);
		_rb.MovePosition(velocity);	
	}

	private void EnteredBoat()
	{
		_characterInBoat = true;
	}

	private void LeftBoat()
	{
		_characterInBoat = false;
	}
}

public class AttachToFishEvent : UnityEvent<GameObject>
{
}
public class DetachFromFishEvent : UnityEvent<GameObject>
{
}
