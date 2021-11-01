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
    public class CreateModel : PageModel
    {
        // vai comparar os dados do post do fromulario com os atributos do objeto pokemon e vai armazenar na variavel abaixo automaticamente.
        [BindProperty]
        // Definindo um atributo pokemon para armazenar os dados vindo formulario.
        public Pokemon Pokemon { get; set; }

        // URL base da API.
        string baseUrl = "https://localhost:44375/";

        // OnPostAsync() captura a ação ou as informações disparadas pelo usuario atraves do formulario.
        public async Task<IActionResult> OnPostAsync()
        {
            // HttpCliente é um serviço que auxilia em chamadas.
            using(var client = new HttpClient())
            {
                // Passando a base URL para o programa atraves do uri.
                client.BaseAddress = new Uri(baseUrl);
                // Limpando o Header para criar o Header do zero sem sujeira dentro.
                client.DefaultRequestHeaders.Clear();
                // Aceitando e adicionando um Header.
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                // Defininco uma variavel para receber a resposta e os dados do Post.
                HttpResponseMessage response = await client
                    // recebendo o Rota da API e referenciando o objeto que vai receber os dados.
                    .PostAsJsonAsync("api/Pokemons", Pokemon);

                // Verificando se a resposta veio com sucesso ou não.
                if(response.IsSuccessStatusCode)
                {
                    // Redirecionando para a pagina Index.
                    return RedirectToPage("./Index");
                } else{
                    // Redirecionando para a propia pagina.
                    return RedirectToPage("./Create");
                }
            }
        }
    }
}
