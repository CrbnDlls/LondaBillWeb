using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Drawing.Printing;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Data.OracleClient;
using System.Diagnostics;


namespace LondaBillWeb
{
    public partial class _Default : System.Web.UI.Page
    {
             
        protected void Page_Load(object sender, EventArgs e)
        {
            bool startSwitch;
            
            try
            {
                startSwitch = (bool)Session["startSwitch"];
                
            }
            catch
            {
                 
                startSwitch = true;
            }

            double screenHeight = double.Parse(Session["ScreenResolution"].ToString().Substring(Session["ScreenResolution"].ToString().IndexOf("x") + 1));
            double screenWidth = double.Parse(Session["ScreenResolution"].ToString().Substring(0, Session["ScreenResolution"].ToString().IndexOf("x")));
            double tableHeight = screenHeight - (screenHeight / 100 * 5);
            int h1 = (int)(tableHeight / 100 * 27.5);
            int h2 = (int)(tableHeight / 100 * 48.75);
            int h3 = (int)(tableHeight / 100 * 23.75);
            int resultH = h1 + h2 + h3;
            int buttonsH = h2 + h3;
            Page.Header.Controls.Add(
            new LiteralControl(
            @"<style type='text/css'>
            #MonitorWrap {
            height: " + resultH + "px; width: 27%; }" +
            "#Monitor1 { height: " + h1 +"px; }" +
            "#Monitor2 { height: " + h2 + "px; }" +
            "#Monitor3 { height: " + h3 + "px; }" +
            "#PreviewWrap { width: 72%; height: " + h1 +"px; }" +
            "#ButtonsWrap { width: 72%; height: " + buttonsH + "px; }" +
            "</style>"
            ));
            //string debugTxt = "Высота экрана: " + (string)Session["ScreenResolutionH"] + ";\n ";
            
            double tableWidth = screenWidth - screenWidth / 100 * 3;
            resultH = (int)tableHeight;
            //debugTxt += "Таблица: " + "width: " + tableWidth.ToString() + "px; height: " + resultH.ToString() + "px;\n ";
            
            double imageHeight = tableHeight - (tableHeight / 100*1);
            double imageWidth = imageHeight / 2.675;
            
            //double imageWidth = screenWidth - screenWidth / 100 * 3;
            //double imageHeight = imageWidth * 2.675;
            resultH = (int)imageHeight;
            int resultW = (int)imageWidth;
            //debugTxt += "Рисунок: " + "height:" + resultH.ToString() + "px; width:" + resultW.ToString() + "px;\n";
            //ImageBill.Style.Value = "height:" + resultH.ToString() + "px; width:" + resultW.ToString() + "px; ";
            int WidthTextBoxMonitorItems = resultW;
            //TextBoxMonitorItems.Style.Value = "height:100%; width:" + resultW.ToString() + "px; ";
            //TextBoxWholeSumm.Style.Value = "height:100%; width:" + resultW.ToString() + "px; ";
            //TextBoxMonitorStaff.Style.Value = "height:100%; width:" + resultW.ToString() + "px; ";
            //debugTxt += "Ширина экрана: " + (string)Session["ScreenResolutionW"] + ";\n ";
            double MyFontSize = ((tableHeight / 100 * 15) - (tableHeight / 100 * 15) / 100 * 43);
            resultH = (int)MyFontSize;
            //debugTxt += "Шрифт кода: " + resultH.ToString() + "px;\n ";
            textBoxCode.Font.Size = FontUnit.Parse(resultH.ToString() + "px"); //FontUnit.Point(resultH);
            
            MyFontSize = ((tableHeight / 100 * 10) - ((tableHeight / 100 * 10) / 100 * 5));
            resultH = (int)MyFontSize;
            SizeF size;
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
            {
                size = graphics.MeasureString("Введіть код позиції прайс-листа або виведіть на друк", new Font("Arial", resultH, FontStyle.Regular, GraphicsUnit.Pixel));
            }
            while (((screenWidth - screenWidth / 4) - size.Width) < 0)
            {
                resultH = resultH - 1;
                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
                {
                    size = graphics.MeasureString("Введіть код позиції прайс-листа або виведіть на друк", new Font("Arial", resultH, FontStyle.Regular, GraphicsUnit.Pixel));
                }
            }
            //debugTxt += "Размер width: " + size.Width.ToString() + "px;\n";
            //debugTxt += "Размер height: " + size.Height.ToString() + "px;\n";
            
            //debugTxt += "Шрифт подсказки: " + resultH.ToString() + "px;\n";
            labelHint.Font.Size = FontUnit.Parse(resultH.ToString() + "px"); //FontUnit.Point(resultH);
            labelHint.Height = Unit.Parse(size.Height.ToString() + "px");
            //ImageBill.ImageUrl = "~/BillImage.aspx";
            double buttonWidth = ((tableWidth - (tableWidth/ 100 * 4)) - imageWidth) / 4;
            //double buttonWidth = ((screenWidth - (screenWidth / 100 * 4))) / 4;
            resultW = (int)buttonWidth;
            double buttonHeight = tableHeight / 100 * 14;
            resultH = (int)buttonHeight;
            MyFontSize = ((tableHeight / 100 * 15) - ((tableHeight / 100 * 15) / 100 * 43)) / 1.5;
            int resultF = (int)MyFontSize;
            //debugTxt += "Кнопки: " + " .CssStyleButtonsNumbers { font: " + resultF + "px Arial; color: Black; width: " + resultW + "px; height: " + resultH + "px; };\n ";
            Page.Header.Controls.Add(
            new LiteralControl(
            @"<style type='text/css'>
            .buttonNumbers
            {
            font: " + resultF + "px Arial; color: Black; }</style>"
            ));
            
            MyFontSize = ((tableHeight / 100 * 15) - ((tableHeight / 100 * 15) / 100 * 43)) / 3;
            resultF = (int)MyFontSize;
            
            //textBoxMonitor.Height = Unit.Parse(resultH.ToString() + "px");
            //textBoxCode.Height = Unit.Parse(resultH.ToString() + "px");
            //textBoxCode.Width = Unit.Parse(resultW.ToString() + "px"); 
            Page.Header.Controls.Add(
            new LiteralControl(
            @"<style type='text/css'>
            .button
            {
            font: " + resultF + "px Arial; color: Black; }</style>"
            ));
            
            MyFontSize = ((tableHeight / 100 * 15) - ((tableHeight / 100 * 15) / 100 * 5)) / 2;
            resultH = (int)MyFontSize;
            
            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
            {
                size = graphics.MeasureString("Ціна - 2000 грн. | Кількість - 100 | Знижка - 10% | Всього - 18000 грн.", new Font("Arial", resultH, FontStyle.Regular, GraphicsUnit.Pixel));
            }
            while (((screenWidth - screenWidth / 33 - buttonWidth) - size.Width) < 0)
            {
                resultH = resultH - 1;
                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
                {
                    size = graphics.MeasureString("Ціна - 2000 грн. | Кількість - 100 | Знижка - 10% | Всього - 18000 грн.", new Font("Arial", resultH, FontStyle.Regular, GraphicsUnit.Pixel));
                }
            }

            //debugTxt += "Шрифт монитора: " + resultH.ToString() + "px;\n ";
            textBoxMonitor.Font.Size = FontUnit.Parse(resultH.ToString() + "px");//FontUnit.Point(resultH);
            //TextBoxDebug.Text = debugTxt;

            MyFontSize = ((tableHeight / 100 * 15) - ((tableHeight / 100 * 15) / 100 * 5)) / 3;
            resultH = (int)MyFontSize;

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
            {                              
                size = graphics.MeasureString("------------------------------------------", new Font("Arial", resultH, FontStyle.Regular, GraphicsUnit.Pixel));
            }
            while ((WidthTextBoxMonitorItems - size.Width) < 0)
            {
                resultH = resultH - 1;
                using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(new Bitmap(1, 1)))
                {
                    size = graphics.MeasureString("------------------------------------------", new Font("Arial", resultH, FontStyle.Regular, GraphicsUnit.Pixel));
                }
            }
            TextBoxMonitorStaff.Font.Size = FontUnit.Parse(resultH.ToString() + "px");
            TextBoxMonitorItems.Font.Size = FontUnit.Parse(resultH.ToString() + "px");
            TextBoxWholeSumm.Font.Size = FontUnit.Parse(resultH.ToString() + "px");

            if (startSwitch)
            {
                if (LoadData() != true)
                {
                    MessageDisplay.DisplayMessage("Помилка", "Неможливо загрузити базові дані\r Програма закінчує працювати\n" + (string)Session["Error"]);
                    Environment.Exit(0);
                    
                }
                textBoxMonitor.Text = "Дані відсутні";
                //DrawBill(false);
                BillToMonitor();
                Session["startSwitch"] = false;
            }
            
