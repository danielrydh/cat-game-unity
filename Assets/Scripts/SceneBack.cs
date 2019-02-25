using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;


public class SceneBack : MonoBehaviour
{
    [SerializeField] int Sceneback = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BackToStart();
    }

    private void BackToStart()
    {
        bool jumped = CrossPlatformInputManager.GetButtonDown("Jump");

        if (jumped)
        {           
                var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex - Sceneback);    
        }
    }
}
