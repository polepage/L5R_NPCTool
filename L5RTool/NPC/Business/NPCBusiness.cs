using NPC.Model;

namespace NPC.Business
{
    class NPCBusiness : INPC
    {
        public IElement CreateElement(ElementType type)
        {
            IElement element = ElementFactory.Create(type);
            return element;
        }
    }
}
