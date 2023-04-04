using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApiFrete
{
    public static class ServicoApi
    {
       private static string _URL = "https://wsibid.portaldecompras.co/";
       //private static TokenApi _TOKENAPI;




        //public static  async void ObterToken()
        //{


        //    if (_TOKENAPI == null || _TOKENAPI.Expirationexpiration < DateTime.Now)
        //    {
        //        var user = new UserApi();                 
        //        var client = new HttpClient();
        //        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        //        client.BaseAddress = new Uri(_URL);
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        HttpResponseMessage response = await client.PostAsync("API/v1/Token", content);

        //        _TOKENAPI = JsonConvert.DeserializeObject<TokenApi>(response.Content.ReadAsStringAsync().Result);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Não foi possivel obter o TOKEN de acesso para aplicação!");
        //    }
        //}
        public static async Task<TokenApi> ObterToken()
        {

            var user = new UserApi();
            var client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            client.BaseAddress = new Uri(_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = await client.PostAsync("API/v1/Token", content);

            return JsonConvert.DeserializeObject<TokenApi>(response.Content.ReadAsStringAsync().Result);

        }


        public static async Task<ModeloFrete> GetAsyncAll (string rota)
        {
            var tokenApi =  ObterToken();
            var client = new HttpClient();

            client.BaseAddress = new Uri(_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenApi.Result.Token);
            HttpResponseMessage response = await client.GetAsync(rota);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return new ModeloFrete { Sucesso = true, Retorno = JsonConvert.DeserializeObject<Frete>(json) };
            }
            else
            {
                return new ModeloFrete { Sucesso = false, Retorno = null };
            }

        }

        public static async Task<PostFrete> PostAsync(string rota, Frete frete)
        {

            try
            {
                var tokenApi = ObterToken();
                var client = new HttpClient();

                var content = new StringContent(JsonConvert.SerializeObject(frete), Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenApi.Result.Token);
                HttpResponseMessage response = await client.PostAsync(rota, content);



                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return new PostFrete { Sucesso = true, Retorno = JsonConvert.DeserializeObject<RetornoPostFrete>(json) };
                }
                else
                {
                    return new PostFrete { Sucesso = false, Retorno = null };
                }
            }
            catch (Exception ex)
            {
                return new PostFrete { Sucesso = false, Retorno = null, Mensagem = ex.Message };

            }
        }





    }
}
