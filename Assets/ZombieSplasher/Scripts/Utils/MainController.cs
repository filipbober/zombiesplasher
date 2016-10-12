
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace ZombieSplasher
{
    public class MainController : MonoBehaviour
    {

        //public string DefaultSceneName = "";
        public enum SceneState { Reset, Preload, Load, Unload, Postload, Ready, Run, Count };

        public static MainController Instance
        {
            get { return _instance; }
        }

        public SceneState GetSceneState { get { return _sceneState; } }

        public float SceneLoadProgress
        {
            get
            {
                if (_sceneLoadTask != null)
                {
                    return _sceneLoadTask.progress;
                }
                else
                {
                    return 1;
                }
            }
        }

        private static MainController _instance;

        //private enum SceneState { Reset, Preload, Load, Unload, Postload, Ready, Run, Count };
        private delegate void UpdateDelegate();

        private string _currentSceneName;
        private string _nextSceneName;
        private AsyncOperation _resourceUnloadTask;
        private AsyncOperation _sceneLoadTask;
        private SceneState _sceneState;
        private UpdateDelegate[] _updateDelegates;

        //-------------------------------------------------
        // public static methods
        //-------------------------------------------------
        public static void SwitchScene(string nextSceneName)
        {
            //// Reset time on scene change
            //Time.timeScale = 1f;

            if (_instance != null)
            {
                if (_instance._currentSceneName != nextSceneName)
                {
                    _instance._nextSceneName = nextSceneName;
                }
            }
        }

        public static void ReloadScene()
        {
            if (_instance != null)
            {
                string nextSceneName = _instance._currentSceneName;
                _instance._currentSceneName = _instance._currentSceneName + "Old";
                SwitchScene(nextSceneName);
            }
        }

        //-------------------------------------------------
        // protected mono methods
        //-------------------------------------------------
        protected void Awake()
        {
            // MainController should be kept alive between scene changes
            Object.DontDestroyOnLoad(gameObject);

            // Setup the singleton instance
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }

            // Setup the array of updateDelegates
            _updateDelegates = new UpdateDelegate[(int)SceneState.Count];

            // Set each update delegate
            _updateDelegates[(int)SceneState.Reset] = UpdateSceneReset;
            _updateDelegates[(int)SceneState.Preload] = UpdateScenePreload;
            _updateDelegates[(int)SceneState.Load] = UpdateSceneLoad;
            _updateDelegates[(int)SceneState.Unload] = UpdateSceneUnload;
            _updateDelegates[(int)SceneState.Postload] = UpdateScenePostload;
            _updateDelegates[(int)SceneState.Ready] = UpdateSceneReady;
            _updateDelegates[(int)SceneState.Run] = UpdateSceneRun;

            // _currentSceneName = Application.loadedLevelName;
            _currentSceneName = SceneManager.GetActiveScene().name;

            _nextSceneName = _currentSceneName;
            //Debug.Log("Loaded scene name = " + _nextSceneName);
            _sceneState = SceneState.Run;
        }

        protected void OnDestroy()
        {
            // Clean up all the updateDelegates
            if (_updateDelegates != null)           // If array itself is not null
            {
                for (int i = 0; i < (int)SceneState.Count; i++)
                {
                    _updateDelegates[i] = null;
                }

                _updateDelegates = null;            // Set the array itself to null
            }

            // Clean up the singleton instance
            if (_instance != null)
            {
                _instance = null;
            }
        }

        protected void OnEnable()
        {

        }

        protected void OnDisable()
        {

        }

        protected void Update()
        {
            if (_updateDelegates[(int)_sceneState] != null)
            {
                _updateDelegates[(int)_sceneState]();
            }
        }

        //-------------------------------------------------
        // private methods
        //-------------------------------------------------
        // Reset the new scene controller to start cascade of loading
        private void UpdateSceneReset()
        {
            //Debug.Log("UpdateSceneReset");
            // Run a gc pass
            System.GC.Collect();
            _sceneState = SceneState.Preload;
        }

        // Handle anything that need to happen before loading
        private void UpdateScenePreload()
        {
            //Debug.Log("UpdateScenePreload" + _nextSceneName);
            //_sceneLoadTask = Application.LoadLevelAsync(_nextSceneName);
            _sceneLoadTask = SceneManager.LoadSceneAsync(_nextSceneName);

            _sceneState = SceneState.Load;
        }

        // Show the loading screen until it's loaded
        private void UpdateSceneLoad()
        {
            //Debug.Log("UpdateSceneLoad");
            // Done loading?
            if (_sceneLoadTask.isDone == true)
            {
                _sceneState = SceneState.Unload;
                //Debug.Log("Scene is loaded!");
            }
            else
            {
                // Update scene loading progress - like scene lading progress bar one the screen.
                Debug.Log("Loading progress = " + _sceneLoadTask.progress);
            }
        }

        // Clean up unused resources by unloading them
        private void UpdateSceneUnload()
        {
            //Debug.Log("UpdateSceneUnload");
            // Cleaning up resources yet?
            if (_resourceUnloadTask == null)
            {
                _resourceUnloadTask = Resources.UnloadUnusedAssets();
            }
            else
            {
                // Done cleaning up?
                if (_resourceUnloadTask.isDone == true)
                {
                    _resourceUnloadTask = null;
                    _sceneState = SceneState.Postload;
                }
            }
        }

        // Handle anything that needs to happen immediately after loading
        private void UpdateScenePostload()
        {
            //Debug.Log("UpdateScenePostload");
            _currentSceneName = _nextSceneName;     // For instance both scenes are Menu when menu is postloaded
            _sceneState = SceneState.Ready;
        }

        private void UpdateSceneReady()
        {
            //Debug.Log("UpdateSceneReady");
            // Run a gc pass
            System.GC.Collect();                    // Optional step
            _sceneState = SceneState.Run;
        }

        private void UpdateSceneRun()
        {
            //Debug.Log("UpdateSceneRun -> " + _currentSceneName);        
            if (_currentSceneName != _nextSceneName)
            {
                _sceneState = SceneState.Reset;
            }
        }

    }
}


