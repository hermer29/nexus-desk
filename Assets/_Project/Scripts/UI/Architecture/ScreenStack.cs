using System.Collections.Generic;
using NexusDesk.Core;
using UnityEngine;

namespace NexusDesk.UI.Architecture
{
    public sealed class ScreenStack
    {
        private ScreenRegistry screenRegistry;

        private bool _isInitialized;
        private ScreenId _root;
        private Transform _parent;
        private Stack<ScreenId> _stack = new();

        public ScreenStack(Transform parent)
        {
            _parent = parent;
        }

        public UIScreen Current { get; private set; }
        public bool CanPop => _stack.Count > 0;

        private void TryInitialize() 
        {
            if(_isInitialized)
                return;

            screenRegistry = ServiceLocator.Get<ScreenRegistry>();
        }

        public void SetRoot(ScreenId id, object args = null) 
        {   
            _stack.Clear();
            _root = id;
            Show(id);
        }

        public void Replace(ScreenId id, object args = null) 
        {
            _stack.Clear();
            _stack.Push(id);
            Show(id);
        }

        public void Push(ScreenId id, object args = null) 
        {
            _stack.Push(id);
            Show(id);
        }

        public bool Pop() 
        {
            if(!CanPop)
                return false;

            _stack.Pop();

            if(_stack.Count >= 1)
                Show(_stack.Peek());
            else 
                Show(_root);

            return true;
        }

        private void Show(ScreenId id) 
        {
            TryInitialize();
        }
    }
}

