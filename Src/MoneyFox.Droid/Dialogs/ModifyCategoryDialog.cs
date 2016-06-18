using System;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using MoneyFox.Shared.ViewModels;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.FullFragging.Fragments;
using MoneyFox.Foundation.Interfaces;
using MoneyFox.Shared.Model;

namespace MoneyFox.Droid.Dialogs
{
    public class ModifyCategoryDialog : MvxDialogFragment<CategoryDialogViewModel>
    {
        public ModifyCategoryDialog(Category category = null)
        {
            if (category != null)
            {
                ViewModel.IsEdit = true;
                ViewModel.Selected = category;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewModel.LoadedCommand.Execute();

            base.OnCreateView(inflater, container, savedInstanceState);
            var view = this.BindingInflate(Resource.Layout.dialog_modify_category, container, true);

            // Handle dismiss button click
            var button = view.FindViewById<Button>(Resource.Id.button_save_category);
            button.Click += Dismiss;

            return view;
        }

        private void Dismiss(object sender, EventArgs e)
        {
            Dismiss();
        }

        public override void OnDismiss(IDialogInterface dialog) {
            (Context as IDialogCloseListener)?.HandleDialogClose();
            base.OnDismiss(dialog);
        }

        public override void OnResume()
        {
            // Auto size the dialog based on it's contents
            Dialog.Window.SetLayout(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            base.OnResume();
        }
    }
}