using MoneyFox.Droid.Dialogs;
using MoneyFox.Foundation.Interfaces;
using MoneyFox.Shared.Resources;
using MoneyFox.Shared.ViewModels;

namespace MoneyFox.Droid.Activities
{
    [Activity(Label = "StatisticMonthlyExpenseActivity",
        Name = "moneyfox.droid.activities.StatisticMonthlyExpenseActivity",
        Theme = "@style/AppTheme",
        LaunchMode = LaunchMode.SingleTop)]
    public class StatisticMonthlyExpenseActivity : MvxAppCompatActivity<StatisticMonthlyExpensesViewModel>,
        IDialogCloseListener
    {
        private PlotView plotModel;

        public void HandleDialogClose()
        {
            plotModel.Model = ViewModel.MonthlyExpensesModel;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_generic_graphical_statistic);

            SetSupportActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            plotModel = FindViewById<PlotView>(Resource.Id.plotViewModel);

            Title = Strings.ExpenseHistoryLabel;
        }

        protected override void OnStart()
        {
            OnResume();

            ViewModel.LoadCommand.Execute();
            plotModel.Model = ViewModel.MonthlyExpensesModel;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_select, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>
        ///     This hook is called whenever an item in your options menu is selected.
        /// </summary>
        /// <param name="item">The menu item that was selected.</param>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                case Resource.Id.action_add:
                    var dialog = new SelectDateRangeDialog();
                    dialog.Show(FragmentManager.BeginTransaction(), Strings.SelectDateTitle);
                    return true;

                default:
                    return false;
            }
        }
    }
}