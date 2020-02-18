using MvcAsync.Services;
using MvcAsync.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MvcAsync.Controllers
{
    public class HomeController : Controller
    {
        [AsyncTimeout(4000)]
        public async Task<ActionResult> Index(CancellationToken ctk)
        {
            DateTime startDate = DateTime.UtcNow;

            HomePageViewModel viewModel = new HomePageViewModel();
            viewModel.AddMessage(string.Concat("Starting Action on thread id ", Thread.CurrentThread.ManagedThreadId));

            Task<string> calculationResultTask = CalculationService.GetResultAsync(ctk);
            Task<string> databaseResultTask = DatabaseService.GetDataAsync(ctk);

            await Task.WhenAll(calculationResultTask, databaseResultTask);

            viewModel.AddMessage(calculationResultTask.GetAwaiter().GetResult());
            viewModel.AddMessage(databaseResultTask.GetAwaiter().GetResult());

            DateTime endDate = DateTime.UtcNow;
            TimeSpan diff = endDate - startDate;

            viewModel.AddMessage(string.Concat("Finishing Action on thread id ", Thread.CurrentThread.ManagedThreadId));
            viewModel.AddMessage(string.Concat("Action processing time: ", diff.TotalSeconds));
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}