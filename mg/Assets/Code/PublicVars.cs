using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicVars : MonoBehaviour
{
    public static int jumpForce = 300;
    
    public static int score;

    public static void Collect() {
        score += 1;
    }
    public static void Collect2() {
        score += 2;
    }
}
