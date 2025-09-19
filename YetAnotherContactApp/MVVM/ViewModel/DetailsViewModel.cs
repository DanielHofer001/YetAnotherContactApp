using YetAnotherContactApp.Resources;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Input;
using YetAnotherContactApp;
using YetAnotherContactApp.Models;

namespace YetAnotherContactApp.MVVM.ViewModel
{
    public class DetailsViewModel : ObservableObject
    {
        private readonly Contact _contact;
        private string _nameText;
        // Change all private fields that are initialized in the constructor or via properties to nullable types
        private string _phoneText;
        private ICommand? _backCommand;
        private ICommand? _updateCommand;
        private ICommand? _deleteCommand;
        public DetailsViewModel(Contact contact)
        {
            //Set the information for all of the text boxes and setting the contact to the selected contact
            _contact = contact;
            _nameText = _contact.Name;
            _phoneText = _contact.Phone;
        }

        public string NameText
        {
            get { return _nameText; }
            set
            {
                _nameText = value;
                OnPropertyChanged(nameof(NameText));
            }
        }
     
        public string PhoneText
        {
            get { return _phoneText; }
            set
            {
                _phoneText = value;
                OnPropertyChanged(nameof(PhoneText));
            }
        }
      
        public ICommand BackCommand 
        { 
            get 
            { 
                if(_backCommand is null)
                {
                    _backCommand = new RelayCommand(p => Exit((ObservableObject)p), p => p is ObservableObject);
                }

                return _backCommand; 
            }
        }
        public ICommand UpdateCommand
        {
            get
            {
                if (_updateCommand is null)
                {
                    _updateCommand = new RelayCommand(p => UpdateContact(), p => true);
                }

                return _updateCommand;
            }
        }
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand is null)
                {
                    _deleteCommand = new RelayCommand(p => DeleteContact((ObservableObject)p), p => p is ObservableObject);
                }

                return _deleteCommand;
            }
        }
 
        private void Exit(ObservableObject p)
        {
            p = new MainViewModel();
            App.Current.MainWindow.DataContext = p;
        }

        private async void UpdateContact()
        {
            //Set the new information into the Contact object
            _contact.Name = NameText;
            _contact.Phone = PhoneText;

            // Ensure ApiClient is not null before using it
            if (ContactApi.ApiClient is null)
                throw new InvalidOperationException("ContactApi.ApiClient is not initialized.");

            //Make a Api call to update the contact using ContactApi
            await ContactApi.ApiClient.PutAsJsonAsync(
                ContactApi.ApiClient.BaseAddress?.ToString() + $"api/Contacts/Update/{_contact.Id}",
                _contact
            );
        }

        private async void DeleteContact(ObservableObject p)
        {
            if (ContactApi.ApiClient is null)
                throw new InvalidOperationException("ContactApi.ApiClient is not initialized.");

            using (HttpResponseMessage response = await ContactApi.ApiClient.DeleteAsync(ContactApi.ApiClient.BaseAddress + $"api/Contacts/Delete/{_contact.Id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    Exit(p);
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
