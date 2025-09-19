using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Input;
using YetAnotherContactApp.Models;
using YetAnotherContactApp.MVVM.ViewModel;
using YetAnotherContactApp.Resources;

namespace YetAnotherContactApp.MVVM.ViewModel
{
    public class ContactsViewModel : ObservableObject
    {
        private string?
                _nameText;
        private string? _phoneText;
        private string? _successText;
        private ICommand? _saveCommand;
        private ICommand? _cancelCommand;


        public string NameText
        {
            get => _nameText ?? string.Empty;
            set
            {
                _nameText = value;
                OnPropertyChanged(nameof(NameText));
            }
        }

        public string PhoneText
        {
            get => _phoneText ?? string.Empty;
            set
            {
                _phoneText = value;
                OnPropertyChanged(nameof(PhoneText));
            }
        }
        public string SuccessText
        {
            get => _successText ?? string.Empty;
            set
            {
                _successText = value;
                OnPropertyChanged(nameof(SuccessText));
            }
        }

        public ICommand SaveCommand 
        {
            get
            {
                if(_saveCommand is null)
                {
                    _saveCommand = new RelayCommand(p => SaveContact((ObservableObject)p), p => CheckContact() && p is ObservableObject);
                }

                return _saveCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand is null)
                {
                    _cancelCommand = new RelayCommand(p => Exit((ObservableObject)p), p => p is ObservableObject);
                }

                return _cancelCommand;
            }
        }

        private async void SaveContact(ObservableObject p)
        {
            // Ensure ApiClient is not null before using it
            if (ContactApi.ApiClient == null)
            {
                SuccessText = "API client is not initialized.";
                return;
            }

            Contact contact = new()
            {
                Name = NameText,
                Phone = PhoneText,
            };

            using (HttpResponseMessage response = await ContactApi.ApiClient.PostAsJsonAsync((ContactApi.ApiClient.BaseAddress + "api/Contacts"), contact))
            {
                if (response.IsSuccessStatusCode)
                {
                    SuccessText = response.Content.ReadAsStringAsync().Result;
                }
            }
            Exit(p);
        }

        private void Exit(ObservableObject p)
        {
            p = new MainViewModel();
            System.Windows.Application.Current.MainWindow.DataContext = p;
        }

        private bool CheckContact()
        {
            if(string.IsNullOrEmpty(NameText)  || string.IsNullOrEmpty(PhoneText))
            {
                return false;
            }

            return true;
        }

    }
}
