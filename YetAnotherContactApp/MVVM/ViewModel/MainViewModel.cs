using YetAnotherContactApp.Resources;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Input;
using YetAnotherContactApp;
using YetAnotherContactApp.Models;
using YetAnotherContactApp.MVVM.ViewModel;

namespace YetAnotherContactApp.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        // Initialize non-nullable fields to default values to fix CS8618

        private IEnumerable<Contact> _contacts = [];
        private IEnumerable<Contact> _allContacts = [];
        private Contact _selectedContact = null!;
        private string _searchBoxText = string.Empty;
        private ICommand _addContactCommand = null!;
        private object? _viewModel;

        public event EventHandler? SelectedContactChanged;
        public event EventHandler? SearchBoxTextChanged;

        public MainViewModel()
        {
            SelectedContactChanged += SelectionChanged;
            SearchBoxTextChanged += TextChanged;

            ViewModel = this;

            LoadAllContacts();
        }

        public object? ViewModel 
        { 
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                OnPropertyChanged(nameof(ViewModel));
            } 
        }

        public string SearchBoxText 
        {
            get { return _searchBoxText; }
            set
            {
                _searchBoxText = value;
                OnPropertyChanged(nameof(SearchBoxText));
                SearchBoxTextChanged?.Invoke(this, new EventArgs());
            }
        }

        public Contact SelectedContact 
        { 
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                OnPropertyChanged(nameof(SelectedContact));
                SelectedContactChanged?.Invoke(this, new EventArgs());
            }
        }

        public IEnumerable<Contact> Contacts 
        { 
            get { return _contacts; }
            set
            {
                _contacts = value;
                OnPropertyChanged(nameof(Contacts));
            }
        }

        public ICommand AddContactCommand 
        {
            get
            {
                if(_addContactCommand is null)
                {
                    _addContactCommand = new RelayCommand(p => AddContact(), p => true);
                }

                return _addContactCommand;
            }
        }

        //Opens a window for adding contacts
        private void AddContact()
        {
            ViewModel = new ContactsViewModel();
        }

        private async void LoadAllContacts()
        {
            if (ContactApi.ApiClient == null)
            {
                throw new InvalidOperationException("ContactApi.ApiClient is not initialized.");
            }

            using (HttpResponseMessage response = await ContactApi.ApiClient.GetAsync(ContactApi.ApiClient.BaseAddress + "api/Contacts"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var contacts = await response.Content.ReadFromJsonAsync<IEnumerable<Contact>>();
                    Contacts = contacts ?? [];
                    Contacts = Contacts.OrderBy(c => c.Name);
                    _allContacts = Contacts;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        //Whenever the inside of the search box changes => filter the list view
        private void TextChanged(object? sender, EventArgs e)
        {
            var filteredList = _allContacts.Where(c => c.Name.ToLower().Contains(SearchBoxText.ToLower())).ToList();

            Contacts = filteredList;
        }

        //Open the details window when a contact is selected
        private void SelectionChanged(object? sender, EventArgs e)
        {
            if(SelectedContact != null)
            {
                ViewModel = new DetailsViewModel(SelectedContact);
            }
        }

    }
}
