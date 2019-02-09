using NPC.Business;

namespace NPC.Registration
{
    public static class RegistrationService
    {
        public static void Register(IRegistrationDelegate container)
        {
            container.Register<IElementFactory, ElementFactory>();
            container.Register<IElementStorage, ElementStorage>();
        }
    }
}
