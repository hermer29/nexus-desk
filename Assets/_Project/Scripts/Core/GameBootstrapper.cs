using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using NexusDesk.UI.Architecture;

namespace NexusDesk.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        const string MainDeskScene = "MainDesk";

        [SerializeField] UIStyleGuide _styleGuide;
        [SerializeField] ScreenRegistry _screenRegistry;

        void Awake()
        {
            ServiceLocator.Clear();
            RegisterServices();
        }

        IEnumerator Start()
        {
            var loadOp = SceneManager.LoadSceneAsync(MainDeskScene);
            yield return loadOp;
        }

        void RegisterServices()
        {
            if (_styleGuide != null)
                ServiceLocator.Register(_styleGuide);

            if (_screenRegistry != null)
                ServiceLocator.Register(_screenRegistry);

            ServiceLocator.Register(new ScreenStack());
        }
    }
}
