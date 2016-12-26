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
using System.Threading.Tasks;

namespace Michaellrg.MOIP.Web.Controllers
{
    public class HomeController : AsyncController
    {
        public ActionResult Index()
        {
            try
            {
                
                var items = new ItemsView();

                List<Item> itemList = items.Load();



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
            catch (Exception e)
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            //Show details product
            var items = new ItemsView();
            List<Item> itemTest = items.Load();
            Item item = itemTest.SingleOrDefault(p => p.Id == id);
            return View(item);
        }
    }
}