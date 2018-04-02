using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dojodachi
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("fullness") == null)
            {
                HttpContext.Session.SetInt32("fullness", 20);
            }

            if(HttpContext.Session.GetInt32("happiness") == null)
            {
                HttpContext.Session.SetInt32("happiness", 20);
            }

            if(HttpContext.Session.GetInt32("meals") == null)
            {
                HttpContext.Session.SetInt32("meals", 3);
            }

            if(HttpContext.Session.GetInt32("energy") == null)
            {
                HttpContext.Session.SetInt32("energy", 50);
            }

            ViewBag.fullness = HttpContext.Session.GetInt32("fullness");
            ViewBag.happiness = HttpContext.Session.GetInt32("happiness");
            ViewBag.meals = HttpContext.Session.GetInt32("meals");
            ViewBag.energy = HttpContext.Session.GetInt32("energy");
            ViewBag.message = HttpContext.Session.GetString("message");

            if(ViewBag.fullness > 100 && ViewBag.happiness > 100 && ViewBag.Energy > 100)
            {
                ViewBag.Message = "You win!";
            }
            else if(ViewBag.fullness <= 0 || ViewBag.happiness <= 0)
            {
                ViewBag.Message = "Your Dojodachi passed away...";
            }
            return View();
        }

        [HttpGet]
        [Route("feed")]
        public IActionResult Feed()
        {
            System.Random rand = new System.Random();

            if(HttpContext.Session.GetInt32("meals") > 0)
            {
                HttpContext.Session.SetInt32("meals", (int)HttpContext.Session.GetInt32("meals") - 1);

                if(rand.Next(101) <= 75)
                {
                    int randFullness = rand.Next(5, 11);
                    HttpContext.Session.SetInt32("fullness", (int)HttpContext.Session.GetInt32("fullness") + randFullness);

                    HttpContext.Session.SetString("message", "You fed your Dojodachi. Meals -1, Fullness +" + randFullness);
                }
                else
                {
                    HttpContext.Session.SetString("message", "Your Dojodachi doesn't like that. Meals -1");
                }
                
            }
            else {
                HttpContext.Session.SetString("message", "You cannot feed your Dojodachi. Not enough meals.");
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("play")]
        public IActionResult Play()
        {

            System.Random rand = new System.Random();

            if(HttpContext.Session.GetInt32("energy") >= 5)
            {
                HttpContext.Session.SetInt32("energy", (int)HttpContext.Session.GetInt32("energy") - 5);

                if(rand.Next(101) <= 75)
                {   
                    int randHappiness = rand.Next(5, 11);
                    HttpContext.Session.SetInt32("happiness", (int)HttpContext.Session.GetInt32("happiness") + randHappiness);
                    HttpContext.Session.SetString("message", "You played with your Dojodachi. Happiness +" + randHappiness + ", Energy -5");
                }
                else
                {
                    HttpContext.Session.SetString("message", "Your Dojodachi doesn't like that. Energy -5");
                } 
            }
            else 
            {
                HttpContext.Session.SetString("message", "Your Dojodachi doesn't have enough energy to play.");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("work")]
        public IActionResult Work()
        {
            System.Random rand = new System.Random();

            if(HttpContext.Session.GetInt32("energy") >= 5)
            {
                HttpContext.Session.SetInt32("energy", (int)HttpContext.Session.GetInt32("energy") - 5);

                int randMeals = rand.Next(1,4);
                HttpContext.Session.SetInt32("meals", (int)HttpContext.Session.GetInt32("meals") + randMeals);

                HttpContext.Session.SetString("message", "Your Dojodachi worked. Meals +" + randMeals + ", Energy -5");
            }
            else
            {
                HttpContext.Session.SetString("message", "Your Dojodachi doesn't have enough energy to work.");
            }

            

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("sleep")]
        public IActionResult Sleep()
        {

            System.Random rand = new System.Random();

            HttpContext.Session.SetInt32("fullness", (int)HttpContext.Session.GetInt32("fullness") - 5);
            HttpContext.Session.SetInt32("happiness", (int)HttpContext.Session.GetInt32("happiness") - 5);
            HttpContext.Session.SetInt32("energy", (int)HttpContext.Session.GetInt32("energy") + 15);
            HttpContext.Session.SetString("message", "Your Dojodachi slept. Energy +15, Fullness -5, Happiness -5");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("restart")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


    }
}