using Michsky.UI.ModernUIPack;
using UnityEngine;

public class MoveDropdown : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera flyCamera;
    public float xOffset = 6f;
    
    public CustomDropdown customDropdown; // assign in Inspector
    public Transform labelsParent;        // assign "Labels" GameObject here

    void Start()
    {
        if (labelsParent == null || customDropdown == null)
        {
            Debug.LogWarning("Labels parent or CustomDropdown not assigned!");
            return;
        }

        // Clear any existing dropdown items
        customDropdown.dropdownItems.Clear();

        // Loop through all *immediate* children of "Labels"
        foreach (Transform child in labelsParent)
        {
            AddDropdownItem(child.name);
        }

        // Refresh UI
        customDropdown.SetupDropdown();
    }

    void AddDropdownItem(string name)
    {
        CustomDropdown.Item newItem = new CustomDropdown.Item();
        newItem.itemName = name;
        newItem.itemIcon = null; // you can assign icons here if needed
        customDropdown.dropdownItems.Add(newItem);
    }
    
    public void HandleDropdown(int index){
        flyCamera.transform.position = new Vector3(xOffset * index, 0, 0);
        flyCamera.transform.eulerAngles = Vector3.zero;
    }
}
