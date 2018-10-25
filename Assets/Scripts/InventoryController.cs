using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
	public List<Image> InventorySlots;
	public Image TextBox;
	public Text TextBoxText;

	public float ItemRadius;

	private bool _itemInRange;

	private void Awake()
	{
		InventorySlots = new List<Image>();
	}

	private void Start()
	{
		TextBox.enabled = false;
		TextBoxText.text = "";
	}

	private void Update()
	{
		Collider detectedObject;
		DetectObjects(out detectedObject);
		if (detectedObject != null)
		{
			
		}
	}

	private void DetectObjects(out Collider detectedObject)
	{
		_itemInRange = false;
		var colliders = Physics.OverlapSphere(transform.position, ItemRadius);

		foreach (var coll in colliders)
		{
			if (!coll.gameObject.CompareTag("BoatObjects")) continue;

			_itemInRange = true;
			TextBox.enabled = true;
			TextBoxText.text = "Pickup item with 'F'";
			detectedObject = coll;
			break;
		}

		if (!_itemInRange)
		{
			TextBox.enabled = false;
			TextBoxText.text = "";
		}

		detectedObject = null;
	}
}
