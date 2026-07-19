using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text cyanCounter;
    public TMP_Text magentaCounter;
    public TMP_Text yellowCounter;
    public TMP_Text jobDoneText;

    private bool cyanFull = false;
    private bool magentaFull = false;
    private bool yellowFull = false;

    private void Start()
    {
        if (jobDoneText != null) jobDoneText.gameObject.SetActive(false);
    }

    // This updates the bilboard display 
    public void UpdateCount(ItemType type, int count)
    {
        switch (type)
        {
            case ItemType.CyanCube:
                if (cyanCounter != null) cyanCounter.text = $"Cyan: {count}/5";
                break;
            case ItemType.Magenta:
                if (magentaCounter != null) magentaCounter.text = $"Magenta: {count}/5";
                break;
            case ItemType.Yellow:
                if (yellowCounter != null) yellowCounter.text = $"Yellow: {count}/5";
                break;
        }
    }

    // When all 3 bins are full, displays Job Done message
    public void NotifyBinFull(ItemType type)
    {
        switch (type)
        {
            case ItemType.CyanCube: cyanFull = true; break;
            case ItemType.Magenta: magentaFull = true; break;
            case ItemType.Yellow: yellowFull = true; break;
        }

        if (cyanFull && magentaFull && yellowFull)
        {
            if (jobDoneText != null) jobDoneText.gameObject.SetActive(true);
        }
    }
}

