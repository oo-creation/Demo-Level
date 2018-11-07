using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBoatEntering : MonoBehaviour
{
    private bool _wantsToLeaveBoat;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boat"))
        {
            transform.parent = other.transform;
            ((BoatController) other.gameObject.GetComponent(typeof(BoatController))).BoatEntered.Invoke();

            StartCoroutine(DisplayMessage());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Boat") && !Input.GetButton("Jump"))
        {
            transform.position = transform.parent.position;
            transform.rotation = transform.parent.rotation;
        }

        if (Input.GetButton("Jump"))
            _wantsToLeaveBoat = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Boat"))
        {
            transform.parent = null;
            ((BoatController) other.gameObject.GetComponent(typeof(BoatController))).BoatLeft.Invoke();
        }
    }

    private IEnumerator DisplayMessage()
    {
        yield return new WaitForSeconds(1.5f);
        ((InventoryController) gameObject.GetComponent(typeof(InventoryController))).AttachToFishMessage();
        yield return new WaitForSeconds(5);
        ((InventoryController) gameObject.GetComponent(typeof(InventoryController))).RemoveAttachToFishMessage();
    }
}
