using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatKeys : MonoBehaviour
{
    CollisionHandler collisionHandler;
    // Start is called before the first frame update
    void Start()
    {
        collisionHandler = GetComponent<CollisionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.L))
        {
            collisionHandler.LoadNextLevel();
        }

        if(Input.GetKey(KeyCode.C))
        {
            collisionHandler.CollisionsDisabled = !collisionHandler.CollisionsDisabled;
        }
    }
}
