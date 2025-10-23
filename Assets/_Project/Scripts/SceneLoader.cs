using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Michsky.UI.ModernUIPack;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] SystemInput systemInput;
    [SerializeField] GameObject mainUI;
    [SerializeField] CustomDropdown dropdown;
    [SerializeField] int initialSceneId;
    SortedDictionary<int, string> scenes; // build index and name
    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
    }

    private void Start()
    {
        Screen.fullScreen = true;

        // init task names
        scenes = new SortedDictionary<int, string>(){
            {0, "Dissolve"},
            {1, "Glow"},
        };

        if (dropdown == null) return;

        // init dropdown options
        foreach (string taskName in scenes.Values)
        {
            //dropdown.options.Add(new Dropdown.OptionData(taskName));
            dropdown.CreateNewItemFast(taskName, null);
        }
        dropdown.SetupDropdown();

        // load initial dropdown option
        dropdown.ChangeDropdownInfo(initialSceneId);
        SceneManager.LoadScene(initialSceneId);
        mainUI.SetActive(true);
    }

    public void HandleDropdown(int option)
    {
        SceneManager.LoadScene(option);
        //LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
