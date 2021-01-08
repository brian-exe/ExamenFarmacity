using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Configuration;
using Domain;
using Newtonsoft.Json;

namespace ClientApp
{
    public class ApiClient
    {
        private HttpClient client;
        public ApiClient()
        {
            client = new HttpClient();
            
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("URL_API"));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Articulo>> GetAllArticulos()
        {
            var response = await this.client.GetAsync("articulos").ConfigureAwait(false);
            List<Articulo> result = new List<Articulo>();
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<List<Articulo>>();
            }

            return result;
        }

        public async Task<Articulo> GetById(string id)
        {
            var response = await this.client.GetAsync("articulos/" +id.ToString()).ConfigureAwait(false);
            Articulo result = new Articulo();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<Articulo>();
            }

            return result;
        }

        public async Task<Articulo> Update(Articulo updated)
        {
            var response = await this.client.PutAsJsonAsync("articulos/" + updated.Id.ToString(),updated).ConfigureAwait(false);
            
            Articulo result = new Articulo();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<Articulo>();
            }

            return result;
        }
        
        public async Task<Articulo> Add(Articulo newArt)
        {
            var response = await this.client.PostAsJsonAsync("articulos" , newArt).ConfigureAwait(false);
            
            Articulo result = new Articulo();

            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsAsync<Articulo>();
            }

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var response = await this.client.DeleteAsync("articulos/"+ id.ToString()).ConfigureAwait(false);

            bool result = false;
            if (response.IsSuccessStatusCode)
            {
                result = true; ;
            }

            return result;
        }

    }
}
