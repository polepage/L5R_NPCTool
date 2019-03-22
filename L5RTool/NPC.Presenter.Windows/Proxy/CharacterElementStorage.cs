﻿using NPC.Presenter.Windows.Binding;
using System.Windows;

namespace NPC.Presenter.Windows.Proxy
{
    class CharacterElementStorage : ExternalBindingProxy
    {
        protected override Freezable CreateInstanceCore()
        {
            return new CharacterElementStorage();
        }
    }
}
