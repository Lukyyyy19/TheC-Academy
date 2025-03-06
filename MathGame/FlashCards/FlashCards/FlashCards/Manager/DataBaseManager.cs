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
        using (SqlCommand command = new SqlCommand($"SELECT f. * FROM master.dbo.FlashCards f JOIN master.dbo.Stack s ON f.id_Flashcard_fk = s.id WHERE s.id = {stackId};",_connection))
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
            _connection.Close();
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

    public FlashCards GetCard(int cardId)
    {
        FlashCards flashCard = null;
        using (SqlCommand command = new SqlCommand($"SELECT * FROM master.dbo.FlashCards WHERE id = {cardId}",_connection))
        {
            _connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    flashCard = new FlashCards
                    {
                        id = reader.GetInt32(0),
                        question = reader.GetString(1),
                        answer = reader.GetString(2),
                        stackId = reader.GetInt32(3)
                    };
                }
            }
            _connection.Close();
        }

        return flashCard;
    }
    
    public void AddCard(FlashCards flashCard)
    {
        using (SqlCommand command = new SqlCommand($"INSERT INTO master.dbo.FlashCards (question, answer, id_Flashcard_fk) VALUES ('{flashCard.question}', '{flashCard.answer}', {flashCard.stackId})", _connection))
        {
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
    
    public void EditCard(FlashCards flashCard)
    {
        using (SqlCommand command = new SqlCommand($"UPDATE master.dbo.FlashCards SET question = '{flashCard.question}', answer = '{flashCard.answer}' WHERE id = {flashCard.id}", _connection))
        {
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
    
    public void DeleteCard(int cardId)
    {
        using (SqlCommand command = new SqlCommand($"DELETE FROM master.dbo.FlashCards WHERE id = {cardId}", _connection))
        {
            _connection.Open();
            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}