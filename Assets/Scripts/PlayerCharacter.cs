using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    //players health variables
    public int maxHealth = 20; 
    public int playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        //set max health to player health
        playerHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        SessionData.ElapsedTime += Time.deltaTime;
        Debug.Log($"Elapsed Time: {SessionData.ElapsedTime} seconds");
    }

    // taking damage
    public float GetElapsedTime(){
        return SessionData.ElapsedTime;
    }
    public void Hurt(int damage)
    {
        playerHealth -= damage;
        Debug.Log($"Helath: {playerHealth}");
        //Player Dies go to Main menu
        if(playerHealth <= 0){
            Debug.Log("YOU DIED");
            //add return to main menu
            SceneManager.LoadSceneAsync("SummaryScene");
        }
    }
}
