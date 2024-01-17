using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;

namespace SESWeb
{
    public class ClsSES
    {
        private SqlParameter[] _params;

        string _Role = null;
        public string Role { get { return _Role; } set { _Role = value; } }
        string _ServiceEntryNo = null;
        public string ServiceEntryNo { get { return _ServiceEntryNo; } set { _ServiceEntryNo = value; } }
        string _SearchString = null;
        public string SearchString { get { return _SearchString; } set { _SearchString = value; } }
        string _WorkflowMember = null;
        public string WorkflowMember { get { return _WorkflowMember; } set { _WorkflowMember = value; } }
        int _Sequence = 0;
        public int Sequence { get { return _Sequence; } set { _Sequence = value; } }
        int _SNo = 0;
        public int SNo { get { return _SNo; } set { _SNo = value; } }
        string _EmpNo = null;
        public string EmpNo { get { return _EmpNo; } set { _EmpNo = value; } }
        double _InvoiceValue = 0;
        public double InvoiceValue { get { return _InvoiceValue; } set { _InvoiceValue = value; } }
        string _Comments = null;
        public string Comments { get { return _Comments; } set { _Comments = value; } }
        string _FileName = null;
        public string FileName { get { return _FileName; } set { _FileName = value; } }
        string _InvoiceReceiptDate = null;
        public string InvoiceReceiptDate { get { return _InvoiceReceiptDate; } set { _InvoiceReceiptDate = value; } }
        string _InvoiceDate = null;
        public string InvoiceDate { get { return _InvoiceDate; } set { _InvoiceDate = value; } }
        string _AliasName = null;
        public string AliasName { get { return _AliasName; } set { _AliasName = value; } }
        string _Mode = null;
        public string Mode { get { return _Mode; } set { _Mode = value; } }
        string _SAPIND = null;
        public string SAPIND { get { return _SAPIND; } set { _SAPIND = value; } }
        string _ValidationMessage = null;
        public string ValidationMessage { get { return _ValidationMessage; } set { _ValidationMessage = value; } }


