using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Component))]
public class DestroyOnTrigger : MonoBehaviour
{
    public string TagToDestroy;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(TagToDestroy))
            Destroy(other.gameObject);
    }
}
