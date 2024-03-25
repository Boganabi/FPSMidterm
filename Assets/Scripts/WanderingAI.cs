using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{

    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;

    public int currentHealth = 10;

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
            // transform.Translate(0, 0, speed * Time.deltaTime);
        }

        // create ray from the wandering game object, pointed in the same diretion as the game object
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit; // contains hit information

        // performs a raycast in every direction around us
        // possible to change this so that it can filter player to consistently shoot fireball?
        // it looks like SphereCastAll returns an array, so that might be useful for that idea
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            // reference to game object in our spherecast
            GameObject hitObject = hit.transform.gameObject;

            // if object hit was a player character, shoot a fireball
            // else, wander as normal
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if(fireball == null)
                {
                    fireball = Instantiate(fireballPrefab) as GameObject;
                    fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    fireball.transform.rotation = transform.rotation;
                }
            }
            else if(hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
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
