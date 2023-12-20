using ExcelDataReader;
using SuperTrail.ExcelData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatXP.Utilities
{
    internal class ExcelUtils
    {
        public static List<SearchAndBuyExcelData> ReadExcelData(string excelfilepath)
        {
            List<SearchAndBuyExcelData> excelDatas = new List<SearchAndBuyExcelData>();
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(excelfilepath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });
                    var dataTable = result.Tables["SearchAndBuy"];
                    foreach (DataRow row in dataTable.Rows)
                    {
                         SearchAndBuyExcelData excelData = new()
                        {
                            Search = row["Search"].ToString(),
                             FirstName = row["FirstName"].ToString(),
                             Email = row["Email"].ToString(),
                             LastName = row["LastName"].ToString(),
                             Address = row["Address"].ToString(),
                             Apartment = row["Apartment"].ToString(),
                             State = row["State"].ToString(),
                             Pincode = row["pin"].ToString(),
                             PhoneNumber = row["PhoneNumber"].ToString(),
                             City = row["City"].ToString()
                           

                        };
                        excelDatas.Add(excelData);
                    }
                }
            }
            return excelDatas;
        }

      
    }
}
