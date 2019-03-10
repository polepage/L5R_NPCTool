using Prism.Ioc;

namespace NPC.Parser
{
    public static class InitializationService
    {
        public static void Initialize(IContainerRegistry container)
        {
            container.RegisterSingleton<IParser, Parser>();
            container.RegisterSingleton<IFormater, Parser>();
        }
    }
}
