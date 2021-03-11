using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NukeeperConfigUI
{
    /// <summary>
    /// Wrapper class which has a Value and a list of possible Values to ease binding to e.g. comboboxes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConfigItem<T> :BindableBase
    {
        private T _value;
        private readonly Action<T> _onSelectedValue;
        /// <summary>
        /// These are valid values for the <see cref="Value"/> property.
        /// </summary>
        public IList<T> AvailableValues { get; }

        /// <summary>
        /// The current Value
        /// </summary>
        public T Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value, OnValueChanged); }
        }

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="availableValues"></param>
        /// <param name="onSelectedValue"></param>
        public ConfigItem(IList<T> availableValues, Action<T> onSelectedValue)
        {
            AvailableValues = availableValues;
            _onSelectedValue = onSelectedValue;
        }

        private void OnValueChanged()
        {
            _onSelectedValue?.Invoke(_value);
        }
    }
}
