using phonebook.Context;
using phonebook.Models;

namespace phonebook.Controllers;

public class PhoneBookController
{
    private PhoneContext _context;

    public PhoneBookController()
    {
        _context = new PhoneContext();
    }
    
    public Contacts GetAllContacts()
    {
        var contacts = _context.Contacts.ToList();
        return new Contacts{contacts = contacts};
    }
    
    public void AddContact(string name, string lastName, string email)
    {
        var contact = new Contact
        {
            FirstName = name,
            LastName = lastName,
            Email = email
        };
        
        _context.Contacts.Add(contact);
        _context.SaveChanges();
    }
    public void AddContact(Contact contact)
    {
        _context.Contacts.Add(contact);
        _context.SaveChanges();
    }

    public void EditContact(Contact contact)
    {
        _context.Contacts.Update(contact);
        _context.SaveChanges();
    }
    
    public void DeleteContact(int id)
    {
        var contact = _context.Contacts.Find(id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            _context.SaveChanges();
        }
    }
    
    public Contact GetContactById(int id)
    {
        return _context.Contacts.Find(id);
    }

}