using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PokemonWebApp.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace PokemonWebApp.Pages.Pokemons
{
    public class IndexModel : PageModel
    {
        public List<Pokemon> Pokemons { get; set; }
        string baseUrl = "https://localhost:44375/";
        public async Task OnGetAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Pokemons");
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    Pokemons = JsonConvert.DeserializeObject<List<Pokemon>>(result);
                }
            }
        }
    }
}
