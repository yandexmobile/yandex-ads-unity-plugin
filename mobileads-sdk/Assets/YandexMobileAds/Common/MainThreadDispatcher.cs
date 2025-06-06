using System;
using System.Collections.Generic;
using UnityEngine;

namespace YandexMobileAds.Common
{
    internal class MainThreadDispatcher : MonoBehaviour
    {
        private static MainThreadDispatcher instance = null;
        private static readonly Queue<System.Action> mainThreadQueue = new Queue<System.Action>();

        private static bool IsRunning
        {
            get { return instance != null; }
        }

        public static void initialize()
        {
            if (IsRunning)
            {
                return;
            }

            // Add an invisible game object to the scene
            GameObject obj = new GameObject("YandexMainThreadDispatcher");
            obj.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(obj);
            instance = obj.AddComponent<MainThreadDispatcher>();
        }

        public static void EnqueueAction(System.Action action)
        {
            lock (mainThreadQueue)
            {
                mainThreadQueue.Enqueue(action);
            }
        }

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            // dispatch actions on the main thread when the queue is not empty
            while (mainThreadQueue.Count > 0)
            {
                System.Action dequeuedAction = null;
                lock (mainThreadQueue)
                {
                    try
                    {
                        dequeuedAction = mainThreadQueue.Dequeue();
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                }
                if (dequeuedAction != null)
                {
                    dequeuedAction.Invoke();
                }
            }
        }

        void OnDisable()
        {
            instance = null;
        }
    }
}
