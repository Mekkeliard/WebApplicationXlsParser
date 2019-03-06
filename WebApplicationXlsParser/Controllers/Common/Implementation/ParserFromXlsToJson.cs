using System;
using System.Linq;
using System.Data.OleDb;
using System.Data.Common;
using Newtonsoft.Json;
using System.IO;
using ExcelDataReader;
using WebApplicationXlsParser.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace WebApplicationXlsParser.Controllers.Common.Implementation
{
    public class ParserFromXlsToJson : IParserFromXlsToJson
    {
        readonly bool header = true;

        public List<Tenders> Parser(string pathXls = null)
        {
            try
            {
                var list = new List<Tenders>();

                var fileXls = new FileInfo(pathXls ?? "C:\\Education\\Tenders.xlsx");
                FileStream stream = File.Open(fileXls.FullName, FileMode.Open, FileAccess.Read);

                IExcelDataReader excelReader;
                switch (fileXls.Extension)
                {
                    case ".xls":
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                        break;
                    case ".xlsx":
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        break;
                    default:
                        throw new Exception("Неверный формат файла");
                }

                try
                {
                    if (header)
                    {
                        excelReader.Read();
                    }
                    while (excelReader.Read())
                    {

                        list.Add(
                            new Tenders
                            {
                                NameTender = excelReader.GetString(0),
                                DateTimeFrom = excelReader.GetDateTime(1),
                                DateTimeTo = excelReader.GetDateTime(2),
                                UrlTender = excelReader.GetString(3)
                            }
                        );
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"Парсер вылетел с ошибкой: {e.Message}");
                }
                finally
                {
                    excelReader.Close();
                }
                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}