﻿using System;
using MoneyFox.Shared.Extensions;
using MoneyFox.Shared.Messages;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using PropertyChanged;

namespace MoneyFox.Shared.ViewModels
{
    [ImplementPropertyChanged]
    public class SelectDateRangeDialogViewModel : BaseViewModel
    {
        public SelectDateRangeDialogViewModel()
        {
            StartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            EndDate = DateTime.Today.GetLastDayOfMonth();
        }

        /// <summary>
        ///     Startdate for the custom date range
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     Enddate for the custom date range
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        ///     Selects the dates and notifies observer via the MessageHub
        /// </summary>
        public MvxCommand DoneCommand => new MvxCommand(Done);

        /// <summary>
        ///     Provides an TextSource for the translation binding on this page.
        /// </summary>
        public IMvxLanguageBinder TextSource => new MvxLanguageBinder("", GetType().Name);

        private void Done()
        {
            MessageHub.Publish(new DateSelectedMessage(this, StartDate, EndDate));
        }
    }
}