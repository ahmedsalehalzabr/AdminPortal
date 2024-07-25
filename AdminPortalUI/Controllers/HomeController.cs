using AdminPortalUI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AdminPortalUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory; 

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<WalkDto> response = new List<WalkDto>();

            try
            {
                // Get All Regions from Web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7044/api/Walks?pageNumber=1&pageSize=1000");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<WalkDto>>());
            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return View(response);
        }
    }
}
