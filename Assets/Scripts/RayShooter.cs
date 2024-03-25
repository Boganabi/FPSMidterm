using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{

    // store reference to camera
    private Camera cam;

    private float timeSinceLastShot = 0;
    public float cooldown = 0.1f;

    public bool useNormalGun = true; // change this in the inspector
    public int damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        // hide cursor in center of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (useNormalGun)
        {
            // run when player clicks left mouse button
            if (Input.GetMouseButtonDown(0)) // if using GetMouseButton(0) instead, it will spawn dozens of spheres every click instead of 1
            {
                // store location of midpoint of the screen
                Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);

                // create a ray using midpoint of screen as the origin
                Ray ray = cam.ScreenPointToRay(point);

                // create raycast hit object to figure out where the ray hit
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    handleShot(hit);
                }
            }
        }
        else
        {
            // the below will be the machine gun code
            timeSinceLastShot += Time.deltaTime;
            if (Input.GetMouseButton(0)) // if using GetMouseButton(0) instead, it will spawn dozens of spheres every click instead of 1
            {
                // store location of midpoint of the screen
                Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);

                // create a ray using midpoint of screen as the origin
                Ray ray = cam.ScreenPointToRay(point);

                // create raycast hit object to figure out where the ray hit
                RaycastHit hit;
                if (timeSinceLastShot > cooldown)
                {
                    if (Physics.Raycast(ray, out hit))
                    {
                        handleShot(hit);
                    }
                    timeSinceLastShot = 0;
                }
            }
        }
    }

    // helper function so that both the machine gun and regular gun have the same code
    // you could copy this into the update function if you want to only use one type of gun
    private void handleShot(RaycastHit hit)
    {
        // debug print where the ray hit
        // Debug.Log("Hit: " + hit.point);

        // get reference to game object that was hit
        // then call reactive script on it if it exists
        GameObject hitObject = hit.transform.gameObject;
        ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

        // if ray hits an enemy (target not null), indicate it was hit
        // otherwise, place a sphere
        if (target != null)
        {
            // Debug.Log("Target hit!");
            //If enemy hit, take damage from wanderingAI
            target.GetComponent<WanderingAI>().TakeDamage(damage);
        }
        else
        {
            StartCoroutine(SphereIndicator(hit.point));
        }
    }

    // OnGUI method to draw crosshairs
    // I believe this is called every time the screen refreshes, sometimes more than once a frame
    private void OnGUI()
    {
        // font size
        int size = 36;

        // coords where crosshair is drawn
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 4;

        // draw crosshairs as text
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    // a coroutine to place sphere at coords then disappear after 1 second
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        // create new gameobject of a sphere
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.localScale = Vector3.one * 0.01f; // change this value if you want a bigger sphere

        // place at given position
        sphere.transform.position = pos;

        // wait 1 second
        yield return new WaitForSeconds(1);

        // then destory sphere
        Destroy(sphere);
    }
}
