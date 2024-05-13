using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    //private float speed = 0;
    // if we get a hit, then react to hit
    // in the future we want to only call this function once, so we dont get multiple "deaths"

    // pool management
    public PoolManager manager;

    public void ResetState() {

        // if we have wandering ai script, set its alive state
        WanderingAI behavior = GetComponent<WanderingAI>();
        // Creating melee AI object
        MeleeAI meleebehavior = GetComponent<MeleeAI>();
        // Creating animation Object
        Animator anim = GetComponent<Animator>();
        if (behavior != null) {
            behavior.SetAlive(true);
        }
        // Checking if enemy AI has died
        if (meleebehavior != null) {

            // If he did, then stop movement
            meleebehavior.SetAlive(true);

            // Enter death animation
            anim.SetBool("death", false);
        }
    }

    public void ReactToHit()
    {
        
        // if we have wandering ai script, set its alive state
        WanderingAI behavior = GetComponent<WanderingAI>();
        // Creating melee AI object
        MeleeAI meleebehavior = GetComponent<MeleeAI>();
        // Creating animation Object
        Animator anim = GetComponent<Animator>();
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }
        // Checking if enemy AI has died
        if (meleebehavior != null)
        {

            // If he did, then stop movement
            meleebehavior.SetAlive(false);

            // Enter death animation
            anim.SetBool("death", true);
        }

        StartCoroutine(Die());
    }

    // death animation as coroutine
    public IEnumerator Die()
    {
        // have target fall over to side
        //this.transform.Rotate(-75, 0, 0); // change this to rotate away from hit direction in the future...

        // Stop all movement
        this.transform.Rotate(0, 0, 0);
        this.transform.Translate(0, 0, 0);

        // check if theres an animation first before waiting, no use in waiting if no death animation
        if(GetComponent<Animator>() != null) {
            // wait
            yield return new WaitForSeconds(1.5f);
        }

        if (manager) {
            manager.DespawnEnemy(this.gameObject);
        }
        else {
            // despawn
            Destroy(this.gameObject); // using "this" isnt necessary here, but it does help keep things straight in your mind
        }
    }

    public void SetManager(PoolManager manager) {
        this.manager = manager;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
