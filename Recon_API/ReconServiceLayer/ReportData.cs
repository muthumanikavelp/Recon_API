using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Google.Protobuf.WellKnownTypes;
using Irony.Parsing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReconModels;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using static ReconModels.CommonModel;
using static ReconModels.ReconModel;
using static ReconModels.ReportModel;
using static ReconModels.UserManagementModel;

namespace ReconDataLayer
{
    public class ReportData
    {
        DataSet ds = new DataSet();
        DataTable result = new DataTable();
        List<IDbDataParameter>? parameters;

        private IConfiguration _configuration;

        public DataTable generatereportData(generatereportmodel objgeneratereportmodel, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_recon_code", objgeneratereportmodel.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_code", objgeneratereportmodel.in_report_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_param", objgeneratereportmodel.in_report_param, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_condition", objgeneratereportmodel.reportcondition, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_ip_addr", headerval.ip_address, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", objgeneratereportmodel.in_user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_outputfile_flag", objgeneratereportmodel.in_outputfile_flag, DbType.Boolean));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_run_report", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_run_report" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objgeneratereportmodel), "pr_run_report", headerval.user_code, constring);
                throw ex;
            }

        }

        public DataTable reconbetweenaccData(reconbetweenaccmodel objreconbetweenaccmodel, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_recon_code", objreconbetweenaccmodel.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_tran_date", objreconbetweenaccmodel.in_tran_date, DbType.Date));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_get_reconsummary", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_reconsummary" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objreconbetweenaccmodel), "pr_get_brs", headerval.user_code, constring);
                throw ex;
            }
        }

        //reconwithinaccData

        public DataTable reconwithinaccData(reconwithinaccmodel objreconwithinaccmodel, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_recon_code", objreconwithinaccmodel.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_tran_date", objreconwithinaccmodel.in_tran_date, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_get_proof", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_proof" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objreconwithinaccmodel), "pr_get_proof", headerval.user_code, constring);
                throw ex;
            }
        }

        //getreportlistData
        public DataTable getreportlistData(UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                ds = dbManager.execStoredProcedure("pr_get_reportlist", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_reportlist" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(headerval), "pr_get_reportlist", headerval.user_code, constring);
                throw ex;
            }

        }

        //getreportparamlistData
        public DataTable getreportparamlistData(reportparamlistmodel objreportparamlistmodel, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_report_code", objreportparamlistmodel.in_report_code, DbType.String));
                ds = dbManager.execStoredProcedure("pr_recon_mst_treportparam", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_recon_mst_treportparam" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objreportparamlistmodel), "pr_recon_mst_treportparam", headerval.user_code, constring);
                throw ex;
            }
        }

        //getreportparamlistserviceData
        public DataTable getreportparamlistreconData(reportparamlistmodel objreportparamlistmodel, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_report_code", objreportparamlistmodel.in_report_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_recon_code", objreportparamlistmodel.in_recon_code, DbType.String));
                ds = dbManager.execStoredProcedure("pr_get_reportparam", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_reportparam" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objreportparamlistmodel), "pr_get_reportparam", headerval.user_code, constring);
                throw ex;
            }

        }
        // Report

        public string[] ExectionReport(Report_model objmodel, UserManagementModel.headerValue headerval, string constring)
        {
            string[] result = { };
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_report_code", objmodel.report_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_recon_gid", objmodel.recon_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_condition", objmodel.Report_condition, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_ip_addr", headerval.ip_address, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_outputfile_flag", objmodel.report_gid, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", objmodel.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_run_report", CommandType.StoredProcedure, parameters.ToArray());
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_run_report" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objmodel), "pr_run_report", objmodel.user_code, constring);
                throw ex;
            }
        }

        //MonthendReportData
        public async Task<DataSet> MonthendReportData(MonthendReportModel objMonthendReport, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_recon_code", objMonthendReport.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_tran_date", objMonthendReport.in_tran_date, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedurelist("pr_get_brsmonthend", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_brsmonthend" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objMonthendReport), "pr_get_brsmonthend", headerval.user_code, constring);
                throw ex;
            }

        }

        //getReportTemplateListData
        //Pandiaraj load grid rpt template list by report permisssion 
        public DataTable getReportTemplateListData(getReportTemplateListModel objgetReportTemplateListModel, string constring, UserManagementModel.headerValue headerval)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_recon_code", objgetReportTemplateListModel.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_custom_flag", objgetReportTemplateListModel.in_custom_flag, DbType.Boolean));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_role_code", headerval.role_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_get_reporttemplate_permission_list", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_reporttemplate_permission_list" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objgetReportTemplateListModel), "pr_get_reporttemplate_list", headerval.user_code, constring);
                throw ex;
            }

        }

        //reportTemplateData

        public DataTable reportTemplateData(reportTemplateModel objreportTemplate, string constring, UserManagementModel.headerValue headerval)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_gid", objreportTemplate.in_reporttemplate_gid, DbType.Int32, ParameterDirection.InputOutput));
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objreportTemplate.in_reporttemplate_code, DbType.String, ParameterDirection.InputOutput));
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_name", objreportTemplate.in_reporttemplate_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_code", objreportTemplate.in_report_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_recon_code", objreportTemplate.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_sortby_code", objreportTemplate.in_sortby_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_action", objreportTemplate.in_action, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_active_status", objreportTemplate.in_active_status, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_system_flag", objreportTemplate.in_system_flag, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_role_code", headerval.role_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_recon_mst_treporttemplate", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_recon_mst_treporttemplate" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objreportTemplate), "pr_recon_mst_treporttemplate", headerval.user_code, constring);
                throw ex;
            }

        }

        //fetchReportTemplateDatareporttemplatefield
        public DataSet fetchReportTemplateData(fetchReportTemplateModel objfetchReportTemplate, string constring, UserManagementModel.headerValue headerval)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objfetchReportTemplate.in_reporttemplate_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplateresultset_code", objfetchReportTemplate.in_reporttemplateresultset_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_recon_code", objfetchReportTemplate.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_code", objfetchReportTemplate.in_report_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_role_code", headerval.role_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
                //ds = dbManager.execStoredProcedurelist("pr_fetch_reporttemplate", CommandType.StoredProcedure, parameters.ToArray());
                ds = dbManager.execStoredProcedurelist("pr_fetch_reporttemplate_multidataset", CommandType.StoredProcedure, parameters.ToArray());
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objfetchReportTemplate), "pr_fetch_reporttemplate_multidataset", headerval.user_code, constring);
                objlog.logger("SP:pr_fetch_reporttemplate_multidataset" + "Error Message:" + ex.Message);
                throw ex;
            }

        }
        //reporttemplatefilterData
        public DataTable reporttemplatefilterData(reporttemplatefilterModel objreporttemplatefilter, string constring, UserManagementModel.headerValue headerval)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reporttemplatefilter_gid", objreporttemplatefilter.in_reporttemplatefilter_gid, DbType.Int32, ParameterDirection.InputOutput));
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objreporttemplatefilter.in_reporttemplate_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_filter_seqno", objreporttemplatefilter.in_filter_seqno, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_report_field", objreporttemplatefilter.in_report_field, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_filter_criteria", objreporttemplatefilter.in_filter_criteria, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_filter_value", objreporttemplatefilter.in_filter_value, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_open_parentheses_flag", objreporttemplatefilter.in_open_parentheses_flag, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_close_parentheses_flag", objreporttemplatefilter.in_close_parentheses_flag, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_join_condition", objreporttemplatefilter.in_join_condition, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_last_system_flag", objreporttemplatefilter.in_last_system_flag, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_action", objreporttemplatefilter.in_action, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplateresultset_code", objreporttemplatefilter.in_reporttemplateresultset_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_action_by", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                //ds = dbManager.execStoredProcedure("pr_recon_mst_treporttemplatefilter", CommandType.StoredProcedure, parameters.ToArray());
                ds = dbManager.execStoredProcedure("pr_recon_mst_treporttemplatefilter_multiset", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_recon_mst_treporttemplatefilter_multiset" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objreporttemplatefilter), "pr_recon_mst_treporttemplatefilter_multiset", headerval.user_code, constring);
                throw ex;
            }
        }

        //runPageReportData

        public DataTable runPageReportData(runPageReportModel objrunPageReport, string constring, UserManagementModel.headerValue headerval)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_archival_code", objrunPageReport.in_archival_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objrunPageReport.in_reporttemplate_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplateresultset_code", objrunPageReport.in_reporttemplateresultset_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_recon_code", objrunPageReport.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_code", objrunPageReport.in_report_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_condition", objrunPageReport.in_report_condition, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_ip_addr", headerval.ip_address, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_rec_count", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_run_pagereport", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_run_pagereport" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objrunPageReport), "pr_run_pagereport", headerval.user_code, constring);
                throw ex;
            }
        }

        //getPaginationreportData

        public DataTable getPaginationreportData(string constring, UserManagementModel.headerValue headerval)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_ip_addr", headerval.ip_address, DbType.String));
                ds = dbManager.execStoredProcedure("pr_get_paginationreport", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_paginationreport" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(headerval), "pr_get_paginationreport", headerval.user_code, constring);
                throw ex;
            }

        }

        //getPageNoReportData

        public DataTable getPageNoReportData(getPageNoReportModel objgetPageNoReport, string constring, UserManagementModel.headerValue headerval)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objgetPageNoReport.in_reporttemplate_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplateresultset_code", objgetPageNoReport.in_reporttemplateresultset_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_code", objgetPageNoReport.in_report_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_recon_code", objgetPageNoReport.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_rptsession_gid", objgetPageNoReport.in_rptsession_gid, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_condition", objgetPageNoReport.in_condition, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_page_no", objgetPageNoReport.in_page_no, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_page_size", objgetPageNoReport.in_page_size, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("in_tot_records", objgetPageNoReport.in_tot_records, DbType.Int32));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_run_pagenoreport", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_run_pagenoreport" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objgetPageNoReport), "pr_run_pagenoreport", headerval.user_code, constring);
                throw ex;
            }

        }

        //cloneReportTemplateData

        public DataTable cloneReportTemplateData(cloneReportTemplateModel objcloneReportTemplate, string constring, UserManagementModel.headerValue headerval)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_gid", objcloneReportTemplate.in_reporttemplate_gid, DbType.Int16, ParameterDirection.InputOutput));
                parameters.Add(dbManager.CreateParameter("in_clone_reporttemplate_code", objcloneReportTemplate.in_clone_reporttemplate_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_name", objcloneReportTemplate.in_reporttemplate_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_code", objcloneReportTemplate.in_report_code, DbType.String, ParameterDirection.InputOutput));
                parameters.Add(dbManager.CreateParameter("in_action", objcloneReportTemplate.in_action, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_active_status", objcloneReportTemplate.in_active_status, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_role_code", headerval.role_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_reporttemplate_code", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_recon_mst_treporttemplate_clone", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_recon_mst_treporttemplate_clone" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objcloneReportTemplate), "pr_recon_mst_treporttemplate_clone", headerval.user_code, constring);
                throw ex;
            }
        }

        //reporttemplatefieldData

        public DataTable reporttemplatefieldData(reporttemplatefieldModel objreporttemplatefield, string constring, UserManagementModel.headerValue headerval)
        {
            try
            {
                DataTable JsonData = new DataTable();
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objreporttemplatefield.in_reporttemplate_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_jsonArray", objreporttemplatefield.templateJSON, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplateresultset_code", objreporttemplatefield.in_reporttemplateresultset_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                //ds = dbManager.execStoredProcedure("pr_recon_mst_treporttemplatefield_new", CommandType.StoredProcedure, parameters.ToArray());
                ds = dbManager.execStoredProcedure("pr_recon_mst_treporttemplatefield_new_multiset", CommandType.StoredProcedure, parameters.ToArray());

                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_recon_mst_treporttemplatefield_new_multiset" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objreporttemplatefield), "pr_recon_mst_treporttemplatefield_new", headerval.user_code, constring);
                throw ex;
            }
        }
        //reporttemplatesortingData

        public DataTable reporttemplatesortingData(reporttemplatesortingModel objreporttemplatesorting, string constring, UserManagementModel.headerValue headerval)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reporttemplatesorting_gid", objreporttemplatesorting.in_reporttemplatesorting_gid, DbType.Int16, ParameterDirection.InputOutput));
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objreporttemplatesorting.in_reporttemplate_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_field", objreporttemplatesorting.in_report_field, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplateresultset_code", objreporttemplatesorting.in_reporttemplateresultset_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_sorting_order", objreporttemplatesorting.in_sorting_order, DbType.Decimal));
                parameters.Add(dbManager.CreateParameter("in_active_status", objreporttemplatesorting.in_active_status, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_action", objreporttemplatesorting.in_action, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_action_by", objreporttemplatesorting.in_action_by, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_delete_flag", objreporttemplatesorting.in_delete_flag, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_sort_type", objreporttemplatesorting.in_sort_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_recon_mst_treporttemplatesorting_multiset", CommandType.StoredProcedure, parameters.ToArray());
               // ds = dbManager.execStoredProcedure("pr_recon_mst_treporttemplatesorting", CommandType.StoredProcedure, parameters.ToArray());
                
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_recon_mst_treporttemplatesorting_multiset" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objreporttemplatesorting), "pr_recon_mst_treporttemplatesorting", headerval.user_code, constring);
                throw ex;
            }
        }

        //uploadreporttempletefileData

        public DataTable uploadreporttempletefileData(uploadreporttempletefileModel objuploadreporttempletefile, string constring, UserManagementModel.headerValue headerval)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objuploadreporttempletefile.in_reporttemplate_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_file_name", objuploadreporttempletefile.in_file_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_file_path", objuploadreporttempletefile.in_file_path, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_action_by", objuploadreporttempletefile.in_action_by, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_upload_templetefile", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_upload_templetefile" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objuploadreporttempletefile), "pr_upload_templetefile", headerval.user_code, constring);
                throw ex;
            }
        }

        //generatedynamicReportData

        public DataTable generatedynamicReportData(generatedynamicReportmodel objgeneratedynamicReport, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DataSet dataset = new DataSet();
                DataTable getmaxrowsresultset = new DataTable();
                getmaxrowsresultset = configvalueData("excel_max_rows",constring);
                DataTable dt = new DataTable();
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_archival_code", objgeneratedynamicReport.in_archival_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objgeneratedynamicReport.in_reporttemplate_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplateresultset_code", objgeneratedynamicReport.in_reporttemplateresultset_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_recon_code", objgeneratedynamicReport.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_code", objgeneratedynamicReport.in_report_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_param", objgeneratedynamicReport.in_report_param, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_condition", objgeneratedynamicReport.in_report_condition, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_outputfile_flag", objgeneratedynamicReport.in_outputfile_flag, DbType.Boolean));
                parameters.Add(dbManager.CreateParameter("in_outputfile_type", objgeneratedynamicReport.in_outputfile_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_ip_addr", headerval.ip_address, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                dataset = dbManager.execStoredProcedurelist("pr_run_dynamicreport", CommandType.StoredProcedure, parameters.ToArray());
                if (objgeneratedynamicReport.in_outputfile_type == "csv")
                {
                    string insertintojob = "";
                    dt = dataset.Tables[0];
                    var job_id = dataset.Tables[0].Rows[0]["result"];
                    var filename = job_id + "_" + objgeneratedynamicReport.in_report_name;
                    if (job_id != null)
                    {
                        insertintojob = insertfileName(filename, job_id, constring, headerval.user_code);

                    }
                    return dt;

                }
                else
                {
                    string sourceFile = "";
                    var job_id = dataset.Tables[1].Rows[0]["result"];
                    var filename = job_id + "_" + objgeneratedynamicReport.in_report_name;
                    string getsourcefolderpath = roleconfig_db("folder_path", constring);
                    if (objgeneratedynamicReport.file_name == "" || objgeneratedynamicReport.in_reporttemplate_code == "")
                    {
                        sourceFile = roleconfig_db("temp_file_folder_path", constring);
                    }
                    else
                    {
                        sourceFile = getsourcefolderpath + objgeneratedynamicReport.in_reporttemplate_code + ".xlsx";
                    }
                    string getdestFile = roleconfig_db("xlsx_folder_path", constring);
                    string destFile = getdestFile + filename + ".xlsx";
                    string insertintojob = "";
                    if (job_id != null)
                    {
                        insertintojob = insertfileName(filename, job_id, constring, headerval.user_code);
                    }

                    if (insertintojob == "Success")
                    {
                        string sheetName = "Data";
                        File.Copy(sourceFile, destFile, true);
                        using (var workbook = new XLWorkbook(destFile))
                        {
                            var worksheet = workbook.Worksheet(sheetName);
                            worksheet.Clear(XLClearOptions.Contents);
                            try
                            {
                                if (dataset.Tables[0].Rows.Count > 0)
                                {
                                    int maxrows = 0;
                                    int.TryParse(getmaxrowsresultset.Rows[0]["out_config_value"]?.ToString(), out maxrows);
                                    if (maxrows == 0)
                                    {
                                        maxrows = 100000;
                                    }
                                    worksheet.Cell(1, 1).InsertTable(dataset.Tables[0].AsEnumerable().Take(maxrows).CopyToDataTable(), "MyTable", true);
                                }
                                else
                                {
                                    worksheet.Cell(1, 1).InsertData("No Record Found");
                                }
                                workbook.SaveAs(destFile);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An error occurred while inserting table: {ex.Message}");
                                throw;
                            }
                        }
                    }
                    dt = dataset.Tables[1];
                    return dt;
                }
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_run_dynamicreport" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objgeneratedynamicReport), "pr_run_dynamicreport", headerval.user_code, constring);
                throw ex;
            }
        }

        public string roleconfig_db(string in_config_name, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_config_name", in_config_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_config_value", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_get_configvalue", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                string outMsgValue = parameters[1].Value.ToString();
                return outMsgValue;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(in_config_name), "pr_get_configvalue", "", constring);
                return "failed";
            }
        }

        public string insertfileName(string file_name, object job_id, string constring, string user_code)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                int getjobId = Convert.ToInt32(job_id.ToString());
                parameters.Add(dbManager.CreateParameter("in_file_name", file_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_job_id", getjobId, DbType.Int64));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_recon_trn_tjob", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                string outMsgValue = parameters[2].Value.ToString();
                return outMsgValue;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(job_id), "pr_get_configvalue", "", constring);
                return "failed";
            }
        }

        public string UpdateJobStatus(object job_id, string job_status, string job_remark, string constring, string user_code)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                int getjobId = Convert.ToInt32(job_id.ToString());
                parameters.Add(dbManager.CreateParameter("in_job_gid", job_id, DbType.Int64));
                parameters.Add(dbManager.CreateParameter("in_job_status", job_status, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_job_remark", job_remark, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_upd_job", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                string outMsgValue = parameters[2].Value.ToString();
                return outMsgValue;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(job_id), "pr_get_configvalue", "", constring);
                return "failed";
            }
        }

        //customReport_brsSummaryData

        public DataSet customReport_brsSummaryData(customReport_brsSummaryModel objbrsSummaryModel, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_recon_code", objbrsSummaryModel.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_tran_date", objbrsSummaryModel.in_tran_date, DbType.Date));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedurelist("pr_get_brssummary", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_brs" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objbrsSummaryModel), "pr_get_brs", headerval.user_code, constring);
                throw ex;
            }
        }

        //generatedynamicReport_typeCData
        public DataSet generatedynamicReport_typeCDataOldbyhari(generatedynamicReport_typeCmodel objgeneratedynamicReport, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DataSet dataset = new DataSet();
                DataTable dt = new DataTable();
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_archival_code", objgeneratedynamicReport.in_archival_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objgeneratedynamicReport.in_reporttemplate_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_recon_code", objgeneratedynamicReport.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_code", objgeneratedynamicReport.in_report_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_param", objgeneratedynamicReport.in_report_param, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_condition", objgeneratedynamicReport.in_report_condition, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_outputfile_flag", objgeneratedynamicReport.in_outputfile_flag, DbType.Boolean));
                parameters.Add(dbManager.CreateParameter("in_outputfile_type", objgeneratedynamicReport.in_outputfile_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_ip_addr", headerval.ip_address, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                dataset = dbManager.execStoredProcedurelist("pr_run_dynamicreport", CommandType.StoredProcedure, parameters.ToArray());
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_run_dynamicreport" + "Response.Tables[0]:" + dataset.Tables[0].Rows);
                objlog.logger("SP:pr_run_dynamicreport" + "Response.Tables[0]:" + dataset.Tables[0].Rows);
                int getsheetcount = dataset.Tables[0].Rows.Count;
                var job_id = dataset.Tables[0].Rows[0]["result"];
                var filename = job_id + "_" + objgeneratedynamicReport.in_report_name;
                var insertintojob = "";
                if (job_id != null)
                {
                    insertintojob = insertfileName(filename, job_id, constring, headerval.user_code);
                }
                if (insertintojob == "Success")
                {
                    string getdestFile = roleconfig_db("xlsx_folder_path", constring);
                    string sourceFolder = roleconfig_db("folder_path", constring);
                    string reportTemplateName = "";
                    string sourceFile = "";
                    if (objgeneratedynamicReport.in_reporttemplate_code != "")
                    {
                        sourceFile = sourceFolder + objgeneratedynamicReport.in_reporttemplate_code + ".xlsx";
                        if(!File.Exists(sourceFile))
                        {
                            sourceFile = roleconfig_db("temp_file_folder_path", constring);
                        }
                    }
                    else
                    {
                        sourceFile = roleconfig_db("temp_file_folder_path", constring);
                    }
                    string destFile = getdestFile + filename + ".xlsx";
                    CreateExcelFile(dataset, sourceFile, destFile,constring);
                    UpdateJobStatus(job_id, "C", "Completed", constring, headerval.user_code);
                }
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objgeneratedynamicReport), "pr_run_dynamicreport", headerval.user_code, constring);
                objlog.logger("SP:pr_run_dynamicreport" + "Error Message:" + ex.Message);
                throw ex;
            }
        }
        public DataSet generatedynamicReport_typeCData(generatedynamicReport_typeCmodel objgeneratedynamicReport, UserManagementModel.headerValue headerval, string constring)
        {           
            try
            {
                DataTable getDateFormatresultset = new DataTable();
                DataTable getDateTimeFormatresultset = new DataTable();

                DataSet dataset = new DataSet();
                DataTable dt = new DataTable();
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_archival_code", objgeneratedynamicReport.in_archival_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objgeneratedynamicReport.in_reporttemplate_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplateresultset_code", objgeneratedynamicReport.in_reporttemplateresultset_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_recon_code", objgeneratedynamicReport.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_code", objgeneratedynamicReport.in_report_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_param", objgeneratedynamicReport.in_report_param, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_condition", objgeneratedynamicReport.in_report_condition, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_outputfile_flag", objgeneratedynamicReport.in_outputfile_flag, DbType.Boolean));
                parameters.Add(dbManager.CreateParameter("in_outputfile_type", objgeneratedynamicReport.in_outputfile_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_ip_addr", headerval.ip_address, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "", DbType.String, ParameterDirection.Output));
                dataset = dbManager.execStoredProcedurelist("pr_run_dynamicreport", CommandType.StoredProcedure, parameters.ToArray());
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_run_dynamicreport" + "Response.Tables[0]:" + dataset.Tables[0].Rows);
                objlog.logger("SP:pr_run_dynamicreport" + "Response.Tables[0]:" + dataset.Tables[0].Rows);
                int getsheetcount = dataset.Tables[0].Rows.Count;
                var job_id = dataset.Tables[0].Rows[0]["result"].ToString();
                var filename = job_id + "_" + objgeneratedynamicReport.in_report_name;
                var insertintojob = "";
                if (job_id != null)
                {
                    insertintojob = insertfileName(filename, job_id, constring, headerval.user_code);
                }
                if (insertintojob == "Success")
                {
                    getDateFormatresultset = configvalueData("excel_dateformat",constring);
                    getDateTimeFormatresultset = configvalueData("excel_datetimeformat",constring);
                    string getdestFile = roleconfig_db("xlsx_folder_path", constring);
                    string sourceFolder = roleconfig_db("folder_path", constring);


                    //getdestFile = "E:\\UAT_RECON\\Recon_files\\prod\\JobFiles\\";// roleconfig_db("xlsx_folder_path", constring);
                    //sourceFolder = "E:\\UAT_RECON\\Recon_files\\prod\\Uploads\\";// roleconfig_db("folder_path", constring);

                    string reportTemplateName = "";
                    string sourceFile = "";
                    string destFile = getdestFile + filename + ".xlsx";
                    if (objgeneratedynamicReport.in_reporttemplate_code != "")
                    {
                        sourceFile = sourceFolder + objgeneratedynamicReport.in_reporttemplate_code + ".xlsx";

                        if (File.Exists(sourceFile))
                        {
                            destFile = getdestFile + filename + ".xlsx";
                            //CreateExcelFile(dataset, sourceFile, destFile);
                            CreateExcelFilemuti(dataset, sourceFile, destFile, getDateFormatresultset, getDateTimeFormatresultset, constring);
                            
                        }
                        else if(File.Exists(sourceFile.Replace("xlsx","xlsm")))
                        {
                            destFile = getdestFile + filename + ".xlsm";
                            sourceFile = sourceFile.Replace("xlsx", "xlsm");
                            CreateExcelFile(dataset, sourceFile, destFile, constring);
                        }
                        else
                        {
                            sourceFile = roleconfig_db("temp_file_folder_path", constring);
                            //  sourceFile = sourceFile.Replace("D:", "E:");
                            destFile = getdestFile + filename + ".xlsx";
                            //  destFile = destFile.Replace("D:", "E:");
                           // CreateTemplateExcel(dataset, sourceFile, destFile);
                            CreateTemplateExcelmulti(dataset, sourceFile, destFile, getDateFormatresultset, getDateTimeFormatresultset,constring);
                        }

                    }
                    else
                    {
                        sourceFile = roleconfig_db("temp_file_folder_path", constring);
                       // sourceFile = sourceFile.Replace("D:", "E:");
                        destFile = getdestFile + filename + ".xlsx";
                        CreateTemplateExcel(dataset, sourceFile, destFile, constring);                       
                    }
                    //sourceFile = sourceFile.Replace("D:", "E:");
                    UpdateJobStatus(job_id, "C", "Completed", constring, headerval.user_code);
                }
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                Updatedynamicerror(constring, objgeneratedynamicReport.in_recon_code, headerval.user_code, ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objgeneratedynamicReport), "pr_run_dynamicreport", headerval.user_code, constring);
                objlog.logger("SP:pr_run_dynamicreport" + "Error Message:" + ex.Message);
                throw ex;
            }
        }

        public DataTable getReportFieldType(ReportModel.DataModel objDataModel, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = JsonConvert.DeserializeObject<DataTable>(objDataModel.Dataset1);
                var objgeneratedynamicReport = dt.Rows[0];
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objgeneratedynamicReport["in_reporttemplate_code"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_recon_code", objgeneratedynamicReport["in_recon_code"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_code", objgeneratedynamicReport["in_report_code"], DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_get_reportfieldtype", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_reportfieldtype" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objDataModel.Dataset1), "pr_get_reportfieldtype", headerval.user_code, constring);
                throw ex;
            }
        }

        public DataTable getreportresultsetFieldType(string reporttemplate_code,string reporttemplateresultset_code, string constring)
        {
            try
            {
                DataTable dt = new DataTable();             
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", reporttemplate_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplateresultset_code", reporttemplateresultset_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedurelist("pr_get_reportresultsetfieldtype", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_reportresultsetfieldtype" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(""), "pr_get_reportresultsetfieldtype", "", constring);
                throw ex;
            }
        }

        public DataTable configvalueData(string objconfigvalue,string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_config_name", objconfigvalue, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_config_value", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_get_configvalue", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_configvalue" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objconfigvalue), "pr_get_configvalue","",constring);
                throw ex;
            }
        }

        Dictionary<int, string> columnFormats = new Dictionary<int, string>
{
    { 1, "#0" },       // Column A - Numeric format
    { 2, "yyyy-mm-dd" },     // Column B - Date format
    { 5, "#,##0.00" },      // Column E - Currency format
    { 6, "#,##0.00" },      // Column F - Currency format
	{9, "#,##0.00;[Red]-#,##0.00" },
    {10, "#,##0.00;[Red]-#,##0.00" },
    {11, "#,##0.00;[Red]-#,##0.00" },
    {12, "#,##0.00;[Red]-#,##0.00" },
    {13, "#,##0.00;[Red]-#,##0.00" }
};

        /* Start Testing for template and macro */
        public DataTable generatedynamicReportData_new_template(ReportModel.DataModel objDataModel, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DataTable getfiledType = new DataTable();
                DataTable getDateFormat = new DataTable();
                DataTable getDateTimeFormat = new DataTable();
                DataTable JsonData = new DataTable();
                DataTable sheetDt = new DataTable();
                DataSet dataset = new DataSet();
                DataTable dt = new DataTable();
                JsonData = JsonConvert.DeserializeObject<DataTable>(objDataModel.Dataset1);
                JArray jsonArray = JArray.Parse(objDataModel.Dataset2);
                sheetDt.Columns.Add("Filter Seq No", typeof(int));
                sheetDt.Columns.Add("(", typeof(string));
                sheetDt.Columns.Add("Filter Field", typeof(string));
                sheetDt.Columns.Add("Filter Criteria", typeof(string));
                sheetDt.Columns.Add("Filter Value", typeof(string));
                sheetDt.Columns.Add(")", typeof(string));
                sheetDt.Columns.Add("Joins", typeof(string));
                foreach (JObject obj in jsonArray)
                {
                    DataRow newRow = sheetDt.NewRow();
                    newRow["Filter Seq No"] = (int)obj["filter_seqno"];
                    newRow["("] = (string)obj["open_parentheses_flag"];
                    newRow["Filter Field"] = (string)obj["reportparam_value"];
                    newRow["Filter Criteria"] = (string)obj["filter_criteria_desc"];
                    newRow["Filter Value"] = (string)obj["filter_value"];
                    newRow[")"] = (string)obj["close_parentheses_flag"];
                    newRow["Joins"] = (string)obj["join_condition"];
                    sheetDt.Rows.Add(newRow);
                }
                var in_archival_code = "";
                var objgeneratedynamicReport = JsonData.Rows[0];
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_archival_code", in_archival_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objgeneratedynamicReport["in_reporttemplate_code"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_recon_code", objgeneratedynamicReport["in_recon_code"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_code", objgeneratedynamicReport["in_report_code"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_param", objgeneratedynamicReport["in_report_param"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_condition", objgeneratedynamicReport["in_report_condition"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_outputfile_flag", objgeneratedynamicReport["in_outputfile_flag"], DbType.Boolean));
                parameters.Add(dbManager.CreateParameter("in_outputfile_type", objgeneratedynamicReport["in_outputfile_type"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_ip_addr", headerval.ip_address, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                dataset = dbManager.execStoredProcedurelist("pr_run_dynamicreport", CommandType.StoredProcedure, parameters.ToArray());
                if (objgeneratedynamicReport["in_outputfile_type"] == "csv")
                {
                    string insertintojob = "";
                    dt = dataset.Tables[0];
                    var job_id = dataset.Tables[0].Rows[0]["result"];
                    var filename = job_id + "_" + objgeneratedynamicReport["in_report_name"];
                    if (job_id != null)
                    {
                        insertintojob = insertfileName(filename, job_id, constring, headerval.user_code);
                    }
                    return dt;
                }
                else
                {
                    string sourceFile = @"D:\Recon_files\Template\template.xltx";
                    //string sourceFile = @"D:\Recon_files\Template\Excel_template.xlsx";					
                    var job_id = dataset.Tables[1].Rows[0]["result"];
                    var filename = job_id + "_" + objgeneratedynamicReport["in_report_name"];
                    //string getsourcefolderpath = roleconfig_db("folder_path", constring);
                    string getdestFile = roleconfig_db("xlsx_folder_path", constring);
                    string destFile = getdestFile + filename + ".xlsx";
                    string insertintojob = "";
                    if (job_id != null)
                    {
                        insertintojob = insertfileName(filename, job_id, constring, headerval.user_code);
                    }
                    if (insertintojob == "Success")
                    {
                        //Debugger.Break();
                        getfiledType = getReportFieldType(objDataModel, headerval, constring);
                        //getDateFormat = configvalueData("excel_dateformat", headerval, constring);
                        //getDateTimeFormat = configvalueData("excel_datetimeformat", headerval, constring);
                        string sheetName = "Data";
                        //File.Copy(sourceFile, destFile, true);
                        using (var workbook = new XLWorkbook(sourceFile))
                        {
                            var worksheet = workbook.Worksheet(sheetName);
                            try
                            {
                                if (dataset.Tables[0].Rows.Count > 0)
                                {
                                    DataTable table = dataset.Tables[0];

                                    int startRow = 2; // Data starts from the second row
                                    int startColumn = 1; // Assuming data starts at column A

                                    for (int i = 0; i < table.Rows.Count; i++)
                                    {
                                        DataRow dataRow = table.Rows[i];
                                        for (int j = 0; j < table.Columns.Count; j++)
                                        {
                                            var cell = worksheet.Cell(startRow + i, startColumn + j);
                                            cell.Value = dataRow[j].ToString();

                                            string cellValue = dataRow[j]?.ToString() ?? "";

                                            // Check if there's a predefined format for this column
                                            columnFormats.TryGetValue(startColumn + j, out string predefinedFormat);

                                            bool valueAssigned = false;

                                            // Handle numeric conversion if the predefined format is numeric or custom format for numbers
                                            if (predefinedFormat != null && predefinedFormat.Contains("#"))
                                            {
                                                if (double.TryParse(cellValue, out double parsedNumber))
                                                {
                                                    cell.Value = parsedNumber;
                                                    valueAssigned = true;
                                                    cell.Style.NumberFormat.Format = predefinedFormat;
                                                }
                                            }

                                            // Handle date conversion explicitly if the format is for a date
                                            if (!valueAssigned && predefinedFormat != null && predefinedFormat.Contains("yyyy"))
                                            {
                                                if (DateTime.TryParse(cellValue, out DateTime parsedDate))
                                                {
                                                    cell.Value = parsedDate;
                                                    valueAssigned = true;
                                                    cell.Style.NumberFormat.Format = predefinedFormat;
                                                }
                                            }

                                            // If neither date nor number format matched, assign the original text value
                                            if (!valueAssigned)
                                            {
                                                cell.Value = cellValue;
                                            }

                                            // If there's no specific format from the template, reapply predefined or general format
                                            if (predefinedFormat != null)
                                            {
                                                cell.Style.NumberFormat.Format = predefinedFormat;
                                            }
                                            else
                                            {
                                                cell.Style.NumberFormat.Format = "General"; // Fallback if no format is found
                                            }

                                            // Ensure numeric values are right-aligned (default for numbers)
                                            if (valueAssigned && predefinedFormat != null && predefinedFormat.Contains("#"))
                                            {
                                                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    worksheet.Cell(1, 1).Value = "No Record Found";
                                }

                                workbook.SaveAs(destFile);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An error occurred while processing the worksheet: {ex.Message}");
                                throw;
                            }

                            UpdateJobStatus(job_id, "C", "Completed", constring, headerval.user_code);
                        }
                    }
                    dt = dataset.Tables[1];
                    return dt;
                }
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_run_dynamicreport" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objDataModel), "pr_run_dynamicreport", headerval.user_code, constring);
                throw ex;
            }
        }

        public DataTable generatedynamicReportData_new(ReportModel.DataModel objDataModel, UserManagementModel.headerValue headerval, string constring)
        {
            var job_id_err = "";          
            try
            {
                DataTable getfiledType = new DataTable();
                DataTable getDateFormat = new DataTable();
                DataTable getDateTimeFormat = new DataTable();
                DataTable getmaxrowsresultset = new DataTable();
                DataTable JsonData = new DataTable();
                DataTable sheetDt = new DataTable();
                DataSet dataset = new DataSet();
                DataTable dt = new DataTable();
                JsonData = JsonConvert.DeserializeObject<DataTable>(objDataModel.Dataset1);
                JArray jsonArray = JArray.Parse(objDataModel.Dataset2);
                sheetDt.Columns.Add("Filter Seq No", typeof(int));
                sheetDt.Columns.Add("(", typeof(string));
                sheetDt.Columns.Add("Filter Field", typeof(string));
                sheetDt.Columns.Add("Filter Criteria", typeof(string));
                sheetDt.Columns.Add("Filter Value", typeof(string));
                sheetDt.Columns.Add(")", typeof(string));
                sheetDt.Columns.Add("Joins", typeof(string));
                foreach (JObject obj in jsonArray)
                {
                    DataRow newRow = sheetDt.NewRow();
                    newRow["Filter Seq No"] = (int)obj["filter_seqno"];
                    newRow["("] = (string)obj["open_parentheses_flag"];
                    newRow["Filter Field"] = (string)obj["reportparam_value"];
                    newRow["Filter Criteria"] = (string)obj["filter_criteria_desc"];
                    newRow["Filter Value"] = (string)obj["filter_value"];
                    newRow[")"] = (string)obj["close_parentheses_flag"];
                    newRow["Joins"] = (string)obj["join_condition"];
                    sheetDt.Rows.Add(newRow);
                }
                var in_archival_code = "";
                var objgeneratedynamicReport = JsonData.Rows[0];
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_archival_code", in_archival_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objgeneratedynamicReport["in_reporttemplate_code"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplateresultset_code", objgeneratedynamicReport["in_reporttemplateresultset_code"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_recon_code", objgeneratedynamicReport["in_recon_code"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_code", objgeneratedynamicReport["in_report_code"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_param", objgeneratedynamicReport["in_report_param"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_condition", objgeneratedynamicReport["in_report_condition"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_outputfile_flag", objgeneratedynamicReport["in_outputfile_flag"], DbType.Boolean));
                parameters.Add(dbManager.CreateParameter("in_outputfile_type", objgeneratedynamicReport["in_outputfile_type"], DbType.String));
                parameters.Add(dbManager.CreateParameter("in_ip_addr", headerval.ip_address, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                dataset = dbManager.execStoredProcedurelist("pr_run_dynamicreport", CommandType.StoredProcedure, parameters.ToArray());
                if (objgeneratedynamicReport["in_outputfile_type"] == "csv")
                {
                    string insertintojob = "";
                    dt = dataset.Tables[0];
                    var job_id = dataset.Tables[0].Rows[0]["result"];
                    job_id_err = dataset.Tables[0].Rows[0]["result"].ToString();
                    var filename = job_id + "_" + objgeneratedynamicReport["in_report_name"];
                    if (job_id != null)
                    {
                        insertintojob = insertfileName(filename, job_id, constring, headerval.user_code);
                    }
                    return dt;
                }
                else
                {
                    UpdateJobStatus(dataset.Tables[1].Rows[0]["result"], "I", "Initiated", constring, headerval.user_code);
                    string uploadedFileName = objgeneratedynamicReport["file_name"].ToString();
                    string fileExtension = "";
                    fileExtension = System.IO.Path.GetExtension(uploadedFileName);
                    string sourceFile = "";
                    var job_id = dataset.Tables[1].Rows[0]["result"];
                    var filename = job_id + "_" + objgeneratedynamicReport["in_report_name"];
                    string getsourcefolderpath = roleconfig_db("folder_path", constring);
                    if (objgeneratedynamicReport["file_name"] == "" || objgeneratedynamicReport["in_reporttemplate_code"] == "")
                    {
                        sourceFile = roleconfig_db("temp_file_folder_path", constring);
                    }
                    else
                    {
                        sourceFile = getsourcefolderpath + objgeneratedynamicReport["in_reporttemplate_code"] + fileExtension;
                    }
                    string getdestFile = roleconfig_db("xlsx_folder_path", constring);
                    string getFileExtension = fileExtension == "" ? ".xlsx" : fileExtension;
                    string destFile = getdestFile + filename + getFileExtension;
                    string insertintojob = "";
                    if (job_id != null)
                    {
                        insertintojob = insertfileName(filename, job_id, constring, headerval.user_code);
                    }
                    if (insertintojob == "Success")
                    {
                        getfiledType = getReportFieldType(objDataModel, headerval, constring);
                        getDateFormat = configvalueData("excel_dateformat",constring);
                        getDateTimeFormat = configvalueData("excel_datetimeformat",constring);
                        getmaxrowsresultset = configvalueData("excel_max_rows", constring);
                        string sheetName = "Data";
                        CommonHeader objlog = new CommonHeader();
                        objlog.logger("SP:pr_run_dynamicreport" + "Source File:" + sourceFile);
                        objlog.logger("SP:pr_run_dynamicreport" + "Destination File:" + destFile);
                        File.Copy(sourceFile, destFile, true);
                        using (var workbook = new XLWorkbook(sourceFile))
                        {
                            var sheetNames = workbook.Worksheets.Select(ws => ws.Name.ToUpper()).ToList();
                            try
                            {
                                IXLWorksheet? worksheet = null;
                                if (sheetNames.Contains(sheetName.ToUpper()))
                                {
                                    worksheet = workbook.Worksheet(sheetName);
                                    worksheet.Clear(XLClearOptions.Contents);
                                }
                                else
                                {
                                    worksheet = workbook.Worksheets.Add(sheetName);
                                }

                                if (dataset.Tables[0].Rows.Count > 0)
                                {

                                    getDateFormat = configvalueData("excel_dateformat",constring);
                                    getDateTimeFormat = configvalueData("excel_datetimeformat", constring);                                   
                                    formatingexcelsheet(dataset.Tables[0], worksheet, getfiledType, getDateFormat, getDateTimeFormat, getmaxrowsresultset);
                                }
                                else
                                {
                                    worksheet.Cell(1, 1).InsertData("No Record Found");
                                }
                            }
                            catch (Exception ex)
                            {
                                objlog.logger("SP:pr_run_dynamicreport" + "Excel write:" + ex.Message);
                                throw;
                            }
                            var sheetName2 = "Condition Criteria";
                            IXLWorksheet? worksheet2 = null;
                            var existingSheet = workbook.Worksheets.FirstOrDefault(ws => ws.Name == sheetName2);
                            if (existingSheet != null)
                            {
                                worksheet2 = workbook.Worksheet(sheetName);
                                worksheet2.Clear(XLClearOptions.Contents);
                            }
                            else
                            {
                                worksheet2 = workbook.AddWorksheet(sheetName2);
                                worksheet2.Clear(XLClearOptions.Contents);
                            }
                            int maxCellLength = 32767;
                            try
                            {
                                if (sheetDt.Rows.Count > 0)
                                {
                                    // var table = worksheet2.Cell(1, 1).InsertTable(sheetDt.AsEnumerable().Take(1048500).CopyToDataTable(), "MyTable", true);
                                    // Create a cleaned copy of DataTable to avoid mutating original
                                    DataTable cleanedTable = sheetDt.Clone();

                                    foreach (DataRow row in sheetDt.Rows)
                                    {
                                        var newRow = cleanedTable.NewRow();

                                        for (int i = 0; i < sheetDt.Columns.Count; i++)
                                        {
                                            var val = row[i]?.ToString() ?? "";

                                            if (val.Length > maxCellLength)
                                            {
                                                val = val.Substring(0, maxCellLength);
                                            }
                                            newRow[i] = val;
                                        }
                                        cleanedTable.Rows.Add(newRow);
                                    }
                                    int maxrows = 0;
                                    int.TryParse(getmaxrowsresultset.Rows[0]["out_config_value"]?.ToString(), out maxrows);
                                    if (maxrows == 0)
                                    {
                                        maxrows = 100000;
                                    }
                                    // Insert into worksheet
                                    var table = worksheet2.Cell(1, 1)
                                                         .InsertTable(cleanedTable.AsEnumerable().Take(maxrows).CopyToDataTable(), "MyTable", true);

                                }
                                else
                                {
                                    worksheet2.Cell(1, 1).InsertData("No Record Found");
                                }
                                workbook.SaveAs(destFile);
                            }
                            catch (Exception ex)
                            {
                                objlog.logger("SP:pr_run_dynamicreport" + "Excel write1:" + ex.Message);
                                throw;
                            }                           
                            UpdateJobStatus(job_id, "C", "Completed", constring, headerval.user_code);
                        }                        
                    }
                    dt = dataset.Tables[1];
                    return dt;
                }
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                Updatedynamicerror(constring,"", headerval.user_code, ex.Message);
                objlog.logger("SP:pr_run_dynamicreport" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objDataModel), "pr_run_dynamicreport", headerval.user_code, constring);
                throw ex;
            }
        }

        /* Ends Testing for template and macro */

        private void CreateExcelFileoldbyhari(DataSet dataSet, string sourceFile, string destFile)
        {
            File.Copy(sourceFile, destFile, true);
            using (var workbook = new XLWorkbook(sourceFile))
            {
                var sheetNames = workbook.Worksheets.Select(ws => ws.Name).ToList();
                DataTable resultset1 = dataSet.Tables[1];

                for (int i = 0; i < resultset1.Rows.Count; i++)
                {
                    string sheetName = resultset1.Rows[i]["sheet_name"].ToString();
                    int dataSetIndex = i + 2;

                    if (sheetNames.Contains(sheetName))
                    {
                        var worksheet = workbook.Worksheet(sheetName);
                        worksheet.Clear(XLClearOptions.Contents);
                        //if(sheetNames.Contains("Dashboard"))
                        //{
                        //	string imagePath = "D:\\HEMA Personal\\Image\\Screenshot_2019-04-23-11-43-50-371_com.facebook.katana.png";
                        //	var imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                        //	worksheet.AddPicture(imageStream)
                        //			.MoveTo(worksheet.Cell("A1"))  // Set the position of the image
                        //			.WithSize(200, 200);  // Set the image size (optional)

                        //}
                        if (worksheet != null && dataSet.Tables.Count > dataSetIndex)
                        {
                            DataTable dataTable = dataSet.Tables[dataSetIndex];
                            try
                            {
                                if (dataTable.Rows.Count > 0)
                                {
                                    //formatingexcelsheet(dataTable, worksheet, getfiledType);
                                    worksheet.Cell(1, 1).InsertTable(dataTable.AsEnumerable().Take(1048500).CopyToDataTable());
                                }
                                else
                                {
                                    worksheet.Cell(1, 1).Value = "No Record Found";
                                }
                            }
                            catch (Exception ex)
                            {
                                CommonHeader objlog = new CommonHeader();
                                objlog.logger("SP:pr_run_dynamicreport" + "Function:CreateExcelFile1" + ex.Message);
                                throw;
                            }
                        }
                    }
                    else
                    {
                        if (dataSet.Tables.Count > dataSetIndex)
                        {
                            DataTable dataTable = dataSet.Tables[dataSetIndex];
                            var worksheet = workbook.Worksheets.Add(sheetName);
                            try
                            {
                                if (dataTable.Rows.Count > 0)
                                {
                                    worksheet.Cell(1, 1).InsertTable(dataTable.AsEnumerable().Take(1048500).CopyToDataTable());
                                }
                                else
                                {
                                    worksheet.Cell(1, 1).Value = "No Record Found";
                                }
                            }
                            catch (Exception ex)
                            {
                                CommonHeader objlog = new CommonHeader();
                                objlog.logger("SP:pr_run_dynamicreport" + "Function:CreateExcelFile2" + ex.Message);
                                throw;
                            }
                        }
                    }
                }
                workbook.SaveAs(destFile);
            }

        }
        private void CreateExcelFile(DataSet dataSet, string sourceFile, string destFile, string constring)
        {
            DataTable getmaxrowsresultset = new DataTable();
            getmaxrowsresultset = configvalueData("excel_max_rows",constring);
            int maxrows = 0;
            int.TryParse(getmaxrowsresultset.Rows[0]["out_config_value"]?.ToString(), out maxrows);
            if (maxrows == 0)
            {
                maxrows = 100000;
            }
            File.Copy(sourceFile, destFile, true);
            using (var workbook = new XLWorkbook(sourceFile))
            {
                var sheetNames = workbook.Worksheets.Select(ws => ws.Name.ToUpper()).ToList();
                DataTable resultset1 = dataSet.Tables[1];

                for (int i = 0; i < resultset1.Rows.Count; i++)
                {
                    string sheetName = resultset1.Rows[i]["sheet_name"].ToString();
                    int dataSetIndex = i + 2;

                    if (sheetNames.Contains(sheetName.ToUpper()))
                    {
                        var worksheet = workbook.Worksheet(sheetName);
                        worksheet.Clear(XLClearOptions.Contents);
                        //if(sheetNames.Contains("Dashboard"))
                        //{
                        //	string imagePath = "D:\\HEMA Personal\\Image\\Screenshot_2019-04-23-11-43-50-371_com.facebook.katana.png";
                        //	var imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                        //	worksheet.AddPicture(imageStream)
                        //			.MoveTo(worksheet.Cell("A1"))  // Set the position of the image
                        //			.WithSize(200, 200);  // Set the image size (optional)

                        //}
                        if (worksheet != null && dataSet.Tables.Count > dataSetIndex)
                        {
                            DataTable dataTable = dataSet.Tables[dataSetIndex];
                            try
                            {
                                if (dataTable.Rows.Count > 0)
                                {
                                    //formatingexcelsheet(dataTable, worksheet, getfiledType);
                                    worksheet.Cell(1, 1).InsertTable(dataTable.AsEnumerable().Take(maxrows).CopyToDataTable());
                                }
                                else
                                {
                                    worksheet.Cell(1, 1).Value = "No Record Found";
                                }
                            }
                            catch (Exception ex)
                            {
                                CommonHeader objlog = new CommonHeader();
                                objlog.logger("SP:pr_run_dynamicreport" + "Function:CreateExcelFile1" + ex.Message);
                                throw;
                            }
                        }
                    }
                    else
                    {
                        if (dataSet.Tables.Count > dataSetIndex)
                        {
                            DataTable dataTable = dataSet.Tables[dataSetIndex];
                            var worksheet = workbook.Worksheets.Add(sheetName);
                            try
                            {
                                if (dataTable.Rows.Count > 0)
                                {
                                    worksheet.Cell(1, 1).InsertTable(dataTable.AsEnumerable().Take(maxrows).CopyToDataTable());
                                }
                                else
                                {
                                    worksheet.Cell(1, 1).Value = "No Record Found";
                                }
                            }
                            catch (Exception ex)
                            {
                                CommonHeader objlog = new CommonHeader();
                                objlog.logger("SP:pr_run_dynamicreport" + "Function:CreateExcelFile2" + ex.Message);
                                throw;
                            }
                        }
                    }
                }
                workbook.SaveAs(destFile);
            }

        }
        private void CreateExcelFilemuti(DataSet dataSet, string sourceFile, string destFile, DataTable getDateFormatresultset, DataTable getDateTimeFormatresultset, string constring)
        {
            DataTable getfiledTyperesultset = new DataTable();
            DataTable getmaxrowsresultset = new DataTable();
            File.Copy(sourceFile, destFile, true);
            using (var workbook = new XLWorkbook(sourceFile))
            {
                var sheetNames = workbook.Worksheets.Select(ws => ws.Name.ToUpper()).ToList();
                DataTable resultset1 = dataSet.Tables[1];

                for (int i = 0; i < resultset1.Rows.Count; i++)
                {
                    getfiledTyperesultset = getreportresultsetFieldType(resultset1.Rows[i]["reporttemplate_code"].ToString(), resultset1.Rows[i]["reporttemplateresultset_code"].ToString(), constring);
                    string sheetName = resultset1.Rows[i]["sheet_name"].ToString();
                    int dataSetIndex = i + 2;

                    if (sheetNames.Contains(sheetName.ToUpper()))
                    {
                        var worksheet = workbook.Worksheet(sheetName);
                        worksheet.Clear(XLClearOptions.Contents);
                        //if(sheetNames.Contains("Dashboard"))
                        //{
                        //	string imagePath = "D:\\HEMA Personal\\Image\\Screenshot_2019-04-23-11-43-50-371_com.facebook.katana.png";
                        //	var imageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                        //	worksheet.AddPicture(imageStream)
                        //			.MoveTo(worksheet.Cell("A1"))  // Set the position of the image
                        //			.WithSize(200, 200);  // Set the image size (optional)

                        //}
                        if (worksheet != null && dataSet.Tables.Count > dataSetIndex)
                        {
                            DataTable dataTable = dataSet.Tables[dataSetIndex];
                            try
                            {
                                if (dataTable.Rows.Count > 0)
                                {
                                    getmaxrowsresultset = configvalueData("excel_max_rows", constring);
                                    //formatingexcelsheet(dataTable, worksheet, getfiledType);
                                    //worksheet.Cell(1, 1).InsertTable(dataTable.AsEnumerable().Take(1048500).CopyToDataTable());
                                    formatingexcelsheet(dataTable, worksheet, getfiledTyperesultset, getDateFormatresultset, getDateTimeFormatresultset, getmaxrowsresultset);
                                }
                                else
                                {
                                    worksheet.Cell(1, 1).Value = "No Record Found";
                                }
                            }
                            catch (Exception ex)
                            {
                                CommonHeader objlog = new CommonHeader();
                                objlog.logger("SP:pr_run_dynamicreport" + "Function:CreateExcelFile1" + ex.Message);
                                throw;
                            }
                        }
                    }
                    else
                    {
                        if (dataSet.Tables.Count > dataSetIndex)
                        {
                            DataTable dataTable = dataSet.Tables[dataSetIndex];
                            var worksheet = workbook.Worksheets.Add(sheetName);
                            try
                            {
                                if (dataTable.Rows.Count > 0)
                                {
                                    worksheet.Cell(1, 1).InsertTable(dataTable.AsEnumerable().Take(1048500).CopyToDataTable());
                                }
                                else
                                {
                                    worksheet.Cell(1, 1).Value = "No Record Found";
                                }
                            }
                            catch (Exception ex)
                            {
                                CommonHeader objlog = new CommonHeader();
                                objlog.logger("SP:pr_run_dynamicreport" + "Function:CreateExcelFile2" + ex.Message);
                                throw;
                            }
                        }
                    }
                }
                workbook.SaveAs(destFile);
            }

        }
        public void CreateTemplateExcel(DataSet dataSet, string sourceFile, string destFile, string constring)
        {

            DataTable getmaxrowsresultset = new DataTable();
            getmaxrowsresultset = configvalueData("excel_max_rows", constring);
            int maxrows = 0;
            int.TryParse(getmaxrowsresultset.Rows[0]["out_config_value"]?.ToString(), out maxrows);
            if (maxrows == 0)
            {
                maxrows = 100000;
            }
            File.Copy(sourceFile, destFile, true);

            using (var workbook = new XLWorkbook(destFile))
            {
                var worksheets = workbook.Worksheets.ToList();
                foreach (var ws in worksheets)
                {
                    workbook.Worksheets.Delete(ws.Name);
                }
                DataTable resultset1 = dataSet.Tables[1]; // contains "sheet_name" column

                for (int i = 0; i < resultset1.Rows.Count; i++)
                {
                    string sheetName = resultset1.Rows[i]["sheet_name"].ToString();
                    int dataSetIndex = i + 2;

                    if (dataSet.Tables.Count > dataSetIndex)
                    {
                        DataTable dataTable = dataSet.Tables[dataSetIndex];

                        // Add new worksheet
                        var worksheet = workbook.Worksheets.Add(sheetName);

                        try
                        {
                            if (dataTable.Rows.Count > 0)
                            {
                                worksheet.Cell(1, 1).InsertTable(dataTable.AsEnumerable().Take(maxrows).CopyToDataTable());
                            }
                            else
                            {
                                worksheet.Cell(1, 1).Value = "No Record Found";
                            }
                        }
                        catch (Exception ex)
                        {
                            CommonHeader objlog = new CommonHeader();
                            objlog.logger("SP:pr_run_dynamicreport" + "Function:CreateExcelFile" + ex.Message);
                            throw;
                        }
                    }
                }

                workbook.Save(); // Save directly to destFile
            }
        }
        public void CreateTemplateExcelmulti(DataSet dataSet, string sourceFile, string destFile,DataTable getDateFormatresultset,DataTable getDateTimeFormatresultset, string constring)
        {
            DataTable getfiledTyperesultset = new DataTable();
            DataTable getmaxrowsresultset = new DataTable();
            File.Copy(sourceFile, destFile, true);

            using (var workbook = new XLWorkbook(destFile))
            {
                var worksheets = workbook.Worksheets.ToList();
                foreach (var ws in worksheets)
                {
                    workbook.Worksheets.Delete(ws.Name);
                }
                DataTable resultset1 = dataSet.Tables[1]; // contains "sheet_name" column

                for (int i = 0; i < resultset1.Rows.Count; i++)
                {
                    getfiledTyperesultset = getreportresultsetFieldType(resultset1.Rows[i]["reporttemplate_code"].ToString(), resultset1.Rows[i]["reporttemplateresultset_code"].ToString(), constring);
                    string sheetName = resultset1.Rows[i]["sheet_name"].ToString();
                    int dataSetIndex = i + 2;

                    if (dataSet.Tables.Count > dataSetIndex)
                    {
                        DataTable dataTable = dataSet.Tables[dataSetIndex];

                        // Add new worksheet
                        var worksheet = workbook.Worksheets.Add(sheetName);

                        try
                        {
                            if (dataTable.Rows.Count > 0)
                            {
                                getmaxrowsresultset = configvalueData("excel_max_rows",constring);
                                //worksheet.Cell(1, 1).InsertTable(dataTable.AsEnumerable().Take(1048500).CopyToDataTable());
                                formatingexcelsheet(dataTable, worksheet, getfiledTyperesultset, getDateFormatresultset, getDateTimeFormatresultset, getmaxrowsresultset);
                            }
                            else
                            {
                                worksheet.Cell(1, 1).Value = "No Record Found";
                            }
                        }
                        catch (Exception ex)
                        {
                            CommonHeader objlog = new CommonHeader();
                            objlog.logger("SP:pr_run_dynamicreport" + "Function:CreateExcelFile" + ex.Message);
                            throw;
                        }
                    }
                }

                workbook.Save(); // Save directly to destFile
            }
        }
        private void formatingexcelsheet(DataTable data, IXLWorksheet? worksheet, DataTable getfiledType, DataTable getDateFormat1, DataTable getDatetimeFormat1,DataTable getmaxrows1)
        {
            DataTable getDateFormat = getDateFormat1;
            DataTable getDateTimeFormat = getDatetimeFormat1;
            DataTable getmaxrows = getmaxrows1;
            worksheet.Clear();
            int maxrows = 0;
            int.TryParse(getmaxrows.Rows[0]["out_config_value"]?.ToString(), out maxrows);
            if (maxrows == 0)
            {
                maxrows = 100000;
            }
            var table = worksheet.Cell(1, 1).InsertTable(data.AsEnumerable().Take(maxrows).CopyToDataTable(), "MyTable", true);
            for (int j = 0; j < data.Columns.Count; j++)
            {
                for (int i = 0; i < getfiledType.Rows.Count; i++)
                {
                    var column = table.Column(i + 1);
                    var dataType = "";
                    if (data.Columns[j].ColumnName.ToString() == getfiledType.Rows[i]["field_alias_name"].ToString())
                    {
                        dataType = getfiledType.Rows[i]["field_type"].ToString();
                        if (dataType == "DATE")
                        {
                            foreach (var cell in column.CellsUsed())
                            {
                                try
                                {
                                    if (DateTime.TryParse(cell.GetString(), out DateTime parsedDate))
                                    {
                                        cell.Value = parsedDate;
                                        cell.Style.DateFormat.Format = getDateFormat.Rows[0]["out_config_value"].ToString(); // Apply date format
                                    }

                                }
                                catch (Exception ex)
                                {
                                    cell.Style.NumberFormat.Format = "";
                                }
                            }
                        }
                        else if (dataType == "DATETIME")
                        {
                            //column.Style.DateFormat.Format = getDateFormat.Rows[0]["out_config_value"].ToString();

                            foreach (var cell in column.CellsUsed())
                            {
                                try
                                {
                                    if (DateTime.TryParse(cell.GetString(), out DateTime parsedDate))
                                    {
                                        cell.Value = parsedDate;
                                        cell.Style.DateFormat.Format = getDateTimeFormat.Rows[0]["out_config_value"].ToString(); // Apply date format
                                    }
                                }
                                catch (Exception ex)
                                {
                                    cell.Style.NumberFormat.Format = "";
                                }
                            }
                        }
                        else if (dataType == "DECIMAL" || dataType == "NUMERIC")
                        {
                            column.Style.NumberFormat.Format = "#,##0.00";
                        }
                        else if (dataType == "INTEGER")
                        {
                            column.Style.NumberFormat.Format = "0";
                        }
                        else
                        {
                            column.Style.NumberFormat.Format = "";
                        }
                    }
                }
            }
        }

        public DataTable updateOncheckquery(updateTemplateStatusOncheckqueryModel objCheckQuery, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DataSet ds = new DataSet();
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_checkquery_status", objCheckQuery.in_checkquery_status, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objCheckQuery.in_reporttemplate_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_updatetemplatestatus_oncheckquery", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_validate_sql_syntax" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objCheckQuery), "pr_validate_sql_syntax", headerval.user_code, constring);
                throw ex;
            }
        }
        //checkQueryData
        public DataTable checkQueryData(checkQueryModel objCheckQuery, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DataSet ds = new DataSet();
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_recon_code", objCheckQuery.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_query", objCheckQuery.in_report_condition, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_code", objCheckQuery.in_report_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_validate_sql_syntax", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_validate_sql_syntax" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objCheckQuery), "pr_validate_sql_syntax", headerval.user_code, constring);
                throw ex;
            }
        }
        public DataSet resultSetData(ResultSetModel objResultset, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DataSet ds = new DataSet();
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_reporttemplate_code", objResultset.reporttemplate_code, DbType.String));// hari changes 08-09-25
                parameters.Add(dbManager.CreateParameter("in_resultset_id", objResultset.reportresultset_gid, DbType.Int64));
                parameters.Add(dbManager.CreateParameter("in_report_code", objResultset.report_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_resultset_name", objResultset.resultset_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_sheet_name", objResultset.sheet_name, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_resultset_order", objResultset.resultset_order, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_exec_type", objResultset.resultset_exec_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_exec_type_data", objResultset.exec_type_data, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_action", objResultset.action, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_recon_mst_treporttemplateresultset", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_recon_mst_treporttemplateresultset" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objResultset), "pr_recon_mst_treporttemplateresultset", headerval.user_code, constring);
                throw ex;
            }
        }
        public DataSet reportData(ResultSetModel.report objReport, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DataSet ds = new DataSet();
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_report_id", objReport.report_gid, DbType.Int64));
                parameters.Add(dbManager.CreateParameter("in_report_code", objReport.report_code, DbType.String, ParameterDirection.InputOutput));
                parameters.Add(dbManager.CreateParameter("in_report_desc", objReport.report_desc, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_exec_type", objReport.report_exec_type, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_action", objReport.action, DbType.String));
                parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_recon_mst_treport", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return ds;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_recon_mst_treport" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objReport), "pr_recon_mst_treport", headerval.user_code, constring);
                throw ex;
            }
        }
		
		//getstandardreportlistservice
        public DataTable getstandardreportlistData(getsreport objgetsreportlist, UserManagementModel.headerValue headerval, string constring)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
                parameters.Add(dbManager.CreateParameter("in_recon_code", objgetsreportlist.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                ds = dbManager.execStoredProcedure("pr_get_standardreportlist", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_reportlist" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(headerval), "pr_get_reportlist", headerval.user_code, constring);
                throw ex;
            }

        }

        //getRoleReportTemplateListData
        //Pandiaraj 14-08-2025
        public DataTable getRoleReportTemplateListData(getReportTemplateListModel objgetReportTemplateListModel, string constring, UserManagementModel.headerValue headerval)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                parameters = new List<IDbDataParameter>();
				parameters.Add(dbManager.CreateParameter("in_recon_code", objgetReportTemplateListModel.in_recon_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_report_flag", objgetReportTemplateListModel.in_report_flag, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_user_code", headerval.user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_role_code", objgetReportTemplateListModel.in_role_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_lang_code", headerval.lang_code, DbType.String));
                ds = dbManager.execStoredProcedurelist("pr_get_role_reporttemplate_list", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                return result;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.logger("SP:pr_get_role_reporttemplate_list" + "Error Message:" + ex.Message);
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(objgetReportTemplateListModel), "pr_get_role_reporttemplate_list", headerval.user_code, constring);
                throw ex;
            }

        }

        public string Updatedynamicerror(string constring,string recon_code, string user_code,string job_remark)
        {
            try
            {
                DBManager dbManager = new DBManager(constring);
                Dictionary<string, Object> values = new Dictionary<string, object>();
                MySqlDataAccess con = new MySqlDataAccess("");
                parameters = new List<IDbDataParameter>();               
                parameters.Add(dbManager.CreateParameter("in_user_code", user_code, DbType.String));
                parameters.Add(dbManager.CreateParameter("in_recon_code", recon_code, DbType.String));
               parameters.Add(dbManager.CreateParameter("in_job_remark", job_remark, DbType.String));
               parameters.Add(dbManager.CreateParameter("out_msg", "out", DbType.String, ParameterDirection.Output));
                parameters.Add(dbManager.CreateParameter("out_result", "out", DbType.String, ParameterDirection.Output));
                ds = dbManager.execStoredProcedure("pr_upd_dynamicerrlog", CommandType.StoredProcedure, parameters.ToArray());
                result = ds.Tables[0];
                string outMsgValue = parameters[2].Value.ToString();
                return outMsgValue;
            }
            catch (Exception ex)
            {
                CommonHeader objlog = new CommonHeader();
                objlog.commonDataapi("", "SP", ex.Message + "Param:" + JsonConvert.SerializeObject(user_code), "pr_upd_dynamicerrlog", "", constring);
                return "failed";
            }
        }
    }
}
