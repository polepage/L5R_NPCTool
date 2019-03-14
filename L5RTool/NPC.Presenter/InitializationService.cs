using Prism.Ioc;

namespace NPC.Presenter
{
    public static class InitializationService
    {
        public static void Initialize(IContainerRegistry container)
        {
            container.RegisterSingleton<IFactory, Factory>();
            container.RegisterSingleton<IStorage, Storage>();
            container.RegisterSingleton<IExternalStorage, ExternalStorage>();
        }
    }
}
