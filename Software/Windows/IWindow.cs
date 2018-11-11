using GHI.Glide.Display;

namespace AquaComp.Windows
{
    interface IWindow
    {
        Window Window{get;}
        void UpdateTime(string time);
    }
}
