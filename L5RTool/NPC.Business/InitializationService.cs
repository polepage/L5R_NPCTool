using Prism.Ioc;

namespace NPC.Business
{
    public static class InitializationService
    {
        public static void Initialize(IContainerRegistry container)
        {
            container.RegisterSingleton<InternalFactory>();
            container.RegisterSingleton<IGameObjectFactory, GameObjectFactory>();
            container.RegisterSingleton<IStorage, Storage>();
        }
    }
}
