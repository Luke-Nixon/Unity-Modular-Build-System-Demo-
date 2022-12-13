using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class material_controller : MonoBehaviour
{
    public GameObject go;
    public GameObject material_menu;
    private bool active;

    public Material brick;
    public Material wood;
    public Material glass;

    public Button brick_button;
    public Button wood_button;
    public Button glass_button;

    public flymode flymode;

    // Start is called before the first frame update
    void Start()
    {
        brick_button.onClick.AddListener(delegate { apply_brick_mat(go); } );
        wood_button.onClick.AddListener(delegate { apply_wood_mat(go); });
        glass_button.onClick.AddListener(delegate { apply_glass_mat(go); });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            active = !active;
            flymode.haltinput = active;
            material_menu.SetActive(active);
            Cursor.visible = active;
            
        }
    }


    void apply_brick_mat(GameObject go)
    {
        if (go != null)
        {
            go.GetComponent<Renderer>().material = brick;
            Debug.Log("mat applied" + brick);
        }
    }
    void apply_wood_mat(GameObject go)
    {
        if (go != null)
        {
            go.GetComponent<Renderer>().material = wood;
        }
    }
    void apply_glass_mat(GameObject go)
    {
        if (go != null)
        {
            go.GetComponent<Renderer>().material = glass;
        }
    }

}
