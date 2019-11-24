using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    //Source: https://blog.teamtreehouse.com/make-loading-screen-unity

    private bool loadScene = false;
    private bool playPressed = false;

    [SerializeField]
    private int scene;
    [SerializeField]
    private TextMeshProUGUI loadingText;
    [SerializeField]
    private GameObject playButton;
    private AsyncOperation async;

    private void Start()
    {
        loadScene = false;
        playPressed = false;
    }

    // Updates once per frame
    void Update()
    {

        // If new scene is not loading yet...
        if (loadScene == false)
        {

            // ...set the loadScene boolean to true to prevent loading a new scene more than once...
            loadScene = true;

            // ...change the instruction text to read "Loading..."
            loadingText.text = "Loading...";

            // ...and start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene());

        }

        // If the new scene has started loading...
        if (loadScene == true)
        {
            //when the scene hits 90% loaded (which is really 100%), then show play button
            if (async != null && async.progress >= 0.9f)
            {
                playButton.SetActive(true);
                loadingText.color = new Color(0, 0, 0, 0);
            }
            else
            {
                // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
                loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
            }

            //switches to the game scene
            if (playPressed)
            {
                async.allowSceneActivation = true;
            }

        }

    }

    /*
     * player pressed play button (call this method in that button)
     */
    public void PlayPressed()
    {
        playPressed = true;
    }


    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene()
    {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        yield return new WaitForSeconds(3);

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;


        // While the asynchronous operation to load the new scene is not yet complete and player hasn't pressed play yet, continue waiting until it's done.
        while (!async.isDone)
        {
            yield return null;
        }
    }

}