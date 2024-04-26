using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Collections;

namespace His.General
{
    /// <summary>
    /// Pozwala grzebac w arkuszu Excel
    /// </summary>
    public class MyExcel
    {
        private Excel.ApplicationClass app = null; // the Excel application.
        private Excel.Workbook book = null;
        private Excel.Worksheet sheet = null;
        private Excel.Range range = null;

        public MyExcel()
        {
            try
            {
                app = new Excel.ApplicationClass();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error opening Excel " + Environment.NewLine + ex.Message);
            }
        }

        public bool Show()
        {
            try
            {
                app.Visible = true;
                app.ScreenUpdating = true;
                app.DisplayAlerts = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error showing Excela" + Environment.NewLine + ex.Message);
                return false;
            }
            return true;

        }


        public bool Open(string path, int sheetNo)
        {
            try
            {
                book = app.Workbooks.Open(path,
                    Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value);
                sheet = (Excel.Worksheet)book.Worksheets[sheetNo];
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error opening." + Environment.NewLine + ex.Message);
                return false;
            }
            return true;
        }

        public bool ChooseSheet(int sheetNo)
        {
            try
            {
                sheet = (Excel.Worksheet)book.Worksheets[sheetNo];
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error choosing " + sheetNo.ToString() + "." + Environment.NewLine + ex.Message);
                return false;
            }
            return true;
        }

        public bool WriteCell(string adres, string myValue)
        {
            try
            {
                Excel.Range range1 = sheet.get_Range(adres, adres);
                range1.set_Value(Missing.Value, myValue);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public string ReadCell(string adres)
        {
            try
            {
                Excel.Range range1 = sheet.get_Range(adres, adres);
                return range1.get_Value(Missing.Value).ToString();
            }
            catch
            {
                return "";
            }
        }

        public bool SaveAs(string path)
        {
            try
            {
                book.SaveAs(path, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlShared, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error saving " + Environment.NewLine + ex.Message);
                return false;
            }
            return true;
        }

        public bool Save()
        {
            try
            {
                book.Save();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error saving " + Environment.NewLine + ex.Message);
                return false;
            }
            return true;
        }

        public bool CloseWorkbook()
        {
            try
            {
                book.Close(Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error closing workbook" + Environment.NewLine + ex.Message);
                return false;
            }
            return true;
        }


        public bool Close()
        {
            try
            {
                app.Quit();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error closing Excela" + Environment.NewLine + ex.Message);
                return false;
            }
            return true;
        }

    }
}