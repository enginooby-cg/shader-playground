using System;
using Michsky.UI.ModernUIPack;
using UnityEngine;

public class MoveDropdown : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera flyCamera;
    public float xOffset = 6f;
    
    public CustomDropdown customDropdown; // assign in Inspector
    public Transform labelsParent;        // assign "Labels" GameObject here

    private int _currentDropdownIndex;

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
        _currentDropdownIndex = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeNextDropdownItem();
        }
    }

    private void ChangeNextDropdownItem()
    {
        var itemCount = customDropdown.dropdownItems.Count;
        _currentDropdownIndex = _currentDropdownIndex >= itemCount - 1 ? 0 : _currentDropdownIndex + 1;
        Debug.Log(_currentDropdownIndex);
        customDropdown.ChangeDropdownInfo(_currentDropdownIndex);
        HandleDropdown(_currentDropdownIndex);
    }

    void AddDropdownItem(string name)
    {
        CustomDropdown.Item newItem = new CustomDropdown.Item();
        newItem.itemName = name;
        newItem.itemIcon = null; // you can assign icons here if needed
        customDropdown.dropdownItems.Add(newItem);
    }
    
    public void HandleDropdown(int index)
    {
        _currentDropdownIndex = index;
        flyCamera.transform.position = new Vector3(xOffset * index, 0, 0);
        flyCamera.transform.eulerAngles = Vector3.zero;
    }
}
