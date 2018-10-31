using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
	public float MinWaitTime;
	public float MaxWaitTime;

	public List<Transform> SpwawnPoints;
	public List<Transform> TargetPoints;
	public GameObject FishPrefab;

	private void Start()
	{
		StartCoroutine(SpawnFishes());
	}

	private IEnumerator SpawnFishes()
	{
		while (true)
		{
			var instance = Instantiate(FishPrefab, SpwawnPoints[Random.Range(0, SpwawnPoints.Count)]);
			((SwimController) instance.GetComponent(typeof(SwimController))).TargetPoint =
				TargetPoints[Random.Range(0, TargetPoints.Count)];
			yield return new WaitForSeconds(Random.Range(MinWaitTime, MaxWaitTime));
		}
	}
}
