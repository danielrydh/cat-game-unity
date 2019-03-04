using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.InteropServices;



public class SceneBack : MonoBehaviour
{
    [SerializeField] int Sceneback = 0;
    [SerializeField] int TheScore = 0;

    // Start is called before the first frame update
    [DllImport("__Internal")]
    private static extern void GameOver(int score);

    // Then create a function that is going to trigger
    // the imported function from our JSLib.

    public void Over(int score)
    {
        GameOver(TheScore);
        
    }


    void Start()
    {
        
        Over(1);

        
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
