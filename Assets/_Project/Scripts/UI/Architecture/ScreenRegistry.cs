using UnityEngine;
using NexusDesk.UI;
using JetBrains.Annotations;

namespace NexusDesk.UI.Architecture
{
    [CreateAssetMenu(menuName = "Nexus Desk/UI/Create Screen Registry", fileName = "ScreenRegistry")]
    public class ScreenRegistry : ScriptableObject
    {
        [SerializeField] ScreenEntry[] _screens;

        [CanBeNull]
        public UIScreen GetPrefab(ScreenId id)
        {
            foreach (var screen  in _screens)
            {
                if(screen.id == id) {
                    return screen.prefab;
                }
            }
            return null;
        }
    }
}