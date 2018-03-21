using AutoMapper;
using Microsoft.WindowsAzure.Storage.Table;
using PetsDairy.Models;
using PetsDairy.Utils;
using PetsDairy.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PetsDairy.Controllers
{
    [Authorize]
    public class PetController : Controller
    {
        public CloudTable PetRepository { get; private set; }

        public PetController()
        {
        }

        // GET: Pet
        public async Task<ActionResult> Index()
        {
            PetRepository = await AzureStorageHelper.CreateTableAsync("pets");
            TableQuery<Pet> rangeQuery = new TableQuery<Pet>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, User.Identity.Name));

            var viewModels = Mapper.Map<IEnumerable<PetListModel>>(PetRepository.ExecuteQuery(rangeQuery));
            return View(viewModels.ToList());
        }

        public ActionResult Create()
        {
            var pet = new PetCreateModel
            {
                Owner = User.Identity.Name
            };

            return View(pet);
        }

        [HttpPost]
        public ActionResult Create(PetCreateModel model)
        {
            return RedirectToAction("Index");
        }
    }
}