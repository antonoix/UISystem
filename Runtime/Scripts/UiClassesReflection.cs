using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Plugins.Antonoix.UISystem.Base;

namespace Plugins.Antonoix.UISystem
{
    public class UiClassesReflection
    {
        public static List<Type> FindAllPresenters()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> presenters = new List<Type>();

            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes()
                    .Where(mytype => mytype.GetInterfaces().Contains(typeof(IBasePresenter)) && !mytype.IsAbstract);
                
                presenters.AddRange(types);
            }

            return presenters;
        }
    }
}