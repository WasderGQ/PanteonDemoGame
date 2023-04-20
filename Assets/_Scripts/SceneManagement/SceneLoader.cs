using System.Collections;
using _Scripts._Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.SceneManagement
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        private AsyncOperation _nextSceneLoadOperation;
        
        [SerializeField] private GameObject _uiPanel;


        private void Start()
        {
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += ActiveScenesChanged;
            Application.targetFrameRate = 60;
            
            
            LoadFirstScene();
        }

        private void ActiveScenesChanged(Scene current, Scene next) => Debug.Log("Active scene has been changed: " + current.name + "-->" + next.name);
        
        private void LoadFirstScene()
        {
            LoadScene(Scenes.MainMenuScene);
        }

        public void LoadScene(Scenes sceneToLoad)
        {
            StartCoroutine(LoadSceneRoutine(sceneToLoad));
        }

        private IEnumerator LoadSceneRoutine(Scenes sceneName)
        {
            //GameEvents.OnSceneLoaderBeginLoad?.Invoke(sceneName.ToString()); //Event
            
            //Open UI
            
            _uiPanel.SetActive(true);

            //if there are more scene than loading scene, that means there is a scene need to unload.
            if (SceneManager.sceneCount > 1)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);//unload active scene
                
                //Set loading scene to active scene
                //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int) Scenes.LoadingScene));
            }
            
            //Load new scene
            _nextSceneLoadOperation = SceneManager.LoadSceneAsync((int)sceneName, LoadSceneMode.Additive);
            
            //Update UI until finishes
            
            
            
            //Set active newly loaded scene
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int) sceneName));
            
            //Close UI
            _uiPanel.SetActive(false);
            //Optimization
            Resources.UnloadUnusedAssets();
            
            //GameEvents.OnSceneLoaderEndLoad?.Invoke(sceneName.ToString()); //Event
            yield break;
        }

        public enum Scenes
        {
            MainMenuScene = 0,
            GameScene= 1,
            
        }
    }
}