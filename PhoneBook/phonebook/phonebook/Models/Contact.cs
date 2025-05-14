using System.ComponentModel.DataAnnotations;

namespace phonebook.Models;

public class Contact
{
    public int Id { get; set; }
    [StringLength(200)]
    public string FirstName { get; set; }
    [StringLength(200)]
    public string LastName { get; set; }
    [StringLength(200)]
    public string Email { get; set; }
}

public class Contacts
{
    public List<Contact> contacts = new List<Contact>();
}