using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplicationXlsParser.Models;

namespace MVCwithBasicAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient m_httpClient = new HttpClient();

        private List<Tenders> listTenders = new List<Tenders>();
        private string currentPath = "C:\\Education\\Tenders.xlsx";

        [Authorize]
        public ActionResult Index()
        {
            listTenders = GetListTenders(true);
            return View(listTenders);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Index(string path = null)
        {
            currentPath = path ?? currentPath;
            listTenders = GetListTenders(false);
            return View(listTenders);
        }

        private List<Tenders> GetListTenders(bool requestGet)
        {
            List<Tenders> result;
            result = requestGet ?
                Get<List<Tenders>>("http://localhost/WebApplicationXlsParser/XlsxToJson")
                :
                Post<List<Tenders>>("http://localhost/WebApplicationXlsParser/XlsxToJson", "=" + currentPath);
            return result ?? new List<Tenders>();
        }

        /// <summary>
        /// Используется для отправки GET-запросов
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого параметра</typeparam>
        /// <param name="url">Адрес запроса</param>
        /// <returns>Возвращает JSON из класса типа T</returns>
        internal T Get<T>(string url) where T : class, new()
        {
            lock (m_httpClient)
            {
                m_httpClient.DefaultRequestHeaders.Accept.Clear();
                m_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = m_httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContext = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<T>(responseContext);

                    return result;
                }

                return new T();
            }
        }

        internal T Post<T>(string url, string json) where T : class, new()
        {
            lock (m_httpClient)
            {
                m_httpClient.DefaultRequestHeaders.Accept.Clear();
                m_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                var request = new StringContent(json, Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = m_httpClient.PostAsync(url, request).Result;
                if (response.IsSuccessStatusCode)
                {
                    var responseContext = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(responseContext);
                }

                return null;
            }
        }

    }
}