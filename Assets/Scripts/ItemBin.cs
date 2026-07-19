using UnityEngine;
using UnityEngine.Events;

public class ItemBin : MonoBehaviour
{
    [SerializeField] private ItemType acceptedItem;
    [SerializeField] private int capacity = 5;

    public ItemTypeIntEvent OnCountChanged;
    public ItemTypeEvent OnBinFull;

    private int currentCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item == null)
        {
            Debug.Log($"[ItemBin] Ignored non-item: {other.gameObject.name}");
            return;
        }

        if (item.type != acceptedItem)
        {
            Debug.Log($"[ItemBin] Wrong type ({item.type}, expected {acceptedItem}) — destroying");
            Destroy(other.gameObject);
            return;
        }

        if (currentCount >= capacity)
        {
            Debug.Log($"[ItemBin] Correct type but bin already full ({currentCount}/{capacity})");
            return;
        }

        currentCount++;
        Debug.Log($"[ItemBin] Counted! {acceptedItem} now at {currentCount}/{capacity}");
        OnCountChanged?.Invoke(acceptedItem, currentCount);
        Destroy(other.gameObject);

        if (currentCount >= capacity)
        {
            Debug.Log($"[ItemBin] {acceptedItem} bin full — invoking OnBinFull");
            OnBinFull?.Invoke(acceptedItem);
        }
    }
}

