using System;

namespace WebApplicationXlsParser.Models
{
    public class Tenders
    {
        /// <summary>
        /// Название тендера
        /// </summary>
        public string NameTender { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime DateTimeFrom { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime DateTimeTo { get; set; }

        /// <summary>
        /// URL тендерной площадки
        /// </summary>
        public string UrlTender { get; set; }

    }
}