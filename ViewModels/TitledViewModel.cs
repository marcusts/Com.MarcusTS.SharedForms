namespace Com.MarcusTS.SharedForms.ViewModels
{
    using Common.Interfaces;

    public interface ITitledViewModel : IPropertyChangedBase
    {
        string Title { get; set; }
    }

    public class TitledViewModel : PropertyChangedBase, ITitledViewModel
    {
        private string _title;

        public string Title
        {
            get => _title;
            set => SetProperty( ref _title, value );
        }
    }
}
