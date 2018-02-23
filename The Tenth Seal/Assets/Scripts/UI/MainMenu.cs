using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public void ChangeSceneByInt(int scene)
    {
            SceneManager.LoadScene(scene);
    }
    
    //IEnumerator LoadAsync(int SceneIndex)
    //{
    //    AsyncOperation loadOp = SceneManager.LoadSceneAsync(SceneIndex, LoadSceneMode.Single);
    //    loadOp.allowSceneActivation = false;

    //    while (!loadOp.isDone)
    //    {

    //        float progress = Mathf.Clamp01(loadOp.progress / 0.9f);

    //        if (loadOp.progress == 0.9f)
    //        {
    //            loadOp.allowSceneActivation = true;
    //        }

    //        yield return null;
    //    }
    //}
}
