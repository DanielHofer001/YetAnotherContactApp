# YetAnotherContactApp

## Structure
- ASP .Net Core Backend 
- WPF Desktop Frontend

## Techstack
- C# .Net 9

 (orignal code reused from https://github.com/AndyJL1999/ContactsApp)

<p> This app uses the MVVM pattern. The contacts, add contacts, and details view are all displayed in the main window. </p>

<p>This WPF app displays a list of contacts. You can add contacts, delete contacts, or modify the details of each contact. Currently the details of a contact are limited to name and phone number. 
Their is also a search bar so that users can find specific contacts more effectively.</p>


<p>The Contacts API is a REST web API. It has a single controller for the contacts. It uses the repository pattern for data access alongside a MSSQLLocalDB server as data storage.
It uses a code first approach when constructing the database. 
Using EntityFrameworkCore migrations can be created to update the database. 
DTOs(Data Transfer Objects) are in use for the  API calls. AutoMapper was used to map my DTOs to the contact entities.</p>

<h2> This project is not yet finished. There are still many improvements to be made and some quality of life behaviours that should be in the app now. </h2>