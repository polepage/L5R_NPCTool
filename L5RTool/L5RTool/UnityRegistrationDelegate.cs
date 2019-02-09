using NPC.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;

namespace L5RTool
{
    class UnityRegistrationDelegate : IRegistrationDelegate
    {
        IUnityContainer _unityContainer;

        public UnityRegistrationDelegate(IUnityContainer container)
        {
            _unityContainer = container;
        }

        public void Register<I, C>() where C: I
        {
            _unityContainer.RegisterSingleton<I, C>();
        }
    }
}
