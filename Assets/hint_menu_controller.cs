using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hint_menu_controller : MonoBehaviour
{

    public GameObject hint_menu;
    private bool active = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // toggle the f1 menu.
        if (Input.GetKeyDown(KeyCode.F1))
        {
            active = !active;
            hint_menu.SetActive(active);
        }
    }
}
