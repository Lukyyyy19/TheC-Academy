using phonebook.Controllers;
using phonebook.Models;
using Spectre.Console;
namespace phonebook;

public class PhoneMenu
{
    private PhoneBookController _phoneBookController;
    private UserInput _userInput;
    public PhoneMenu()
    {
        _phoneBookController = new PhoneBookController();
        _userInput = new UserInput();
    }
    public void ShowMenu()
    {
        Console.Clear();
        var contacts = _phoneBookController.GetAllContacts().contacts;
        var choice = AnsiConsole.Prompt(new SelectionPrompt<MenuChoices>()
            .Title("[green]PhoneBook Contacts:[/]")
            .AddChoices(new [] { 
                MenuChoices.ShowContacts,
                MenuChoices.AddContact ,
                MenuChoices.UpdateContacts,
                MenuChoices.DeleteContact,
                MenuChoices.Exit
            }));
        switch (choice)
        {
            case MenuChoices.ShowContacts:
                ShowContacts();
                break;
            case MenuChoices.AddContact:
                AddContact();
                break;
            case MenuChoices.Exit:
                Environment.Exit(0);
                break;
            case MenuChoices.DeleteContact:
                DeleteContact();
                break;
            case MenuChoices.UpdateContacts:
                UpdateContact();
                break;
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
        AnsiConsole.MarkupLine("[green]Press any key to go back...[/]");
        Console.ReadKey();
        Console.Clear();
        ShowMenu();
    }

    public void AddContact()
    {
        var contact = _userInput.CreateContact();
        _phoneBookController.AddContact(contact);
        AnsiConsole.MarkupLine($"Contact [green] {contact.FirstName} {contact.LastName}[/] added!... Press any key to continue");
        Console.ReadKey();
        Console.Clear();
        ShowMenu();
    }

    public void UpdateContact()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[green]Update Contact[/]");
        var id = _userInput.GetContactId();
        var contact = _phoneBookController.GetContactById(id);
        var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .AddChoices(contact.GetType().GetProperties().Where(p=>p.Name!="Id").Select(p => p.Name).ToArray())
            );
        var newValue = AnsiConsole.Ask<string>($"Enter new value for {choice}");
        var property = contact.GetType().GetProperty(choice);
        property.SetValue(contact, newValue);
        _phoneBookController.EditContact(contact);
        ShowMenu();
        
    }

    public void DeleteContact()
    {
        var id = _userInput.GetContactId();
        var contact = _phoneBookController.GetContactById(id);
        var confirmation = AnsiConsole.Confirm($"Are you sure you want to delete {contact.FirstName} {contact.LastName} contact?", false);
        if (confirmation)
        {
            _phoneBookController.DeleteContact(id);
            AnsiConsole.MarkupLine("[green]Contact deleted![/]");
            Console.ReadKey();
        }
        else 
            AnsiConsole.MarkupLine("[red]Contact not deleted![/]");
        ShowMenu();
    }
    
}

public enum MenuChoices
{
    AddContact,
    DeleteContact,
    ShowContacts,
    UpdateContacts,
    Exit
}