namespace NPC.Registration
{
    public interface IRegistrationDelegate
    {
        void Register<I, C>() where C : I;
    }
}
