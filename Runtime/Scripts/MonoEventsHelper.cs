using System;
using System.Collections.Generic;
using Plugins.Antonoix.UISystem.Base;
using UnityEngine;

namespace Plugins.Antonoix.UISystem
{
    internal class MonoEventsHelper : MonoBehaviour
    {
        private readonly List<IUpdatable> _updatables = new();
        
        private void Update()
        {
            _updatables.ForEach(x => x.Update());
        }

        public void RegisterUpdatable(IUpdatable updatable)
        {
            _updatables.Add(updatable);
        }
    }
}