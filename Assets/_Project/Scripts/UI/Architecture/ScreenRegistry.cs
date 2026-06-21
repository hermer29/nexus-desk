using UnityEngine;

namespace NexusDesk.UI.Architecture
{
    [CreateAssetMenu(menuName = "Nexus Desk/UI/Create Screen Registry", fileName = "ScreenRegistry")]
    public class ScreenRegistry : ScriptableObject
    {
        [SerializeField] ScreenEntry[] _screens;

        public UIScreen GetPrefab(ScreenId id)
        {
            
        }
    }
}