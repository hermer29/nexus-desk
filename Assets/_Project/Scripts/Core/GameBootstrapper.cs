using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NexusDesk
{
    public class GameBootstrapper : MonoBehaviour
    {
        const string MainDeskScene = "MainDesk";

        [SerializeField] UIStyleGuide _styleGuide;

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
        }
    }
}
