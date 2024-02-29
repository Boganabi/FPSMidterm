using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{

    // if we get a hit, then react to hit
    // in the future we want to only call this function once, so we dont get multiple "deaths"
    public void ReactToHit()
    {
        // if we have wandering ai script, set its alive state
        WanderingAI behavior = GetComponent<WanderingAI>();
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }
        StartCoroutine(Die());
    }

    // death animation as coroutine
    public IEnumerator Die()
    {
        // have target fall over to side
        this.transform.Rotate(-75, 0, 0); // change this to rotate away from hit direction in the future...

        // wait
        yield return new WaitForSeconds(1.5f);

        // despawn
        Destroy(this.gameObject); // using "this" isnt necessary here, but it does help keep things straight in your mind
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
