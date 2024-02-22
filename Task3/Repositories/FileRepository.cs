using Newtonsoft.Json;
using Task3.Models;

public class FileRepository
{
    private const string FilePath = "data.txt";

    public IEnumerable<Client> GetAllItems()
    {
        LoadDataFromFile();
        return items;
    }

    public Client GetItemById(int id)
    {
        LoadDataFromFile();
        return items.FirstOrDefault(item => item.Id == id);
    }

    public Client GetItemByCN(string cardNum)
    {
        LoadDataFromFile();
        return items.FirstOrDefault(item => item.CardNumber == cardNum);
    }

    public void AddItem(Client item)
    {
        LoadDataFromFile();
        item.Id = GenerateNextId();
        items.Add(item);
        SaveDataToFile();
    }

    public void UpdateItem(Client updatedItem)
    {
        LoadDataFromFile();
        var existingItem = items.FirstOrDefault(item => item.Id == updatedItem.Id);
        if (existingItem != null)
        {
            existingItem.Name = updatedItem.Name;
            existingItem.Surname = updatedItem.Surname;
            existingItem.DateOfBirth = updatedItem.DateOfBirth;
            existingItem.PhoneNumber = updatedItem.PhoneNumber;
            existingItem.age = updatedItem.age;
            existingItem.job = updatedItem.job;
            existingItem.married = updatedItem.married;
            existingItem.education = updatedItem.education;
            existingItem.def = updatedItem.def;
            existingItem.balance = updatedItem.balance;
            existingItem.housing = updatedItem.housing;
            existingItem.loan = updatedItem.loan;
            //existingItem.contact = updatedItem.contact;
            //existingItem.day = updatedItem.day;
            //existingItem.month = updatedItem.month;
            //existingItem.campaign = updatedItem.campaign;
            //existingItem.pdays = updatedItem.pdays;
            //existingItem.previous = updatedItem.previous;
            //existingItem.poutcome = updatedItem.poutcome;
            //existingItem.wasCalled = updatedItem.wasCalled;
            //existingItem.isParticipating = updatedItem.isParticipating;
            //
            SaveDataToFile();
        }
    }

    public void DeleteItem(int id)
    {
        LoadDataFromFile();
        items.RemoveAll(item => item.Id == id);
        SaveDataToFile();
    }

    private List<Client> items = new List<Client>();
    private int nextId;

    private void LoadDataFromFile()
    {
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            items = JsonConvert.DeserializeObject<List<Client>>(json);
            int maxId = items.Max(client => client.Id);
            nextId = maxId + 1;
        }
        else
        {
            nextId = 1;
        }
    }

    private void SaveDataToFile()
    {
        var json = JsonConvert.SerializeObject(items, Formatting.Indented);
        File.WriteAllText(FilePath, json);
    }

    private int GenerateNextId()
    {
        return nextId++;
    }
}
