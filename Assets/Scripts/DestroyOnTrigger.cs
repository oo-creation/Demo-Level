using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour {
    private void OnTriggerStay(Collider other)
    {
        Destroy(other.gameObject);
    }
}
