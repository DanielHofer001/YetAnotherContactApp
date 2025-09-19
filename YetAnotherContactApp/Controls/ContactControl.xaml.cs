using System.Windows;
using System.Windows.Controls;
using YetAnotherContactApp.Models;

namespace YetAnotherContactApp.Controls
{
    /// <summary>
    /// Interaction logic for ContactControl.xaml
    /// </summary>
    public partial class ContactControl : UserControl
    {
        public Contact Contact
        {
            get { return (Contact)GetValue(ContactProperty); }
            set { SetValue(ContactProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Contact.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactProperty =
            DependencyProperty.Register("Contact", typeof(Contact), typeof(ContactControl), 
                new PropertyMetadata(new Contact { Name="Name",Phone="Phone Number"}, SetText));

        //Sets the text for the contact within the list item view
        private static void SetText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContactControl control = d as ContactControl;

            if(control != null)
            {
                control.nameTextBlock.Text = (e.NewValue as Contact).Name;
                control.phoneTextBlock.Text = (e.NewValue as Contact).Phone;
            } 
        }

        public ContactControl()
        {
            InitializeComponent();
        }
    }
}
