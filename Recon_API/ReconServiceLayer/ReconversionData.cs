using iTextSharp.text;
using iTextSharp.text.pdf;
using ReconModels;
using System.Data;
using static ReconModels.ReconVersionmodel;
using Font = iTextSharp.text.Font;
using System.Globalization;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ReconDataLayer
{
    public class ReconversionData
    {
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        DBManager dbManager = new DBManager("ConnectionString");
        List<IDbDataParameter>? parameters;
        public DataSet ReconVersionfetchdata(ReconVersionmodellist Objmodel, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_recon_code", Objmodel.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_role_code", headerval.role_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
                ds = dbManager.execStoredProcedure("pr_fetch_reconversion", CommandType.StoredProcedure, parameters.ToArray());
                if (ds.Tables.Count >= 4)
                {
                    ds.Tables[0].TableName = "versionlist";
                    ds.Tables[1].TableName = "Reconversion";
                    ds.Tables[2].TableName = "theme";
                    ds.Tables[3].TableName = "preprocess";
                }
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_fetch_reconversion" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(Objmodel), "pr_fetch_reconversion", headerval.user_code, constring);
                return ds;
            }
        }
        public DataSet ReconVersionhistorydata(ReconVersionhsitorylist Objmodel, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_recon_code", Objmodel.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_version_code", Objmodel.in_version_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_role_code", headerval.role_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
                ds = dbManager.execStoredProcedure("pr_fetch_reconversionhistory", CommandType.StoredProcedure, parameters.ToArray());
                ds.Tables[0].TableName = "versionlist";
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_fetch_reconversionhistory" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(Objmodel), "pr_fetch_reconversionhistory", headerval.user_code, constring);
                return ds;
            }
        }
        public DataTable ReconVersionsavedata(ReconVersionmodelsave Objmodel, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_recon_code", Objmodel.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_rule_code", Objmodel.in_rule_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_theme_code", Objmodel.in_theme_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_preprocess_code", Objmodel.in_preprocess_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reconrule_version", Objmodel.in_reconrule_version, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_set_reconrule_version", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_set_reconrule_version" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(Objmodel), "pr_set_reconrule_version", headerval.user_code, constring);
                return result;
            }
        }
        private PdfPTable PdfdynamicTableGenration(DataTable dt)
        {
            PdfPTable table = new PdfPTable(dt.Columns.Count) { WidthPercentage = 100 };
            BaseColor borderColor = new BaseColor(217, 222, 227);
            float[] columnWidths = new float[dt.Columns.Count];
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string colName = dt.Columns[i].ColumnName;
                if (colName == "index_order")
                {
                    continue;
                }
                string widthValue = dt.Rows[0][i].ToString().Trim();
                if (widthValue.EndsWith("f", StringComparison.OrdinalIgnoreCase))
                {
                    string numericPart = widthValue.Substring(0, widthValue.Length - 1);
                    if (float.TryParse(numericPart, NumberStyles.Any, CultureInfo.InvariantCulture, out float parsedWidth))
                    {
                        if (parsedWidth == 0)
                        {
                            columnWidths[i] = 10f;
                        }
                        else
                        {
                            columnWidths[i] = parsedWidth;
                        }
                    }
                    else
                    {
                        columnWidths[i] = 50f;
                    }

                }
                else
                {
                    if (float.TryParse(widthValue, NumberStyles.Any, CultureInfo.InvariantCulture, out float parsedWidth))
                    {
                        if (parsedWidth == 0)
                        {
                            columnWidths[i] = 10f;
                        }
                        else
                        {
                            columnWidths[i] = parsedWidth;
                        }
                    }
                    else
                    {
                        columnWidths[i] = 50f;
                    }
                }
                string columnName = dt.Columns[i].ColumnName;
                if (columnName == "-")
                {
                    columnName = " ";
                }
                else if (columnName == "--")
                {
                    columnName = "  ";
                }
                if (columnName != "index_order")
                {
                    Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.WHITE);
                    table.AddCell(CreateHeaderCell(columnName, headerFont, new BaseColor(70, 130, 180)));
                }
            }
            table.SetWidths(columnWidths);
            foreach (DataRow row in dt.Rows.Cast<DataRow>().Skip(1))
            {
                foreach (DataColumn column in dt.Columns)
                {
                    Font dataFont = FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.BLACK);

                    PdfPCell cell = new PdfPCell(new Phrase(row[column].ToString(), dataFont))
                    {
                        BorderColor = borderColor
                    };
                    if (column.DataType == typeof(string))
                    {
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    }
                    else if (column.DataType == typeof(int) || column.DataType == typeof(float) || column.DataType == typeof(double) || column.DataType == typeof(decimal))
                    {
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    table.AddCell(cell);
                }
            }
            return table;
        }
        private PdfPCell CreateLabelCell(string labelText, bool isRequired)
        {
            Font font = new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL);
            Chunk labelChunk = new Chunk(labelText, font);
            Phrase labelPhrase = new Phrase(labelChunk);
            PdfPCell labelCell = new PdfPCell(labelPhrase)
            {
                Border = PdfPCell.NO_BORDER,
            };
            return labelCell;
        }
        private PdfPCell CreateHeaderCell(string text, Font font, BaseColor backgroundColor)
        {
            PdfPCell headerCell = new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = backgroundColor,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                Padding = 5,
                BorderColor = new BaseColor(217, 222, 227)
            };
            return headerCell;
        }
        private Paragraph CreateMainTitle1(string title)
        {
            iTextSharp.text.Font boldFont = new iTextSharp.text.Font();
            boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10); //, iTextSharp.text.Font.BOLD);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            Chunk headingChunk = new Chunk(title.ToUpper(), boldFont);
            headingChunk.SetUnderline(0.5f, -1.5f);
            prgHeading.Add(headingChunk);
            Paragraph spaceAfter = new Paragraph(" ", boldFont);
            spaceAfter.SpacingBefore = 4f;
            prgHeading.Add(spaceAfter);
            return prgHeading;
        }
        private Paragraph CreateTitle(string title, BaseColor dynamicColor)
        {
            iTextSharp.text.Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
            Paragraph prgHeading = new Paragraph("", boldFont)
            {
                Alignment = Element.ALIGN_LEFT,
                SpacingAfter = 6f
            };

            string camelCaseTitle = ToInitialCaps(title);
            Chunk headingChunk = new Chunk(camelCaseTitle, boldFont);
            PdfPCell cell = new PdfPCell(new Phrase(headingChunk));
            cell.BackgroundColor = dynamicColor;
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingBottom = 3;
            cell.Bottom = 4;

            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;
            table.AddCell(cell);
            prgHeading.Add(table);
            return prgHeading;
        }
        public static string ToInitialCaps(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return title;
            }

            TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
            return textInfo.ToTitleCase(title.ToLowerInvariant());
        }
        public byte[] ReconReportVersionhistoryData(ReconReportVersionhistoryModel objReconReportVersionhistory, UserManagementModel.headerValue headerval, string constring, string logoPath)
        {
            int lineNumber = 0;
            try
            {
                string[] alphabet = new string[] { ".1", ".2", ".3", ".4", ".5", ".6", ".7", ".8", ".9", ".10", ".11", ".12", ".13", ".14", ".15", ".16", ".17", ".18", ".19", ".20", ".21", ".22", ".23", ".24", ".25", ".26", ".27", ".28", ".29", ".30", ".31", ".32", ".33", ".34", ".35", ".36" };
                string tab1 = "     ";
                string tab2 = "          ";
                float fh = 8f;
                BaseColor mustardYellow = new BaseColor(235, 147, 22);
                BaseColor grassGreen = new BaseColor(0, 102, 0);
                BaseColor reconPurpple = new BaseColor(135, 46, 123);
                BaseColor ironbrown = new BaseColor(153, 0, 0);
                DBManager dbManager = new DBManager(constring);
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_recon_code", objReconReportVersionhistory.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_version_code", objReconReportVersionhistory.in_version_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_role_code", headerval.role_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
                ds = dbManager.execStoredProcedure("pr_report_reconversionhistory", CommandType.StoredProcedure, parameters.ToArray());
                if (ds.Tables.Count >= 1)
                {
                    ds.Tables[0].TableName = "Recondetail";
                    ds.Tables[1].TableName = "processlist";
                    ds.Tables[2].TableName = "list";
					ds.Tables[3].TableName = "Dataset";
					ds.Tables[4].TableName = "Datasetorder";
					ds.Tables[5].TableName = "Datasetmapping";
				}
                MemoryStream ms = new MemoryStream();
                Rectangle rec = new Rectangle(PageSize.A4);
                //Dictionary<string, int> tocEntries = new Dictionary<string, int>();
                List<TOCEntry> tocEntries = new List<TOCEntry>();
                using (Document document = new Document(rec, 30f, 30f, 30f, 30f))
                {
                    // Dynamic Table data
                    DataTable dt1 = new DataTable();
                    DataTable dt2 = new DataTable();
                    DataTable dt3 = new DataTable();
					DataTable dt4 = new DataTable();
					DataTable dt5 = new DataTable();
					DataTable dt6 = new DataTable();
					dt1 = ds.Tables["Recondetail"];
                    dt2 = ds.Tables["processlist"];
                    dt3 = ds.Tables["list"];
					dt4 = ds.Tables["Dataset"];
					dt5 = ds.Tables["Datasetorder"];
					dt6 = ds.Tables["Datasetmapping"];

					// Track headings and their page numbers
					lineNumber = 316;
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);
                    // writer.PageEvent = new PdfFooter(logoPath);
                    document.Open();
                    BaseColor borderColor = new BaseColor(169, 169, 169);
                    lineNumber = 320;
                    /* Header Start */

                    // Input field
                    PdfPTable mainTable = new PdfPTable(3) { WidthPercentage = 100 };
                    mainTable.SetWidths(new float[] { 10f, 5f, 10f });
                    //// Title
                    string recon_name = dt1.Rows[0]["Recon Name"].ToString();
                    document.Add(CreateMainTitle1(recon_name + " - " + "Version History - " + objReconReportVersionhistory.in_version_code));
                    //tocEntries.Add(new TOCEntry("Content", "Page"));
                    //tocEntries.Add(new TOCEntry(recon_name + " - " + "Version History - " + objReconReportVersionhistory.in_version_code, writer.PageNumber));
                    //lineNumber = 332;
                    // Recon Code Column
                    PdfPTable reconCodeTable = new PdfPTable(1);
                    reconCodeTable.AddCell(CreateLabelCell("Recon Code : " + dt1.Rows[0]["Recon Code"], true));
                    lineNumber = 336;
                    PdfPCell reconCodeCell = new PdfPCell(reconCodeTable)
                    {
                        Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                        BackgroundColor = new BaseColor(230, 230, 250),
                        BorderColor = borderColor
                    };
                    mainTable.AddCell(reconCodeCell);
                    // Add an empty blank space
                    mainTable.AddCell(new PdfPCell(new Phrase("")) { Border = PdfPCell.NO_BORDER });
                    // Recon Name Column
                    PdfPTable reconNameTable = new PdfPTable(1);
                    reconNameTable.AddCell(CreateLabelCell("Recon Name : " + dt1.Rows[0]["Recon Name"], true));
                    PdfPCell reconNameCell = new PdfPCell(reconNameTable)
                    {
                        Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                        BackgroundColor = new BaseColor(230, 230, 250),
                        BorderColor = borderColor
                    };
                    mainTable.AddCell(reconNameCell);
                    // Empty Line
                    PdfPCell blankCell = new PdfPCell(new Phrase(" "))
                    {
                        Colspan = 3,
                        FixedHeight = 8f, // Adjust height as needed
                        Border = PdfPCell.NO_BORDER
                    };
                    mainTable.AddCell(blankCell);
                    // History Version Column
                    PdfPTable historyVersionTable = new PdfPTable(1);
                    historyVersionTable.AddCell(CreateLabelCell("History Version : " + objReconReportVersionhistory.in_version_code, true));
                    PdfPCell historyVersionCell = new PdfPCell(historyVersionTable)
                    {
                        Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                        BackgroundColor = new BaseColor(230, 230, 250),
                        BorderColor = borderColor
                    };
                    mainTable.AddCell(historyVersionCell);
                    // Add an empty blank space
                    mainTable.AddCell(new PdfPCell(new Phrase("")) { Border = PdfPCell.NO_BORDER });
                    // Recon Type Column
                    PdfPTable reconTypeTable = new PdfPTable(1);
                    reconTypeTable.AddCell(CreateLabelCell("Recon Type : " + dt1.Rows[0]["Recon Type"], true));
                    PdfPCell reconTypeCell = new PdfPCell(reconTypeTable)
                    {
                        Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                        BackgroundColor = new BaseColor(230, 230, 250),
                        BorderColor = borderColor
                    };
                    mainTable.AddCell(reconTypeCell);
                    // Empty Line
                    PdfPCell blankCell1 = new PdfPCell(new Phrase(" "))
                    {
                        Colspan = 3,
                        FixedHeight = fh,
                        Border = PdfPCell.NO_BORDER
                    };
                    mainTable.AddCell(blankCell1);
                    // Period From Column
                    PdfPTable periodFromTable = new PdfPTable(1);
                    periodFromTable.AddCell(CreateLabelCell("Active From : " + dt1.Rows[0]["Period From"], true));
                    PdfPCell periodFromTypeCell = new PdfPCell(periodFromTable)
                    {
                        Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                        BackgroundColor = new BaseColor(230, 230, 250),
                        BorderColor = borderColor
                    };
                    mainTable.AddCell(periodFromTypeCell);
                    // Add an empty blank space
                    mainTable.AddCell(new PdfPCell(new Phrase("")) { Border = PdfPCell.NO_BORDER });
                    // Period To Column
                    if (dt1.Rows[0]["until_active_flag"].ToString() == "YES")
                    {
                        PdfPTable periodToTable = new PdfPTable(1);
                        periodToTable.AddCell(CreateLabelCell("Active To : " + "Until Inactive", true));
                        PdfPCell periodToTypeCell = new PdfPCell(periodToTable)
                        {
                            Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                            BackgroundColor = new BaseColor(230, 230, 250),
                            BorderColor = borderColor
                        };
                        mainTable.AddCell(periodToTypeCell);
                    }
                    else
                    {
                        PdfPTable periodToTable = new PdfPTable(1);
                        periodToTable.AddCell(CreateLabelCell("Active To : " + dt1.Rows[0]["Period To"], true));

                        PdfPCell periodToTypeCell = new PdfPCell(periodToTable)
                        {
                            Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                            BackgroundColor = new BaseColor(230, 230, 250),
                            BorderColor = borderColor
                        };
                        mainTable.AddCell(periodToTypeCell);
                    }
                    // Adding Blank Space
                    PdfPCell blankCell2 = new PdfPCell(new Phrase(" "))
                    {
                        Colspan = 3,
                        FixedHeight = fh, // Adjust height as needed
                        Border = PdfPCell.NO_BORDER
                    };
                    mainTable.AddCell(blankCell2);

                    // Recon Closure Date
                    PdfPTable reconClosureTable = new PdfPTable(1);
                    reconClosureTable.AddCell(CreateLabelCell("Recon Closure Date : " + dt1.Rows[0]["Recon Closure Date"], true));
                    PdfPCell reconClosureCell = new PdfPCell(reconClosureTable)
                    {
                        Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                        BackgroundColor = new BaseColor(230, 230, 250),
                        BorderColor = borderColor
                    };
                    mainTable.AddCell(reconClosureCell);

                    // Add an empty blank space
                    mainTable.AddCell(new PdfPCell(new Phrase("")) { Border = PdfPCell.NO_BORDER });

                    // Recon Cycle Date
                    PdfPTable reconCycleTable = new PdfPTable(1);
                    reconCycleTable.AddCell(CreateLabelCell("Recon Cycle Date : " + dt1.Rows[0]["Recon Cycle Date"], true));
                    PdfPCell reconcycleCell = new PdfPCell(reconCycleTable)
                    {
                        Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                        BackgroundColor = new BaseColor(230, 230, 250),
                        BorderColor = borderColor
                    };
                    mainTable.AddCell(reconcycleCell);

                    PdfPCell blankCell3 = new PdfPCell(new Phrase(" "))
                    {
                        Colspan = 3,
                        FixedHeight = fh,
                        Border = PdfPCell.NO_BORDER
                    };
                    mainTable.AddCell(blankCell3);
                    PdfPCell blankCell4 = new PdfPCell(new Phrase(" "))
                    {
                        Colspan = 3,
                        FixedHeight = fh,
                        Border = PdfPCell.NO_BORDER
                    };
                    mainTable.AddCell(blankCell4);

                    PdfPCell blankCell5 = new PdfPCell(new Phrase(" "))
                    {
                        Colspan = 3,
                        FixedHeight = fh, // Adjust height as needed
                        Border = PdfPCell.NO_BORDER
                    };
                    mainTable.AddCell(blankCell5);

                    document.Add(mainTable);
                    /* Header Ends */

                    /* Table Starts PreProcess */
                    if (dt3.Rows.Count > 1)
                    {
                        lineNumber = 484;
                        string mainTitle = "Process List";
                        document.Add(CreateTitle("Process List", mustardYellow));
                        document.Add(PdfdynamicTableGenration(dt3));
                        PdfPTable spacerTableAfter = new PdfPTable(1);
                        PdfPCell spacerCellAfter = new PdfPCell(new Phrase(" "))
                        {
                            FixedHeight = fh,
                            Border = PdfPCell.NO_BORDER
                        };
                        spacerTableAfter.AddCell(spacerCellAfter);
                        document.Add(spacerTableAfter);
                    }
                    document.NewPage();
                    if (dt2.Rows.Count > 1)
                    {
                        lineNumber = 534;
                        int preprocessalphabet;
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            if (Convert.ToString(dt2.Rows[i]["type"]) == "Preprocess" || Convert.ToString(dt2.Rows[i]["type"]) == "Postprocess")
                            {
                                preprocessalphabet = 0;
                                PdfPTable mainTable3 = new PdfPTable(3) { WidthPercentage = 100 }; // PdfPTable(3) Number of columns
                                mainTable3.SetWidths(new float[] { 10f, 10f, 10f }); // set width for the three column
                                parameters = new List<IDbDataParameter>();
                                parameters.Add(dbManager.CreateParameter("in_preprocess_code", Convert.ToString(dt2.Rows[i]["master_code"]), DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_recon_code", Convert.ToString(objReconReportVersionhistory.in_recon_code), DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_version_code", Convert.ToString(objReconReportVersionhistory.in_version_code), DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_role_code", headerval.role_code, DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
                                ds = dbManager.execStoredProcedure("pr_report_preprocessdetails", CommandType.StoredProcedure, parameters.ToArray());
                                document.Add(CreateTitle(i+1 + ". " + "Preprocess Details" + " - " + dt2.Rows[i]["seqno"], ironbrown));
                                DataTable dtpreprocess = new DataTable();
                                dtpreprocess = ds.Tables[0];
                                //PreProcess Code
                                PdfPTable preprocessCodeTable = new PdfPTable(1);
                                preprocessCodeTable.AddCell(CreateLabelCell("PreProcess Code : " + dtpreprocess.Rows[0]["preprocess_code"], true));
                                PdfPCell preprocessCodeCell = new PdfPCell(preprocessCodeTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable3.AddCell(preprocessCodeCell);
                                //PreProcess Name
                                PdfPTable preprocessNameTable = new PdfPTable(1);
                                preprocessNameTable.AddCell(CreateLabelCell("PreProcess Name : " + dtpreprocess.Rows[0]["preprocess_desc"], true));
                                PdfPCell preprocessNameCell = new PdfPCell(preprocessNameTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable3.AddCell(preprocessNameCell);
                                //PreProcess Method
                                PdfPTable preprocessMethodTable = new PdfPTable(1);
                                preprocessMethodTable.AddCell(CreateLabelCell("PreProcess Method : " + dtpreprocess.Rows[0]["process_method_desc"], true));
                                PdfPCell preprocessMethodCell = new PdfPCell(preprocessMethodTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable3.AddCell(preprocessMethodCell);
                                PdfPCell blankCella4 = new PdfPCell(new Phrase(" "))
                                {
                                    Colspan = 3,
                                    FixedHeight = fh,
                                    Border = PdfPCell.NO_BORDER
                                };
                                mainTable3.AddCell(blankCella4);
                                //Hold Flag
                                PdfPTable preprocessHoldTable = new PdfPTable(1);
                                preprocessHoldTable.AddCell(CreateLabelCell("Hold Flag : " + dtpreprocess.Rows[0]["hold_flag_desc"], true));
                                PdfPCell preprocessHoldCell = new PdfPCell(preprocessHoldTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable3.AddCell(preprocessHoldCell);
                                //Preprocess Order
                                PdfPTable preprocessOrderTable = new PdfPTable(1);
                                preprocessOrderTable.AddCell(CreateLabelCell("Preprocess Order : " + dtpreprocess.Rows[0]["preprocess_order"], true));
                                PdfPCell preprocessOrderCell = new PdfPCell(preprocessOrderTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable3.AddCell(preprocessOrderCell);
                                // PostProcess Flag
                                PdfPTable postProcessTable = new PdfPTable(1);
                                postProcessTable.AddCell(CreateLabelCell("Post Process : " + dtpreprocess.Rows[0]["Post Process"], true));
                                PdfPCell postProcessCell = new PdfPCell(postProcessTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable3.AddCell(postProcessCell);
                                // Space

                                PdfPCell blankCella5 = new PdfPCell(new Phrase(" "))
                                {
                                    Colspan = 3,
                                    FixedHeight = fh,
                                    Border = PdfPCell.NO_BORDER
                                };
                                mainTable3.AddCell(blankCella5);
                                //Preprocess source Dataset
                                PdfPTable sourceDatasetTable = new PdfPTable(1);
                                sourceDatasetTable.AddCell(CreateLabelCell("Source Dataset : " + dtpreprocess.Rows[0]["Source Dataset"], true));
                                PdfPCell sourceDatasetCell = new PdfPCell(sourceDatasetTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable3.AddCell(sourceDatasetCell);

                                // Preprocess Comparision Dataset
                                PdfPTable comparisonDatasetTable = new PdfPTable(1);
                                comparisonDatasetTable.AddCell(CreateLabelCell("Comparison Dataset : " + dtpreprocess.Rows[0]["Comparison Dataset"], true));
                                PdfPCell comparisonDatasetCell = new PdfPCell(comparisonDatasetTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable3.AddCell(comparisonDatasetCell);

                                // Lookup Dataset
                                PdfPTable lookupDatasetTable = new PdfPTable(1);
                                lookupDatasetTable.AddCell(CreateLabelCell("Lookup Dataset : " + dtpreprocess.Rows[0]["Lookup Dataset"], true));
                                PdfPCell lookupDatasetCell = new PdfPCell(lookupDatasetTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable3.AddCell(lookupDatasetCell);
                                // Space
                                PdfPCell blankCell6 = new PdfPCell(new Phrase(" "))
                                {
                                    Colspan = 3,
                                    FixedHeight = fh,
                                    Border = PdfPCell.NO_BORDER
                                };
                                mainTable3.AddCell(blankCell6);
                                document.Add(mainTable3);

                                // Function
                                if (dtpreprocess.Rows[0]["process_method"].ToString() == "QCD_FUNCTION")
                                {
                                    AddSectionDocument(document, ds.Tables[10], "Function", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[5], "Recon Filter", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                }

                                // Lookup

                                if (dtpreprocess.Rows[0]["process_method"].ToString() == "QCD_LOOKUP")
                                {
                                    AddSectionDocument(document, ds.Tables[1], "Lookup Dataset", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[2], "Recon Vs Lookup Condition", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[4], "Lookup Filter", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[5], "Recon Filter", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                }
                                // Query
                                if (dtpreprocess.Rows[0]["process_method"].ToString() == "QCD_QUERY")
                                {
                                    AddSectionDocument(document, ds.Tables[6], "Query", mustardYellow, ref preprocessalphabet, alphabet, fh);

                                }
                                // Expression
                                if (dtpreprocess.Rows[0]["process_method"].ToString() == "QCD_EXPRESSION")
                                {
                                    AddSectionDocument(document, ds.Tables[7], "Expression", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[5], "Recon Filter", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                }
                                // Cummulative Expression
                                if (dtpreprocess.Rows[0]["process_method"].ToString() == "QCD_CUMULATIVEXP")
                                {
                                    AddSectionDocument(document, ds.Tables[7], "Expression", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[8], "Recon Order", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[5], "Recon Filter", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                }
                                // Aggregation Expression
                                if (dtpreprocess.Rows[0]["process_method"].ToString() == "QCD_AGGEXP")
                                {
                                    AddSectionDocument(document, ds.Tables[7], "Expression", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[9], "Aggregation Grouping", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[5], "Recon Filter", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                }
                                //Comparison

                                if (dtpreprocess.Rows[0]["process_method"].ToString() == "QCD_COMPARISONEXP")
                                {
                                    AddSectionDocument(document, ds.Tables[7], "Expression", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[14], "PreProcess Condition", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[12], "Source Filter", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[13], "Comparision Filter", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[5], "Recon Filter", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                }
                                // Lookup Expression

                                if (dtpreprocess.Rows[0]["process_method"].ToString() == "QCD_LOOKUP_EXPRESSION")
                                {
                                    AddSectionDocument(document, ds.Tables[11], "Lookup Expression", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[4], "Lookup Filter", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                }
                                // Lookup Aggregation Expression

                                if (dtpreprocess.Rows[0]["process_method"].ToString() == "QCD_LOOKUP_EXP_AGG")
                                {
                                    AddSectionDocument(document, ds.Tables[15], "Lookup Expression", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[16], "Aggregation Lookup Grouping", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[17], "Lookup Order", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[4], "Lookup Filter", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                }
                                // Lookup Aggregation Expression
                                if (dtpreprocess.Rows[0]["process_method"].ToString() == "QCD_LOOKUP_COMPARISON")
                                {
                                    AddSectionDocument(document, ds.Tables[18], "Lookup Dataset", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[14], "PreProcess Condition", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[12], "Source Filter", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                    AddSectionDocument(document, ds.Tables[13], "Comparision Filter", mustardYellow, ref preprocessalphabet, alphabet, fh);
                                }
                            }
                            else if (Convert.ToString(dt2.Rows[i]["type"]) == "Rule")
                            {
                                lineNumber = 866;
                                int rulealphabet;
                                rulealphabet = 0;
                                PdfPTable mainTable1 = new PdfPTable(3) { WidthPercentage = 100 };
                                mainTable1.SetWidths(new float[] { 10f, 10f, 10f });
                                parameters = new List<IDbDataParameter>();
                                parameters.Add(dbManager.CreateParameter("in_rule_code", Convert.ToString(dt2.Rows[i]["master_code"]), DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_recon_code", Convert.ToString(objReconReportVersionhistory.in_recon_code), DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_rule_version", Convert.ToString(objReconReportVersionhistory.in_version_code), DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_role_code", headerval.role_code, DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
                                ds = dbManager.execStoredProcedure("pr_report_rulecondition", CommandType.StoredProcedure, parameters.ToArray());
                                DataTable dtrule = new DataTable();
                                dtrule = ds.Tables[0];

                                // Rule Code Column
                                document.Add(CreateTitle(i+1 + ". " + "Rule Details" + " - " + dtrule.Rows[0]["Rule Order"], grassGreen));
                                PdfPTable ruleCodeTable = new PdfPTable(1);
                                ruleCodeTable.AddCell(CreateLabelCell("Rule Code : " + dtrule.Rows[0]["Rule Code"], true));
                                PdfPCell ruleCodeCell = new PdfPCell(ruleCodeTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable1.AddCell(ruleCodeCell);

                                // Rule Name column
                                PdfPTable ruleNameTable = new PdfPTable(1);
                                ruleNameTable.AddCell(CreateLabelCell("Rule Name : " + dtrule.Rows[0]["Rule Name"], true));
                                PdfPCell ruleNameCell = new PdfPCell(ruleNameTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable1.AddCell(ruleNameCell);

                                // Rule Applied On

                                PdfPTable ruleAppliedOn = new PdfPTable(1);
                                ruleAppliedOn.AddCell(CreateLabelCell("Rule Applied On : " + dtrule.Rows[0]["Rule Applied On"], true));
                                PdfPCell RuleAppliedOnCell = new PdfPCell(ruleAppliedOn)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable1.AddCell(RuleAppliedOnCell);

                                PdfPCell blankCelll3 = new PdfPCell(new Phrase(" "))
                                {
                                    Colspan = 3,
                                    FixedHeight = fh,
                                    Border = PdfPCell.NO_BORDER
                                };
                                mainTable1.AddCell(blankCelll3);

                                // Source Dataset Column
                                PdfPTable ruleSourceTable = new PdfPTable(1);
                                ruleSourceTable.AddCell(CreateLabelCell("Source Dataset : " + dtrule.Rows[0]["Source Dataset"], true));
                                PdfPCell ruleSourceCell = new PdfPCell(ruleSourceTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable1.AddCell(ruleSourceCell);

                                // Source Account Mode Column
                                PdfPTable ruleSourceModeTable = new PdfPTable(1);
                                ruleSourceModeTable.AddCell(CreateLabelCell("Source Acc Mode : " + dtrule.Rows[0]["Source Acc Mode"], true));
                                PdfPCell ruleSourceModeCell = new PdfPCell(ruleSourceModeTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable1.AddCell(ruleSourceModeCell);

                                //Comparison Dataset
                                PdfPTable ruleComparisonTable = new PdfPTable(1);
                                ruleComparisonTable.AddCell(CreateLabelCell("Comparison Dataset : " + dtrule.Rows[0]["Comparison Dataset"], true));
                                PdfPCell ruleComparisonCell = new PdfPCell(ruleComparisonTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable1.AddCell(ruleComparisonCell);
                                PdfPCell blankCell15 = new PdfPCell(new Phrase(" "))
                                {
                                    Colspan = 3,
                                    FixedHeight = fh,
                                    Border = PdfPCell.NO_BORDER
                                };
                                mainTable1.AddCell(blankCell15);

                                //Comparison Account Mode Dataset
                                PdfPTable ruleComparisonModeTable = new PdfPTable(1);
                                ruleComparisonModeTable.AddCell(CreateLabelCell("Comparison Acc Mode : " + dtrule.Rows[0]["Comparison Acc Mode"], true));
                                PdfPCell ruleComparisonModeCell = new PdfPCell(ruleComparisonModeTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable1.AddCell(ruleComparisonModeCell);

                                // Group Flag Column
                                PdfPTable ruleGroupTable = new PdfPTable(1);
                                ruleGroupTable.AddCell(CreateLabelCell("Rule Group : " + dtrule.Rows[0]["Group Flag"], true));
                                PdfPCell ruleGroupCell = new PdfPCell(ruleGroupTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable1.AddCell(ruleGroupCell);

                                // Rule Order Column
                                PdfPTable ruleOrderTable = new PdfPTable(1);
                                ruleOrderTable.AddCell(CreateLabelCell("Rule Order : " + dtrule.Rows[0]["Rule Order"], true));
                                PdfPCell ruleOrderCell = new PdfPCell(ruleOrderTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable1.AddCell(ruleOrderCell);
                                PdfPCell blankCella4 = new PdfPCell(new Phrase(" "))
                                {
                                    Colspan = 3,
                                    FixedHeight = fh,
                                    Border = PdfPCell.NO_BORDER
                                };
                                mainTable1.AddCell(blankCella4);

                                // Probable match

                                PdfPTable probableMatch = new PdfPTable(1);
                                probableMatch.AddCell(CreateLabelCell("Probable Match : " + dtrule.Rows[0]["Probable Flag"], true));
                                PdfPCell probableMatchCell = new PdfPCell(probableMatch)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable1.AddCell(probableMatchCell);

                                // Rule Remark

                                PdfPTable ruleRemark = new PdfPTable(1);
                                ruleRemark.AddCell(CreateLabelCell("Rule Remark : " + dtrule.Rows[0]["Rule Remark"], true));
                                PdfPCell ruleRemarkCell = new PdfPCell(ruleRemark)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable1.AddCell(ruleRemarkCell);

                                //Period From
                                PdfPTable PeriodFromTable = new PdfPTable(1);
                                string periodTo = dtrule.Rows[0]["Period To"] == DBNull.Value ? string.Empty : dtrule.Rows[0]["Period To"].ToString();

                                if (string.IsNullOrEmpty(periodTo))
                                {
                                    periodTo = "Until Inactive";
                                }
                                var period = dtrule.Rows[0]["Period From"].ToString() + "-" + periodTo;
                                PeriodFromTable.AddCell(CreateLabelCell("Period : " + period, true));
                                PdfPCell periodFromCell = new PdfPCell(PeriodFromTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable1.AddCell(periodFromCell);

                                PdfPCell blankCell6 = new PdfPCell(new Phrase(" "))
                                {
                                    Colspan = 3,
                                    FixedHeight = fh,
                                    Border = PdfPCell.NO_BORDER
                                };
                                mainTable1.AddCell(blankCell6);

                                document.Add(mainTable1);
                                //Table and space between

                                if (ds.Tables[1].Rows.Count > 1)
                                {
                                    string _alpha = alphabet[rulealphabet];
                                    document.Add(CreateTitle("Rule Condition", mustardYellow));
                                    document.Add(PdfdynamicTableGenration(ds.Tables[1]));
                                    PdfPTable spacerTableAfter = new PdfPTable(1);
                                    PdfPCell spacerCellAfter = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter.AddCell(spacerCellAfter);
                                    document.Add(spacerTableAfter);
                                    rulealphabet = rulealphabet + 1;
                                }
                                //Table and space between
                                if (ds.Tables[2].Rows.Count > 1)
                                {
                                    string _alpha = alphabet[rulealphabet];
                                    document.Add(CreateTitle("Rule Source Filter", mustardYellow));
                                    document.Add(PdfdynamicTableGenration(ds.Tables[2]));
                                    PdfPTable spacerTableAfter1 = new PdfPTable(1);
                                    PdfPCell spacerCellAfter1 = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter1.AddCell(spacerCellAfter1);
                                    document.Add(spacerTableAfter1);
                                    rulealphabet = rulealphabet + 1;

                                }
                                //Table and space between
                                if (ds.Tables[3].Rows.Count > 1)
                                {
                                    string _alpha = alphabet[rulealphabet];
                                    document.Add(CreateTitle("Rule Comparision Filter", mustardYellow));
                                    document.Add(PdfdynamicTableGenration(ds.Tables[3]));
                                    PdfPTable spacerTableAfter2 = new PdfPTable(1);
                                    PdfPCell spacerCellAfter2 = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter2.AddCell(spacerCellAfter2);
                                    document.Add(spacerTableAfter2);
                                    rulealphabet = rulealphabet + 1;

                                }
                                //Table and space between Group Filter
                                if (ds.Tables[4].Rows.Count > 1)
                                {
                                    string _alpha = alphabet[rulealphabet];
                                    document.Add(CreateTitle("Rule Group Filter", mustardYellow));
                                    document.Add(PdfdynamicTableGenration(ds.Tables[4]));
                                    PdfPTable spacerTableAfter3 = new PdfPTable(1);
                                    PdfPCell spacerCellAfter3 = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter3.AddCell(spacerCellAfter3);
                                    document.Add(spacerTableAfter3);
                                    rulealphabet = rulealphabet + 1;

                                }
                                //Table and space between for Source Processing
                                if (ds.Tables[5].Rows.Count > 1)
                                {
                                    string _alpha = alphabet[rulealphabet];
                                    document.Add(CreateTitle("Source Processing Order", mustardYellow));
                                    document.Add(PdfdynamicTableGenration(ds.Tables[5]));
                                    PdfPTable spacerTableAfter4 = new PdfPTable(1);
                                    PdfPCell spacerCellAfter4 = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter4.AddCell(spacerCellAfter4);
                                    document.Add(spacerTableAfter4);
                                    rulealphabet = rulealphabet + 1;

                                }
                                //Table and space between for Target Processing
                                if (ds.Tables[6].Rows.Count > 1)
                                {
                                    string _alpha = alphabet[rulealphabet];
                                    document.Add(CreateTitle("Target Processing Order", mustardYellow));
                                    document.Add(PdfdynamicTableGenration(ds.Tables[6]));
                                    PdfPTable spacerTableAfter5 = new PdfPTable(1);
                                    PdfPCell spacerCellAfter5 = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter5.AddCell(spacerCellAfter5);
                                    document.Add(spacerTableAfter5);
                                    rulealphabet = rulealphabet + 1;
                                }
                                //Table and space between for Aggregation Function
                                if (ds.Tables[7].Rows.Count > 1)
                                {
                                    string _alpha = alphabet[rulealphabet];
                                    document.Add(CreateTitle("Aggregation Function", mustardYellow));
                                    document.Add(PdfdynamicTableGenration(ds.Tables[7]));
                                    PdfPTable spacerTableAfter6 = new PdfPTable(1);
                                    PdfPCell spacerCellAfter6 = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter6.AddCell(spacerCellAfter6);
                                    document.Add(spacerTableAfter6);
                                    rulealphabet = rulealphabet + 1;
                                }
                                //Table and space between for Aggregation Condition
                                if (ds.Tables[8].Rows.Count > 1)
                                {
                                    string _alpha = alphabet[rulealphabet];
                                    document.Add(CreateTitle("Aggregation Condition", mustardYellow));
                                    document.Add(PdfdynamicTableGenration(ds.Tables[8]));
                                    PdfPTable spacerTableAfter7 = new PdfPTable(1);
                                    PdfPCell spacerCellAfter7 = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter7.AddCell(spacerCellAfter7);
                                    document.Add(spacerTableAfter7);
                                    rulealphabet = rulealphabet + 1;
                                }
                            }
                            else if (Convert.ToString(dt2.Rows[i]["type"]) == "Theme")
                            {
                                lineNumber = 1217;
                                int themealphabet;
                                themealphabet = 0;
                                PdfPTable mainTable2 = new PdfPTable(3) { WidthPercentage = 100 }; // PdfPTable(3) Number of columns
                                mainTable2.SetWidths(new float[] { 10f, 10f, 10f }); // set width for the three column
                                parameters = new List<IDbDataParameter>();
                                parameters.Add(dbManager.CreateParameter("in_theme_code", Convert.ToString(dt2.Rows[i]["master_code"]), DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_recon_code", Convert.ToString(objReconReportVersionhistory.in_recon_code), DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_theme_version", Convert.ToString(objReconReportVersionhistory.in_version_code), DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_role_code", headerval.role_code, DbType.String));
                                parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
                                ds = dbManager.execStoredProcedure("pr_report_themedetails", CommandType.StoredProcedure, parameters.ToArray());
                                document.Add(CreateTitle(i+1 + ". " + "Theme Details" + " - " + dt2.Rows[i]["seqno"], reconPurpple));
                                DataTable dttheme = new DataTable();
                                dttheme = ds.Tables[0];
                                //Theme Code
                                PdfPTable themeCodeTable = new PdfPTable(1);
                                themeCodeTable.AddCell(CreateLabelCell("Theme Code : " + dttheme.Rows[0]["Theme Code"], true));
                                PdfPCell themeCodeCell = new PdfPCell(themeCodeTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable2.AddCell(themeCodeCell);
                                // Theme Name Column
                                PdfPTable themeNameTable = new PdfPTable(1);
                                themeNameTable.AddCell(CreateLabelCell("Theme Name : " + dttheme.Rows[0]["Theme Name"], true));
                                PdfPCell themeNameCell = new PdfPCell(themeNameTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable2.AddCell(themeNameCell);
                                //Theme Order
                                PdfPTable themeOrderTable = new PdfPTable(1);
                                themeOrderTable.AddCell(CreateLabelCell("Theme Order : " + dttheme.Rows[0]["Theme Order"], true));
                                PdfPCell themeOrderCell = new PdfPCell(themeOrderTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable2.AddCell(themeOrderCell);
                                PdfPCell blankCell13 = new PdfPCell(new Phrase(" "))
                                {
                                    Colspan = 3,
                                    FixedHeight = fh,
                                    Border = PdfPCell.NO_BORDER
                                };
                                mainTable2.AddCell(blankCell13);
                                // Hold Flag Column
                                PdfPTable themeholdTable = new PdfPTable(1);
                                themeholdTable.AddCell(CreateLabelCell("Hold Flag : " + dttheme.Rows[0]["Hold Flag"], true));
                                PdfPCell themeHoldCell = new PdfPCell(themeholdTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable2.AddCell(themeHoldCell);

                                // Theme Type Column
                                PdfPTable themeTypeTable = new PdfPTable(1);
                                themeTypeTable.AddCell(CreateLabelCell("Theme Type: " + dttheme.Rows[0]["Theme Type"], true));

                                PdfPCell themeTypeCell = new PdfPCell(themeTypeTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable2.AddCell(themeTypeCell);

                                // Theme Source Dataset
                                PdfPTable themeSourceDatasetTable = new PdfPTable(1);
                                themeSourceDatasetTable.AddCell(CreateLabelCell("Source Dataset: " + dttheme.Rows[0]["Source Dataset"], true));

                                PdfPCell themeSourceCell = new PdfPCell(themeSourceDatasetTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable2.AddCell(themeSourceCell);

                                // Add Empty Cell for Space
                                PdfPCell blankCell14 = new PdfPCell(new Phrase(" "))
                                {
                                    Colspan = 3,
                                    FixedHeight = fh,
                                    Border = PdfPCell.NO_BORDER
                                };
                                mainTable2.AddCell(blankCell14);

                                // Theme Comparision Dataset
                                PdfPTable themeComparisonDatasetTable = new PdfPTable(1);
                                themeComparisonDatasetTable.AddCell(CreateLabelCell("Comparision Dataset: " + dttheme.Rows[0]["Comparison Dataset"], true));

                                PdfPCell themeComparisonCell = new PdfPCell(themeComparisonDatasetTable)
                                {
                                    Border = PdfPCell.BOTTOM_BORDER | PdfPCell.TOP_BORDER | PdfPCell.LEFT_BORDER | PdfPCell.RIGHT_BORDER,
                                    BackgroundColor = new BaseColor(230, 230, 250),
                                    BorderColor = borderColor
                                };
                                mainTable2.AddCell(themeComparisonCell);

                                //Blank Cell

                                PdfPCell blankCell15 = new PdfPCell(new Phrase(" "))
                                {
                                    Colspan = 3,
                                    FixedHeight = fh,
                                    Border = PdfPCell.NO_BORDER
                                };
                                mainTable2.AddCell(blankCell15);

                                ////Blank Line

                                //PdfPCell blankCell16 = new PdfPCell(new Phrase(" "))
                                //{
                                //    Colspan = 3,
                                //    FixedHeight = fh,
                                //    Border = PdfPCell.NO_BORDER
                                //};
                                //mainTable2.AddCell(blankCell16);

                                //                     // Add Empty Cell for Space
                                //                     PdfPCell blankCellTheme = new PdfPCell(new Phrase(" "))
                                //{
                                //	Colspan = 3,
                                //	FixedHeight = fh,
                                //	Border = PdfPCell.NO_BORDER
                                //};
                                //mainTable2.AddCell(blankCellTheme);
                                PdfPCell blankCella15 = new PdfPCell(new Phrase(" "))
                                {
                                    Colspan = 3,
                                    FixedHeight = fh,
                                    Border = PdfPCell.NO_BORDER
                                };
                                mainTable2.AddCell(blankCella15);
                                document.Add(mainTable2);
                                if (ds.Tables[1].Rows.Count > 1)
                                {
                                    string _alpha = alphabet[themealphabet];
                                    document.Add(CreateTitle("Theme Condition", mustardYellow));
                                    document.Add(PdfdynamicTableGenration(ds.Tables[1]));
                                    PdfPTable spacerTableAfter = new PdfPTable(1);
                                    PdfPCell spacerCellAfter = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter.AddCell(spacerCellAfter);
                                    document.Add(spacerTableAfter);
                                    themealphabet = themealphabet + 1;
                                }
                                if (ds.Tables[2].Rows.Count > 1)
                                {

                                    string _alpha = alphabet[themealphabet];
                                    document.Add(CreateTitle("Theme Source Identifier", mustardYellow));
                                    document.Add(PdfdynamicTableGenration(ds.Tables[2]));
                                    PdfPTable spacerTableAfter3 = new PdfPTable(1);
                                    PdfPCell spacerCellAfter3 = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter3.AddCell(spacerCellAfter3);
                                    document.Add(spacerTableAfter3);
                                    themealphabet = themealphabet + 1;
                                }
                                if (ds.Tables[3].Rows.Count > 1)
                                {
                                    string _alpha = alphabet[themealphabet];
                                    document.Add(CreateTitle("Theme Comparision Identifier", mustardYellow));
                                    document.Add(PdfdynamicTableGenration(ds.Tables[3]));
                                    PdfPTable spacerTableAfter3 = new PdfPTable(1);
                                    PdfPCell spacerCellAfter3 = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter3.AddCell(spacerCellAfter3);
                                    document.Add(spacerTableAfter3);
                                    themealphabet = themealphabet + 1;
                                }
                                if (ds.Tables[4].Rows.Count > 1)
                                {
                                    string _alpha = alphabet[themealphabet];
                                    document.Add(CreateTitle("Additional Grouping Field", mustardYellow));
                                    document.Add(PdfdynamicTableGenration(ds.Tables[4]));
                                    PdfPTable spacerTableAfter4 = new PdfPTable(1);
                                    PdfPCell spacerCellAfter4 = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter4.AddCell(spacerCellAfter4);
                                    document.Add(spacerTableAfter4);
                                    themealphabet = themealphabet + 1;
                                }
                                if (ds.Tables[5].Rows.Count > 1)
                                {
                                    string _alpha = alphabet[themealphabet];
                                    document.Add(CreateTitle("Aggregate Function", mustardYellow));
                                    document.Add(PdfdynamicTableGenration(ds.Tables[5]));
                                    PdfPTable spacerTableAfter5 = new PdfPTable(1);
                                    PdfPCell spacerCellAfter5 = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter5.AddCell(spacerCellAfter5);
                                    document.Add(spacerTableAfter5);
                                    themealphabet = themealphabet + 1;
                                }
                                if (ds.Tables[6].Rows.Count > 1)
                                {
                                    string _alpha = alphabet[themealphabet];

                                    document.Add(CreateTitle("Aggregate Condition", mustardYellow));
                                    document.Add(PdfdynamicTableGenration(ds.Tables[6]));
                                    PdfPTable spacerTableAfter6 = new PdfPTable(1);
                                    PdfPCell spacerCellAfter6 = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter6.AddCell(spacerCellAfter6);
                                    document.Add(spacerTableAfter6);
                                    themealphabet = themealphabet + 1;
                                }
                                if (dttheme.Rows[0]["Theme Type"].ToString() == "Query" && ds.Tables[7].Rows.Count > 1)
                                {
                                    string _alpha = alphabet[themealphabet];
                                    document.Add(CreateTitle("Theme Query", mustardYellow));

                                    document.Add(PdfdynamicTableGenration(ds.Tables[7]));
                                    PdfPTable spacerTableAfter7 = new PdfPTable(1);
                                    PdfPCell spacerCellAfter7 = new PdfPCell(new Phrase(" "))
                                    {
                                        FixedHeight = fh,
                                        Border = PdfPCell.NO_BORDER
                                    };
                                    spacerTableAfter7.AddCell(spacerCellAfter7);
                                    document.Add(spacerTableAfter7);
                                    themealphabet = themealphabet + 1;
                                }
                            }
                            lineNumber = 1487;
                            /* Table Ends Theme */
                            lineNumber = 1490;
                        }
                    }

					/* Table Starts Dataset */
					if (dt4.Rows.Count > 1)
					{
						lineNumber = 500;
						document.Add(CreateTitle("Dataset", grassGreen));
						document.Add(PdfdynamicTableGenration(dt4));
						PdfPTable spacerTableAfter = new PdfPTable(1);
						PdfPCell spacerCellAfter = new PdfPCell(new Phrase(" "))
						{
							FixedHeight = fh,
							Border = PdfPCell.NO_BORDER
						};
						spacerTableAfter.AddCell(spacerCellAfter);
						document.Add(spacerTableAfter);
					}
					/* Table Ends Dataset */


					/* Table Starts Dataset Mapping*/
					if (dt5.Rows.Count > 1)
					{
						lineNumber = 500;
						document.Add(CreateTitle("Dataset Fields", grassGreen));
						document.Add(PdfdynamicTableGenration(dt5));
						PdfPTable spacerTableAfter = new PdfPTable(1);
						PdfPCell spacerCellAfter = new PdfPCell(new Phrase(" "))
						{
							FixedHeight = fh,
							Border = PdfPCell.NO_BORDER
						};
						spacerTableAfter.AddCell(spacerCellAfter);
						document.Add(spacerTableAfter);
					}

					if (dt6.Rows.Count > 1)
					{
						lineNumber = 500;
						document.Add(CreateTitle("Dataset Mapping", grassGreen));
						document.Add(PdfdynamicTableGenration(dt6));
						PdfPTable spacerTableAfter = new PdfPTable(1);
						PdfPCell spacerCellAfter = new PdfPCell(new Phrase(" "))
						{
							FixedHeight = fh,
							Border = PdfPCell.NO_BORDER
						};
						spacerTableAfter.AddCell(spacerCellAfter);
						document.Add(spacerTableAfter);
					}
					/* Table Ends Dataset Mapping*/

					document.NewPage();

					document.Close();
                    lineNumber = 1491;
                    byte[] pdfBytes = ms.ToArray();
                    lineNumber = 1493;
                    using (MemoryStream finalStream = new MemoryStream())
                    {
                        lineNumber = 1496;
                        using (Document finalDocument = new Document(rec, 30f, 30f, 30f, 30f))
                        {
                            lineNumber = 1499;
                            PdfCopy pdfCopy = new PdfCopy(finalDocument, finalStream);
                            finalDocument.Open();
                            lineNumber = 1495;
                            //PdfReader tocReader = new PdfReader(GenerateTocPdf(tocEntries));
                            lineNumber = 1496;
                            //for (int i = 1; i <= tocReader.NumberOfPages; i++)
                            //{
                            //    pdfCopy.AddPage(pdfCopy.GetImportedPage(tocReader, i));
                            //}
                            PdfReader contentReader = new PdfReader(ms.ToArray());
                            for (int i = 1; i <= contentReader.NumberOfPages; i++)
                            {
                                pdfCopy.AddPage(pdfCopy.GetImportedPage(contentReader, i));
                            }
                            finalDocument.Close();
                        }
                        return finalStream.ToArray();
                    }
                }

            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_report_reconversionhistory" + "Error Message:" + ex.Message);
                var stackTrace = new System.Diagnostics.StackTrace(ex, true); // 'true' to include file info
                var frame = stackTrace.GetFrame(0); // Get the first stack frame (i.e., where the exception occurred)
                objlog.commonDataapi("", "SP", lineNumber.ToString() + ex.Message + "Param:" + JsonConvert.SerializeObject(objReconReportVersionhistory), "pr_report_reconversionhistory", headerval.user_code, constring);
                return null;
            }
        }

        public void AddSectionDocument(Document document, DataTable table, string titleText, BaseColor titleColor, ref int preprocessalphabet, string[] alphabet, float spacerHeight)
        {
            if (table.Rows.Count > 1)
            {
                string _alpha = alphabet[preprocessalphabet];
                document.Add(CreateTitle(titleText, titleColor));
                document.Add(PdfdynamicTableGenration(table));
                PdfPTable spacerTable = new PdfPTable(1);
                PdfPCell spacerCell = new PdfPCell(new Phrase(" "))
                {
                    FixedHeight = spacerHeight,
                    Border = PdfPCell.NO_BORDER
                };
                spacerTable.AddCell(spacerCell);
                document.Add(spacerTable);
                preprocessalphabet = preprocessalphabet + 1;
            }
        }



        private byte[] GenerateTocPdf(List<TOCEntry> tocEntries)
        {
            using (MemoryStream tocStream = new MemoryStream())
            {
                using (Document tocDocument = new Document(PageSize.A4, 30f, 30f, 30f, 30f))
                {
                    PdfWriter tocWriter = PdfWriter.GetInstance(tocDocument, tocStream);
                    tocDocument.Open();

                    PdfPTable tocTable = new PdfPTable(2);
                    tocTable.WidthPercentage = 100;
                    tocTable.SetWidths(new float[] { 90f, 10f });

                    foreach (var entry in tocEntries)
                    {
                        if (entry.Title is string)
                        {
                            if (entry.Title == "Content")
                            {
                                PdfPCell pageCell = new PdfPCell(new Phrase(entry.Title.ToString(), new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.BLACK)));
                                pageCell.Border = PdfPCell.BOX;
                                pageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tocTable.AddCell(pageCell);
                            }
                            else
                            {
                                PdfPCell titleCell = new PdfPCell(new Phrase(entry.Title, new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK)));
                                titleCell.Border = PdfPCell.BOX;
                                tocTable.AddCell(titleCell);
                            }
                        }
                        if (entry.PageInfo is string)
                        {
                            if (entry.PageInfo == "Page")
                            {
                                PdfPCell pageCell = new PdfPCell(new Phrase(entry.PageInfo.ToString(), new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD, BaseColor.BLACK)));
                                pageCell.Border = PdfPCell.BOX;
                                pageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                tocTable.AddCell(pageCell);
                            }
                            else
                            {
                                PdfPCell pageCell = new PdfPCell(new Phrase(entry.PageInfo.ToString(), new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK)));
                                pageCell.Border = PdfPCell.BOX;
                                pageCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                tocTable.AddCell(pageCell);
                            }

                        }
                        else
                        {
                            PdfPCell pageCell = new PdfPCell(new Phrase(entry.PageInfo.ToString(), new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK)));
                            pageCell.Border = PdfPCell.BOX;
                            pageCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                            tocTable.AddCell(pageCell);
                        }

                    }

                    tocDocument.Add(tocTable);

                    tocDocument.Close();
                }
                return tocStream.ToArray();
            }
        }
        public class TOCEntry
        {
            public string Title { get; set; }
            public object PageInfo { get; set; }
            public TOCEntry(string title, object pageInfo)
            {
                Title = title;
                PageInfo = pageInfo;
            }
        }


    }
}

