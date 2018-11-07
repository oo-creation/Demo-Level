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
			var instance = Instantiate(FishPrefab, SpwawnPoints[Random.Range(0, SpwawnPoints.Count)]);
			var controller = (SwimController) instance.GetComponent(typeof(SwimController));
			controller.TargetPoint =TargetPoints[Random.Range(0, TargetPoints.Count)];
			_boatController.HighlightFishToAttach.AddListener(controller.HighlightFish);
			_boatController.RemoveHighLight.AddListener(controller.RemoveHighlight);
			yield return new WaitForSeconds(Random.Range(MinWaitTime, MaxWaitTime));
		}
	}
}
