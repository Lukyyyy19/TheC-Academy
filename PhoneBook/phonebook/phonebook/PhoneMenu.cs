using phonebook.Controllers;
using phonebook.Models;
using Spectre.Console;
namespace phonebook;

public class PhoneMenu
{
    private PhoneBookController _phoneBookController;
    public PhoneMenu()
    {
        _phoneBookController = new PhoneBookController();
    }
    public void ShowMenu()
    {
        var contacts = _phoneBookController.GetAllContacts().contacts;
        var choice = AnsiConsole.Prompt(new SelectionPrompt<MenuChoices>()
            .Title("[green]PhoneBook Contacts:[/]")
            .AddChoices(new [] { MenuChoices.AddContact ,
                MenuChoices.DeleteContact,
                MenuChoices.ShowContacts,
                MenuChoices.Exit
            }));
        if (choice == MenuChoices.ShowContacts)
        {
            ShowContacts();
        }
    }
    
    public void ShowContacts()
    {
        var contacts = _phoneBookController.GetAllContacts().contacts;
        var contactTable = new Table()
            .AddColumn("Id")
            .AddColumn("First Name")
            .AddColumn("Last Name")
            .AddColumn("Email");
        foreach (var contact in contacts)
        {
            contactTable.AddRow(contact.Id.ToString(), contact.FirstName, contact.LastName, contact.Email);
        }

        contactTable.ShowRowSeparators();
        AnsiConsole.Write(contactTable);

    }
    
}

public enum MenuChoices
{
    AddContact,
    DeleteContact,
    ShowContacts,
    Exit
}