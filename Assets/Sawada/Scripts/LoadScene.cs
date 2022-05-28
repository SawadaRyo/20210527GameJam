using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void ScenesLoad(int sceneNameNumber)
    {
        switch (sceneNameNumber)
        {
            case 1:
                {
                    SceneManager.LoadScene("TitleScene");
                }
                break;
            case 2:
                {
                    SceneManager.LoadScene("GameScene");
                }
                break;
            case 3:
                {
                    SceneManager.LoadScene("RezultScene");
                }
                break;
        }
    }
}