            this.Header.Title = (string)Session["version"];
            //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
            Literal1.Text = "";
            //textBoxMonitor.Text = Session["Tmp"].ToString() + " " + Session["ScreenResolutionW"].ToString() + " " + Session["ScreenResolutionH"].ToString();
        }

        

            // Загрузка базовых данных
            private bool LoadData()
    {
        // файл сотрудников
        XmlDocument staffXml = new XmlDocument();
            
        if (!File.Exists(Server.MapPath("~/App_Data/Staff.xml")))
        {
            Session["Error"] = "Файла списка співробітників не існує";
            //MessageDisplay.DisplayMessage("Помилка", "Файла списка співробітників не існує");
            //MessageBox.Show("Файла списка співробітників не існує\r", "Помилка");
            return false;
        }
        else
        {
            try
            {
                staffXml.Load(Server.MapPath("~/App_Data/Staff.xml"));
            }
            catch (Exception ex)
            {
                Session["Error"] = "Не можливо відкрити файл списка співробітників\r" + ex.Message;
                //MessageDisplay.DisplayMessage("Помилка", "Не можливо відкрити файл списка співробітників\r" + ex.Message);
                //MessageBox.Show("Не можливо відкрити файл списка співробітників\r" + ex.Message, "Помилка");
                return false;
            }
        }
        Session["staffXml"] = staffXml;
        XmlNode node = staffXml.SelectSingleNode("/staff/updates");
        Session["version"] = "ЛОНDА | Версия: " + node.Attributes["ID"].Value;
        //файл прайс листа
        XmlDocument priceXml = new XmlDocument();
        if (!File.Exists(Server.MapPath("~/App_Data/Price.xml")))
        {
            Session["Error"] = "Файла прайс-листа не існує\r";
            //MessageBox.Show("Файла прайс-листа не існує\r", "Помилка");
            return false;
        }
        else
        {
            try
            {
                priceXml.Load(Server.MapPath("~/App_Data/Price.xml"));
            }
            catch (Exception ex)
            {
                Session["Error"] = "Не можливо відкрити файл прайс-листа\r" + ex.Message;
                //MessageBox.Show("Не можливо відкрити файл прайс-листа\r" + ex.Message, "Помилка");
                return false;
            }
        }
        Session["priceXml"] = priceXml;
        //Файл салона
        XmlDocument salonXml = new XmlDocument();
        if (!File.Exists(Server.MapPath("~/App_Data/salon.xml")))
        {
            Session["Error"] = "Файла салонів не існує\r";
            //MessageBox.Show("Файла салонів не існує\r", "Помилка");
            return false;
        }
        else
        {
            try
            {
                salonXml.Load(Server.MapPath("~/App_Data/salon.xml"));
            }
            catch (Exception ex)
            {
                Session["Error"] = "Не можливо відкрити файл салонів\r" + ex.Message;
                //MessageBox.Show("Не можливо відкрити файл салонів\r" + ex.Message, "Помилка");
                return false;
            }
        }
        
        //Файл настроек салона
        XmlDocument settingsXml = new XmlDocument();
        if (!File.Exists(Server.MapPath("~/App_Data/settings.xml")))
        {
            Session["Error"] = "Файла налаштувань не існує\r";
            //MessageBox.Show("Файла налаштувань не існує\r", "Помилка");
            return false;
        }
        else
        {
            try
            {
                settingsXml.Load(Server.MapPath("~/App_Data/settings.xml"));
            }
            catch (Exception ex)
            {
                Session["Error"] = "Не можливо відкрити файл налаштувань\r" + ex.Message;
                //MessageBox.Show("Не можливо відкрити файл налаштувань\r" + ex.Message, "Помилка");
                return false;
            }
        }
        Session["settingsXml"] = settingsXml;
        //САЛОН
       
        node = salonXml.SelectSingleNode("/salon/item[@ID='" + settingsXml.SelectSingleNode("/settings/salon").Attributes["ID"].Value + "']");
            
        if (node == null)
        {
            Session["Error"] = "Даний салон не існує\r";
            //MessageBox.Show("Даний салон не існує\rПрограма закінчує працювати\r", "Помилка");
            return false;
        }
        LabelSalon.Text = node.Attributes["TYPE"].Value + " ЛОНDА " + node.Attributes["SUB"].Value + " " + node.Attributes["ADR"].Value + " " + node.Attributes["TEL1"].Value + " " + node.Attributes["TEL2"].Value;
                
        //Переключатель мастера или позиции прейскуранта или количества в начальное положение
        Session["MasterOrItemOrQuantity"] = "Item";
        //Переключатель акционной цены
        string[] Discount = new string[2];
        Discount[0] = "no";
        Session["Discount"] = Discount;

        //Файл рабочих листов
        XmlDocument worksheetsXml = new XmlDocument();
        XmlElement wrksh = worksheetsXml.CreateElement("wrksh");
        worksheetsXml.AppendChild(wrksh);
        string wrkshPath = Server.MapPath("~/App_Data");//Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Lndbll\\WorkSheets";
        Session["wrkshPath"] = wrkshPath;
        //string wrkshXmlName = "\\wrksh" + DateTime.Now.Year + Functions.AddZero(DateTime.Now.Month) + Functions.AddZero(DateTime.Now.Day) + ".xml";
       
        if (Directory.Exists(wrkshPath))
        {
            /*if (!File.Exists(wrkshPath + wrkshXmlName))
            {
                try
                {
                    using (XmlWriter writer = XmlWriter.Create(wrkshPath + wrkshXmlName))
                    {
                        // Write XML data.
                        writer.WriteStartElement("wrksh");
                        writer.WriteEndElement();
                        writer.Flush();
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Не можливо створити файл 38\r" + ex.Message, "Помилка");
                    return false;
                }
            }*/
        }
        else
        {
            try
            {
                Directory.CreateDirectory(wrkshPath);
            }
            catch (Exception ex)
            {
                Session["Error"] = "Не можливо створити директорію 38\r" + ex.Message;
                //MessageBox.Show("Не можливо створити директорію 38\r" + ex.Message, "Помилка");
                return false;
            }

            /*try
            {
                using (XmlWriter writer = XmlWriter.Create(wrkshPath + wrkshXmlName))
                {
                    // Write XML data.
                    writer.WriteStartElement("wrksh");
                    writer.WriteEndElement();
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Не можливо створити файл 38\r" + ex.Message, "Помилка");
                return false;
            }*/
        }
        /*try
        {
            worksheetsXml.Load(wrkshPath + wrkshXmlName);
        }
        catch (Exception ex)
        {
            //MessageBox.Show("Не можливо відкрити файл 38\r" + ex.Message, "Помилка");
            return false;
        }*/
        Session["worksheetsXml"] = worksheetsXml;
        //генерация случайного номера счета
        Session["billNumber"] = GetBillNumber();

        // Проверка что настройки принтера внесены в программу
        XmlNode printerNode = settingsXml.SelectSingleNode("/settings/printer");
        string printerName;
        if (printerNode != null)
        {
            try
            {
                printerName = printerNode.Attributes["NAME"].Value;
            }
            catch
            {
                Session["Error"] = "В налаштуваннях програми не вказан принтер\r";
                //MessageBox.Show("В налаштуваннях програми не вказан принтер\r", "Помилка");
                return false;
            }
        }
        else
        {
            Session["Error"] = "В налаштуваннях програми не вказан принтер\r";
            //MessageBox.Show("В налаштуваннях програми не вказан принтер\r", "Помилка");
            return false;
        }
        //Проверка что инсталирован нужный принтер
        PrinterSettings.StringCollection printers = PrinterSettings.InstalledPrinters;
        PrinterSettings printBillSettings = null;
        PageSettings printBillPage = null;
        bool printerOK = false;
        bool paperOK = false;
        foreach (string printer in printers)
        {
            if (printer == printerName)
            {
                printBillSettings = new PrinterSettings();
                printBillSettings.PrinterName = printerName;
                PrinterSettings.PaperSizeCollection paperSizes = printBillSettings.PaperSizes;
                /*foreach (PaperSize sizeP in paperSizes)
                {
                    if (sizeP.PaperName == "Рабочий лист")// & sizeP.Height == 740 & sizeP.Width == 300)
                    {
                        paperOK = true;
                        printBillSettings.Copies = 2;

                        printBillPage = new PageSettings(printBillSettings);
                        printBillPage.PaperSize = sizeP;
                        //printBillPage.Landscape = true;
                        break;
                    }
                }
                */
                paperOK = true;
                printBillSettings.Copies = 2;

                printBillPage = new PageSettings(printBillSettings);

                PrinterSettings.PaperSourceCollection paperSources = printBillSettings.PaperSources;

                printerOK = true;
                break;
            }
        }
        if (!printerOK || !printBillSettings.IsValid)
        {
            string error = "<br>Потрібний принтер не встановлен: " + printerName + "<br>Встановлені принтери:";
            foreach (string printer in printers)
            {
                error = error + " " + printer + ";" ;
            }
            Session["Error"] = error;
            //MessageBox.Show("Потрібний принтер не встановлен\r", "Помилка");
            return false;
        }
        if (!paperOK)
        {
            Session["Error"] = "Принтер не вірно налаштован\r";
            //MessageBox.Show("Принтер не вірно налаштован\r", "Помилка");
            return false;
        }
        PrintDocument printBill = new PrintDocument();
        printBill.PrinterSettings = printBillSettings;
        printBill.DefaultPageSettings = printBillPage;
        printBill.PrintPage += new PrintPageEventHandler(printBill_PrintPage);
        Session["printBill"] = printBill;
        Session["BO"] = false;

        //labelVersion.Text = labelVersion.Text.Substring(0, labelVersion.Text.IndexOf(":") + 1) + " " + updatesXml.DocumentElement.Attributes["NUMA"].Value;
        // Дошли до конца - успех
        return true;
    }
        
    //генерация случайного номера счета
    private int GetBillNumber()
    {
        Random number = new Random();
        return number.Next(10000, 99999);
    }
    
    #region Кнопки цифр
    protected void button0_Click(object sender, EventArgs e)
    {
        textBoxCode.Text = textBoxCode.Text + "0";
        DisplayDataToChoose();
        //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
    }

    protected void button1_Click(object sender, EventArgs e)
    {
        textBoxCode.Text = textBoxCode.Text + "1";
        DisplayDataToChoose();
        //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
    }

    protected void button2_Click(object sender, EventArgs e)
    {
        textBoxCode.Text = textBoxCode.Text + "2";
        DisplayDataToChoose();
        //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
    }

    protected void button3_Click(object sender, EventArgs e)
    {
        textBoxCode.Text = textBoxCode.Text + "3";
        DisplayDataToChoose();
        //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
    }

    protected void button4_Click(object sender, EventArgs e)
    {
        textBoxCode.Text = textBoxCode.Text + "4";
        DisplayDataToChoose();
        //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
    }

    protected void button5_Click(object sender, EventArgs e)
    {
        textBoxCode.Text = textBoxCode.Text + "5";
        DisplayDataToChoose();
        //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
    }

    protected void button6_Click(object sender, EventArgs e)
    {
        textBoxCode.Text = textBoxCode.Text + "6";
        DisplayDataToChoose();
        //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
    }

    protected void button7_Click(object sender, EventArgs e)
    {
        textBoxCode.Text = textBoxCode.Text + "7";
        DisplayDataToChoose();
        //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
    }

    protected void button8_Click(object sender, EventArgs e)
    {
        textBoxCode.Text = textBoxCode.Text + "8";
        DisplayDataToChoose();
        //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
    }

    protected void button9_Click(object sender, EventArgs e)
    {
        textBoxCode.Text = textBoxCode.Text + "9";
        DisplayDataToChoose();
        //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
    }

    protected void buttonDel_Click(object sender, EventArgs e)
    {
        if (textBoxCode.Text.Length > 0)
        {
            textBoxCode.Text = textBoxCode.Text.Remove(textBoxCode.Text.Length - 1);
        }
        //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
        DisplayDataToChoose();
    }
    #endregion;

    //Функция вывода данных в текстбокс для предварительного просмотра
    private void DisplayDataToChoose()
    {
        string[] priceS;
        XmlNode resultNode;
        switch ((string)Session["MasterOrItemOrQuantity"])
        {
            case "Master":
                XmlDocument staffXml = (XmlDocument)Session["staffXml"];
                resultNode = staffXml.SelectSingleNode("/staff/worker[@ID='" + textBoxCode.Text + "']");
                if (resultNode == null)
                {
                    textBoxMonitor.Text = "Дані відсутні";
                }
                else
                {
                    textBoxMonitor.Text = resultNode.Attributes["NAME"].Value + " | " + GetRank(resultNode.Attributes["ID"].Value);
                }
                Session["resultNode"] = resultNode;
                break;
            case "Item":
                XmlDocument priceXml = (XmlDocument)Session["priceXml"];
                resultNode = priceXml.SelectSingleNode("/price/item[@ID='" + textBoxCode.Text + "']");
                if (resultNode == null)
                {
                    textBoxMonitor.Text = "Дані відсутні";
                }
                else
                {
                    priceS = GetPriceDiscount(resultNode);
                    if ((bool)Session["BO"])
                    {
                        
                        textBoxMonitor.Text = resultNode.Attributes["NAME"].Value + "\nЦіна - " + priceS[2] + " грн. | Кількість - 1 | Знижка - " + priceS[1] + " | Всього - 0,00 грн.";
                    }
                    else
                    {
                        textBoxMonitor.Text = resultNode.Attributes["NAME"].Value + "\nЦіна - " + priceS[2] + " грн. | Кількість - 1 | Знижка - " + priceS[1] + " | Всього - " + priceS[0] + " грн.";
                    }
                    Session["DisplayStringLenth"] = textBoxMonitor.Text.Length;
                }
                Session["resultNode"] = resultNode;
                break;
            case "Quantity":
                decimal priceD;
                resultNode = (XmlNode)Session["resultNode"];
                priceS = GetPriceDiscount(resultNode);
                if (textBoxCode.Text.Length != 0 & decimal.TryParse(priceS[0], out priceD))
                {
                    if ((bool)Session["BO"])
                    {
                        textBoxMonitor.Text = resultNode.Attributes["NAME"].Value + "\nЦіна - " + priceS[2] + " грн. | Кількість - " + textBoxCode.Text + " | Знижка - " + priceS[1] + " | Всього - 0,00 грн.";
                    }
                    else
                    {
                        textBoxMonitor.Text = resultNode.Attributes["NAME"].Value + "\nЦіна - " + priceS[2] + " грн. | Кількість - " + textBoxCode.Text + " | Знижка - " + priceS[1] + " | Всього - " + priceD * decimal.Parse(textBoxCode.Text) + " грн.";
                    }
                }
                else
                {
                    if ((bool)Session["BO"])
                    {
                        textBoxMonitor.Text = resultNode.Attributes["NAME"].Value + "\nЦіна - " + priceS[2] + " грн. | Кількість - 1 | Знижка - " + priceS[1] + " | Всього - 0,00 грн.";
                    }
                    else
                    {
                        textBoxMonitor.Text = resultNode.Attributes["NAME"].Value + "\nЦіна - " + priceS[2] + " грн. | Кількість - 1 | Знижка - " + priceS[1] + " | Всього - " + priceS[0] + " грн.";
                    }
                }
                break;
        }
        
    }

    //Выбор какую цену брать из файла прайс листа
    private string[] GetPriceDiscount(XmlNode node)
    {
        string[] result = new string[3];
        //result[0] - цена со скидкой
        //result[1] - скидка
        //result[2] - цена без скидки
        string[] Discount = (string[])Session["Discount"];
        switch (Discount[0])
        {
            case "50%":
                try
                {
                    result[0] = node.Attributes["PRICE50"].Value;
                    result[1] = "50%";
                    result[2] = node.Attributes["PRICE0"].Value;
                }
                catch
                {
                    try
                    {
                        result[0] = node.Attributes["PRICE0"].Value;
                        result[1] = "";
                        result[2] = node.Attributes["PRICE0"].Value;
                    }
                    catch
                    {
                        result[0] = "Ціни не має";
                        result[1] = "";
                        result[2] = "Ціни не має";
                    }
                }
                return result;
            case "10%":
                if (Discount[1] == "50%")
                {
                    try
                    {
                        result[0] = node.Attributes["PRICE50"].Value;
                        result[1] = "50%";
                        result[2] = node.Attributes["PRICE0"].Value;
                    }
                    catch
                    {
                            try
                            {
                                result[0] = node.Attributes["PRICE10"].Value;
                                result[2] = node.Attributes["PRICE0"].Value;

                                result[1] = (100 - (decimal.Parse(result[0]) / decimal.Parse(result[2]) * 100)).ToString() + "%";

                            }
                            catch
                            {
                                try
                                {
                                    result[0] = node.Attributes["PRICE0"].Value;
                                    result[1] = "";
                                    result[2] = node.Attributes["PRICE0"].Value;
                                }
                                catch
                                {
                                    result[0] = "Ціни не має";
                                    result[1] = "";
                                    result[2] = node.Attributes["PRICE0"].Value;
                                }
                            }
                        }
                }
                else
                {
                    try
                    {
                        result[0] = node.Attributes["PRICE10"].Value;
                        result[2] = node.Attributes["PRICE0"].Value;
                        result[1] = (100 - (decimal.Parse(result[0]) / decimal.Parse(result[2]) * 100)).ToString() + "%";
                    }
                    catch
                    {
                        try
                        {
                            result[0] = node.Attributes["PRICE0"].Value;
                            result[1] = "";
                            result[2] = node.Attributes["PRICE0"].Value;
                        }
                        catch
                        {
                            result[0] = "Ціни не має";
                            result[1] = "";
                            result[2] = "Ціни не має";
                        }
                    }
                }

                return result;
            case "staff":
                try
                {
                    result[0] = node.Attributes["PRICESTAFF"].Value;
                    result[1] = "с";
                    result[2] = node.Attributes["PRICE0"].Value;
                }
                catch
                {
                    try
                    {
                        result[0] = node.Attributes["PRICE0"].Value;
                        result[1] = "";
                        result[2] = node.Attributes["PRICE0"].Value;
                    }
                    catch
                    {
                        result[0] = "Ціни не має";
                        result[1] = "";
                        result[2] = node.Attributes["PRICE0"].Value;
                    }
                }
                return result;
            default:
                try
                {
                    result[0] = node.Attributes["PRICE0"].Value;
                    result[1] = "";
                    result[2] = node.Attributes["PRICE0"].Value;
                }
                catch
                {
                    result[0] = "Ціни не має";
                    result[1] = "";
                    result[2] = "Ціни не має";
                }
                return result;
        }
    }



    #region Кнопки акций
    protected void button50procent_Click(object sender, EventArgs e)
    {
        string[] Discount = (string[])Session["Discount"];
        switch (Discount[0])
        {
            case "50%":
                Discount[0] = "no";
                Discount[1] = "no";
                button50procent.BackColor = Color.Empty;
                //button50procent.BackgroundImage = LondaBill.Properties.Resources.bDefault;
                break;
            case "10%":
                if (Discount[1] == "50%")
                {
                    Discount[1] = "no";
                    button50procent.BackColor = Color.Empty;
                    //button50procent.BackgroundImage = LondaBill.Properties.Resources.bDefault;
                }
                else
                {
                    Discount[1] = "50%";
                    button50procent.BackColor = Color.Green;
                    //button50procent.BackgroundImage = LondaBill.Properties.Resources.bGreen;
                }
                break;
            default:
                Discount[0] = "50%";
                Discount[1] = "50%";
                button50procent.BackColor = Color.Green;
                buttonStaff.BackColor = Color.Empty;
                //button50procent.BackgroundImage = LondaBill.Properties.Resources.bGreen;
                //buttonStaff.BackgroundImage = LondaBill.Properties.Resources.bDefault;
                break;
        }
        Session["Discount"] = Discount;
        ChangePriceAccordingToDiscount();
        DisplayDataToChoose();
        //DrawBill(false);
        BillToMonitor();
    }

    protected void button10Procent_Click(object sender, EventArgs e)
    {
        string[] Discount = (string[])Session["Discount"];
        switch (Discount[0])
        {
            case "50%":
                Discount[0] = "10%";
                button10Procent.BackColor = Color.Orange;
                buttonStaff.BackColor = Color.Empty;    
                //button10Procent.BackgroundImage = LondaBill.Properties.Resources.bOrange;
                //buttonStaff.BackgroundImage = LondaBill.Properties.Resources.bDefault;
                break;
            case "10%":
                if (Discount[1] == "50%")
                {
                    Discount[0] = "50%";
                }
                else
                {
                    Discount[0] = "no";
                }
                button10Procent.BackColor = Color.Empty;
                //button10Procent.BackgroundImage = LondaBill.Properties.Resources.bDefault;
                break;
            default:
                Discount[0] = "10%";
                Discount[1] = "no";
                button10Procent.BackColor = Color.Orange;
                buttonStaff.BackColor = Color.Empty;
                //button10Procent.BackgroundImage = LondaBill.Properties.Resources.bOrange;
                //buttonStaff.BackgroundImage = LondaBill.Properties.Resources.bDefault;
                break;
        }
        Session["Discount"] = Discount;
        ChangePriceAccordingToDiscount();
        DisplayDataToChoose();
        //DrawBill(false);
        BillToMonitor();
    }
    protected void buttonStaff_Click(object sender, EventArgs e)
    {
        string[] Discount = (string[])Session["Discount"];
        if (Discount[0] == "staff")
        {
            Discount[0] = "no";
            buttonStaff.BackColor = Color.Empty;
            //buttonStaff.BackgroundImage = LondaBill.Properties.Resources.bDefault;
        }
        else
        {
            Discount[0] = "staff";
            Discount[1] = "no";
            button50procent.BackColor = Color.Empty;
            button10Procent.BackColor = Color.Empty;
            buttonStaff.BackColor = Color.Blue;
            //button50procent.BackgroundImage = LondaBill.Properties.Resources.bDefault;
            //button10Procent.BackgroundImage = LondaBill.Properties.Resources.bDefault;
            //buttonStaff.BackgroundImage = LondaBill.Properties.Resources.bBlue;
        }
        Session["Discount"] = Discount;
        ChangePriceAccordingToDiscount();
        DisplayDataToChoose();
        //DrawBill(false);
        BillToMonitor();
    }
    protected void buttonBO_Click(object sender, EventArgs e)
    {
        if ((bool)Session["BO"])
        {
            Session["BO"] = false;
            buttonBO.BackColor = Color.Empty;
            //buttonBO.BackgroundImage = LondaBill.Properties.Resources.bDefault;
        }
        else
        {
            Session["BO"] = true;
            buttonBO.BackColor = Color.Violet;
            //buttonBO.BackgroundImage = LondaBill.Properties.Resources.bViolet;
        }
        ChangePriceAccordingToDiscount();
        DisplayDataToChoose();
        //DrawBill(false);
        BillToMonitor();
    }
    #endregion

    //Изменение цены в зависимости кнопок акций
    private void ChangePriceAccordingToDiscount()
    {
        XmlElement currentWrkshXml = null;
        try
        {
            currentWrkshXml = (XmlElement)Session["currentWrkshXml"];
        }
        catch
        { }
        if (currentWrkshXml != null)
        {
            for (int i = 0; i < currentWrkshXml.ChildNodes.Count; i++)
            {
                XmlDocument priceXml = (XmlDocument)Session["priceXml"];
                XmlNode node = priceXml.SelectSingleNode("/price/item[@ID='" + currentWrkshXml.ChildNodes[i].Attributes["ID"].Value + "']");
                string[] PriceDiscount = GetPriceDiscount(node);
                if ((bool)Session["BO"])
                {
                    currentWrkshXml.ChildNodes[i].Attributes["SELLPRICE"].Value = "0";
                    currentWrkshXml.ChildNodes[i].Attributes["BO"].Value = "1";
                }
                else
                {
                    currentWrkshXml.ChildNodes[i].Attributes["SELLPRICE"].Value = PriceDiscount[0];
                    currentWrkshXml.ChildNodes[i].Attributes["BO"].Value = "0";
                }
                currentWrkshXml.ChildNodes[i].Attributes["DISCOUNT"].Value = PriceDiscount[1];
                currentWrkshXml.ChildNodes[i].Attributes["PRICE"].Value = PriceDiscount[2];
            }
            Session["currentWrkshXml"] = currentWrkshXml;
        }
    }

    protected void buttonSave_Click(object sender, EventArgs e)
    {
        XmlNode resultNode = null;
        try
        { resultNode = (XmlNode)Session["resultNode"]; }
        catch
        { }

        if (resultNode != null)
        {
            XmlElement currentWrkshXml = null;
            XmlDocument worksheetsXml = (XmlDocument)Session["worksheetsXml"];
            try
            { currentWrkshXml = (XmlElement)Session["currentWrkshXml"]; }
            catch
            { 
            }
            if (currentWrkshXml == null)
            {
                currentWrkshXml = worksheetsXml.CreateElement("WORKSHEET");
                Session["currentWrkshXml"] = currentWrkshXml;
            }
            switch ((string)Session["MasterOrItemOrQuantity"])
            {
                case "Master":
                    currentWrkshXml.SetAttribute("ID", "0");
                    currentWrkshXml.SetAttribute("NAME", resultNode.Attributes["NAME"].Value);
                    currentWrkshXml.SetAttribute("STAFFID", resultNode.Attributes["ID"].Value);
                    currentWrkshXml.SetAttribute("LEVEL", resultNode.Attributes["LEVEL"].Value);
                    currentWrkshXml.SetAttribute("PROF", resultNode.Attributes["PROF"].Value);
                    currentWrkshXml.SetAttribute("SALON", "0");
                    Session["MasterOrItemOrQuantity"] = "Item";
                    if (currentWrkshXml.HasChildNodes)
                    {
                        labelHint.Text = "Введіть код позиції прайс-листа або виведіть на друк";
                    }
                    else
                    {
                        labelHint.Text = "Введіть код позиції прайс-листа";
                        buttonPrint.Enabled = false;
                    }
                    buttonSave.BackColor = Color.Empty;
                    Session["currentWrkshXml"] = currentWrkshXml;
                    //DrawBill(false);
                    buttonPrint.Text = "Надрукувати";
                    BillToMonitor();
                    
                    break;
                case "Item":
                    string[] PriceDiscount = GetPriceDiscount(resultNode);
                    decimal price;
                    if (decimal.TryParse(PriceDiscount[0], out price))
                    {
                        XmlElement item = worksheetsXml.CreateElement("ITEM");
                        item.SetAttribute("ID", resultNode.Attributes["ID"].Value);
                        item.SetAttribute("NAME", resultNode.Attributes["NAME"].Value);
                        if ((bool)Session["BO"])
                        {
                            item.SetAttribute("SELLPRICE", "0");
                            item.SetAttribute("BO", "1");
                        }
                        else
                        {
                            item.SetAttribute("SELLPRICE", PriceDiscount[0]);
                            item.SetAttribute("BO", "0");
                        }
                        item.SetAttribute("DISCOUNT", PriceDiscount[1]);
                        item.SetAttribute("PRICE", PriceDiscount[2]);
                        item.SetAttribute("QUANTITY", "1");
                        item.SetAttribute("DELETE", "0");
                        currentWrkshXml.AppendChild(item);
                        Session["currentWrkshXml"] = currentWrkshXml;
                        Session["MasterOrItemOrQuantity"] = "Quantity";
                        buttonDelString.Enabled = false;
                        labelHint.Text = "Введіть кількість";
                        buttonSave.BackColor = Color.Red;
                        //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
                        //buttonSave.BackgroundImage = LondaBill.Properties.Resources.bRed;
                    }
                    else
                    {
                        //MessageBox.Show("Не можливо внести позицію, не вірно вказана ціна", "Помилка");
                    }
                    break;
                case "Quantity":
                    XmlNode itemQ = currentWrkshXml.LastChild;
                    int quan;
                    if (int.TryParse(textBoxCode.Text, out quan))
                    {
                        itemQ.Attributes["QUANTITY"].Value = textBoxCode.Text;
                    }
                    else
                    {
                        itemQ.Attributes["QUANTITY"].Value = "1";
                    }
                    Session["MasterOrItemOrQuantity"] = "Item";
                    buttonDelString.Enabled = true;
                    if (buttonPrint.Text == "Обрати майстра")
                    {
                        labelHint.Text = "Введіть код позиції прайс-листа або оберіть майстра";
                    }
                    else
                    {
                        labelHint.Text = "Введіть код позиції прайс-листа або виведіть на друк";
                    }
                    buttonSave.BackColor = Color.Empty;
                    Session["currentWrkshXml"] = currentWrkshXml;
                    //buttonSave.BackgroundImage = LondaBill.Properties.Resources.bDefault;
                    if (!buttonPrint.Enabled)
                    {
                        buttonPrint.Enabled = true;
                    }
                    //DrawBill(false);
                    BillToMonitor();
                    break;
            }
            textBoxCode.Text = "";
            DisplayDataToChoose();

        }
        else
        {
            //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
        }

    }

    protected void buttonBegin_Click(object sender, EventArgs e)
    {
        EverythingToBegining();
    }

    //приводим в начальные значения
    private void EverythingToBegining()
    {
        //SendDataToBase(worksheetsXml, wrkshPath + wrkshXmlName);
        button50procent.BackColor = Color.Empty;
        button10Procent.BackColor = Color.Empty;
        buttonStaff.BackColor = Color.Empty;
        buttonSave.BackColor = Color.Empty;
        buttonPrint.Text = "Обрати майстра";
        Session["currentWrkshXml"] = null;
        //Session.Clear();
        if (LoadData() != true)
        {
            MessageDisplay.DisplayMessage("Помилка", "Неможливо загрузити базові дані\rПрограма закінчує працювати\r");
            Environment.Exit(0);

        }
        textBoxCode.Text = "";
        labelHint.Text = "Введіть код позиції прайс-листа";
        textBoxMonitor.Text = "Дані відсутні";
        if (!buttonPrint.Enabled)
        {
            buttonPrint.Enabled = true;
        }
        //DrawBill(false);
        BillToMonitor();
        
        
        /*
        Session["MasterOrItemOrQuantity"] = "Master";
        
        string[] Discount = new string[2];
        Discount[0] = "no";
        Discount[1] = "no";
        Session["Discount"] = Discount;
        button50procent.BackColor = Color.Empty;
        button10Procent.BackColor = Color.Empty;
        buttonStaff.BackColor = Color.Empty;
        buttonSave.BackColor = Color.Empty;
        //button50procent.BackgroundImage = LondaBill.Properties.Resources.bDefault;
        //button10Procent.BackgroundImage = LondaBill.Properties.Resources.bDefault;
        //buttonStaff.BackgroundImage = LondaBill.Properties.Resources.bDefault;
        //buttonSave.BackgroundImage = LondaBill.Properties.Resources.bDefault;
        Session["billNumber"] = GetBillNumber();
        DrawBill(false);
        DisplayDataToChoose();*/
    }
    
    //Просчитуем счет и выводим на экран
    private void BillToMonitor()
    {

        TextBoxMonitorStaff.Text = "РАХУНОК";
        TextBoxMonitorStaff.Text = TextBoxMonitorStaff.Text + "\n" + Session["billNumber"].ToString();
        TextBoxMonitorStaff.Text = TextBoxMonitorStaff.Text + "\n------------------------------------------";
        //TextBoxMonitorStaff.Text = TextBoxMonitorStaff.Text + "\nНА ВСІ ПОСЛУГИ САЛОНУ";
        TextBoxMonitorStaff.Text = TextBoxMonitorStaff.Text + "\nЗНИЖКА 10% ДІЙСНА ДО";
        TextBoxMonitorStaff.Text = TextBoxMonitorStaff.Text + "\n" + Functions.AddZero(DateTime.Now.AddDays(30).Day.ToString()) + " / " + Functions.AddZero(DateTime.Now.AddDays(30).Month.ToString()) + " / " + DateTime.Now.AddDays(30).Year.ToString();
        TextBoxMonitorStaff.Text = TextBoxMonitorStaff.Text + "\n------------------------------------------";
        XmlElement currentWrkshXml = null;
        try { currentWrkshXml = (XmlElement)Session["currentWrkshXml"]; }
        catch { }
        
        decimal wholeSumm = 0;
        decimal wholeSummWithoutDiscount = 0;

        if (currentWrkshXml != null)
        {
            if (currentWrkshXml.HasAttributes)
            {//Фамилия и имя
                TextBoxMonitorStaff.Text = TextBoxMonitorStaff.Text + "\n" + currentWrkshXml.Attributes["NAME"].Value;
                // Звание и номер
                TextBoxMonitorStaff.Text = TextBoxMonitorStaff.Text + "\n" + GetRank(currentWrkshXml.Attributes["STAFFID"].Value) + " " + currentWrkshXml.Attributes["STAFFID"].Value;
                //Позиции прайс листа
                //TextBoxMonitorItems.Text = TextBoxMonitorItems.Text + "\n------------------------------------------";
            }
            if (currentWrkshXml.HasChildNodes)
            {

                float nameY = 502.02F;
                float PictureLenth;

                for (int i = 0; i < currentWrkshXml.ChildNodes.Count; i++)
                {
                    //Наименование позиции
                    int zeroCount;
                    string[] item = AllocateString(addZeroToCode(currentWrkshXml.ChildNodes[i].Attributes["ID"].Value, out zeroCount) + " " + currentWrkshXml.ChildNodes[i].Attributes["NAME"].Value);
                    item[0] = item[0].Substring(zeroCount);

                    float nameH = 44.57F * (item.Count() + 2);
                    if (i == 0)
                    {
                        TextBoxMonitorItems.Text = currentWrkshXml.ChildNodes[i].Attributes["ID"].Value + " " + currentWrkshXml.ChildNodes[i].Attributes["NAME"].Value;
                    }
                    else
                    {
                        TextBoxMonitorItems.Text = TextBoxMonitorItems.Text + "\n" + currentWrkshXml.ChildNodes[i].Attributes["ID"].Value + " " + currentWrkshXml.ChildNodes[i].Attributes["NAME"].Value;
                    }
                    decimal summWithoutDiscount = decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["PRICE"].Value) * decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["QUANTITY"].Value);
                    wholeSummWithoutDiscount = wholeSummWithoutDiscount + summWithoutDiscount;
                    TextBoxMonitorItems.Text = TextBoxMonitorItems.Text + "\n" + currentWrkshXml.ChildNodes[i].Attributes["QUANTITY"].Value + ".000  X  " + addZeroToSumm(currentWrkshXml.ChildNodes[i].Attributes["PRICE"].Value) + "=  " + addZeroToSumm((summWithoutDiscount).ToString());

                    if (decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["PRICE"].Value) != decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["SELLPRICE"].Value))
                    {
                        TextBoxMonitorItems.Text = TextBoxMonitorItems.Text + "\nЗНИЖКА: " + currentWrkshXml.ChildNodes[i].Attributes["DISCOUNT"].Value + "=  -" + addZeroToSumm((decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["PRICE"].Value) * decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["QUANTITY"].Value) - decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["SELLPRICE"].Value) * decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["QUANTITY"].Value)).ToString());
                    }

                    //Сумма 
                    wholeSumm = wholeSumm + (decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["SELLPRICE"].Value) * decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["QUANTITY"].Value));

                    nameY = nameY + nameH + 10F;
                    TextBoxMonitorItems.Text = TextBoxMonitorItems.Text + "\n------------------------------------------";
                }
                PictureLenth = nameY + 262.59F;
                Session["PictureLenth"] = PictureLenth;
            }
            else
            {
                TextBoxMonitorItems.Text = "";
            }
        }
        else
        {
            TextBoxMonitorItems.Text = "";
        }
        
        if (wholeSummWithoutDiscount == 0)
        {
            TextBoxWholeSumm.Text = "ВАРТІСТЬ     0 грн. 00 коп.";
        }
        else
        {
            if (wholeSummWithoutDiscount.ToString().IndexOf(",") == -1)
            {
                TextBoxWholeSumm.Text = "ВАРТІСТЬ     " + wholeSummWithoutDiscount.ToString() + " грн. 00 коп.";
            }
            else
            {
                if (wholeSummWithoutDiscount.ToString().Substring(wholeSummWithoutDiscount.ToString().IndexOf(",") + 1).Length == 1)
                {
                    TextBoxWholeSumm.Text = "ВАРТІСТЬ     " + wholeSummWithoutDiscount.ToString().Substring(0, wholeSummWithoutDiscount.ToString().IndexOf(",")) + " грн. " + wholeSummWithoutDiscount.ToString().Substring(wholeSummWithoutDiscount.ToString().IndexOf(",") + 1) + "0 коп.";
                }
                else
                {
                    TextBoxWholeSumm.Text = "ВАРТІСТЬ     " + wholeSummWithoutDiscount.ToString().Substring(0, wholeSummWithoutDiscount.ToString().IndexOf(",")) + " грн. " + wholeSummWithoutDiscount.ToString().Substring(wholeSummWithoutDiscount.ToString().IndexOf(",") + 1) + " коп.";
                }
            }
        }
        TextBoxWholeSumm.Text = TextBoxWholeSumm.Text + "\n------------------------------------------";
        //Сумма Знижки
        decimal summDiscount = wholeSummWithoutDiscount - wholeSumm;
        if (summDiscount == 0)
        {
            TextBoxWholeSumm.Text = TextBoxWholeSumm.Text + "\nЗНИЖКА       0 грн. 00 коп.";
        }
        else
        {
            if (summDiscount.ToString().IndexOf(",") == -1)
            {
                TextBoxWholeSumm.Text = TextBoxWholeSumm.Text + "\nЗНИЖКА       -" + summDiscount.ToString() + " грн. 00 коп.";
            }
            else
            {
                if (summDiscount.ToString().Substring(summDiscount.ToString().IndexOf(",") + 1).Length == 1)
                {
                    TextBoxWholeSumm.Text = TextBoxWholeSumm.Text + "\nЗНИЖКА       -" + summDiscount.ToString().Substring(0, summDiscount.ToString().IndexOf(",")) + " грн. " + summDiscount.ToString().Substring(summDiscount.ToString().IndexOf(",") + 1) + "0 коп.";
                }
                else
                {
                    TextBoxWholeSumm.Text = TextBoxWholeSumm.Text + "\nЗНИЖКА       -" + summDiscount.ToString().Substring(0, summDiscount.ToString().IndexOf(",")) + " грн. " + summDiscount.ToString().Substring(summDiscount.ToString().IndexOf(",") + 1) + " коп.";
                }
            }
        }
        TextBoxWholeSumm.Text = TextBoxWholeSumm.Text + "\n------------------------------------------";
        TextBoxWholeSumm.Text = TextBoxWholeSumm.Text + "\nДО СПЛАТИ";
        if (wholeSumm == 0)
        {
            TextBoxWholeSumm.Text = TextBoxWholeSumm.Text + "\nВСЬОГО       0 грн. 00 коп.";

        }
        else
        {

            if (wholeSumm.ToString().IndexOf(",") == -1)
            {
                TextBoxWholeSumm.Text = TextBoxWholeSumm.Text + "\nВСЬОГО       " + wholeSumm.ToString() + " грн. 00 коп.";
            }
            else
            {
                if (wholeSumm.ToString().Substring(wholeSumm.ToString().IndexOf(",") + 1).Length == 1)
                {
                    TextBoxWholeSumm.Text = TextBoxWholeSumm.Text + "\nВСЬОГО       " + wholeSumm.ToString().Substring(0, wholeSumm.ToString().IndexOf(",")) + " грн. " + wholeSumm.ToString().Substring(wholeSumm.ToString().IndexOf(",") + 1) + "0 коп.";
                }
                else
                {
                    TextBoxWholeSumm.Text = TextBoxWholeSumm.Text + "\nВСЬОГО       " + wholeSumm.ToString().Substring(0, wholeSumm.ToString().IndexOf(",")) + " грн. " + wholeSumm.ToString().Substring(wholeSumm.ToString().IndexOf(",") + 1) + " коп.";
                }
            }

        }
    }

    //рисуем счет
    private void DrawBill(bool addDate)
    {
        /*if (billBmp != null)
        {
            billBmp.Dispose();
        }*/
        string PictureLenthFloat = ((float)Session["PictureLenth"]).ToString();
        if (PictureLenthFloat.IndexOf(",") != -1)
        {
            PictureLenthFloat = PictureLenthFloat.Substring(0,PictureLenthFloat.IndexOf(","));
        }

        int PictureLenth = int.Parse(PictureLenthFloat) + 1;
        Bitmap billBmp = new Bitmap(782, PictureLenth);

        billBmp.SetResolution(300F, 300F);
        Graphics billGraphics = Graphics.FromImage(billBmp);
        Brush textBrush;
        Font arial6, arial8, arial12, arial14, cSCBook12, cSCBook11, cSCBook10, cGoth14;
        if (addDate)
        {
            textBrush = Brushes.Black;
            arial6 = new Font("Arial", 6, FontStyle.Regular);
            arial8 = new Font("Arial", 8, FontStyle.Regular);
            arial12 = new Font("Arial", 12, FontStyle.Regular);
            cSCBook12 = new Font("Century Schoolbook", 12, FontStyle.Regular);
            cSCBook11 = new Font("Century Schoolbook", 11, FontStyle.Regular);
            cSCBook10 = new Font("Century Schoolbook", 10, FontStyle.Regular);
            cGoth14 = new Font("Century Gothic", 14, FontStyle.Regular);

        }
        else
        {
            arial6 = new Font("Arial", 6, FontStyle.Bold);
            arial8 = new Font("Arial", 8, FontStyle.Bold);
            arial12 = new Font("Arial", 12, FontStyle.Bold);
            cSCBook12 = new Font("Century Schoolbook", 12, FontStyle.Bold);
            cSCBook11 = new Font("Century Schoolbook", 11, FontStyle.Bold);
            cSCBook10 = new Font("Century Schoolbook", 10, FontStyle.Bold);
            cGoth14 = new Font("Century Gothic", 14, FontStyle.Bold);
            textBrush = Brushes.Black;
        }
        Font arial12r = new Font("Arial", 12, FontStyle.Regular);
        arial14 = new Font("Arial", 14, FontStyle.Bold);
        Font arial12b = new Font("Arial", 12, FontStyle.Bold);
        Font arial8b = new Font("Arial", 8, FontStyle.Bold);
        RectangleF Place = new RectangleF(0F, 34.38F, 782F, 61.71F);
        StringFormat strFormat = new StringFormat();
        strFormat.Alignment = StringAlignment.Center;
        strFormat.LineAlignment = StringAlignment.Center;
        Pen linePen = new Pen(textBrush, 4.18F);
        //РАХУНОК
        billGraphics.DrawString("РАХУНОК", arial12, textBrush, Place, strFormat);
        //МАЙСТЕР
        //Place.Y = 182.14F;
        //billGraphics.DrawString("МАЙСТЕР", arial12, textBrush, Place, strFormat);
        //ЗНИЖКА 10%
        Place.Y = 300.41F;
        billGraphics.DrawString("НА ВСІ ПОСЛУГИ САЛОНУ", arial12, textBrush, Place, strFormat);
        //ДІЙСНА ДО
        Place.Y = Place.Y + 63.87F;
        billGraphics.DrawString("ЗНИЖКА 10% ДІЙСНА ДО", arial12, textBrush, Place, strFormat);
        //ДАТА ЗНИЖКА 10% ДІЙСНА ДО
        Place.Y = Place.Y + 63.87F;
        billGraphics.DrawString(Functions.AddZero(DateTime.Now.AddDays(30).Day.ToString()) + " / " + Functions.AddZero(DateTime.Now.AddDays(30).Month.ToString()) + " / " + DateTime.Now.AddDays(30).Year.ToString(), cSCBook12, textBrush, Place, strFormat);
        
        XmlDocument settingsXml = (XmlDocument)Session["settingsXml"];
        /*
        //САЛОН
        Place.Y = 300.41F;
        XmlDocument salonXml = (XmlDocument)Session["salonXml"];
        XmlNode node = salonXml.SelectSingleNode("/salon/item[@ID='" + settingsXml.SelectSingleNode("/settings/salon").Attributes["ID"].Value + "']");
            
        if (node == null)
        {
            MessageDisplay.DisplayMessage("Помилка", "Даний салон не існує\rПрограма закінчує працювати\r");
            //MessageBox.Show("Даний салон не існує\rПрограма закінчує працювати\r", "Помилка");
            Environment.Exit(0);
        }
        
        billGraphics.DrawString(node.Attributes["TYPE"].Value, arial12, textBrush, Place, strFormat);
        //"ЛОНДА"
        Place.Y = Place.Y + 56.83F;
        billGraphics.DrawString("ЛОНDА", cGoth14, textBrush, Place, strFormat);
        //Метро
        Place.Y = Place.Y + 56.83F;
        billGraphics.DrawString(node.Attributes["SUB"].Value, cSCBook11, textBrush, Place, strFormat);
        //адрес
        Place.Y = Place.Y + 56.83F;
        billGraphics.DrawString(node.Attributes["ADR"].Value, cSCBook11, textBrush, Place, strFormat);
        //телефон 1
        Place.Y = Place.Y + 56.83F;
        billGraphics.DrawString(node.Attributes["TEL1"].Value, cSCBook11, textBrush, Place, strFormat);
        //телефон 2
        Place.Y = Place.Y + 56.83F;
        billGraphics.DrawString(node.Attributes["TEL2"].Value, cSCBook11, textBrush, Place, strFormat);
        */

        //номер счета
        Place.Y = 94.80F;
        int billNumber = (int)Session["billNumber"];
        XmlElement currentWrkshXml = null;
        try { currentWrkshXml = (XmlElement)Session["currentWrkshXml"]; }
        catch { }
        if (addDate)
        {
            
            string printNum = billNumber.ToString() + DateTime.Now.Year + Functions.AddZero(DateTime.Now.Month) + Functions.AddZero(DateTime.Now.Day) + Functions.AddZero(DateTime.Now.Hour) + Functions.AddZero(DateTime.Now.Minute);
            billGraphics.DrawString(printNum, arial12, textBrush, Place, strFormat);
            currentWrkshXml.Attributes["ID"].Value = printNum;
            currentWrkshXml.Attributes["SALON"].Value = settingsXml.SelectSingleNode("/settings/salon").Attributes["ID"].Value;
            Session["currentWrkshXml"] = currentWrkshXml;
        }
        else
        {
            billGraphics.DrawString(billNumber.ToString(), arial12, textBrush, Place, strFormat);
        }
        //горизонтальные линии
        billGraphics.DrawLine(linePen, 0F, 154.41F, 782F, 154.41F);
        billGraphics.DrawLine(linePen, 0F, 290.41F, 782F, 290.41F);
        billGraphics.DrawLine(linePen, 0F, 492.02F, 782F, 492.02F);



        decimal wholeSumm = 0;
        decimal wholeSummWithoutDiscount = 0;
        float wholeSummY = 900.41F;
        if (currentWrkshXml != null)
        {
            //Фамилия и имя
            Place.Y = 168.86F;
            billGraphics.DrawString(currentWrkshXml.Attributes["NAME"].Value, cSCBook11, textBrush, Place, strFormat);

            // Звание и номер
            Place.Y = 225.30F;
            billGraphics.DrawString(GetRank(currentWrkshXml.Attributes["STAFFID"].Value) + " " + currentWrkshXml.Attributes["STAFFID"].Value, cSCBook10, textBrush, Place, strFormat);
            //Позиции прайс листа

            if (currentWrkshXml.HasChildNodes)
            {
                float nameX = 5.80F;
                float nameY = 502.02F;
                float nameW = 587.33F;
                float nameH;
                /*float priceW = 160.25F;
                float priceH = 40.57F;
                float priceX = 381.69F;
                float priceY;
                float discountX = 526.94F;
                float discountY;
                float discountW = 80F;
                float discountH = 40.57F;
                float summX = 590.94F;
                float summY;
                float summW = 191.06F;
                float summH = 72F;*/


                for (int i = 0; i < currentWrkshXml.ChildNodes.Count; i++)
                {
                    //Наименование позиции
                    int zeroCount;
                    string[] item = AllocateString(addZeroToCode(currentWrkshXml.ChildNodes[i].Attributes["ID"].Value, out zeroCount) + " " + currentWrkshXml.ChildNodes[i].Attributes["NAME"].Value);
                    //string[] item = AllocateString(currentWrkshXml.ChildNodes[i].Attributes["ID"].Value + " " + currentWrkshXml.ChildNodes[i].Attributes["NAME"].Value);
                    item[0] = item[0].Substring(zeroCount);
                    /*if (item.Count() < 3)
                    {
                        nameH = 40.57F * 3.5F / 1.5F;
                    }
                    else
                    {
                        nameH = 40.57F * item.Count();
                    }*/

                    nameH = 44.57F * (item.Count() + 2);
                    /*if ((nameY + nameH) > 1829.41F)
                    {
                        //удалить последнюю строку
                        DropLastItem();
                        Session["resultNode"] = null;
                        MessageBox messageBox = new MessageBox();
                        messageBox.MessageText = "Неможливо додати останню позицію\nНевистачає місця для друку\nНадрукуйте ще один рахунок";
                        messageBox.MessageTitle = "Увага";
                        messageBox.MessageButtons = MessageBox.MessageBoxButtons.Ok;
                        messageBox.MessageIcons = MessageBox.MessageBoxIcons.Warnning;
                        Literal1.Text = messageBox.Show(this);

                        //MessageDisplay.DisplayMessage("Неможливо додати останню позицію", "Невистачає місця для друку<br>Надрукуйте ще один рахунок", "~/staffonly/Default.aspx", 5);
                        //MessageBox.Show("Неможливо додати останню позицію\rНевистачає місця для друку\rНадрукуйте ще один рахунок\r", "Помилка");
                        break;
                    }*/
                    wholeSummY = nameY + nameH;
                    Place.X = nameX;
                    Place.Y = nameY;
                    Place.Width = nameW;
                    Place.Height = 44.57F;
                    strFormat.LineAlignment = StringAlignment.Center;
                    strFormat.Alignment = StringAlignment.Near;
                        
                    /*if (addDate)
                    {*/

                        //billGraphics.FillRectangle(Brushes.DarkOliveGreen, Place);
                        for (int f = 0; f < item.Count(); f++)
                        {
                            string[] temp = new string[1];
                            if (f == 0)
                            {

                                if (item[f].IndexOf(" ") == -1)
                                {
                                    temp[0] = item[f];
                                    Place.Width = 150.49F;
                                    DrawSpacedText(billGraphics, temp, arial12r, textBrush, Place, strFormat, 5F, 44.57F);
                                    Place.X = nameX;
                                    Place.Width = nameW;
                                }
                                else
                                {
                                    temp[0] = item[f].Substring(0, item[f].IndexOf(" "));
                                    Place.Width = 150.49F;
                                    DrawSpacedText(billGraphics, temp, arial12r, textBrush, Place, strFormat, 5F, 44.57F);
                                    Place.X = 150.49F;
                                    Place.Width = 436.84F;
                                    temp[0] = item[f].Substring(item[f].IndexOf(" ") + 1);
                                    DrawSpacedText(billGraphics, temp, arial12r, textBrush, Place, strFormat, 5F, 44.57F);
                                    Place.X = nameX;
                                    Place.Width = nameW;
                                }
                            }
                            else
                            {
                                temp[0] = item[f];
                                //billGraphics.FillRectangle(Brushes.AntiqueWhite, Place);
                                DrawSpacedText(billGraphics, temp, arial12r, textBrush, Place, strFormat, 5F, 44.57F);
                            }
                            Place.Y = Place.Y + 44.57F;
                        }
                        //DrawSpacedText(billGraphics, item, arial6, textBrush, Place, strFormat, 5F, 40.57F);

                    /*}
                    else
                    {
                        for (int f = 0; f < item.Count(); f++)
                        {
                            if (f == 0)
                            {
                                Place.Width = 90.49F;
                                billGraphics.DrawString(item[f].Substring(0, item[f].IndexOf(" ")), arial12r, textBrush, Place, strFormat);
                                Place.X = 90.49F;
                                Place.Width = 305.4F;
                                billGraphics.DrawString(item[f].Substring(item[f].IndexOf(" ") + 1), arial12r, textBrush, Place, strFormat);
                                Place.X = nameX;
                                Place.Width = nameW;
                            }
                            else
                            {
                                billGraphics.DrawString(item[f], arial8, textBrush, Place, strFormat);
                            }
                            Place.Y = Place.Y + 40.57F;
                        }
                        //billGraphics.DrawString(itemToDisplay, arial8, textBrush, Place, strFormat);
                    }*/
                    //strFormat.LineAlignment = StringAlignment.Center;
                    //strFormat.Alignment = StringAlignment.Center;

                    // Цена и количество + сумма без скидки
                    /*priceY = nameY + (nameH / 2) - (3.5F / 3F * priceH);
                    Place.X = priceX;
                    Place.Y = priceY;
                    Place.Width = priceW;
                    Place.Height = priceH;*/
                    
                    Place.Width = 200F;
                    //billGraphics.FillRectangle(Brushes.AntiqueWhite, Place);
                    billGraphics.DrawString(currentWrkshXml.ChildNodes[i].Attributes["QUANTITY"].Value + ".000", arial12r, textBrush, Place, strFormat);
                    Place.X = Place.X + 200F;
                    Place.Width = 40F;
                    //billGraphics.FillRectangle(Brushes.Blue, Place);
                    billGraphics.DrawString("X", arial12r, textBrush, Place, strFormat);
                    Place.X = Place.X + 50F;
                    Place.Width = 250.33F;
                    //billGraphics.FillRectangle(Brushes.Brown, Place);
                    billGraphics.DrawString(addZeroToSumm(currentWrkshXml.ChildNodes[i].Attributes["PRICE"].Value) + "=", arial12r, textBrush, Place, strFormat);
                    //Place.Y = Place.Y + priceH / 1.5F;
                    //billGraphics.FillRectangle(Brushes.Blue, Place);
                    //billGraphics.DrawString("=", arial8, textBrush, Place, strFormat);
                    //Place.Y = Place.Y + priceH / 1.5F;
                    Place.X = Place.X + 260.33F;
                    Place.Width = 255.87F;
                    //billGraphics.FillRectangle(Brushes.AntiqueWhite, Place);
                    decimal summWithoutDiscount = decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["PRICE"].Value) * decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["QUANTITY"].Value);
                    wholeSummWithoutDiscount = wholeSummWithoutDiscount + summWithoutDiscount;
                    strFormat.Alignment = StringAlignment.Far;
                    billGraphics.DrawString(addZeroToSumm((summWithoutDiscount).ToString()), arial12r, textBrush, Place, strFormat);


                    if (decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["PRICE"].Value) != decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["SELLPRICE"].Value))
                    {
                        // Знижка
                        /*discountY = nameY + (nameH / 2) - discountH / 2;
                        Place.X = discountX;
                        Place.Y = discountY;
                        Place.Width = discountW;
                        Place.Height = discountH;*/
                        Place.Y = Place.Y + 44.57F;
                        Place.X = nameX;
                        Place.Width = 500.33F;
                        //billGraphics.FillRectangle(Brushes.Coral, Place);
                        strFormat.Alignment = StringAlignment.Near;
                        billGraphics.DrawString("ЗНИЖКА: " + currentWrkshXml.ChildNodes[i].Attributes["DISCOUNT"].Value, arial12r, textBrush, Place, strFormat);
                        Place.X = Place.X + 510.33F;
                        Place.Width = 255.87F;
                        strFormat.Alignment = StringAlignment.Far;
                        billGraphics.DrawString("-" + addZeroToSumm((decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["PRICE"].Value) * decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["QUANTITY"].Value) - decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["SELLPRICE"].Value) * decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["QUANTITY"].Value)).ToString()), arial12r, textBrush, Place, strFormat);
                    }
                    
                    //Сумма 
                    /*summY = discountY - 15.72F;
                    Place.X = summX;
                    Place.Y = summY;
                    Place.Width = summW;
                    Place.Height = summH;
                    //billGraphics.FillRectangle(Brushes.AntiqueWhite, Place);
                    billGraphics.DrawString((decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["SELLPRICE"].Value) * decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["QUANTITY"].Value)).ToString(), arial12r, Brushes.Black, Place, strFormat);
                    */
                    wholeSumm = wholeSumm + (decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["SELLPRICE"].Value) * decimal.Parse(currentWrkshXml.ChildNodes[i].Attributes["QUANTITY"].Value));
                    
                    nameY = nameY + nameH + 10F;

                }
            }
        }/*
        //горизонтальная пунктирная линия с ножницами

        billGraphics.DrawLine(linePen, 136.22F, 806.41F, 182.67F, 806.41F);
        billGraphics.DrawLine(linePen, 187.67F, 806.41F, 234.13F, 806.41F);
        billGraphics.DrawLine(linePen, 239.13F, 806.41F, 285.58F, 806.41F);
        billGraphics.DrawLine(linePen, 290.58F, 806.41F, 337.04F, 806.41F);
        billGraphics.DrawLine(linePen, 342.04F, 806.41F, 388.5F, 806.41F);
        billGraphics.DrawLine(linePen, 393.5F, 806.41F, 439.95F, 806.41F);
        billGraphics.DrawLine(linePen, 444.95F, 806.41F, 491.41F, 806.41F);
        billGraphics.DrawLine(linePen, 496.41F, 806.41F, 542.86F, 806.41F);
        billGraphics.DrawLine(linePen, 547.86F, 806.41F, 594.32F, 806.41F);
        billGraphics.DrawLine(linePen, 599.32F, 806.41F, 645.78F, 806.41F);

        billGraphics.DrawLine(linePen, 650.78F, 806.41F, 697.24F, 806.41F);
        billGraphics.DrawLine(linePen, 702.24F, 806.41F, 748.7F, 806.41F);
        billGraphics.DrawLine(linePen, 753.7F, 806.41F, 782F, 806.41F);

        //ножницы левые
        Place.X = 0F;
        Place.Width = 200F;
        Place.Y = 746.41F;
        Place.Height = 100F;
        RectangleF scissorsRect = new RectangleF(0, 0, 136F, 68F);
        if (addDate)
        {
            billGraphics.DrawImage(LondaBillWeb.Properties.Resources.scissorsgray, Place, scissorsRect, GraphicsUnit.Pixel);
        }
        else
        {
            billGraphics.DrawImage(LondaBillWeb.Properties.Resources.scissors, Place, scissorsRect, GraphicsUnit.Pixel);
        }*/
        //горизонтальная линия
        billGraphics.DrawLine(linePen, 0F, wholeSummY, 782F, wholeSummY);
        //ВАРТІСТЬ
        Place.Y = wholeSummY + 10.56F;// 1956.97F;
        Place.X = 0F;
        Place.Width = 391F;
        Place.Height = 47.18F;
        strFormat.Alignment = StringAlignment.Near;
        billGraphics.DrawString("ВАРТІСТЬ", arial12r, textBrush, Place, strFormat);
        //Сумма ВАРТІСТЬ
        Place.X = 302F;
        Place.Width = 481F;
        strFormat.Alignment = StringAlignment.Far;
        if (wholeSummWithoutDiscount == 0)
        {
            billGraphics.DrawString("0 грн. 00 коп.", arial12r, textBrush, Place, strFormat);
        }
        else
        {
            if (wholeSummWithoutDiscount.ToString().IndexOf(",") == -1)
            {
                billGraphics.DrawString(wholeSummWithoutDiscount.ToString() + " грн. 00 коп.", arial12r, textBrush, Place, strFormat);
            }
            else
            {
                if (wholeSummWithoutDiscount.ToString().Substring(wholeSummWithoutDiscount.ToString().IndexOf(",") + 1).Length == 1)
                {
                    billGraphics.DrawString(wholeSummWithoutDiscount.ToString().Substring(0, wholeSummWithoutDiscount.ToString().IndexOf(",")) + " грн. " + wholeSummWithoutDiscount.ToString().Substring(wholeSummWithoutDiscount.ToString().IndexOf(",") + 1) + "0 коп.", arial12r, textBrush, Place, strFormat);
                }
                else
                {
                    billGraphics.DrawString(wholeSummWithoutDiscount.ToString().Substring(0, wholeSummWithoutDiscount.ToString().IndexOf(",")) + " грн. " + wholeSummWithoutDiscount.ToString().Substring(wholeSummWithoutDiscount.ToString().IndexOf(",") + 1) + " коп.", arial12r, textBrush, Place, strFormat);
                }
            }
        }
        //billGraphics.FillRectangle(Brushes.Blue, Place);
        wholeSummY = Place.Y + Place.Height + 5F;

        //горизонтальная линия
        billGraphics.DrawLine(linePen, 0F, wholeSummY, 782F, wholeSummY);
        //Знижка
        Place.Y = wholeSummY + 10.56F;// 1956.97F;
        Place.X = 0F;
        Place.Width = 391F;
        Place.Height = 47.18F;
        strFormat.Alignment = StringAlignment.Near;
        billGraphics.DrawString("ЗНИЖКА", arial12r, textBrush, Place, strFormat);
        //Сумма Знижки
        decimal summDiscount = wholeSummWithoutDiscount - wholeSumm;
        Place.X = 302F;
        Place.Width = 481F;
        strFormat.Alignment = StringAlignment.Far;
        if (summDiscount == 0)
        {
            billGraphics.DrawString("0 грн. 00 коп.", arial12r, textBrush, Place, strFormat);
        }
        else
        {
            if (summDiscount.ToString().IndexOf(",") == -1)
            {
                billGraphics.DrawString("-" + summDiscount.ToString() + " грн. 00 коп.", arial12r, textBrush, Place, strFormat);
            }
            else
            {
                if (summDiscount.ToString().Substring(summDiscount.ToString().IndexOf(",") + 1).Length == 1)
                {
                    billGraphics.DrawString("-" + summDiscount.ToString().Substring(0, summDiscount.ToString().IndexOf(",")) + " грн. " + summDiscount.ToString().Substring(summDiscount.ToString().IndexOf(",") + 1) + "0 коп.", arial12r, textBrush, Place, strFormat);
                }
                else
                {
                    billGraphics.DrawString("-" + summDiscount.ToString().Substring(0, summDiscount.ToString().IndexOf(",")) + " грн. " + summDiscount.ToString().Substring(summDiscount.ToString().IndexOf(",") + 1) + " коп.", arial12r, textBrush, Place, strFormat);
                }
            }
        }
        //billGraphics.FillRectangle(Brushes.Blue, Place);
        wholeSummY = Place.Y + Place.Height + 5F;

        //горизонтальная линия
        billGraphics.DrawLine(linePen, 0F, wholeSummY, 782F, wholeSummY);
        //"ВСЬОГО ДО СПЛАТИ"
        Place.Y = wholeSummY + 18.56F;// 1956.97F;
        Place.X = 0F;
        Place.Width = 391F;
        Place.Height = 57.18F;
        strFormat.Alignment = StringAlignment.Near;
        billGraphics.DrawString("ДО СПЛАТИ", arial14, textBrush, Place, strFormat);
        Place.Y = Place.Y + 57.18F;
        billGraphics.DrawString("ВСЬОГО", arial14, textBrush, Place, strFormat);

        //сумма всего
        Place.X = 252F;
        Place.Width = 531F;
        Place.Y = wholeSummY + 18.56F + 57.18F;
        Place.Height = 57.18F;
        strFormat.Alignment = StringAlignment.Far;
        //billGraphics.FillRectangle(Brushes.Blue, Place);
        if (wholeSumm == 0)
        {
            billGraphics.DrawString("0 грн. 00 коп.", arial12r, Brushes.Black, Place, strFormat);
            
        }
        else
        {

            if (wholeSumm.ToString().IndexOf(",") == -1)
            {
                billGraphics.DrawString(wholeSumm.ToString() + " грн. 00 коп.", arial12r, Brushes.Black, Place, strFormat);
            }
            else
            {
                if (wholeSumm.ToString().Substring(wholeSumm.ToString().IndexOf(",") + 1).Length == 1)
                {
                    billGraphics.DrawString(wholeSumm.ToString().Substring(0, wholeSumm.ToString().IndexOf(",")) + " грн. " + wholeSumm.ToString().Substring(wholeSumm.ToString().IndexOf(",") + 1) + "0 коп.", arial12r, Brushes.Black, Place, strFormat);
                }
                else
                {
                    billGraphics.DrawString(wholeSumm.ToString().Substring(0, wholeSumm.ToString().IndexOf(",")) + " грн. " + wholeSumm.ToString().Substring(wholeSumm.ToString().IndexOf(",") + 1) + " коп.", arial12r, Brushes.Black, Place, strFormat);
                }
            }

        }
        
            
        strFormat.Alignment = StringAlignment.Center;
        /*//Ціна
        Place.X = 396.69F;
        Place.Y = 837.12F;
        Place.Width = 102.75F;
        Place.Height = 41.15F;
        billGraphics.DrawString("ЦІНА", arial8, textBrush, Place, strFormat);
        //Знижка
        Place.X = 499.44F;
        Place.Y = 844.27F;
        Place.Width = 120.77F;
        Place.Height = 30.86F;
        billGraphics.DrawString("ЗНИЖКА", arial6, textBrush, Place, strFormat);
        //Сума
        Place.X = 620.21F;
        Place.Y = 837.12F;
        Place.Width = 131.79F;
        Place.Height = 41.15F;
        billGraphics.DrawString("СУМА", arial8, textBrush, Place, strFormat);
        */
        billGraphics.Dispose();

        //Вывод на экран рисунка
        Bitmap billToMonitor = new Bitmap(281, 750);
        billToMonitor.SetResolution(300F, 300F);
        RectangleF destRectangle = new RectangleF(0F, 0F, 281F, 750F);
        RectangleF sourceRectangle = new RectangleF(0F, 0F, 782F, PictureLenth);
        Graphics billToMonitorGraphics = Graphics.FromImage(billToMonitor);
        billToMonitorGraphics.DrawImage(billBmp, destRectangle, sourceRectangle, GraphicsUnit.Pixel);
        billToMonitorGraphics.Dispose();
        PictureLenthFloat = (PictureLenth / 1.379).ToString();
        if (PictureLenthFloat.IndexOf(",") != -1)
        {
            PictureLenthFloat = PictureLenthFloat.Substring(0, PictureLenthFloat.IndexOf(","));
        }
        PictureLenth = int.Parse(PictureLenthFloat);
        Bitmap billToPrint = new Bitmap(567, PictureLenth);
        billToPrint.SetResolution(300F, 300F);
        destRectangle = new RectangleF(0F, 0F, 567F, PictureLenth);
        billToMonitorGraphics = Graphics.FromImage(billToPrint);
        billToMonitorGraphics.DrawImage(billBmp, destRectangle, sourceRectangle, GraphicsUnit.Pixel);
        billToMonitorGraphics.Dispose();
        Session["billBmp"] = billToPrint;
        ImageConverter converter = new ImageConverter();
        //Session["billToMonitor"] = converter.ConvertTo(billToMonitor, typeof(byte[]));
        Session["billToMonitor"] = converter.ConvertTo(billBmp, typeof(byte[]));
        //ImageBill.ImageUrl = "~/staffonly/ImageHandler1.ashx";
        //pictureBoxBill.Image = billToMonitor;
    }

    //Добавляем ноли к коду товара
    private string addZeroToCode(string input, out int count)
    {
        string result;
        switch (input.Length)
        {
            case 1: result = "000" + input;
                count = 3;
                break;
            case 2: result = "00" + input;
                count = 2;
                break;
            case 3: result = "0" + input;
                count = 1;
                break;
            default: result = input;
                count = 0;
                break;
        }
        return result;
    }

    private string addZeroToSumm(string input)
    {
        string result;
        int y = input.IndexOf(","); 
        if (y == -1)
        {
            result = input + ".00";
        }
        else
        {
            if (input.Substring(y).Length == 2)
            {
                result = input.Replace(",", ".") + "0";
            }
            else
            {
                result = input.Replace(",", ".");
            }
        }
        return result;
    }

    //Переносим слова на разные строчки
    private string[] AllocateString(string input)
    {
        string[] result = new string[1];
        string[] newLine = new string[1];
        string Code = "";

        //Убираем лишние пробелы
        int startIntSpace = 0;
        Char space = new Char();
        space = (" ").ToCharArray()[0];
        bool textStarted = false;
        string text = null;
        for (int i = 0; i < input.Length; i++)
        {

            if (input[i] == space & textStarted)
            {
                if (startIntSpace == 0)
                {
                    text = input.Substring(startIntSpace, i);
                }
                else
                {
                    text = text + " " + input.Substring(startIntSpace, i - startIntSpace);
                }
                startIntSpace = i + 1;
                textStarted = false;
            }
            else
            {
                if (input[i] != space)
                {
                    textStarted = true;
                    if (i == input.Length - 1)
                    {
                        text = text + " " + input.Substring(startIntSpace);
                    }

                }
                else
                {
                    startIntSpace = startIntSpace + 1;
                }
            }
        }
        input = text;

        //Разбиваем на строки
        if (input.Length > 19)
        {
            bool start = true;
            while (input.Length > 19)
            {
                int startIndex = 0;
                if (input.IndexOf(" ", startIndex) > 19 | input.IndexOf(" ", startIndex) == -1)
                {
                    input = Code + " В назві присутнє слово більше 19 символів в довжину";
                    result = new string[1];
                    start = true;
                }

                while (input.IndexOf(" ", startIndex) < 20 & input.IndexOf(" ", startIndex) != -1)
                {
                    if (start)
                    {

                        if (startIndex == 0)
                        {
                            result[0] = input.Substring(startIndex, input.IndexOf(" ", startIndex));
                            Code = result[0];
                        }
                        else
                        {
                            result[0] = result[0] + " " + input.Substring(startIndex, input.IndexOf(" ", startIndex) - startIndex);
                        }
                    }
                    else
                    {
                        if (startIndex == 0)
                        {

                            newLine[0] = input.Substring(startIndex, input.IndexOf(" ", startIndex));
                        }
                        else
                        {
                            newLine[0] = newLine[0] + " " + input.Substring(startIndex, input.IndexOf(" ", startIndex) - startIndex);
                        }
                    }

                    startIndex = input.IndexOf(" ", startIndex) + 1;
                }
                if (!start)
                {
                    result = result.Concat(newLine).ToArray();
                }
                start = false;

                input = input.Substring(startIndex);
            }
            newLine[0] = input;
            result = result.Concat(newLine).ToArray();
        }
        else
        {
            result[0] = input;
        }
        return result;
    }

    // Перевод звания из цифры в надпись
    private string GetRank(string inputData)
    {
        XmlDocument staffXml = (XmlDocument)Session["staffXml"];
        XmlNode node = staffXml.SelectSingleNode(("/staff/worker[@ID='" + inputData + "']"));
        if (node != null)
        {

            if (node.Attributes["LEVEL"].Value == "0")
            {
                node = staffXml.SelectSingleNode(("/staff/profession[@ID='" + node.Attributes["PROF"].Value + "']"));
                if (node != null)
                {
                    return node.Attributes["NAME"].Value;
                }
                else
                {
                    return "Немає даних";
                }
            }
            else
            {
                node = staffXml.SelectSingleNode(("/staff/rank[@ID='" + node.Attributes["LEVEL"].Value + "']"));
                if (node != null)
                {
                    return node.Attributes["NAME"].Value;
                }
                else
                {
                    return "Немає даних";
                }
            }
        }
        else
        {
            return "Немає даних";
        }
    }

    protected void buttonDelLast_Click(object sender, EventArgs e)
    {
        DropLastItem();
        //DrawBill(false);
        BillToMonitor();
    }
    //Удаляем последнюю строку позиции прайс листа
    private void DropLastItem()
    {
        XmlElement currentWrkshXml = null;
        try
        {
            currentWrkshXml = (XmlElement)Session["currentWrkshXml"];
        }
        catch
        { }
        if (currentWrkshXml != null)
        {
            if (currentWrkshXml.HasChildNodes)
            {
                currentWrkshXml.RemoveChild(currentWrkshXml.LastChild);
            }
            Session["currentWrkshXml"] = currentWrkshXml;
            if (!currentWrkshXml.HasChildNodes & buttonPrint.Text == "Надрукувати")
            {
                buttonPrint.Enabled = false;
                labelHint.Text = "Введіть код позиції прайс-листа";
            }
        }
    }

    protected void buttonPrint_Click(object sender, EventArgs e)
    {
        if (buttonPrint.Text == "Обрати майстра")
        {
            Session["MasterOrItemOrQuantity"] = "Master";
            labelHint.Text = "Введіть код майстра";
            buttonSave.BackColor = Color.Red;
            DisplayDataToChoose();
        }
        else
        {
            DrawBill(true);
            PrintDocument printBill = (PrintDocument)Session["printBill"];
            //LogFile.WriteToLogFile(Server.MapPath("~/App_Data/debug.log"), "Перед printBill.Print()");
            printBill.Print();
            /*try
            {
                printBill.Print();
            }
            catch (Exception ex)
            {
            
                MessageDisplay.DisplayMessage("Error", "Data: " + ex.Data + "/n " + "HelpLink: " + ex.HelpLink + "/n " + "InnerException: " + ex.InnerException + "/n " + "Message: " + ex.Message + "/n " + "Source: " + ex.Source + "/n " + "StackTrace: " + ex.StackTrace + "/n " + "TargetSite: " + ex.TargetSite, "~/staffonly/Default.aspx",30);
            
            }*/
            int checkCount = 0;
        STARTSAVE:
            XmlDocument worksheetsXml = (XmlDocument)Session["worksheetsXml"];
            XmlElement currentWrkshXml = (XmlElement)Session["currentWrkshXml"];
            if (checkCount < 3)
            {
                currentWrkshXml.SetAttribute("LINES", currentWrkshXml.ChildNodes.Count.ToString());
                worksheetsXml.DocumentElement.AppendChild(currentWrkshXml);
                if (File.Exists((string)Session["wrkshPath"] + "\\check" + currentWrkshXml.Attributes["ID"].Value + ".xml"))
                {
                    File.Delete((string)Session["wrkshPath"] + "\\check" + currentWrkshXml.Attributes["ID"].Value + ".xml");
                }
                worksheetsXml.Save((string)Session["wrkshPath"] + "\\check" + currentWrkshXml.Attributes["ID"].Value + ".xml");

                #region Проверка записи данных в файл
                XmlDocument checkXml = new XmlDocument();
                checkXml.Load((string)Session["wrkshPath"] + "\\check" + currentWrkshXml.Attributes["ID"].Value + ".xml");
                XmlNodeList checkNodeList = checkXml.SelectNodes("//wrksh/WORKSHEET");
                if (checkNodeList.Count > 1 | checkNodeList.Count == 0)
                {
                    checkCount = checkCount + 1;
                    goto STARTSAVE;
                }
                if (checkNodeList[0].ChildNodes.Count.ToString() != checkNodeList[0].Attributes["LINES"].Value)
                {
                    checkCount = checkCount + 1;
                    goto STARTSAVE;
                }
                decimal etalon = 0;
                for (int x = 0; x < currentWrkshXml.ChildNodes.Count; x++)
                {
                    etalon = etalon + (decimal.Parse(currentWrkshXml.ChildNodes[x].Attributes["SELLPRICE"].Value) * decimal.Parse(currentWrkshXml.ChildNodes[x].Attributes["QUANTITY"].Value));
                }
                decimal checkSumm = 0;
                for (int z = 0; z < currentWrkshXml.ChildNodes.Count; z++)
                {
                    checkSumm = checkSumm + (decimal.Parse(checkNodeList[0].ChildNodes[z].Attributes["SELLPRICE"].Value) * decimal.Parse(checkNodeList[0].ChildNodes[z].Attributes["QUANTITY"].Value));
                }
                if (etalon != checkSumm)
                {
                    checkCount = checkCount + 1;
                    goto STARTSAVE;
                }
                File.Move((string)Session["wrkshPath"] + "\\check" + currentWrkshXml.Attributes["ID"].Value + ".xml", (string)Session["wrkshPath"] + "\\wrksh" + currentWrkshXml.Attributes["ID"].Value + ".xml");
            }
            else
            {
                currentWrkshXml.SetAttribute("LINES", "ERROR1");
                worksheetsXml.DocumentElement.AppendChild(currentWrkshXml);
                worksheetsXml.Save((string)Session["wrkshPath"] + "\\wrksh" + currentWrkshXml.Attributes["ID"].Value + ".xml");
            }
            
#endregion

            EverythingToBegining();
        }
    }
   
    //Печать рисунка
    private void printBill_PrintPage(object sender, PrintPageEventArgs e)
    {
        e.Graphics.DrawImage((Bitmap)Session["billBmp"], 0, 0);        
    }

    //Рисование текста с интервалами между буквами
    public void DrawSpacedText(Graphics g, string[] text, Font font, Brush brush, RectangleF rectangle, StringFormat strFormat, float desiredWidth, float strHight)
    {
        float lineY = rectangle.Y;
        if (strFormat.LineAlignment == StringAlignment.Center)
        {
            lineY = (rectangle.Y + rectangle.Height / 2) - (strHight * text.Count() / 2);
        }

        for (int z = 0; z < text.Count(); z++)
        {
            CharacterRange[] ranges = null;
            for (int i = 0; i < text[z].Length; i++)
            {

                if (i == 0)
                {
                    ranges = new CharacterRange[] { new CharacterRange(i, 1) };
                }
                else
                {
                    ranges = ranges.Concat(new CharacterRange[] { new CharacterRange(i, 1) }).ToArray();
                }
            }



            strFormat.SetMeasurableCharacterRanges(ranges);
            //Calculate spacing
            float widthNeeded = 0;
            int intChar, intLines;
            SizeF sizeStr = g.MeasureString(text[z], font, rectangle.Size, strFormat, out intChar, out intLines);
            Region[] regions = g.MeasureCharacterRanges(text[z], font, rectangle, strFormat);

            float[] widths = new float[regions.Length];
            for (int i = 0; i < widths.Length; i++)
            {
                widths[i] = regions[i].GetBounds(g).Width;
                widthNeeded += widths[i];
            }
            float spacing = desiredWidth;

            //draw text
            float indent = 0;
            int index = 0;
            RectangleF place = new RectangleF();
            foreach (char c in text[z])
            {
                place.X = rectangle.X + indent;
                place.Y = lineY;
                place.Width = widths[index] + spacing;
                place.Height = strHight;
                g.DrawString(c.ToString(), font, brush, place, strFormat);
                indent += widths[index] + spacing;
                index++;
            }


            lineY = lineY + strHight;
        }
    }

     
    
   
    
    }
}
