using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private static float gravity = -1f;
    void Start()
    {
        
    }

    public static float getGravity(){
        return gravity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
