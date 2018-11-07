using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
	public float MinWaitTime;
	public float MaxWaitTime;

	public List<GameObject> SpawnPoints;
	public GameObject FishPrefab;
	public GameObject BoatController;

	private BoatController _boatController;

	private void Start()
	{
		_boatController = (BoatController) BoatController.GetComponent(typeof(BoatController));
		StartCoroutine(SpawnFishes());
	}

	private IEnumerator SpawnFishes()
	{
		while (true)
		{
			var spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Count)];
			var targetPoints = ((TargetPointList) spawnPoint.GetComponent(typeof(TargetPointList))).TargetPoints;
			
			var instance = Instantiate(FishPrefab, spawnPoint.transform);
			var controller = (SwimController) instance.GetComponent(typeof(SwimController));
			
			controller.TargetPoint = targetPoints[Random.Range(0, targetPoints.Count)];
			_boatController.HighlightFishToAttach.AddListener(controller.HighlightFish);
			_boatController.RemoveHighLight.AddListener(controller.RemoveHighlight);
			
			yield return new WaitForSeconds(Random.Range(MinWaitTime, MaxWaitTime));
		}
	}
}
