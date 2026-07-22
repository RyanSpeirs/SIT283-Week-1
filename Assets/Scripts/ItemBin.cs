using UnityEngine;
using UnityEngine.Events;

public class ItemBin : MonoBehaviour
{
    // set capacity and item type
    [SerializeField] private ItemType acceptedItem;
    [SerializeField] private int capacity = 5;

    // sets up our UI and spawner
    public ItemTypeIntEvent OnCountChanged;
    public ItemTypeEvent OnBinFull;

    private int currentCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        // First we determine is the object an item or not
        Item item = other.GetComponent<Item>();
        if (item == null)
        {
            Debug.Log($"[ItemBin] Ignored non-item: {other.gameObject.name}");
            return;
        }
        
        // if its an item we need to check if its the wrong one and destroy it
        if (item.type != acceptedItem)
        {
            Debug.Log($"[ItemBin] Wrong type ({item.type}, expected {acceptedItem}) — destroying");
            Destroy(other.gameObject);
            return;
        }

        //  If the item is correct but the bin is full we log the contact but do nothing
        if (currentCount >= capacity)
        {
            Debug.Log($"[ItemBin] Correct type but bin already full ({currentCount}/{capacity})");
            Destroy(other.gameObject);
            return;
        }

        //  increment the count, plus log it, and then destroy it
        currentCount++;
        Debug.Log($"[ItemBin] Counted! {acceptedItem} now at {currentCount}/{capacity}");
        OnCountChanged?.Invoke(acceptedItem, currentCount);
        Destroy(other.gameObject);

        // Lastly if this fills the bin, we trigger the bin-full event for the scoreboard and the spawner
        if (currentCount >= capacity)
        {
            Debug.Log($"[ItemBin] {acceptedItem} bin full — invoking OnBinFull");
            OnBinFull?.Invoke(acceptedItem);
        }
    }
}

