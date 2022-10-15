using System;

namespace UI.Menus
{
    public interface IHideMenuNotification
    {
        public event Action HideCurrent;
    }
}
