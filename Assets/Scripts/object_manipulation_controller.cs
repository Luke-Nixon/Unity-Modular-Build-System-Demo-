using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class object_manipulation_controller : MonoBehaviour
{
    public bool active = false;
    public GameObject go;
    public float distance = 10f;

    public SphereCollider Collider;

    public Vector3 hold_pos;

    public List<GameObject> found_snap_points = new List<GameObject>();
    public List<GameObject> go_snap_points = new List<GameObject>();

    public material_controller material_Controller;
    


    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        // if the game object is a snap point, and the game object is not a snap point of the manipulated object.
        if (collider.gameObject.CompareTag("snap point") & !go_snap_points.Contains(collider.gameObject))
        {
            found_snap_points.Add(collider.gameObject);

        }
    }

    private void OnTriggerExit(Collider collider)
    {
        found_snap_points.Remove(collider.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            distance += Input.mouseScrollDelta.y;

            distance = Mathf.Clamp(distance, 10, 100);
        }

        this.hold_pos = Camera.main.transform.forward * this.distance + this.transform.position;


        // manipulation logic
        if (active && go != null)
        {
            go.transform.position = hold_pos;
            this.Collider.center = new Vector3(0, 0, 1) * this.distance;

            // find all snap points on the go
            // find all snap points within the sphere colider.
            // do an all to all comparison to find the closest snap points.

            GameObject closest_snap_point_go = null;
            GameObject closest_snap_point_found = null;

            if (found_snap_points.Count >= 1)
            {
                float smallest_found_distance = 0;

                foreach (GameObject found_snap_point in found_snap_points.ToArray())
                {
                    if (found_snap_point == null)
                    {
                        found_snap_points.Remove(found_snap_point);
                        continue;
                    }

                    foreach (GameObject go_snap_point in go_snap_points)
                    {
                        if (Vector3.Distance(found_snap_point.transform.position, go_snap_point.transform.position) > smallest_found_distance)
                        {
                            smallest_found_distance = Vector3.Distance(found_snap_point.transform.position, go_snap_point.transform.position);

                            closest_snap_point_go = go_snap_point;
                            closest_snap_point_found = found_snap_point;
                        }
                    }
                }
                // recheck that snap points still exsist as they may not due to being removed in the null check.
                if (found_snap_points.Count >= 1)
                {
                    //align the snap points.
                    Vector3 base_pos = closest_snap_point_found.transform.position;

                    Vector3 offset = closest_snap_point_go.transform.localPosition;
                    Vector3 rotated_offset = Quaternion.Euler(0, go.transform.rotation.eulerAngles.y, 0) * offset;
                    Vector3 scaled_offset = (Vector3.Scale(rotated_offset, go.transform.localScale));

                    go.transform.position = base_pos + scaled_offset;
                }
            }

            


        }


        // exit the object manipulation with right click
        if (Input.GetMouseButtonDown(1) & this.active)
        {
            deactivate_manipulation();
        }
        // rotate object in 45 degree increments
        if (Input.GetKeyDown(KeyCode.R) & this.active)
        {
            go.transform.Rotate(new Vector3(0, 45, 0));
        }

        // delete/remove objects
        if (Input.GetKeyDown(KeyCode.Backspace) & this.active )
        {
            Destroy(go);
            deactivate_manipulation();
            
            
        }

        //pickup objects with left click
        if (Input.GetMouseButtonDown(0) & !active)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                activate_manipulation(hit.transform.gameObject);
            }
        }

    }

    public void activate_manipulation(GameObject go)
    {
        Debug.Log("manipulation activated");

        this.go = go;
        this.active = true;
        this.material_Controller.go = go;
        // find all snap points within the game object, and add it to the go_snap_points_list.

        go_snap_points.Clear();

        foreach (Transform transform in go.transform)
        {
            if (transform.CompareTag("snap point"))
            {
                go_snap_points.Add(transform.gameObject);
            }
        }
    }

    public void deactivate_manipulation()
    {
        this.active = false;
        this.go = null;
        this.material_Controller.go = null;
    }

}
