using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HPSF;

namespace Temperatura
{
    static class Excel
    {
        static string pathExcel = Directory.GetCurrentDirectory() + @"\Temperatura.xls";        

        static HSSFWorkbook hssfworkbook;
        static private bool createExcel()
        {            
            InitializeWorkbook();
            Sheet sheet1 = hssfworkbook.CreateSheet("Arkusz1");
            
            Row row = sheet1.CreateRow(0);
            row.CreateCell(0).SetCellValue("Temp °C");
            row.CreateCell(1).SetCellValue("Godzina");
            row.CreateCell(2).SetCellValue("Data");
            
            return WriteToFile() ? true : false;  
        }

        static public bool excelOperations(float data)
        {
            //utworzy plik.xls jeśli nie istnieje
            if (File.Exists(pathExcel) == false)
            {
                createExcel();
            }
            //doda wiersz z danymi
            addRecord(data);
            return true;
        }

        static private bool addRecord(float data)
        {
            try
            {
                //close the FileStream before specifying any changes in the HSSFSheet object
                FileStream fs = new FileStream(pathExcel, FileMode.Open, FileAccess.ReadWrite);
                HSSFWorkbook templateWorkbook = new HSSFWorkbook(fs);
                HSSFSheet sheet = (HSSFSheet)templateWorkbook.GetSheet("Arkusz1");
                fs.Close();

                DateTime current = DateTime.Now;
                DateTime thisDay = DateTime.Today;

                Row row = sheet.CreateRow(sheet.LastRowNum + 1);
                row.CreateCell(0).SetCellValue(data);
                row.CreateCell(1).SetCellValue(DateTime.Now.ToShortTimeString());
                row.CreateCell(2).SetCellValue(thisDay.ToString("d"));
                
                //reinitialize data before having the workbook object write to the Stream
                sheet.ForceFormulaRecalculation = true;
                fs = new FileStream(pathExcel, FileMode.Open, FileAccess.ReadWrite);
                templateWorkbook.Write(fs);
                fs.Close();
                return true;
            }
            catch
            {
                return false;
            }

        }
               

        static private bool WriteToFile()
        {
            //Write the stream data of workbook to the root directory
            try
            {
                FileStream file = null;
                try
                {
                    file = new FileStream(pathExcel, FileMode.Create);
                    hssfworkbook.Write(file);
                    file.Close();
                }
                finally
                {
                    if (file != null)
                    {
                        file.Dispose();
                    }
                }
            }
            catch (Exception)
            {
                return false;   // jeśli nie można utworzyć pliku wyrzuci błąd
            }
            return true; //jeśli plik został stworzony lub już istnieje
        }

        static void InitializeWorkbook()
        {
            hssfworkbook = new HSSFWorkbook();

            ////create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "DOM";
            hssfworkbook.DocumentSummaryInformation = dsi;

            ////create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "Informacje nt. temperatury w domu";
            hssfworkbook.SummaryInformation = si;

            //here, we must insert at least one sheet to the workbook. otherwise, Excel will say 'data lost in file'
            //So we insert sheet just like what Excel does
            //hssfworkbook.CreateSheet("Arkusz1");

        }
       
    }
}
