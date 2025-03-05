using Microsoft.Data.SqlClient;
using Models;

namespace Manager;

public class DataBaseManager
{
    private readonly string _connectionString;
    private SqlConnection _connection;
    public SqlConnection Connection => _connection;

    public DataBaseManager(string connectionString)
    {
        _connectionString = connectionString;
        _connection = new SqlConnection(_connectionString);
    }

    public List<FlashCards> GetCards(int stackId)
    {
        List<FlashCards> flashCards = new List<FlashCards>();
        using (SqlCommand command = new SqlCommand($"SELECT f. * FROM FlashCards f JOIN Stack s ON f.id_Flashcard_fk = s.id WHERE s.id = {stackId}"))
        {
            _connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var flashCard = new FlashCards()
                    {
                        id = reader.GetInt32(0),
                        question = reader.GetString(1),
                        answer = reader.GetString(2),
                        stackId = reader.GetInt32(3)
                    };
                    flashCards.Add(flashCard);
                }
            }
        }

        return flashCards;
    }

    public List<Stack> GetStacks()
    {
        List<Stack> stacks = new List<Stack>();
        using (SqlCommand command = new SqlCommand("SELECT * FROM Stack", _connection))
        {
            _connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Stack stack = new Stack
                    {
                        id = reader.GetInt32(0),
                        name = reader.GetString(1)
                    };
                    // stack.flashCards = GetFlashCards(stack.id);
                    stacks.Add(stack);
                }
            }

            _connection.Close();
        }

        return stacks;
    }
}