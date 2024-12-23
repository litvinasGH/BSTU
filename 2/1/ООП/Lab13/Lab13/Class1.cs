using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab4OOP
{
    [Serializable]
    public enum IndexE
    {
        first = 1000, second = 2000, third = 3000, fourth = 4000, fifth = 5000, sixth = 6000, seven = 7000
    }
    [Serializable]
    public struct AdressS
    {
        public int Num;
        public string Street;
        public IndexE Index;

        public AdressS(int num, string street, IndexE index)
        {
            Num = num;
            Street = street;
            Index = index;
        }
    }

    interface ICanBe
    {
        bool CanBePaper();
    }

    //[XmlInclude(typeof(Receipt))]
    //[XmlInclude(typeof(Invoice))]
    //[XmlInclude(typeof(Check))]
    [Serializable]
    public abstract class DocumentBase
    {
        public DateInfo Date { get; set; }
        public abstract bool CanBePaper();
        public virtual string GetDocumentType() => "Неизвестный документ";

        public DocumentBase(DateInfo dateinfo)
        {
            Date = dateinfo;
        }

        public DocumentBase() 
        {
            Date = new DateInfo();
        }

        public override string ToString()
        {
            return $"{GetDocumentType()} | Детали: {GetDetails()}";
        }

        public abstract string GetDetails();
    }
    [Serializable]
    public class Organization
    {
        public string Name { get; set; }
        public AdressS Address { get; set; }

        public Organization(string name, AdressS address) {
            try
            {
                if (address.Num < 1)
                    throw new BadDataException("Не верный номер дома", address.Num);
            }
            catch { Console.WriteLine("Ошибка"); throw; }
            Name = name;
            Address = address;
        }

        public Organization()
        {
            Name = string.Empty; 
            Address = new AdressS(); 
        }

        public override string ToString() => $"{Name}, {Address.Street} {Address.Num} {Address.Index}";
    }
    [Serializable]
    public partial class DateInfo
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public DateInfo(int day, int month, int year)
        {
            if (day < 0 || day > 31)
                throw new BadDateException("Не верно указан день", day);
            else if (month < 0 || month > 12)
                throw new BadDateException("Не верно указан месяц", month);
             
            Day = day;
            Month = month;
            Year = year;
        }

        public DateInfo()
        {
            Day = 1;
            Month = 1;
            Year = 2000;
        }

        public override string ToString() => Day.ToString("D2") + "." + Month.ToString("D2") + "." + Year.ToString("D4");
    }

    [Serializable]
    public sealed class Receipt : DocumentBase, ICanBe
    {
        public Organization Organization { get; set; }
        public double Amount { get; set; }

        bool ICanBe.CanBePaper() => true;
        public override bool CanBePaper() => false;

        public Receipt(Organization organization, DateInfo dateinfo ,double amount) : base(dateinfo) { 
            Organization = organization;
            Amount = amount;
        }

        public Receipt() : base() { Organization = new Organization(); Amount = 0; }

        public override string GetDetails()
        {
            return $"Организация: {Organization}, Дата:{Date}, Колличество: {Amount}";
        }

        public override string GetDocumentType() => "Квитанци";

    }

    [Serializable]
    public sealed class Invoice : DocumentBase, ICanBe
    {
        public Organization Sender { get; set; }
        public Organization Receiver { get; set; }


        public string ItemDescription { get; set; }

        public double Amount { get; set; }


        public Invoice(Organization sender, Organization receiver, DateInfo dateInfo, string item, double amount) : base(dateInfo)
        {
            Sender = sender;
            Receiver = receiver;
            ItemDescription = item;
            Amount = amount;
        }

        public Invoice() : base()
        {
            Sender = new Organization();
            Receiver = new Organization();
            ItemDescription = string.Empty;
            Amount = 0;
        }

        bool ICanBe.CanBePaper() => true;
        public override bool CanBePaper() => false;


        public override string GetDetails()
        {
            return $"Отправитель: {Sender} Получатель: {Receiver} Дата: {Date} Предмет: {ItemDescription}";
        }

        public override string GetDocumentType() => "Накладная";
    }

    [Serializable]
    public sealed class Check : DocumentBase, ICanBe
    {
        public Organization Issuer { get; set; }
        public double Amount { get; set; }

        [NonSerialized]
        public string someInfo = "Info";

        bool ICanBe.CanBePaper() => true;
        public override bool CanBePaper() => false;

        public Check(Organization issuer, DateInfo dateInfo, double amount) : base(dateInfo) 
        {
            Issuer = issuer;
            Amount = amount;
        }

        public Check() : base()
        {
            Issuer = new Organization();
            Amount = 0;
        }

        public override string GetDetails()
        {
            return $"Организация: {Issuer} Дата: {Date} Стоймость: {Amount}";
        }

        public override string GetDocumentType() => "Чек";
    }
    static class Printer
    {
        static public void IAmPrinting(DocumentBase document)
        {
            Console.WriteLine(document.ToString());
        }
    }








}
