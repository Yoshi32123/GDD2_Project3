using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public string LevelSceneName;

    /**<summary>
     * switches to the scene whose name matches the given game.</summary>
     * <param name="nextSceneName">  Scene name to switch to</param>
     */
    public void NextScene(string nextSceneName)
    {
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(nextSceneName);
    }

    /**<summary>
     * Adds a scene on top of our current scene and sets it to active.</summary>
     * <param name="nextSceneName">  Scene name to add</param>
     */
    public void NextSceneAdditive(string nextSceneName)
    {
        PlayerPrefs.SetString("previousScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
        Scene Game_scene = SceneManager.GetSceneByName(nextSceneName);
        StartCoroutine(WaitForSceneLoad(SceneManager.GetSceneByName(Game_scene.name)));
    }

    /**
     * <summary>Will switch to the previous store scene</summary>
     */
    public void GoToPreviousScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("previousScene"));
    }

    /**
     * <summary>Will switch active scene to the loaded additive scene after it loads</summary>
     * <param name="scene">  Scene name to make active</param>
     */
    public IEnumerator WaitForSceneLoad(Scene scene)
    {
        while (!scene.isLoaded)
        {
            yield return null;
        }
        Debug.Log("Setting active scene..");
        SceneManager.SetActiveScene(scene);
    }

    /**
     * <summary>Will set previous scene to active (Should be loaded and stored in previous scene) and unload additive scene</summary>
     */
    public void UnloadAdditiveScene()
    {
        string additiveScene = SceneManager.GetActiveScene().name;
        string mainScene = PlayerPrefs.GetString("previousScene");

        //main loaded scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(mainScene));

        //the additive scene
        PlayerPrefs.SetString("previousScene", additiveScene);
        SceneManager.UnloadSceneAsync(additiveScene);
    }
}
