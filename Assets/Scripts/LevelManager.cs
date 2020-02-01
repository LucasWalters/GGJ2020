using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int currentLevel = 0;
    private GameObject currentTool;
    public Transform leftHand, rightHand;
    public int CurrentLevel
    {
        get { return currentLevel; }
    }

    public List<GameObject> tools;

    public void Start()
    {
        SetNewTool();
    }

    public void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown(KeyCode.R))
        {
            GoToNextLevel();
        }
    }

    public void GoToNextLevel()
    {
        currentLevel++;
        if (currentLevel >= tools.Count)
        {
            currentLevel = 0;
        }
        SetNewTool();
    }

    private void SetNewTool()
    {
        if (currentTool != null)
        {
            Destroy(currentTool);
        }
        currentTool = Instantiate(tools[currentLevel], rightHand.position, Quaternion.identity);
        currentTool.GetComponent<FollowHand>().handToFollow = rightHand.transform;
    }

}
