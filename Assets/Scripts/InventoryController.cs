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

	public List<string> Dialog;

	private bool _objectInRange;
	private ObjectScriptableObject[] Carrying = new ObjectScriptableObject[4];
	private AudioSource _audioSource;
	private bool _carryingItems;
	private bool _inDialog;
	private int _currentDialogText;
	
	private void Start()
	{
		TextBox.enabled = false;
		TextBoxText.text = "";
		_audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		PickupItem(DetectObjectsAndShowMessage());
	}

	private void PickupItem(GameObject detectedObject)
	{
		if (detectedObject != null && Input.GetKeyDown(KeyCode.F))
		{
			if (detectedObject.gameObject.CompareTag("BoatObjects"))
			{
				PickupBoatObject(detectedObject);
			} 
			else if (detectedObject.gameObject.CompareTag("BoatConstructor"))
			{
				ConstructBoat(detectedObject);
			}
			else if (detectedObject.gameObject.CompareTag("WiseInsect"))
			{
				TalkWithWiseInsect(detectedObject);
			}
		}
	}

	private void TalkWithWiseInsect(GameObject detectedObject)
	{
		_inDialog = true;
		TextBox.enabled = true;
		TextBoxText.text = Dialog[_currentDialogText];

		_currentDialogText++;
		if (_currentDialogText >= Dialog.Count)
			_currentDialogText = 0;
	}

	private void ConstructBoat(GameObject detectedObject)
	{
		var boatConstructor = detectedObject.GetComponent(typeof(BoatConstructor)) as BoatConstructor;
		if (boatConstructor == null) return;

		for (var i = 0; i < Carrying.Length; i++)
		{
			if (Carrying[i] != null)
			{
				boatConstructor.ConstructStep();
				Carrying[i] = null;
			}
		}

		_carryingItems = false;
	}

	private void PickupBoatObject(GameObject detectedObject)
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

		_carryingItems = true;
		_audioSource.Play();
		Destroy(detectedObject);
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

	private GameObject DetectObjectsAndShowMessage()
	{
		_objectInRange = false;
		var colliders = Physics.OverlapSphere(transform.position, ItemRadius);

		foreach (var coll in colliders)
		{
			if (coll.gameObject.CompareTag("BoatObjects"))
			{
				return DisplayText(coll, "Pickup item with 'F'");
			}

			if (coll.gameObject.CompareTag("BoatConstructor"))
			{
				var boatConstructor = coll.gameObject.GetComponent(typeof(BoatConstructor)) as BoatConstructor;
				if (boatConstructor == null) continue;
				
				_objectInRange = true;
				TextBox.enabled = true;
				
				if (_carryingItems && !boatConstructor.BoatComplete)
					TextBoxText.text = "Construct the boat with 'F'";
				else if (!_carryingItems && !boatConstructor.BoatComplete)
					TextBoxText.text = "Collect more items!";
				else
					TextBoxText.text = "You finished the boat! Get in and start your journey across";

				return coll.gameObject;
			}

			if (coll.gameObject.CompareTag("WiseInsect"))
			{
				return _inDialog ? 
					coll.gameObject : 
					DisplayText(coll, "Press 'F' to talk to the wise insect");
			}
		}

		if (!_objectInRange)
		{
			TextBox.enabled = false;
			TextBoxText.text = "";
		}

		return null;
	}

	private GameObject DisplayText(Collider coll, string message)
	{
		_objectInRange = true;
		TextBox.enabled = true;
		TextBoxText.text = message;
		return coll.gameObject;
	}
}
