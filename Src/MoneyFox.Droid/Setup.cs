using MoneyFox.Droid.Services;
using MoneyFox.Shared;
using MoneyFox.Shared.Interfaces;
using MoneyFox.Shared.Resources;

namespace MoneyFox.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies
            => new List<Assembly>(base.AndroidViewAssemblies)
            {
                typeof(NavigationView).Assembly,
                typeof(FloatingActionButton).Assembly,
                typeof(Toolbar).Assembly,
                typeof(DrawerLayout).Assembly,
                typeof(ViewPager).Assembly
            };

        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            base.LoadPlugins(pluginManager);
            pluginManager.EnsurePluginLoaded<PluginLoader>();
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterType<IDialogService, DialogService>();
            Mvx.RegisterType<IOneDriveAuthenticator, OneDriveAuthenticator>();
            Mvx.RegisterType<IProtectedData, ProtectedData>();
            Mvx.RegisterType<INotificationService, NotificationService>();
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFragmentsPresenter = new CustomPresenter(AndroidViewAssemblies);
            Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);
            return mvxFragmentsPresenter;
        }

        protected override IMvxApplication CreateApp()
        {
            Strings.Culture = new Localize().GetCurrentCultureInfo();

            return new App();
        }

        protected override IMvxTrace CreateDebugTrace() => new DebugTrace();
    }
}