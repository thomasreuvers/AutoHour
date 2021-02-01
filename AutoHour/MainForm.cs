using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using AutoHour.Handlers;
using AutoHour.Objects;
using System.Timers;

namespace AutoHour
{
    public partial class MainForm : Form
    {
        private WebHandler _webHandler;
        private readonly BackgroundWorker _backgroundWorker;

        public MainForm()
        {
            InitializeComponent();

            #region ConfigureTimePickers

            StartTimePicker.Format = DateTimePickerFormat.Custom;
            EndTimePicker.Format = DateTimePickerFormat.Custom;

            StartTimePicker.CustomFormat = "HH:mm";
            EndTimePicker.CustomFormat = "HH:mm";

            StartTimePicker.ShowUpDown = true;
            EndTimePicker.ShowUpDown = true;

            #endregion

            _backgroundWorker = new BackgroundWorker();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += _backgroundWorker_DoWork; ;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
        }

        private void AddHoursBtn_Click(object sender, EventArgs e)
        {
            if (_backgroundWorker.IsBusy)
            {
                ExceptionHandler.ShowException("Warning", $"The program is still busy please wait!", boxIcon: MessageBoxIcon.Warning);
                return;
            }

            // Check if the given controls contain any input
            var (empty, controlName) = CheckIfEmpty(new List<Control>
            {
                StartTimePicker,
                EndTimePicker,
                DatePicker,
                DescriptionTextBox,
                PasswordTextbox,
                UsernameTextBox,
            });

            if (empty)
            {
                ExceptionHandler.ShowException("Empty element", $"Element with name {controlName} was empty.");
                return;
                // throw new Exception($"Element with name {controlName} was empty.");
            }

            // Check if start time is bigger than end time
            if (StartTimePicker.Value > EndTimePicker.Value)
            {
                ExceptionHandler.ShowException("Invalid input", $"The starting time cannot be bigger than the ending time.");
                return;
                // throw new Exception("The starting time cannot be bigger than the ending time.");
            }

            AddHoursBtn.Enabled = false;

            // Run background worker
            _backgroundWorker.RunWorkerAsync();
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _webHandler = new WebHandler();

            _webHandler.Start(new HourFillForm
            {
                Date = DatePicker.Value,
                Description = DescriptionTextBox.Text,
                StartTime = StartTimePicker.Value,
                EndTime = EndTimePicker.Value,
                Username = UsernameTextBox.Text,
                Password = PasswordTextbox.Text
            });
        }


        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                ExceptionHandler.ShowException("Error", "Please try running again!");
            }
            AddHoursBtn.Enabled = true;
        }

        /// <summary>
        /// Check if given controls do contain any input
        /// </summary>
        /// <param name="controls"></param>
        /// <returns></returns>
        Tuple<bool, string> CheckIfEmpty(IEnumerable<Control> controls)
        {
            foreach (var control in controls)
            {
                if (string.IsNullOrWhiteSpace(control.Text))
                {
                    return new Tuple<bool, string>(true, control.Name);
                }
            }

            return new Tuple<bool, string>(false, string.Empty);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _webHandler.Close();
        }
    }
}
