using MvcApiPracticaChollos.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcApiPracticaChollos.Services
{
    public class ServiceChollos
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApi;

        public ServiceChollos(IConfiguration configuration)
        {
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>("ApiUrl:Chollos");
        }

        private async Task<T> CallApiAync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }else
                {
                    return default;
                }
            }
        }

        public async Task<List<Chollo>> GetChollosAsync()
        {
            string request = "api/chollos";
            List<Chollo> chollos = await this.CallApiAync<List<Chollo>>(request);
            return chollos;
            
        }

        public async Task<Chollo> FindChollo(int id)
        {
            string request = "api/chollos/" + id;
            Chollo chollo = await this.CallApiAync<Chollo>(request);
            return chollo;
        }

        public async Task InsertChollo(int id, string titulo, string link, string descripcion, DateTime fecha)
        {
            
            using (HttpClient client = new HttpClient())
            {
                string request = "api/chollos";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //tenemos que enviar un objeto JSON
                //nos creamos un objeto de la clase cholloç

                Chollo chollo = new Chollo
                {
                    IdChollo = id,
                    Titulo = titulo,
                    Link = link,
                    Descripcion = descripcion,
                    Fecha = fecha
                };
                //convertimos el objeto a json
                string json = JsonConvert.SerializeObject(chollo);
                //para enviar datos al servicio se utiliza 
                //la clase SytringContent, donde debemos indicar
                //los datos, de ending  y su tipo
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request,content);
            }

        }

        public async Task UpdateChollo(int id, string titulo, string link, string descripcion, DateTime fecha)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/chollos";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);

                Chollo chollo = new Chollo
                {
                    IdChollo = id,
                    Titulo = titulo,
                    Link = link,
                    Descripcion = descripcion,
                    Fecha = fecha
                };
                //convertimos a string 
                string json = JsonConvert.SerializeObject(chollo);
                //uso el stringContent
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task DeleteChollo(int id)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/chollos/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }
    }
}
