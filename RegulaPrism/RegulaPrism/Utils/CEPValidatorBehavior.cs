using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RegulaPrism.Utils
{
    public class CEPValidatorBehavior: Behavior<Entry>
    {
        const string cepRegex = "^\\d{5}-\\d{3}$";

        static readonly BindablePropertyKey IsValidPropertyKey = BindableProperty.CreateReadOnly("IsValid", typeof(bool), typeof(double), false);

        public static readonly BindableProperty IsValidProperty = IsValidPropertyKey.BindableProperty;

        public bool IsValid
        {
            get { return (bool)base.GetValue(IsValidProperty); }
            private set { base.SetValue(IsValidPropertyKey, value); }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            var ev = e as TextChangedEventArgs;
            string text = Regex.Replace(ev.NewTextValue, @"[^0-9]", "");

            text = text.PadRight(8);

            // removendo todos os digitos excedentes 
            if (text.Length > 8)
            {
                text = text.Remove(8);
            }

            text = text.Insert(5, "-").TrimEnd(new char[] { ' ', '-' });
            if (entry.Text != text)
                entry.Text = text;

            IsValid = (Regex.IsMatch(e.NewTextValue, cepRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;

        }
    }
}
