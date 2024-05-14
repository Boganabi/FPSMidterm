using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionData : MonoBehaviour
{
    // Start is called before the first frame update
    public static float ElapsedTime { get; set; }
  //  public static int EnemiesKilled { get; set; }

    public static void Reset()
    {
        ElapsedTime = 0f;
   //     EnemiesKilled = 0;
    }
}
