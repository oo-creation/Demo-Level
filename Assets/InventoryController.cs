using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{

	public List<Image> InventorySlots;
	public Image TextBox;
	public Text TextBoxText;

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
		
	}
}
