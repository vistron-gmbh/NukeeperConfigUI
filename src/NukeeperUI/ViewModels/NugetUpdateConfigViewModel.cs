using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NukeeperConfigUI.ViewModels
{
    /// <summary>
    /// Allows the user to set a nukeeper config through the UI.
    /// </summary>
    public class NugetUpdateConfigViewModel :BindableBase, IDialogAware
    {
        /// <summary>
        /// The prism dialog identifies this dialogs parameter using this key.
        /// </summary>
        public const string PARAMETER_KEY_CONFIG = "NukeeperConfig";

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        public ICommand CloseCommand { get; }

        /// <summary>
        /// Configures the change parameter.
        /// </summary>
        public ConfigItem<Change> ChangeItem { get; }
        /// <summary>
        /// Configures the prerelease parameter.
        /// </summary>
        public ConfigItem<Prerelease> PrereleaseItem { get; }
        /// <summary>
        /// Configures the source parameter.
        /// </summary>
        public ConfigItem<NugetSource> SourceItem { get; }

        public NukeeperConfig NukeeperConfig { get; set; } = new NukeeperConfig();

        /// <summary>
        /// Default ctor.
        /// </summary>
        public NugetUpdateConfigViewModel()
        {
            CloseCommand = new DelegateCommand<ButtonResult?>(CloseAction);

            Change[] changeValues = Enum.GetValues(typeof(Change)) as Change[];
            Prerelease[] preReleaseValues = Enum.GetValues(typeof(Prerelease)) as Prerelease[];
            IList<NugetSource> availableNugetSources = NukeeperHelper.GetNugetSourcesOfMachine();
            availableNugetSources.Insert(0, (new NugetSource() { Name = "Any", Target = null })); //We need to add the possibilty to revert back to no selection

            ChangeItem = new ConfigItem<Change>(changeValues, (c) => NukeeperConfig.Change = c);
            PrereleaseItem = new ConfigItem<Prerelease>(preReleaseValues, (c) => NukeeperConfig.UsePreRelease = c);
            SourceItem = new ConfigItem<NugetSource>(availableNugetSources, (c) => NukeeperConfig.Source = c.Target);
        }

        /// <summary>
        /// Closes the dialog and returns the build config.
        /// </summary>
        /// <param name="parameter"></param>
        private void CloseAction(ButtonResult? parameter)
        {
            ButtonResult button = parameter ?? ButtonResult.None;
            DialogParameters parameters = new DialogParameters
            {
                { PARAMETER_KEY_CONFIG, NukeeperConfig }
            };
            RequestClose.Invoke(new DialogResult(button, parameters));
        }

        #region IDialogAware
        /// <summary>
        /// <inheritdoc cref="IDialogAware"></inheritdoc>
        /// </summary>
        public string Title => Properties.Resources.NugetUpdateHeader;
        /// <summary>
        /// <inheritdoc cref="IDialogAware"></inheritdoc>
        /// </summary>
        public event Action<IDialogResult> RequestClose;
        /// <summary>
        /// <inheritdoc cref="IDialogAware"></inheritdoc>
        /// </summary>
        public bool CanCloseDialog()
        {
            return true;
        }
        /// <summary>
        /// <inheritdoc cref="IDialogAware"></inheritdoc>
        /// </summary>
        public void OnDialogClosed()
        {

        }
        /// <summary>
        /// <inheritdoc cref="IDialogAware"></inheritdoc>
        /// </summary>
        public void OnDialogOpened(IDialogParameters parameters)
        {

        }
        #endregion
    }
}
