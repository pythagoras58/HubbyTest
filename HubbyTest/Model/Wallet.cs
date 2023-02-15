namespace HubbyTest.Model;

public class Wallet
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string AccountNumber { get; set; }
    public string AccountScheme { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Owner { get; set; }
}