        public DataSet GetUserRole()
        {
            try
            {
                _params = new SqlParameter[2];
                _params[0] = new SqlParameter("@EmpNo", EmpNo);
                _params[1] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_GetUserRole", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetBulkApprovalMode()
        {
            try
            {
                _params = new SqlParameter[2];
                _params[0] = new SqlParameter("@EmpNo", EmpNo);
                _params[1] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_GetBulkApprovalMode", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetDashboardDetails()
        {
            try
            {
                _params = new SqlParameter[2];
                _params[0] = new SqlParameter("@EmpNo", EmpNo);
                _params[1] = new SqlParameter("@Role", Role);

                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_GetDashboardDetails", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetSESDetails()
        {
            try
            {
                _params = new SqlParameter[1];
                _params[0] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_GetSESDetails", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetEmployee()
        {
            try
            {
                _params = new SqlParameter[2];
                _params[0] = new SqlParameter("@SearchString", SearchString);
                _params[1] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_GetEmployee", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool AddWorkflowMember()
        {
            try
            {
                _params = new SqlParameter[3];
                _params[0] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                _params[1] = new SqlParameter("@WorkflowMember", WorkflowMember);
                _params[2] = new SqlParameter("@Return", DBNull.Value);
                _params[2].SqlDbType = SqlDbType.Int;
                _params[2].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_AddWorkflowMember", _params);
                if ((int)_params[2].Value > 0)
                    return true;
                else
                {
                    ValidationMessage = "Unable To Process Your Request, Please Try Again Later";
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteWorkflowMember()
        {
            try
            {
                _params = new SqlParameter[4];
                _params[0] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                _params[1] = new SqlParameter("@Sequence", Sequence);
                _params[2] = new SqlParameter("@WorkflowMember", WorkflowMember);
                _params[3] = new SqlParameter("@Return", DBNull.Value);
                _params[3].SqlDbType = SqlDbType.Int;
                _params[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_DeleteWorkflowMember", _params);
                if ((int)_params[3].Value > 0)
                    return true;
                else
                {
                    ValidationMessage = "Unable To Process Your Request, Please Try Again Later";
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetWorkflowMembers()
        {
            try
            {
                _params = new SqlParameter[1];
                _params[0] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_GetWorkflowMembers", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetFiles()
        {
            try
            {
                _params = new SqlParameter[1];
                _params[0] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_GetFiles", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet GetOtherFiles()
        {
            try
            {
                _params = new SqlParameter[1];
                _params[0] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_GetOtherFiles", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool SaveFile()
        {
            try
            {
                _params = new SqlParameter[5];
                _params[0] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                _params[1] = new SqlParameter("@FileName", FileName);
                _params[2] = new SqlParameter("@AliasName", AliasName);
                _params[3] = new SqlParameter("@EmpNo", EmpNo);
                _params[4] = new SqlParameter("@Return", DBNull.Value);
                _params[4].SqlDbType = SqlDbType.Int;
                _params[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_SaveFiles", _params);
                if ((int)_params[4].Value > 0)
                    return true;
                else
                {
                    ValidationMessage = "Unable To Process Your Request, Please Try Again Later";
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool SaveOtherFile()
        {
            try
            {
                _params = new SqlParameter[4];
                _params[0] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                _params[1] = new SqlParameter("@FileName", FileName);
                _params[2] = new SqlParameter("@EmpNo", EmpNo);
                _params[3] = new SqlParameter("@Return", DBNull.Value);
                _params[3].SqlDbType = SqlDbType.Int;
                _params[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_SaveOtherFiles", _params);
                if ((int)_params[3].Value > 0)
                    return true;
                else
                {
                    ValidationMessage = "Unable To Process Your Request, Please Try Again Later";
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteFile()
        {
            try
            {
                _params = new SqlParameter[4];
                _params[0] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                _params[1] = new SqlParameter("@AliasName", AliasName);
                _params[2] = new SqlParameter("@EmpNo", EmpNo);
                _params[3] = new SqlParameter("@Return", DBNull.Value);
                _params[3].SqlDbType = SqlDbType.Int;
                _params[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_DeleteFiles", _params);
                if ((int)_params[3].Value > 0)
                    return true;
                else
                {
                    ValidationMessage = "Unable To Process Your Request, Please Try Again Later";
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteOtherFile()
        {
            try
            {
                _params = new SqlParameter[5];
                _params[0] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                _params[1] = new SqlParameter("@SNo", SNo);
                _params[2] = new SqlParameter("@FileName", FileName);
                _params[3] = new SqlParameter("@EmpNo", EmpNo);
                _params[4] = new SqlParameter("@Return", DBNull.Value);
                _params[4].SqlDbType = SqlDbType.Int;
                _params[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_DeleteOtherFiles", _params);
                if ((int)_params[4].Value > 0)
                    return true;
                else
                {
                    ValidationMessage = "Unable To Process Your Request, Please Try Again Later";
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateFinanceFileDownloadStatus()
        {
            try
            {
                _params = new SqlParameter[4];
                _params[0] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                _params[1] = new SqlParameter("@FileName", FileName);
                _params[2] = new SqlParameter("@EmpNo", EmpNo);
                _params[3] = new SqlParameter("@Return", DBNull.Value);
                _params[3].SqlDbType = SqlDbType.Int;
                _params[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_UpdateFinanceFileDownloadStatus", _params);
                if ((int)_params[3].Value > 0)
                    return true;
                else
                {
                    ValidationMessage = "Unable To Process Your Request, Please Try Again Later";
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateSESDetails()
        {
            try
            {
                _params = new SqlParameter[9];
                _params[0] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                _params[1] = new SqlParameter("@EmpNo", EmpNo);
                _params[2] = new SqlParameter("@InvoiceValue", InvoiceValue);
                _params[3] = new SqlParameter("@InvoiceDate", InvoiceDate);
                _params[4] = new SqlParameter("@InvoiceReceiptDate", InvoiceReceiptDate);
                _params[5] = new SqlParameter("@Comments", Comments);
                _params[6] = new SqlParameter("@SAPIND", SAPIND);
                _params[7] = new SqlParameter("@Mode", Mode);
                _params[8] = new SqlParameter("@Return", DBNull.Value);
                _params[8].SqlDbType = SqlDbType.Int;
                _params[8].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_UpdateSESDetails", _params);
                if ((int)_params[8].Value > 0)
                    return true;
                else
                {
                    ValidationMessage = "Unable To Process Your Request, Please Try Again Later";
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public class MailSendDetails
        {
            public string MailFrom { get; set; }
            public string MailTo { get; set; }
            public string MailCC { get; set; }
            public string MailBCC { get; set; }
            public string MailSubject { get; set; }
            public string MailBody { get; set; }
        }
        public MailSendDetails GetMailSendDetails()
        {
            try
            {
                _params = new SqlParameter[3];
                _params[0] = new SqlParameter("@ServiceEntryNo", ServiceEntryNo);
                _params[1] = new SqlParameter("@EmpNo", EmpNo);
                _params[2] = new SqlParameter("@Mode", Mode);

                DataSet _DSGetMailList = SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_SES_GetMailSendDetails", _params);

                MailSendDetails objMailSendDetails = new MailSendDetails();
                if (_DSGetMailList.Tables[0].Rows.Count > 0)
                {
                    DataRow drMailSendDetails = _DSGetMailList.Tables[0].Rows[0];
                    objMailSendDetails.MailFrom = drMailSendDetails["MailFrom"].ToString().Trim();
                    objMailSendDetails.MailTo = drMailSendDetails["MailTo"].ToString().Trim();
                    objMailSendDetails.MailCC = drMailSendDetails["MailCC"].ToString().Trim();
                    objMailSendDetails.MailBCC = drMailSendDetails["MailBCC"].ToString().Trim();
                    objMailSendDetails.MailSubject = drMailSendDetails["MailSubject"].ToString().Trim();
                    objMailSendDetails.MailBody = drMailSendDetails["MailBody"].ToString().Trim();
                }
                else
                {
                    objMailSendDetails.MailFrom = "";
                    objMailSendDetails.MailTo = "";
                    objMailSendDetails.MailCC = "";
                    objMailSendDetails.MailBCC = "";
                    objMailSendDetails.MailSubject = "";
                    objMailSendDetails.MailBody = "";
                }
                return objMailSendDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool SendMail(MailSendDetails _Mail)
        {
            try
            {
                if (_Mail.MailFrom.Equals(""))
                {
                    this._ValidationMessage = "SES Mail Error : Your Email id not updated in system.";
                    return false;
                }
                if (_Mail.MailTo.Equals(""))
                {
                    this._ValidationMessage = "SES Mail Error : No Recipient mail available.";
                    return false;
                }
                if (_Mail.MailSubject.Equals(""))
                {
                    this._ValidationMessage = "SES Mail Error : Email Subject not available.";
                    return false;
                }
                if (_Mail.MailBody.Equals(""))
                {
                    this._ValidationMessage = "SES Mail Error : Email Body not available.";
                    return false;
                }

                CommonMasters objCommonMasters = new CommonMasters();
                objCommonMasters.MailBody = _Mail.MailBody;
                objCommonMasters.MailFrom = _Mail.MailFrom;
                objCommonMasters.MailTo = _Mail.MailTo;
                objCommonMasters.MailSubject = _Mail.MailSubject;
                if (_Mail.MailCC.Length > 0)
                    objCommonMasters.MailCC = _Mail.MailCC;
                if (_Mail.MailBCC.Length > 0)
                    objCommonMasters.MailBCC = _Mail.MailBCC;
                Thread EmailHandler = new Thread(delegate ()
                {
                    objCommonMasters.SendMail();
                });
                EmailHandler.IsBackground = true;
                EmailHandler.Start();
                return true;
                //return false; // set in development
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}