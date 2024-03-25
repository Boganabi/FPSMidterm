using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{

    public int swordAttackDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {

        PlayerCharacter player = other.GetComponent<PlayerCharacter>();

        if (player != null)
        {

            player.Hurt(swordAttackDamage);
        }
    }
}
