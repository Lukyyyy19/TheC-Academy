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
        var contacts = _phoneBookController.GetAllContacts().contacts;
        var choice = AnsiConsole.Prompt(new SelectionPrompt<MenuChoices>()
            .Title("[green]PhoneBook Contacts:[/]")
            .AddChoices(new [] { MenuChoices.AddContact ,
                MenuChoices.DeleteContact,
                MenuChoices.ShowContacts,
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
        AnsiConsole.MarkupLine("[green]Press any key to continue...[/]");
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
    
}

public enum MenuChoices
{
    AddContact,
    DeleteContact,
    ShowContacts,
    Exit
}