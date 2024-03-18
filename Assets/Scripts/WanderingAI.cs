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

//        // RaycastHit hit; // contains hit information
//        bool foundPlayer = false;

//        // performs a raycast in every direction around us
//        // possible to change this so that it can filter player to consistently shoot fireball?
//        // it looks like SphereCastAll returns an array, so that might be useful for that idea
//        // what i want to do is check if the player is within shooting range, and if so then shoot at them.
//        // if player is too close to ai then it will back away from the player. else, just wander
//        // if (Physics.SphereCast(ray, 10.0f, out hit))
//        RaycastHit[] hitObjs = Physics.SphereCastAll(ray, pathfindingRadius, maxPathfindingRadius, layermask);
//        Debug.DrawRay(ray.origin, ray.direction);
//        if (hitObjs.Length > 0)
//        {
//            Debug.Log("Found!");
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
//                        foundPlayer = true;
//                    }
//                }
//            }
//            if (foundPlayer == false)
//            {
//                float angle = Random.Range(-110, 110);
//                transform.Rotate(0, angle, 0);
//                Debug.Log("changing direction...");
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
    public float pathfindingRadius = 0.75f;
    public float maxPathfindingRadius = 10.0f;

    // current state
    private bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
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
        // possible to change this so that it can filter player to consistently shoot fireball?
        // it looks like SphereCastAll returns an array, so that might be useful for that idea
        RaycastHit[] hitObjs = Physics.SphereCastAll(ray, pathfindingRadius, maxPathfindingRadius, layermask);
        // if (Physics.SphereCast(ray, 0.75f, out hit))
        if (hitObjs.Length > 0)
        {
            // reference to game object in our spherecast
            // GameObject hitObject = hit.transform.gameObject;
            foreach (RaycastHit hitObj in hitObjs)
            {
                // reference to game object in our spherecast
                GameObject hitObject = hitObj.transform.gameObject;
                // if object hit was a player character, shoot a fireball
                // else, wander as normal
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (fireball == null)
                    {
                        fireball = Instantiate(fireballPrefab) as GameObject;
                        fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hitObj.distance < obstacleRange)
                {
                    Debug.Log(hitObj.distance);
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }

            //// if object hit was a player character, shoot a fireball
            //// else, wander as normal
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
            //{
            //    float angle = Random.Range(-110, 110);
            //    transform.Rotate(0, angle, 0);
            //}
        }
        else
        {
            Debug.Log("nothing found!");
        }
        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, pathfindingRadius, layermask);
        //if (hitColliders.Length > 0)
        //{
        //    foreach (Collider collider in hitColliders)
        //    {
        //        if (collider.gameObject.GetComponent<PlayerCharacter>())
        //        {
        //            if (fireball == null)
        //            {
        //                fireball = Instantiate(fireballPrefab) as GameObject;
        //                fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
        //                fireball.transform.rotation = transform.rotation;
        //            }
        //        }
        //        else if(Vector3.Distance(transform.position, collider.transform.position) < obstacleRange)
        //        {
        //            float angle = Random.Range(-110, 110);
        //            transform.Rotate(0, angle, 0);
        //        }
        //    }
        //}
        //else
        //{
        //    Debug.Log("nothing here");
        //}
    }

    // function for other classes to set dead or alive when hit
    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
}
