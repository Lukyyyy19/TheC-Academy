using Microsoft.Data.Sqlite;

string connectionString = @"Data Source=Habit_Logger.db";
using (var connection = new SqliteConnection(connectionString))
{
    connection.Open();
    var command = connection.CreateCommand();
    command.CommandText =
        @"CREATE TABLE IF NOT EXISTS drinking_water(
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Date TEXT,
            Quantity INTEGER
        )";
    command.ExecuteNonQuery();
    connection.Close();
}

bool wantToContinue = true;
while (wantToContinue)
{
    switch (GetUserInput())
    {
        case 1:
            LogDrinkingWater();
            break;
        case 2:
            DisplayDrinkingWaterLog();
            break;
        case 3:
            DeleteDrinkingWaterLog();
            break;
        case 4:
            UpdateDrinkginWaterLog();
            break;
        case 0:
            wantToContinue = false;
            return;
    }
}

int GetUserInput()
{
    Console.Clear();
    Console.WriteLine("Main Menu");
    Console.WriteLine("-----------------------");
    Console.WriteLine("1. Log Drinking Water");
    Console.WriteLine("2. View Drinking Water Log");
    Console.WriteLine("3. Delete Drinking Water Log");
    Console.WriteLine("4. Update Drinking Water Log");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your choice: ");
    string choice = Console.ReadLine();
    return int.Parse(choice);
}

void LogDrinkingWater()
{
    Console.Clear();
    Console.WriteLine("Log Drinking Water");
    Console.WriteLine("-----------------------");
    Console.Write("Enter the quantity of glasses of water you drank: ");
    string quantity = Console.ReadLine();
    Console.WriteLine("Enter the date (dd-MM-yyyy) or press Enter to use today's date: ");
    var date = Console.ReadLine();
    //string date = DateTime.Now.ToString("yyyy-MM-dd");
    if (string.IsNullOrEmpty(date))
    {
        date = DateTime.Now.ToString("dd-MM-yyyy");
    }

    Console.WriteLine("Water logged successfully!");
    using (var connection = new SqliteConnection(connectionString))
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"INSERT INTO drinking_water(Date, Quantity) VALUES('{date}', {quantity})";
        command.ExecuteNonQuery();
        connection.Close();
    }
}

void DisplayDrinkingWaterLog()
{   
    Console.Clear();
    Console.WriteLine("Drinking Water Log");
    Console.WriteLine("-----------------------");
    using (var connection = new SqliteConnection(connectionString))
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM drinking_water";
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                Console.WriteLine($"Date: {reader["Date"]}, Quantity: {reader["Quantity"]}");
            }
        }
        connection.Close();
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

void UpdateDrinkginWaterLog()
{
    Console.Clear();
    Console.WriteLine("Update Drinking Water Log");
    Console.WriteLine("-----------------------");
    Console.Write("Enter the date (dd-MM-yyyy) to update the log: ");
    string date = Console.ReadLine();
    Console.Write("Enter the new quantity: ");
    string quantity = Console.ReadLine();
    using (var connection = new SqliteConnection(connectionString))
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"UPDATE drinking_water SET Quantity = {quantity} WHERE Date = '{date}'";
        command.ExecuteNonQuery();
        connection.Close();
    }
    Console.WriteLine("Log updated successfully!");
}

void DeleteDrinkingWaterLog()
{
    Console.Clear();
    Console.WriteLine("Delete Drinking Water Log");
    Console.WriteLine("-----------------------");
    Console.WriteLine("0. Delete all logs");
    Console.WriteLine("1. Delete by date");
    Console.WriteLine("2. Return to main menu");
    Console.Write("Enter your choice: ");
    string choice = Console.ReadLine();
    switch (choice)
    {
        case "0":
            DeleteAllLogs();
            break;
        case "1":
            DeleteByDate();
            break;
        case "2":
            return;
    }
}

void DeleteAllLogs()
{
    Console.WriteLine("Are you sure you want to delete all logs? (Y/N)");
    string choice = Console.ReadLine();
    if (choice.ToUpper() == "Y")
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM drinking_water";
            command.ExecuteNonQuery();
            connection.Close();
        }
        Console.WriteLine("All logs deleted successfully!");
    }
}

void DeleteByDate()
{
    Console.Write("Enter the date (dd-MM-yyyy) to delete the log: ");
    string date = Console.ReadLine();
    using (var connection = new SqliteConnection(connectionString))
    {
        connection.Open();
        var command = connection.CreateCommand();
        command.CommandText = $"DELETE FROM drinking_water WHERE Date = '{date}'";
        command.ExecuteNonQuery();
        connection.Close();
    }
    Console.WriteLine("Log deleted successfully!");
}