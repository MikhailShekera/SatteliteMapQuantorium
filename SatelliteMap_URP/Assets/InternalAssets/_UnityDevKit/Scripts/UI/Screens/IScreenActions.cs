namespace UnityDevKit.UI.Screens
{
    public interface IScreenActions
    {
        IScreen PowerAction(bool isPowered);
        IScreen UpAction();
        IScreen DownAction();
        IScreen ConfirmAction();
        IScreen BackAction();
        IScreen StartStopAction();
    }
}