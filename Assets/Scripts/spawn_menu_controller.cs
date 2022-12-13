using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawn_menu_controller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cube_prefab;
    public GameObject wall_prefab;
    public GameObject floor_prefab;

    public Button spawn_cube_button;
    public Button spawn_wall_button;
    public Button spawn_floor_button;
    
    public GameObject spawn_menu_ui;

    public object_manipulation_controller object_controller;

    public flymode flymode_controller;

    private bool active = false;

    void Start()
    {
        spawn_cube_button.onClick.AddListener(delegate { spawn_object(cube_prefab); });
        spawn_wall_button.onClick.AddListener(delegate { spawn_object(wall_prefab); });
        spawn_floor_button.onClick.AddListener(delegate { spawn_object(floor_prefab); });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            active = !active;
            spawn_menu_ui.SetActive(active);
            flymode_controller.haltinput = active;
            Cursor.visible = active;
        }

    }

    void spawn_object(GameObject gameObject)
    {
       GameObject go = Instantiate(gameObject);

       object_controller.activate_manipulation(go);

    }


}
