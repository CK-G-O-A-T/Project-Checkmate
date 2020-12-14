using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject exitPanel;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.uiManager.exitPanel = exitPanel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
