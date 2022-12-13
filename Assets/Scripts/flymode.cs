using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flymode : MonoBehaviour
{
    public float sensitivity = 2f;
    public float movement_speed = 100f;
    private Rigidbody rb;
    public bool haltinput = false;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.transform.GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!haltinput)
        {
            // camera WASD controls
            if (Input.GetKey(KeyCode.W))
            {
                Vector3 forcedir = Quaternion.Euler(XYRot.x, XYRot.y, 0) * new Vector3(0, 0, Time.deltaTime * movement_speed);

                rb.AddRelativeForce(forcedir, ForceMode.Force);

            }

            if (Input.GetKey(KeyCode.A))
            {
                Vector3 forcedir = Quaternion.Euler(XYRot.x, XYRot.y, 0) * new Vector3(Time.deltaTime * -movement_speed, 0, 0);

               rb.AddRelativeForce(forcedir, ForceMode.Force);

            }

            if (Input.GetKey(KeyCode.S))
            {
                Vector3 forcedir = Quaternion.Euler(XYRot.x, XYRot.y, 0) * new Vector3(0, 0, Time.deltaTime * -movement_speed);

               rb.AddRelativeForce(forcedir, ForceMode.Force);

            }

            if (Input.GetKey(KeyCode.D))
            {
                Vector3 forcedir = Quaternion.Euler(XYRot.x, XYRot.y, 0) * new Vector3(Time.deltaTime * movement_speed, 0, 0);

               rb.AddRelativeForce(forcedir, ForceMode.Force);

            }

            if (Input.GetKey(KeyCode.Space))
            {
                Vector3 forcedir = Quaternion.Euler(XYRot.x, XYRot.y, 0) * new Vector3(0, Time.deltaTime * movement_speed, 0);

               rb.AddRelativeForce(forcedir, ForceMode.Force);

            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Vector3 forcedir = Quaternion.Euler(XYRot.x, XYRot.y, 0) * new Vector3(0, Time.deltaTime * movement_speed, 0);

                rb.AddRelativeForce(forcedir, ForceMode.Force);

            }
        }

        // if the flymode goes beneth the world, reset the height to aboove the world.
        if (this.rb.position.y < 0.1)
        {
            this.transform.position = new Vector3( transform.position.x, transform.position.y + 10, transform.position.z );
        }

    }

    private Vector2 XYRot;
    

    void Update()
    {
        if (!haltinput)
        {
            Vector2 old = XYRot;

            // camera rotation
            Vector2 mouse = new Vector2
            {
                x = Input.GetAxis("Mouse X"),
                y = Input.GetAxis("Mouse Y")
            };

            XYRot.x -= mouse.y  * sensitivity;
            XYRot.y += mouse.x  * sensitivity;

            XYRot.x = Mathf.Clamp(XYRot.x, -90, 90);

            cam.transform.localEulerAngles =  Vector2.Lerp(  old, XYRot , Time.deltaTime);

            

            Cursor.visible = false;


        }

    }

}
