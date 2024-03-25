using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : MonoBehaviour
{

    // Need to Fix this!!!!
    public LayerMask layerMask;

    public float speed = 5.0f;
    public float obstacleRange = 5.0f;
    
    // Setting minimum distance for enemy to enter attack animation
    private float meleeAttack = 1.0f;

    private bool isAlive;

    // Creating animator object
    private Animator anim;

    // Melee attack damage
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;

        // Calling animator
        anim = GetComponent<Animator>();

        // Setting death animation to false
        anim.SetBool("death", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive) {

            transform.Translate(0, 0, speed * Time.deltaTime);

            // Activating walking animation
            anim.SetFloat("speed", speed);

        }

        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

          if (Physics.SphereCast(ray, 0.75f, out hit))
          {
              GameObject hitObject = hit.transform.gameObject;
              if (hitObject.GetComponent<PlayerCharacter>())
              {

                // When soilder spots player he moves toward him
                transform.Translate(0, 0, speed * Time.deltaTime);

                  // Activating the running animation
                  anim.SetFloat("speed" , 5);

                  // Checking if player is within attack range
                  if (hit.distance < meleeAttack)
                  {
                          //Debug.Log("Attak Player!");

                          // If player is within attack range then activate attack animation
                          anim.SetBool("attack", true);
                  }

              }

              else if (hit.distance < obstacleRange)
              {
                  anim.SetFloat("speed", speed);
                  float angle = Random.Range(-110, 110);
                  transform.Rotate(0, angle, 0);
              }

              // Checking if player is still within attack range
              if (hit.distance > meleeAttack)
              {
                  // If not then stop attack animation
                  anim.SetBool("attack", false);
              }

        }
    }
    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
}
