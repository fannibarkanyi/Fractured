using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasKey = false;
    public GameObject keyVisualRosemary;

    public void GiveKey()
    {
        if (hasKey) return;
        hasKey = true;
        if (keyVisualRosemary != null) keyVisualRosemary.SetActive(true);
    }
}
