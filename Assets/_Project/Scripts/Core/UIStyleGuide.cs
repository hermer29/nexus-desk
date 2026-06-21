using UnityEngine;

namespace NexusDesk.Core
{
    [CreateAssetMenu(fileName = "UIStyleGuide", menuName = "Nexus Desk/UI/Create Style Guide")]
    public class UIStyleGuide : ScriptableObject
    {
        public Color Success;
        public Color Warning;
        public Color Critical;
        public Color Info;
        public Color Neutral;
        public Color Primary;
        public Color Disabled;
        public Severity Severity;

    }

    [System.Serializable]
    public class Severity
    {
        public Color Low;
        public Color Medium;
        public Color High;
        public Color Critical;
    }
}