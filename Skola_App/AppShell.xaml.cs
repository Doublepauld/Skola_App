namespace Skola_App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Modal), typeof(Modal));
            Routing.RegisterRoute(nameof(Modal_udrzbar), typeof(Modal_udrzbar));
            Routing.RegisterRoute(nameof(Modal_podpora), typeof(Modal_podpora));
        }
    }
}