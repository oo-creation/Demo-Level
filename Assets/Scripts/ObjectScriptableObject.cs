using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Object item", menuName = "Object item", order = 1)]
public class ObjectScriptableObject : ScriptableObject
{

    public Sprite InventoryIcon;
    public GameObject ObjectItself;

}
