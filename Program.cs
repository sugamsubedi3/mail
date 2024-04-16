using System;
using System.Collections.Generic;

public abstract class Mail
{
    protected string destinationAddress;

    public Mail(string destinationAddress)
    {
        this.destinationAddress = destinationAddress;
    }

    public abstract double Stamp();
    public abstract bool IsValid();
    public abstract void Display();
}

public class Letter : Mail
{
    private double weight;
    private bool express;
    private string format;

    public Letter(string destinationAddress, double weight, bool express, string format) : base(destinationAddress)
    {
        this.weight = weight;
        this.express = express;
        this.format = format;
    }

    public override double Stamp()
    {
        double baseFare = format == "A4" ? 2.50 : 3.50;
        double amount = express ? (baseFare + weight) * 2 : baseFare + weight;
        return amount;
    }

    public override bool IsValid()
    {
        return !string.IsNullOrEmpty(destinationAddress);
    }

    public override void Display()
    {
        Console.WriteLine("Letter");
        Console.WriteLine($"Weight: {weight} grams");
        Console.WriteLine($"Express: {(express ? "yes" : "no")}");
        Console.WriteLine($"Destination: {(IsValid() ? destinationAddress : "Invalid destination")}");
        Console.WriteLine($"Price: ${Stamp()}");
        Console.WriteLine($"Format: {format}");
    }
}

public class Parcel : Mail
{
    private double weight;
    private bool express;
    private double volume;

    public Parcel(string destinationAddress, double weight, bool express, double volume) : base(destinationAddress)
    {
        this.weight = weight;
        this.express = express;
        this.volume = volume;
    }

    public override double Stamp()
    {
        double amount = express ? (weight + volume * 0.25) * 2 : weight + volume * 0.25;
        return amount;
    }

    public override bool IsValid()
    {
        return !string.IsNullOrEmpty(destinationAddress) && volume <= 50;
    }

    public override void Display()
    {
        Console.WriteLine("Parcel");
        Console.WriteLine($"Weight: {weight} grams");
        Console.WriteLine($"Express: {(express ? "yes" : "no")}");
        Console.WriteLine($"Destination: {(IsValid() ? destinationAddress : "Invalid destination")}");
        Console.WriteLine($"Price: ${Stamp()}");
        Console.WriteLine($"Volume: {volume} liters");
    }
}

public class Advertisement : Mail
{
    private double weight;
    private bool express;

    public Advertisement(string destinationAddress, double weight, bool express) : base(destinationAddress)
    {
        this.weight = weight;
        this.express = express;
    }

    public override double Stamp()
    {
        double amount = express ? weight * 5 * 2 : weight * 5;
        return amount;
    }

    public override bool IsValid()
    {
        return !string.IsNullOrEmpty(destinationAddress);
    }

    public override void Display()
    {
        Console.WriteLine("Advertisement");
        Console.WriteLine($"Weight: {weight} grams");
        Console.WriteLine($"Express: {(express ? "yes" : "no")}");
        Console.WriteLine($"Destination: {(IsValid() ? destinationAddress : "Invalid destination")}");
        Console.WriteLine($"Price: ${Stamp()}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Mail> mailbox = new List<Mail>();

        mailbox.Add(new Letter("Chemin des Acacias 28, 1009 Pully", 200, true, "A3"));
        mailbox.Add(new Letter("", 800, false, "A4"));
        mailbox.Add(new Advertisement("Les Moilles 13A, 1913 Saillon", 1500, true));
        mailbox.Add(new Advertisement("", 3000, false));
        mailbox.Add(new Parcel("Grand rue 18, 1950 Sion", 5000, true, 30));
        mailbox.Add(new Parcel("Chemin des fleurs 48, 2800 Delemont", 3000, true, 70));

        double totalPostage = 0;
        int invalidMails = 0;

        foreach (var mail in mailbox)
        {
            double postage = mail.Stamp();
            totalPostage += postage;

            if (!mail.IsValid())
            {
                invalidMails++;
            }

            mail.Display();
            Console.WriteLine();
        }

        Console.WriteLine($"The total amount of postage is {totalPostage}");
        Console.WriteLine($"The box contains {invalidMails} invalid mails.");
    }
}
