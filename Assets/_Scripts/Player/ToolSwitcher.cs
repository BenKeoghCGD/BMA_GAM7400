/*
 * Branch: BenS (Stott, Ben)
 * Commit: 316fd398a67098195c6ed06692bcd3651ad80271
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: 
 */

using UnityEngine;
using UnityEngine.UI;

public class ToolSwitcher : MonoBehaviour
{
    int selectedTool = 0;
    public Button tool1;
    public Button tool2;
    public Button tool3;
    public Button tool4;

    // Start is called before the first frame update
    void Start()
    {
        SelectTool();
        tool1.onClick.AddListener(Task1);
        tool2.onClick.AddListener(Task2);
        tool3.onClick.AddListener(Task3);
        tool4.onClick.AddListener(Task4);
    }

    void SelectTool()
    {
        // loops through all the child tools under toolswitcher and activates only the selected tool that match the current tool
        int currentTool = 0;
        foreach (Transform tool in transform)
        {
            if (currentTool == selectedTool)
            {
                tool.gameObject.SetActive(true);
            }
            else
            {
                tool.gameObject.SetActive(false);
            }

            currentTool++;
        }
<<<<<<< Updated upstream
    }

=======

        
    }

    
>>>>>>> Stashed changes
    void Task1()
    {
        // manages the tool switching by cycling through the selected tools and starts again after the last index, if it changes it calls the 'select tool' to update
        int previousSelectedTool = selectedTool;
        
        selectedTool= 0;

        if (previousSelectedTool != selectedTool) SelectTool();
    }

    void Task2()
    {
        // manages the tool switching by cycling through the selected tools and starts again after the last index, if it changes it calls the 'select tool' to update
        int previousSelectedTool = selectedTool;
        
        selectedTool = 1;

        if (previousSelectedTool != selectedTool) SelectTool();
    }
    void Task3()
    {
        // manages the tool switching by cycling through the selected tools and starts again after the last index, if it changes it calls the 'select tool' to update
        int previousSelectedTool = selectedTool;
        
        selectedTool = 2;
<<<<<<< Updated upstream

        if (previousSelectedTool != selectedTool) SelectTool();
=======
        
        if (previousSelectedTool != selectedTool)
        {
            SelectTool();
            playerscript.equippedTool = ToolType.LitterPicker;
        }
>>>>>>> Stashed changes
    }
    void Task4()
    {
        // manages the tool switching by cycling through the selected tools and starts again after the last index, if it changes it calls the 'select tool' to update
        int previousSelectedTool = selectedTool;
        
        selectedTool = 3;

        if (previousSelectedTool != selectedTool) SelectTool();
    }

}
