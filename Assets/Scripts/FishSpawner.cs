using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
	public Transform SpwawnPoint;
	public GameObject FishPrefab;

	private void Start()
	{
		StartCoroutine(SpawnFishes()	);
	}

	private IEnumerator SpawnFishes()
	{
		Debug.Log("Start Coroutine");
		Instantiate(FishPrefab, SpwawnPoint);
		yield return new WaitForSeconds(Random.Range(1f, 5f));
		
	}
}
