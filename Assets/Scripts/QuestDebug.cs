using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDebug : MonoBehaviour
{
    public static QuestDebug Instance;
    bool inMenu;

    Text logText;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = DebugUIBuilder.instance.AddLabel("Debug");
        logText = rt.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Start) || Input.GetKeyDown(KeyCode.Q))
        {
            if (inMenu) DebugUIBuilder.instance.Hide();
            else DebugUIBuilder.instance.Show();
            inMenu = !inMenu;
        }
    }

    public void Log(string msg)
    {
        logText.text = msg;
    }
}
