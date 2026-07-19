using TMPro;
using UnityEngine;
using System.Collections.Generic;

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

    // Wire each ItemBin's OnCountChanged to this method in the Inspector
    public void UpdateCount(ItemType type, int count)
    {
        switch (type)
        {
            case ItemType.CyanCube:
                if (cyanCounter != null) cyanCounter.text = $"Cyan: {count}/10";
                break;
            case ItemType.Magenta:
                if (magentaCounter != null) magentaCounter.text = $"Magenta: {count}/10";
                break;
            case ItemType.Yellow:
                if (yellowCounter != null) yellowCounter.text = $"Yellow: {count}/10";
                break;
        }
    }

    // Wire each ItemBin's OnBinFull to this method in the Inspector
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

