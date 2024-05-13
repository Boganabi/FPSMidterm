
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WanderingAI : MonoBehaviour
//{

//    [SerializeField] private LayerMask layermask; // used to ignore certain objects
//    [SerializeField] GameObject fireballPrefab;
//    private GameObject fireball;

//    public float speed = 3.0f;
//    public float obstacleRange = 5.0f;
//    public float pathfindingRadius = 0.75f;
//    public float maxPathfindingRadius = 10.0f;

//    // current state
//    private bool isAlive;

//    // Start is called before the first frame update
//    void Start()
//    {
//        isAlive = true;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // check if alive first before moving
//        if (isAlive)
//        {
//            // move forward
//            transform.Translate(0, 0, speed * Time.deltaTime);
//        }

//        // create ray from the wandering game object, pointed in the same diretion as the game object
//        Ray ray = new Ray(transform.position, transform.forward);

//        RaycastHit hit; // contains hit information

//        // performs a raycast in every direction around us
//        // possible to change this so that it can filter player to consistently shoot fireball?
//        // it looks like SphereCastAll returns an array, so that might be useful for that idea
//        RaycastHit[] hitObjs = Physics.SphereCastAll(ray, pathfindingRadius, maxPathfindingRadius, layermask);
//        // if (Physics.SphereCast(ray, 0.75f, out hit))
//        if (hitObjs.Length > 0)
//        {
//            // reference to game object in our spherecast
//            // GameObject hitObject = hit.transform.gameObject;
//            foreach (RaycastHit hitObj in hitObjs)
//            {
//                // reference to game object in our spherecast
//                GameObject hitObject = hitObj.transform.gameObject;
//                // if object hit was a player character, shoot a fireball
//                // else, wander as normal
//                if (hitObject.GetComponent<PlayerCharacter>())
//                {
//                    if (fireball == null)
//                    {
//                        fireball = Instantiate(fireballPrefab) as GameObject;
//                        fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
//                        fireball.transform.rotation = transform.rotation;
//                    }
//                }
//                else if (hitObj.distance < obstacleRange && hitObject.gameObject != this.gameObject)
//                {
//                    Debug.Log(hitObj.distance);
//                    Debug.Log(hitObject.name);
//                    float angle = Random.Range(-110, 110);
//                    transform.Rotate(0, angle, 0);
//                }
//            }
//        }
//        else
//        {
//            Debug.Log("nothing found!");
//        }
//    }

//    // function for other classes to set dead or alive when hit
//    public void SetAlive(bool alive)
//    {
//        isAlive = alive;
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{

    [SerializeField] private LayerMask layermask; // used to ignore certain objects
    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public float detectionRadius = 10f;
    public float detectionRadiusOffset = 0f;

    public int currentHealth = 10;

    // current state
    private bool isAlive;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // check if alive first before moving
        if (isAlive)
        {
            // move forward
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        // create ray from the wandering game object, pointed in the same diretion as the game object
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit; // contains hit information

        // performs a raycast in every direction around us
        // this just focuses on pathfinding, no fireball
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            // reference to game object in our spherecast
            GameObject hitObject = hit.transform.gameObject;

            // if object hit was a player character, shoot a fireball
            // else, wander as normal
            //if (hitObject.GetComponent<PlayerCharacter>())
            //{
            //    if (fireball == null)
            //    {
            //        fireball = Instantiate(fireballPrefab) as GameObject;
            //        fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
            //        fireball.transform.rotation = transform.rotation;
            //    }
            //}
            //else if (hit.distance < obstacleRange)
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }

        // i will now do a separate raycast to detect the player, or anything the AI can attack
        //RaycastHit[] hitObjs = Physics.SphereCastAll(ray, detectionRadius, detectionRadiusOffset, layermask);
        //if(hitObjs.Length > 0)
        //{
        //    // attack all objects that are tagged to be attacked
        //    if (fireball == null)
        //    {
        //        foreach (RaycastHit target in hitObjs)
        //        {
        //            // first need to do raycast to see if we can see object
        //            RaycastHit lineOfSight;
        //            Vector3 rayDirection = target.point - transform.position;
        //            if(Physics.Raycast(transform.position, rayDirection, out lineOfSight))
        //            {
        //                int test1 = lineOfSight.transform.gameObject.layer;
        //                int test2 = (int)Mathf.Log(layermask.value, 2); // since the layer is bitmasked we need to use log base 2
        //                if (test1 == test2)
        //                {
        //                    Debug.Log(target.transform.gameObject.name);
        //                    // change y rotation to look at target
        //                    Transform currTrans = transform;
        //                    transform.LookAt(target.transform);

        //                    // shoot fireball
        //                    fireball = Instantiate(fireballPrefab) as GameObject;
        //                    fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        //                    fireball.transform.rotation = transform.rotation;

        //                    // revert back to old rotation to keep moving around
        //                    transform.Rotate(currTrans.rotation.eulerAngles);
        //                }
        //                else
        //                {
        //                    Debug.Log(test1);
        //                    Debug.Log(test2);
        //                }
        //            }
        //        }
        //    }
        //}
        // first need to do raycast to see if we can see player
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

            if(fireball == null)
            {
                // Debug.Log("pew");
                // change y rotation to look at target
                Transform currTrans = transform;
                transform.LookAt(player.transform);

                // shoot fireball
                fireball = Instantiate(fireballPrefab) as GameObject;
                fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                fireball.transform.rotation = transform.rotation;

                // revert back to old rotation to keep moving around
                transform.Rotate(currTrans.rotation.eulerAngles);
            }
        }
    }

    //Enemy takes damage to health
    public void TakeDamage(int damage)
    {
        //enemy takes damage to health
        currentHealth = currentHealth - damage;
        Debug.Log($"Health: {currentHealth}");
        //Once enemy dies go to ReactToHit
        if (currentHealth <= 0 && isAlive == true)
        {
            Debug.Log("ENEMY DEAD");
            gameObject.GetComponent<ReactiveTarget>().ReactToHit();
            
        }
    }

    // function for other classes to set dead or alive when hit
    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
}

