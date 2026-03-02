using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Lab4OOP
{
    internal class MainClass
    {
        static void Main()
        {
            try
            {
                //AdressS adressE = new AdressS();
                //adressE.Street = "Street";
                //adressE.Num = -123;
                //adressE.Index = IndexE.fifth;
                //Organization orgE = new Organization("Company A", adressE);
                //DateInfo dateE = new DateInfo(-1, 2, 2000);
                //DateInfo dateE2 = new DateInfo(1, 100, 2000);
                //DocumentContainer containerE = new DocumentContainer();
                //containerE.Get(5);
                //int EN = 0;
                //EN /= EN;
                Debug.Assert(false, "Test");



                AdressS adress = new AdressS();
                adress.Street = "Street";
                adress.Num = 123;
                adress.Index = IndexE.fifth;
                Organization org1 = new Organization("Company A", adress);
                AdressS adress2 = new AdressS();
                adress2.Street = "Avenue";
                adress2.Num = 456;
                adress2.Index = IndexE.third;
                Organization org2 = new Organization("Company B", adress2);
                DateInfo date = new DateInfo(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);

                Receipt receipt = new Receipt(org1, date, 150.50);
                Invoice invoice = new Invoice(org1, org2, date, "Чай", 20);
                Check check = new Check(org2, date, 1000);

                DocumentBase[] documents = { receipt, invoice, check };

                foreach (var doc in documents)
                {
                    Printer.IAmPrinting(doc);
                }

                Console.WriteLine(((ICanBe)receipt).CanBePaper());
                Console.WriteLine(receipt.CanBePaper());


                foreach (var doc in documents)
                {
                    if (doc is Receipt)
                        Console.WriteLine("Квитанция");
                    else if (doc is Invoice)
                        Console.WriteLine("Накладная");
                    else if (doc is Check)
                        Console.WriteLine("Чек");
                    Check? item = doc as Check;
                }

                DocumentContainer container = new DocumentContainer();
                container.Add(documents);
                container.Add(new Invoice(org1, org2, new DateInfo(5, 2, 2000), "Чай", 10));
                container.Add(new Invoice(org1, org2, date, "Чай", 30));
                container.Add(new Invoice(org1, org2, date, "Не Чай", 30));
                container.Add(new Check(org2, date, 1000));
                container.Add(new Check(org2, new DateInfo(1, 2, 2000), 1000));
                container.Add(new Check(org2, new DateInfo(10, 2, 2000), 1000));

                Console.WriteLine(DocumentController.GetTotalAmount(container, "Чай"));
                Console.WriteLine(DocumentController.GetCheckCount(container));
                List<DocumentBase> documentsList = DocumentController.GetDocumentsByPeriod(container, new DateInfo(1, 2, 2000),
                    new DateInfo(28, 2, 2000));
                DocumentController.PrintDocuments(documentsList);



                string filePath = "documents.xml";
                FileHandler.SaveToXmlFile(filePath, container);
                container.Add(new Check(org2, new DateInfo(1, 2, 2000), 4000));
                container.Add(new Check(org2, new DateInfo(10, 2, 2000), 7000));
                Console.WriteLine("----------------------");
                container.PrintAll();
                FileHandler.LoadFromXmlFile(filePath, container);
                Console.WriteLine("----------------------");
                container.PrintAll();
                
            }
            catch (IndexOutOfRangeContainerException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            catch (BadDateException ex)
            {
                Console.WriteLine("Дату пиши норм\n" + ex.ToString());
                Console.WriteLine("Значение: " + ex.Value);
            }
            catch (BadDataException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Значение: " + ex.Value);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Нельзя делить на 0");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally { Console.WriteLine("Наконец-то"); }


        }
    }
}
