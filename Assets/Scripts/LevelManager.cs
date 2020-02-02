using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private int currentLevel = 0;
    private GameObject currentTool;
    public Transform leftHand, rightHand;
    public UnityEvent onNextLevel;
    public int CurrentLevel
    {
        get { return currentLevel; }
    }
    public GameObject CurrentTool
    {
        get { return currentTool; }
    }

    public List<GameObject> tools;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;

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
        onNextLevel.Invoke();
    }

    private void SetNewTool()
    {
        foreach (GameObject tool in tools)
        {
            tool.SetActive(false);
        }
        tools[currentLevel].SetActive(true);
        currentTool = tools[currentLevel];
    }

}
