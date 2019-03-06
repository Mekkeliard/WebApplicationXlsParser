using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationXlsParser.Controllers.Common;
using WebApplicationXlsParser.Controllers.Common.Implementation;
using WebApplicationXlsParser.Models;

namespace WebApplicationXlsParser.Controllers
{

    public class ValuesController : ApiController
    {
        private readonly IParserFromXlsToJson parserFromXlsToJson = new ParserFromXlsToJson();
        
        // GET /XlsxToJson
        [Route("XlsxToJson")]
        [HttpGet]
        public IEnumerable<Tenders> Get()
        {
            return parserFromXlsToJson.Parser();
        }

        // Post /XlsxToJson
        [Route("XlsxToJson")]
        [HttpPost]
        public IEnumerable<Tenders> Post([FromBody]string value)
        {
            return parserFromXlsToJson.Parser(value);
        }
        
    }
}
