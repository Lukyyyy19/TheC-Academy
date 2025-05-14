using phonebook.Models;
using Spectre.Console;
namespace phonebook;

public class UserInput
{
    public Contact CreateContact()
    {
        var firstName = AnsiConsole.Ask<string>("New contact [green]First Name[/]?");
        AnsiConsole.Clear();
        var lastName = AnsiConsole.Ask<string>("New contact [green]Last Name[/]?");
        AnsiConsole.Clear();
        var email = AnsiConsole.Prompt(new TextPrompt<string>("New contact [green]Email[/]?").Validate(email => email.Contains("@") && email.Contains(".com")));
        var contact = new Contact
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };
        return contact;
    }
}