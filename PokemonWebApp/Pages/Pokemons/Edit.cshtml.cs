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
    public class EditModel : PageModel
    {
        [BindProperty]
        // Definindo um atributo pokemon para armazenar os dados vindo formulario.
        public Pokemon Pokemon { get; set; }
        // URL base da API.
        string baseUrl = "https://localhost:44375/";

        // Esse metodo é disparado toda vez que o cliente execulta uma ação.
        // A interrogação indica que esse parameto pode vir vazio.
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("aplication/json"));
                // recebendo o Rota da API e id do objeto.
                HttpResponseMessage response = await client.GetAsync("api/Pokemons/" + id);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Pokemon = JsonConvert.DeserializeObject<Pokemon>(result);
                }
            }

            return Page();
        }

        // Capturando as informações
        public async Task<IActionResult> OnpostAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("aplication/json"));
                HttpResponseMessage response = await client
                    .PutAsJsonAsync("api/Pokemons/" + Pokemon.ID, Pokemon);
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                } else
                {
                    return Page();
                }
            }
        }
    }
}
