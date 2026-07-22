using UnityEngine;
using UnityEngine.Events;

//  We keep this here to prevent script bloat
public enum ItemType
{
    CyanCube,
    Magenta,
    Yellow
}

public class Item : MonoBehaviour
{

    // defines the item's type
    public ItemType type;
    // sets life span to 60s
    [SerializeField] private float despawnTime = 60f;

    private void Start()
    {
        //  item cleans itself up
        Destroy(gameObject, despawnTime);
    }
}

//  Same as the ItemType enum, we keep these here so its in one place
[System.Serializable] public class ItemTypeEvent : UnityEvent<ItemType> { }
[System.Serializable] public class ItemTypeIntEvent : UnityEvent<ItemType, int> { }
