using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float LoadDelayTime = 1.5f;

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void WaitAndLoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(WaitAndLoad(currentSceneIndex));
    }

    IEnumerator WaitAndLoad(int currentSceneIndex)
    {
        yield return new WaitForSeconds(LoadDelayTime);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void Quit()
    {
        Application.Quit();
    }



}
