// Copyright (c) 2021 Lins Ian
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file

using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mediapipe;
using Mediapipe.Unity;
using Logger = Mediapipe.Logger;

namespace LiveSystem
{
    [System.Serializable]
    public enum LiveMode
    {
        Live2D,
        Home3D,
    }

    public class Bootstrapper : MonoBehaviour
    {
        private const string _TAG = nameof(Bootstrapper);

        [SerializeField] private Image consoleImage;
        [SerializeField] private GameObject consolePrefab;
        [SerializeField] private GameObject startMenu;
        [SerializeField] private ImageSource.SourceType defaultImageSource;
        [SerializeField] private InferenceMode preferableInferenceMode;
        [SerializeField] private bool enableGlog = true;

        public InferenceMode inferenceMode { get; private set; }
        public bool isFinished { get; private set; }
        private bool isGlogInitialized;

        private IEnumerator Start()
        {
            Application.targetFrameRate = 60;

            Logger.SetLogger(new MemoizedLogger(100));
            Logger.minLogLevel = Logger.LogLevel.Debug;

            Logger.LogInfo(_TAG, "Starting console window...");
            Instantiate(consolePrefab, consoleImage.transform);
            yield return new WaitForEndOfFrame();

            Logger.LogInfo(_TAG, "Setting global flags...");
            GlobalConfigManager.SetFlags();

            if (enableGlog)
            {
                if (Glog.LogDir != null)
                {
                    if (!Directory.Exists(Glog.LogDir))
                    {
                        Directory.CreateDirectory(Glog.LogDir);
                    }
                    Logger.LogVerbose(_TAG, $"Glog will output files under {Glog.LogDir}");
                }
                Glog.Initialize("MediaPipeUnityPlugin");
                isGlogInitialized = true;
            }

            Logger.LogInfo(_TAG, "Initializing AssetLoader...");
            AssetLoader.Provide(new StreamingAssetsResourceManager());     

            DecideInferenceMode();
            if (inferenceMode == InferenceMode.GPU)
            {
                Logger.LogInfo(_TAG, "Initializing GPU resources...");
                yield return GpuManager.Initialize();
            }

            Logger.LogInfo(_TAG, "Preparing ImageSource...");
            ImageSourceProvider.SwitchSource(defaultImageSource);

            DontDestroyOnLoad(GameObject.Find("Image Source"));
            DontDestroyOnLoad(gameObject);
            isFinished = true;

            Logger.LogInfo(_TAG, "Showing Start Menu...");
            yield return new WaitForSeconds(1);
            startMenu.SetActive(true);
            consoleImage.gameObject.SetActive(false);
        }

        //public IEnumerator LoadSceneAsync(int sceneBuildIndex)
        //{
        //    var sceneLoadReq = SceneManager.LoadSceneAsync(sceneBuildIndex);
        //    yield return new WaitUntil(() => sceneLoadReq.isDone);
        //}

        private void DecideInferenceMode()
        {
#if UNITY_EDITOR_OSX || UNITY_EDITOR_WIN
            if (preferableInferenceMode == InferenceMode.GPU)
            {
                Logger.LogWarning(_TAG, "Current platform does not support GPU inference mode, so falling back to CPU mode");
            }
            inferenceMode = InferenceMode.CPU;
#else
            inferenceMode = _preferableInferenceMode;
#endif
        }

        private void OnApplicationQuit()
        {
            GpuManager.Shutdown();

            if (isGlogInitialized)
            {
                Glog.Shutdown();
            }

            Logger.SetLogger(null);
        }
    }
}
