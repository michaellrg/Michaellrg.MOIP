using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moip.Net.V2;
using Moip.Net.V2.Model;
using Moip.Net;
using Michaellrg.MOIP.Web.Models;
using Slack.Webhooks;

namespace Michaellrg.MOIP.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            var items = new ItemsView();

            List<Item> itemList = items.Load();

             var v2Client = new V2Client(
            new Uri("https://sandbox.moip.com.br/"),
            "01010101010101010101010101010101",
            "ABABABABABABABABABABABABABABABABABABABAB"
        );
           /*
                    try
                    {
                        var Customer = new Cliente()
                        {
                            OwnId = "SEU_ID_CLIENTE",
                            Fullname = "José Silva",
                            Email = "josesilva@acme.com.br",
                            BirthDate = DateTime.Now.Date.AddYears(-18).ToString("yyyy-MM-dd"),
                            TaxDocument = new Documento()
                            {
                                Type = DocumentType.CPF,
                                Number = "65374721054"
                            },
                            Phone = new Telefone()
                            {
                                CountryCode = 55,
                                AreaCode = 11,
                                Number = 999999999
                            },
                            ShippingAddress = new Endereco()
                            {
                                ZipCode = "01234000",
                                Street = "Avenida Faria Lima",
                                StreetNumber = "2927",
                                Complement = "SL 1",
                                City = "São Paulo",
                                District = "Itaim",
                                State = "SP",
                                Country = "BRA"
                            }
                        };

                        v2Client.CriarCliente(Customer);

                    }
                    catch (MoipException e) {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.ResponseError.Errors);
                    }*/
            try
            {

                //Integration with webhook(Slack) Test
                var slackClient = new SlackClient("https://hooks.slack.com/services/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");
                var slackMessage = new SlackMessage
                {
                    Channel = "#general",
                    Text = "Test",
                    IconEmoji = Emoji.Ghost,
                    Username = "nerdfury"
                };
                // slackClient.Post(slackMessage);
                return View(itemList);
            }
            catch (Exception e) {
                return View();
            }
        }

       
        [HttpGet]
        public ActionResult Details(int? id) {
            //Show details product
            var items = new ItemsView();
            List<Item> itemTest = items.Load();
            Item item = itemTest.SingleOrDefault(p => p.Id == id);
            return View(item);
        }
    }
}