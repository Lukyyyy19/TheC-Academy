using Manager;
using Microsoft.EntityFrameworkCore;
using Models;
using Spectre.Console;
using Microsoft.Extensions.Configuration;

List<MenuOptions> menuOptions = new List<MenuOptions>()
{
    new MenuOptions() { id = 1, option = "Manage Stacks" },
    new MenuOptions() { id = 2, option = "Manage FlashCards" },
    new MenuOptions() { id = 3, option = "Study" },
    /// new MenuOptions() { id = 4, option = "View all stacks" },
    new MenuOptions() { id = 0, option = "Exit" }
};

var dbManager = new DataBaseManager("Server=(localdb)\\MSSQLLocalDB");
bool running = true;

var confirmation = AnsiConsole.Prompt(
    new SelectionPrompt<MenuOptions>()
        .Title("Select an option")
        .AddChoices(menuOptions)
        .UseConverter(x => x.option));
while (running)
{

    switch (confirmation.id)
    {
        case 0:
            running = false;
            break;
        case 1:
            StackSection();
            break;
    }
}

void StackSection()
{
    var stacks = dbManager.GetStacks();
    var stackSelected = AnsiConsole.Prompt(new SelectionPrompt<Stack>()
        .Title("Select a stack")
        .AddChoices(stacks)
        .UseConverter(x => x.name));
}