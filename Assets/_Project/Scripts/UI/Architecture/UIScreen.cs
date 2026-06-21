using UnityEngine;

namespace NexusDesk.UI.Architecture
{
    public abstract class UIScreen : MonoBehaviour
    {
        public virtual void OnShow(object args = null) { gameObject.SetActive(true); }
        public virtual void OnHide() { gameObject.SetActive(false); }
        public virtual void OnRefresh() { }
    }
}