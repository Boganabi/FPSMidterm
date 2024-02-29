using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // get a reference to the playercharacter component
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();

        // if its not null, say the player was hit
        if(player != null)
        {
            // Debug.Log("Player hit!");
            player.Hurt(damage);
        }

        // destory this game object
        Destroy(this.gameObject);
    }
}
