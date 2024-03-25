using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAI : MonoBehaviour
{

    // Need to Fix this!!!!
    public LayerMask layerMask;
    private GameObject player;

    public float speed = 5.0f;
    public float obstacleRange = 5.0f;
    
    // Setting minimum distance for enemy to enter attack animation
    private float meleeAttack = 1.0f;

    private bool isAlive;

    // Creating animator object
    private Animator anim;
    private bool isAttacking = false;

    // Melee attack damage
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        player = GameObject.FindWithTag("Player");

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

          if (Physics.SphereCast(ray, 0.75f, out hit) && !isAttacking)
          {
            // Debug.Log("No attack rn");
              //GameObject hitObject = hit.transform.gameObject;
              //if (hitObject.GetComponent<PlayerCharacter>())
              //{

              //  // When soilder spots player he moves toward him
              //  transform.Translate(0, 0, speed * Time.deltaTime);

              //    // Activating the running animation
              //    anim.SetFloat("speed" , 5);

              //    // Checking if player is within attack range
              //    if (hit.distance < meleeAttack)
              //    {
              //            //Debug.Log("Attak Player!");

              //            // If player is within attack range then activate attack animation
              //            anim.SetBool("attack", true);
              //    }

              //}

              //else
              if (hit.distance < obstacleRange)
              {
                  anim.SetFloat("speed", speed);
                  float angle = Random.Range(-110, 110);
                  transform.Rotate(0, angle, 0);
              }

              //// Checking if player is still within attack range
              //if (hit.distance > meleeAttack)
              //{
              //    // If not then stop attack animation
              //    anim.SetBool("attack", false);
              //}

        }

        // do anther raycast to check for the player
        RaycastHit lineOfSight;
        Vector3 rayDirection = player.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayDirection, out lineOfSight))
        {
            //int test1 = lineOfSight.transform.gameObject.layer;
            //int test2 = (int)Mathf.Log(layermask.value, 2); // since the layer is bitmasked we need to use log base 2
            //if (test1 == test2)
            //{
            //    Debug.Log("pew");
            //    // change y rotation to look at target
            //    Transform currTrans = transform;
            //    transform.LookAt(player.transform);

            //    // shoot fireball
            //    fireball = Instantiate(fireballPrefab) as GameObject;
            //    fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
            //    fireball.transform.rotation = transform.rotation;

            //    // revert back to old rotation to keep moving around
            //    transform.Rotate(currTrans.rotation.eulerAngles);
            //}
            //else
            //{
            //    Debug.Log(test1);
            //    Debug.Log(test2);
            //}

            // charge and do melee attack
            isAttacking = true;
            Debug.Log(isAttacking);
            GameObject hitObject = lineOfSight.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                // change y rotation to look at target
                Transform currTrans = transform;
                transform.LookAt(player.transform);
                Transform newRot = transform;
                // transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                transform.rotation = Quaternion.Euler(currTrans.eulerAngles.x, newRot.eulerAngles.y, currTrans.eulerAngles.z);
                // transform.rotation = Quaternion.Euler(0, newRot.eulerAngles.y, currTrans.eulerAngles.z);

                // When soilder spots player he moves toward him
                transform.Translate(0, 0, speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);

                // Activating the running animation
                anim.SetFloat("speed", 5);

                // Checking if player is within attack range
                if (lineOfSight.distance < meleeAttack)
                {
                    //Debug.Log("Attak Player!");

                    // If player is within attack range then activate attack animation
                    anim.SetBool("attack", true);
                }

            }

            //else if (hit.distance < obstacleRange)
            //{
            //    anim.SetFloat("speed", speed);
            //    float angle = Random.Range(-110, 110);
            //    transform.Rotate(0, angle, 0);
            //}

            // Checking if player is still within attack range
            if (lineOfSight.distance > meleeAttack)
            {
                // If not then stop attack animation
                anim.SetBool("attack", false);
                // isAttacking = false;
            }

        }
        else
        {
            isAttacking = false;
            Debug.Log("lost");
        }
    }
        public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
}
