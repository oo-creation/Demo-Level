using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
	public List<Image> InventorySlots;
	public Image TextBox;
	public Text TextBoxText;
	public Sprite TransparentImage;

	public float ItemRadius;

	private bool _itemInRange;
	private ObjectScriptableObject[] Carrying = new ObjectScriptableObject[4];
	private AudioSource _audioSource;
	
	private void Start()
	{
		TextBox.enabled = false;
		TextBoxText.text = "";
		_audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		GameObject detectedObject = DetectObjects();
		if (detectedObject != null && Input.GetKeyDown(KeyCode.F))
		{
			int slotIndex = FindEmptySlot();
			if (slotIndex == -1)
			{
				TextBoxText.text = "Inventory full";
				return;
			}

			var script = detectedObject.GetComponent(typeof(ObjectItem)) as ObjectItem;
			if (script != null)
			{
				InventorySlots[slotIndex].sprite = script.ObjectSO.InventoryIcon;
				Carrying[slotIndex] = script.ObjectSO;
			}
			_audioSource.Play();
			Destroy(detectedObject);
		}
	}

	private int FindEmptySlot()
	{
		for (int i = 0; i < Carrying.Length; i++)
		{
			Debug.Log(Carrying[i]);
			if (Carrying[i] == null)
				return i;
		}

		return -1;
	}

	private GameObject DetectObjects()
	{
		_itemInRange = false;
		var colliders = Physics.OverlapSphere(transform.position, ItemRadius);

		foreach (var coll in colliders)
		{
			if (!coll.gameObject.CompareTag("BoatObjects")) continue;

			_itemInRange = true;
			TextBox.enabled = true;
			TextBoxText.text = "Pickup item with 'F'";
			return coll.gameObject;
		}

		if (!_itemInRange)
		{
			TextBox.enabled = false;
			TextBoxText.text = "";
		}

		return null;
	}
}
