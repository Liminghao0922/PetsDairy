using Microsoft.WindowsAzure.Storage.Table;
using PetsDairy.Models;
using PetsDairy.Utils;
using System.Web.Mvc;
using System.Linq;
using System.Threading.Tasks;

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
            return View(PetRepository.ExecuteQuery(rangeQuery).ToList());
        }
    }
}