using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationXlsParser.Models;

namespace WebApplicationXlsParser.Controllers.Common
{
    public interface IParserFromXlsToJson
    {
        /// <summary>
        /// Парсит Xlsx файл и возвращает лист информации по тендерам
        /// </summary>
        /// <param name="pathXlsx">Путь к файлу Xlsx</param>
        /// <returns>Лист информации по тендерам</returns>
        List<Tenders> Parser(string pathXlsx = null);
    }
}
