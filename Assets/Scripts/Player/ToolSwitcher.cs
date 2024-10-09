using System;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class ToolSwitcher : MonoBehaviour
{
    
    
    public int selectedTool = 0;
    public Button toolButton;

    // Start is called before the first frame update
    void Start()
    {
        SelectTool();
        toolButton.onClick.AddListener(Task);
    }

    // Update is called once per frame
    void Update() 
    {

    }

    void SelectTool()
    {
        // loops through all the child tools under toolswitcher and activates only the selected tool that match the current tool
        int currentTool = 0;
        foreach (Transform tool in transform)
        {
            if (currentTool == selectedTool)
                tool.gameObject.SetActive(true);
            else
                tool.gameObject.SetActive(false);
            currentTool++;
        }
    }

    void Task()
    {
        // manages the tool switching by cycling through the selected tools and starts again after the last index, if it changes it calls the 'select tool' to update
        int previousSelectedTool = selectedTool;
        if (selectedTool >= transform.childCount - 1)
            selectedTool = 0;
        else
            selectedTool++;

        if (previousSelectedTool != selectedTool)
        {
            SelectTool();
        }
    }
   
}
