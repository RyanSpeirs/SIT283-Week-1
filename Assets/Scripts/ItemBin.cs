using UnityEngine;
using UnityEngine.Events;

public class ItemBin : MonoBehaviour
{
    [SerializeField] private ItemType acceptedItem;
    [SerializeField] private int capacity = 10;

    public ItemTypeIntEvent OnCountChanged;
    public ItemTypeEvent OnBinFull;

    private int currentCount = 0;

    private void OnTriggerEnter(Collider other)
    {

        Item item = other.GetComponent<Item>();
        if (item == null) return; // we only care about Items

        if (item.type != acceptedItem) // wrong items get destroyed
        {
            Destroy(other.gameObject);
            return;
        }

        if (currentCount >= capacity) return; // If bin is full, stop accepting items

        currentCount++;
        OnCountChanged?.Invoke(acceptedItem, currentCount);
        Destroy(other.gameObject);

        if (currentCount >= capacity)  //  tells the spawner and UI the bin is full
            OnBinFull?.Invoke(acceptedItem);
    }
}

