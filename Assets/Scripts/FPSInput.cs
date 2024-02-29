using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{

    public float speed = 3f;
    public float gravity = -9.8f;

    private CharacterController charController;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        //float deltaZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        //transform.Translate(deltaX, 0, deltaZ);

        // possible modification: create a boolean that will stop player movement if health is 0

        // instead of using the above to move the character, we can simply use the character controller
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        // ensure we don't move too fast
        movement = Vector3.ClampMagnitude(movement, speed);

        // apply gravity
        movement.y = gravity;

        // make movement frame dependent
        movement *= Time.deltaTime;

        // transform from local to global
        movement = transform.TransformDirection(movement);

        // move charater controller
        charController.Move(movement);
    }
}
