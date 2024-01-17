using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Data.OleDb;
using System.Drawing;
using System.Net.Mail;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace SESWeb
{
    public class CommonMasters
    {
        string WebAPIUrl = System.Configuration.ConfigurationManager.AppSettings["WebAPIUrl"].ToString().Trim();

        #region PrivateVariables

        private string[] _CustomerNo = null;
        private string[] _SupplierCode = null;

        private string _Customer = null;
        private string _Supplier = null;

        private string _Description = null;
        private string _Group = null;
        private string _City = null;
        private string _ECNo = null;
        private string _CSTNo = null;
        private string _LSTNo = null;
        private string _Region = null;
        private string _Street1 = null;
        private string _Street2 = null;
        private string _City1 = null;
        private string _Pincode = null;
        private string _isOEM = null;
        private string _isOES = null;
        private string _isIAM = null;

        private string _Material = null;
        private string[] _Matnr = null;
        private string _Maktx = null;

        private string _Aennr = null;
        private string _Aedat = null;
        private string _Revlv = null;

        private string _ValidInd = null;

        private string[] _Pernr = null;
        private string _Ename = null;
        private string _Cptxt = null;
        private string _Cbtxt = null;
        private string _Orgunit = null;
        private string _Dtext = null;
        private string _Cdtxt = null;
        private string _Cetxt = null;
        private string _Fgbdt = null;
        private string _Zapdt = null;
        private string _Natxt = null;
        private string _Retxt = null;
        private string _Cmtxt = null;
        private string _Bgtxt = null;
        private string _Pfnum = null;
        private string _Penum = null;
        private string _Uanum = null;
        private string _Esino = null;
        private string _Pnume = null;
        private string[] _Spernr = null;
        private string[] _Rpernr = null;
        private string _Sname = null;
        private string _Rname = null;
        private string _Persk = null;
        private string[] _Parea = null;
        private string _Patxt = null;
        private string _Gesch = null;
        private string[] _Werks = null;
        private string[] _Objid = null;
        private string _Matxt = null;
        private string _Mddat = null;
        private string _Stell = null;
        private string _Wkwdy = null;
        private string _Mofid = null;

        private string _Plant = null;
        private string _Name1 = null;
        private string _Name2 = null;
        private string _Ort01 = null;
        private string _Pstlz = null;
        private string _Stras = null;
        private string _Cinno = null;
        private string _Bukrs = null;

        private string _SearchUnit = null;
        private string[] _Unit = null;
        private string _Udesc = null;
        private string _IsCorporate = null;

        private string _MailFrom = null;
        private string _MailTo = null;
        private string _MailCC = null;
        private string _MailBCC = null;
        private string _MailSubject = null;
        private string _MailBody = null;

        private string _AuthorizationToken = null;

        #endregion

        #region PublicVariables

        public string[] CustomerNo { get { return _CustomerNo; } set { _CustomerNo = value; } }
        public string[] SupplierCode { get { return _SupplierCode; } set { _SupplierCode = value; } }
        public string Customer { get { return _Customer; } set { _Customer = value; } }
        public string Supplier { get { return _Supplier; } set { _Supplier = value; } }

        public string Description { get { return _Description; } set { _Description = value; } }
        public string Group { get { return _Group; } set { _Group = value; } }
        public string City { get { return _City; } set { _City = value; } }
        public string ECNo { get { return _ECNo; } set { _ECNo = value; } }
        public string CSTNo { get { return _CSTNo; } set { _CSTNo = value; } }
        public string LSTNo { get { return _LSTNo; } set { _LSTNo = value; } }
        public string Region { get { return _Region; } set { _Region = value; } }
        public string Street1 { get { return _Street1; } set { _Street1 = value; } }
        public string Street2 { get { return _Street2; } set { _Street2 = value; } }
        public string City1 { get { return _City1; } set { _City1 = value; } }
        public string Pincode { get { return _Pincode; } set { _Pincode = value; } }
        public string isOEM { get { return _isOEM; } set { _isOEM = value; } }
        public string isOES { get { return _isOES; } set { _isOES = value; } }
        public string isIAM { get { return _isIAM; } set { _isIAM = value; } }

        public string Material { get { return _Material; } set { _Material = value; } }
        public string[] Matnr { get { return _Matnr; } set { _Matnr = value; } }
        public string Maktx { get { return _Maktx; } set { _Maktx = value; } }

        public string ValidInd { get { return _ValidInd; } set { _ValidInd = value; } }

        public string Aennr { get { return _Aennr; } set { _Aennr = value; } }
        public string Aedat { get { return _Aedat; } set { _Aedat = value; } }
        public string Revlv { get { return _Revlv; } set { _Revlv = value; } }

        public string[] Pernr { get { return _Pernr; } set { _Pernr = value; } }
        public string Ename { get { return _Ename; } set { _Ename = value; } }
        public string Cptxt { get { return _Cptxt; } set { _Cptxt = value; } }
        public string Cbtxt { get { return _Cbtxt; } set { _Cbtxt = value; } }
        public string Orgunit { get { return _Orgunit; } set { _Orgunit = value; } }
        public string Dtext { get { return _Dtext; } set { _Dtext = value; } }
        public string Cdtxt { get { return _Cdtxt; } set { _Cdtxt = value; } }
        public string Cetxt { get { return _Cetxt; } set { _Cetxt = value; } }
        public string Fgbdt { get { return _Fgbdt; } set { _Fgbdt = value; } }
        public string Zapdt { get { return _Zapdt; } set { _Zapdt = value; } }
        public string Natxt { get { return _Natxt; } set { _Natxt = value; } }
        public string Retxt { get { return _Retxt; } set { _Retxt = value; } }
        public string Cmtxt { get { return _Cmtxt; } set { _Cmtxt = value; } }
        public string Bgtxt { get { return _Bgtxt; } set { _Bgtxt = value; } }
        public string Pfnum { get { return _Pfnum; } set { _Pfnum = value; } }
        public string Penum { get { return _Penum; } set { _Penum = value; } }
        public string Uanum { get { return _Uanum; } set { _Uanum = value; } }
        public string Esino { get { return _Esino; } set { _Esino = value; } }
        public string Pnume { get { return _Pnume; } set { _Pnume = value; } }
        public string[] Spernr { get { return _Spernr; } set { _Spernr = value; } }
        public string[] Rpernr { get { return _Rpernr; } set { _Rpernr = value; } }
        public string Sname { get { return _Sname; } set { _Sname = value; } }
        public string Rname { get { return _Rname; } set { _Rname = value; } }
        public string Persk { get { return _Persk; } set { _Persk = value; } }
        public string[] Parea { get { return _Parea; } set { _Parea = value; } }
        public string Patxt { get { return _Patxt; } set { _Patxt = value; } }
        public string Gesch { get { return _Gesch; } set { _Gesch = value; } }
        public string[] Werks { get { return _Werks; } set { _Werks = value; } }
        public string[] Objid { get { return _Objid; } set { _Objid = value; } }
        public string Matxt { get { return _Matxt; } set { _Matxt = value; } }
        public string Mddat { get { return _Mddat; } set { _Mddat = value; } }
        public string Stell { get { return _Stell; } set { _Stell = value; } }
        public string Wkwdy { get { return _Wkwdy; } set { _Wkwdy = value; } }
        public string Mofid { get { return _Mofid; } set { _Mofid = value; } }

        public string Plant { get { return _Plant; } set { _Plant = value; } }
        public string Name1 { get { return _Name1; } set { _Name1 = value; } }
        public string Name2 { get { return _Name2; } set { _Name2 = value; } }
        public string Ort01 { get { return _Ort01; } set { _Ort01 = value; } }
        public string Pstlz { get { return _Pstlz; } set { _Pstlz = value; } }
        public string Stras { get { return _Stras; } set { _Stras = value; } }
        public string Cinno { get { return _Cinno; } set { _Cinno = value; } }
        public string Bukrs { get { return _Bukrs; } set { _Bukrs = value; } }


        public string SearchUnit { get { return _SearchUnit; } set { _SearchUnit = value; } }
        public string[] Unit { get { return _Unit; } set { _Unit = value; } }
        public string Udesc { get { return _Udesc; } set { _Udesc = value; } }
        public string IsCorporate { get { return _IsCorporate; } set { _IsCorporate = value; } }


        public string MailFrom { get { return _MailFrom; } set { _MailFrom = value; } }
        public string MailTo { get { return _MailTo; } set { _MailTo = value; } }
        public string MailCC { get { return _MailCC; } set { _MailCC = value; } }
        public string MailBCC { get { return _MailBCC; } set { _MailBCC = value; } }
        public string MailSubject { get { return _MailSubject; } set { _MailSubject = value; } }
        public string MailBody { get { return _MailBody; } set { _MailBody = value; } }

        public string AuthorizationToken { get { return _AuthorizationToken; } set { _AuthorizationToken = value; } }

        #endregion

        #region CommonMethods


        public DataSet GetCustomerDetails()
        {
            try
            {
                SqlParameter[] _param = new SqlParameter[17];

                _param[0] = new SqlParameter("@customer", (object)_Customer ?? DBNull.Value);
                if (_CustomerNo != null)
                {
                    if (_CustomerNo.Length > 0)
                    {
                        _param[1] = new SqlParameter("@customerno", (object)string.Join(",", _CustomerNo) ?? DBNull.Value);
                    }
                    else
                    {
                        _param[1] = new SqlParameter("@customerno", DBNull.Value);
                    }
                }
                else
                {
                    _param[1] = new SqlParameter("@customerno", DBNull.Value);
                }
                _param[2] = new SqlParameter("@customerdesc", (object)_Description ?? DBNull.Value);
                _param[3] = new SqlParameter("@customergroup", (object)_Group ?? DBNull.Value);
                _param[4] = new SqlParameter("@customercity", (object)_City ?? DBNull.Value);
                _param[5] = new SqlParameter("@customerecno", (object)_ECNo ?? DBNull.Value);
                _param[6] = new SqlParameter("@customercstno", (object)_CSTNo ?? DBNull.Value);
                _param[7] = new SqlParameter("@customerlstno", (object)_LSTNo ?? DBNull.Value);
                _param[8] = new SqlParameter("@customerregion", (object)_Region ?? DBNull.Value);
                _param[9] = new SqlParameter("@customerstreet1", (object)_Street1 ?? DBNull.Value);
                _param[10] = new SqlParameter("@customerstreet2", (object)_Street2 ?? DBNull.Value);
                _param[11] = new SqlParameter("@customercity1", (object)_City1 ?? DBNull.Value);
                _param[12] = new SqlParameter("@customerpincode", (object)_Pincode ?? DBNull.Value);
                _param[13] = new SqlParameter("@isOEM", (object)_isOEM ?? DBNull.Value);
                _param[14] = new SqlParameter("@isOES", (object)_isOES ?? DBNull.Value);
                _param[15] = new SqlParameter("@isIAM", (object)_isIAM ?? DBNull.Value);
                _param[16] = new SqlParameter("@customervalidind", (object)_ValidInd ?? DBNull.Value);

                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SPCommon_GetCustomerMaster", _param);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataSet GetSupplierDetails()
        {
            try
            {
                SqlParameter[] _param = new SqlParameter[13];

                _param[0] = new SqlParameter("@supplier", (object)_Supplier ?? DBNull.Value);

                if (_SupplierCode != null)
                {
                    if (_SupplierCode.Length > 0)
                    {
                        _param[1] = new SqlParameter("@suppliercode", (object)string.Join(",", _SupplierCode) ?? DBNull.Value);
                    }
                    else
                    {
                        _param[1] = new SqlParameter("@suppliercode", DBNull.Value);
                    }
                }
                else
                {
                    _param[1] = new SqlParameter("@suppliercode", DBNull.Value);
                }

                _param[2] = new SqlParameter("@supplierdesc", (object)_Description ?? DBNull.Value);
                _param[3] = new SqlParameter("@suppliercity", (object)_City ?? DBNull.Value);
                _param[4] = new SqlParameter("@supplierecno", (object)_ECNo ?? DBNull.Value);
                _param[5] = new SqlParameter("@suppliercstno", (object)_CSTNo ?? DBNull.Value);
                _param[6] = new SqlParameter("@supplierlstno", (object)_LSTNo ?? DBNull.Value);
                _param[7] = new SqlParameter("@supplierregion", (object)_Region ?? DBNull.Value);
                _param[8] = new SqlParameter("@supplierstreet1", (object)_Street1 ?? DBNull.Value);
                _param[9] = new SqlParameter("@supplierstreet2", (object)_Street2 ?? DBNull.Value);
                _param[10] = new SqlParameter("@suppliercity1", (object)_City1 ?? DBNull.Value);
                _param[11] = new SqlParameter("@supplierpincode", (object)_Pincode ?? DBNull.Value);
                _param[12] = new SqlParameter("@suppliervalidind", (object)_ValidInd ?? DBNull.Value);

                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SPCommon_GetSupplierMaster", _param);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataSet GetMaterialDetails()
        {
            try
            {
                SqlParameter[] _param = new SqlParameter[4];

                _param[0] = new SqlParameter("@material", (object)_Material ?? DBNull.Value);
                if (_Matnr != null)
                {
                    if (_Matnr.Length > 0)
                    {
                        _param[1] = new SqlParameter("@matnr", (object)string.Join(",", _Matnr) ?? DBNull.Value);
                    }
                    else
                    {
                        _param[1] = new SqlParameter("@matnr", DBNull.Value);
                    }
                }
                else
                {
                    _param[1] = new SqlParameter("@matnr", DBNull.Value);
                }

                _param[2] = new SqlParameter("@maktx", (object)_Maktx ?? DBNull.Value);
                _param[3] = new SqlParameter("@matnrvalidind", (object)_ValidInd ?? DBNull.Value);

                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SPCommon_GetMaterialMaster", _param);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataSet GetMaterialRevisionDetails()
        {
            try
            {
                SqlParameter[] _param = new SqlParameter[4];

                _param[0] = new SqlParameter("@aennr", (object)_Aennr ?? DBNull.Value);
                if (_Matnr != null)
                {
                    if (_Matnr.Length > 0)
                    {
                        _param[1] = new SqlParameter("@matnr", (object)string.Join(",", _Matnr) ?? DBNull.Value);
                    }
                    else
                    {
                        _param[1] = new SqlParameter("@matnr", DBNull.Value);
                    }
                }
                else
                {
                    _param[1] = new SqlParameter("@matnr", DBNull.Value);
                }
                _param[2] = new SqlParameter("@aedat", (object)_Aedat ?? DBNull.Value);
                _param[3] = new SqlParameter("@revlv", (object)_Revlv ?? DBNull.Value);

                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SPCommon_GetMaterialRevision", _param);
            }
            catch (Exception)
            {
                throw;
            }

        }


        public DataSet GetPersonalDetails()
        {
            try
            {
                SqlParameter[] _param = new SqlParameter[34];

                if (_Pernr != null)
                {
                    if (_Pernr.Length > 0)
                    {
                        _param[0] = new SqlParameter("@pernr", (object)string.Join(",", _Pernr) ?? DBNull.Value);
                    }
                    else
                    {
                        _param[0] = new SqlParameter("@pernr", DBNull.Value);
                    }
                }
                else
                {
                    _param[0] = new SqlParameter("@pernr", DBNull.Value);
                }
                _param[1] = new SqlParameter("@ename", (object)_Ename ?? DBNull.Value);
                _param[2] = new SqlParameter("@cptxt", (object)_Cptxt ?? DBNull.Value);
                _param[3] = new SqlParameter("@cbtxt", (object)_Cbtxt ?? DBNull.Value);
                _param[4] = new SqlParameter("@orgunit", (object)_Orgunit ?? DBNull.Value);
                _param[5] = new SqlParameter("@dtext", (object)_Dtext ?? DBNull.Value);
                _param[6] = new SqlParameter("@cdtxt", (object)_Cdtxt ?? DBNull.Value);
                _param[7] = new SqlParameter("@cetxt", (object)_Cetxt ?? DBNull.Value);
                _param[8] = new SqlParameter("@fgbdt", (object)_Fgbdt ?? DBNull.Value);
                _param[9] = new SqlParameter("@zapdt", (object)_Zapdt ?? DBNull.Value);
                _param[10] = new SqlParameter("@natxt", (object)_Natxt ?? DBNull.Value);
                _param[11] = new SqlParameter("@retxt", (object)_Retxt ?? DBNull.Value);
                _param[12] = new SqlParameter("@cmtxt", (object)_Cmtxt ?? DBNull.Value);
                _param[13] = new SqlParameter("@bgtxt", (object)_Bgtxt ?? DBNull.Value);
                _param[14] = new SqlParameter("@pfnum", (object)_Pfnum ?? DBNull.Value);
                _param[15] = new SqlParameter("@penum", (object)_Penum ?? DBNull.Value);
                _param[16] = new SqlParameter("@uanum", (object)_Uanum ?? DBNull.Value);
                _param[17] = new SqlParameter("@esino", (object)_Esino ?? DBNull.Value);
                _param[18] = new SqlParameter("@pnume", (object)_Pnume ?? DBNull.Value);

                if (_Spernr.Length > 0)
                {
                    _param[19] = new SqlParameter("@spernr", (object)string.Join(",", _Spernr) ?? DBNull.Value);
                }
                else
                {
                    _param[19] = new SqlParameter("@spernr", DBNull.Value);
                }

                if (_Rpernr.Length > 0)
                {
                    _param[20] = new SqlParameter("@rpernr", (object)string.Join(",", _Rpernr) ?? DBNull.Value);
                }
                else
                {
                    _param[20] = new SqlParameter("@rpernr", DBNull.Value);
                }

                _param[21] = new SqlParameter("@sname", (object)_Sname ?? DBNull.Value);
                _param[22] = new SqlParameter("@rname", (object)_Rname ?? DBNull.Value);
                _param[23] = new SqlParameter("@persk", (object)_Persk ?? DBNull.Value);

                if (_Parea.Length > 0)
                {
                    _param[24] = new SqlParameter("@parea", (object)string.Join(",", _Parea) ?? DBNull.Value);
                }
                else
                {
                    _param[24] = new SqlParameter("@parea", DBNull.Value);
                }

                _param[25] = new SqlParameter("@patxt", (object)_Patxt ?? DBNull.Value);
                _param[26] = new SqlParameter("@gesch", (object)_Gesch ?? DBNull.Value);

                if (_Werks.Length > 0)
                {
                    _param[27] = new SqlParameter("@werks", (object)string.Join(",", _Werks) ?? DBNull.Value);
                }
                else
                {
                    _param[27] = new SqlParameter("@werks", DBNull.Value);
                }

                if (_Objid.Length > 0)
                {
                    _param[28] = new SqlParameter("@objid", (object)string.Join(",", _Objid) ?? DBNull.Value);
                }
                else
                {
                    _param[28] = new SqlParameter("@objid", DBNull.Value);
                }

                _param[29] = new SqlParameter("@matxt", (object)_Matxt ?? DBNull.Value);
                _param[30] = new SqlParameter("@mddat", (object)_Mddat ?? DBNull.Value);
                _param[31] = new SqlParameter("@stell", (object)_Stell ?? DBNull.Value);
                _param[32] = new SqlParameter("@wkwdy", (object)_Wkwdy ?? DBNull.Value);
                _param[33] = new SqlParameter("@mofid", (object)_Mofid ?? DBNull.Value);


                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SPCommon_GetPersonalDetails", _param);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataSet GetPlantDetails()
        {
            try
            {
                SqlParameter[] _param = new SqlParameter[10];

                _param[0] = new SqlParameter("@plant", (object)_Plant ?? DBNull.Value);
                if (_Werks != null)
                {
                    if (_Werks.Length > 0)
                    {
                        _param[1] = new SqlParameter("@werks", (object)string.Join(",", _Werks) ?? DBNull.Value);
                    }
                    else
                    {
                        _param[1] = new SqlParameter("@werks", DBNull.Value);
                    }
                }
                else
                {
                    _param[1] = new SqlParameter("@werks", DBNull.Value);
                }
                _param[2] = new SqlParameter("@name1", (object)_Name1 ?? DBNull.Value);
                _param[3] = new SqlParameter("@name2", (object)_Name2 ?? DBNull.Value);
                _param[4] = new SqlParameter("@ort01", (object)_Ort01 ?? DBNull.Value);
                _param[5] = new SqlParameter("@pstlz", (object)_Pstlz ?? DBNull.Value);
                _param[6] = new SqlParameter("@stras", (object)_Stras ?? DBNull.Value);
                _param[7] = new SqlParameter("@region", (object)_Region ?? DBNull.Value);
                _param[8] = new SqlParameter("@cinno", (object)_Cinno ?? DBNull.Value);
                _param[9] = new SqlParameter("@bukrs", (object)_Bukrs ?? DBNull.Value);

                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SPCommon_GetPlantMaster", _param);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataSet GetUnitDetails()
        {
            try
            {
                SqlParameter[] _param = new SqlParameter[4];

                _param[0] = new SqlParameter("@searchunit", (object)_SearchUnit ?? DBNull.Value);
                if (_Werks != null)
                {
                    if (_Werks.Length > 0)
                    {
                        _param[1] = new SqlParameter("@werks", (object)string.Join(",", _Werks) ?? DBNull.Value);
                    }
                    else
                    {
                        _param[1] = new SqlParameter("@werks", DBNull.Value);
                    }
                }
                else
                {
                    _param[1] = new SqlParameter("@werks", DBNull.Value);
                }

                if (_Unit != null)
                {
                    if (_Unit.Length > 0)
                    {
                        _param[2] = new SqlParameter("@unit", (object)string.Join(",", _Unit) ?? DBNull.Value);
                    }
                    else
                    {
                        _param[2] = new SqlParameter("@unit", DBNull.Value);
                    }
                }
                else
                {
                    _param[2] = new SqlParameter("@unit", DBNull.Value);
                }
                _param[3] = new SqlParameter("@udesc", (object)_Udesc ?? DBNull.Value);
                // _param[4] = new SqlParameter("@iscorporate", (object)_IsCorporate ?? DBNull.Value);


                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SPCommon_GetUnitMaster", _param);
            }
            catch (Exception)
            {
                throw;
            }

        }


        public string GetSMTPIP()
        {
            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SPAPP_GETONPREMIPMASTER");

                return ds.Tables[0].Rows[0]["smtpip"].ToString();
            }
            catch (Exception)
            {
                throw;
            }

        }


        public string GetPIPIP()
        {
            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SPAPP_GETONPREMIPMASTER");

                return ds.Tables[0].Rows[0]["pipip"].ToString();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public string GetDBIP()
        {
            try
            {
                DataSet ds = SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SPAPP_GETONPREMIPMASTER");

                return ds.Tables[0].Rows[0]["dbip"].ToString();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void SendMail()
        {

            string smtpIP = GetSMTPIP();
            string smtpPort = ConfigurationManager.AppSettings["smtpPort"].ToString();
            string BODY1 = ConfigurationManager.AppSettings["mailBody1"].ToString();

            MailMessage Email = new MailMessage(_MailFrom, _MailTo);
            Email.Subject = _MailSubject;
            Email.IsBodyHtml = true;

            Email.Body = MailBody + BODY1;

            if (_MailCC != "" && _MailCC != null)
                Email.CC.Add(_MailCC);

            if (_MailBCC != "" && _MailBCC != null)
                Email.Bcc.Add(_MailBCC);

            System.Net.Mail.SmtpClient client = new SmtpClient(smtpIP, int.Parse(smtpPort));
            client.UseDefaultCredentials = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(Email);
            Email.Dispose();
        }

        public string ConvertRupeesToWords(long number)
        {
            if (number == 0) return "Zero";
            if (number < 0) return "minus " + ConvertRupeesToWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertRupeesToWords(number / 100000) + " Lakhs ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertRupeesToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertRupeesToWords(number / 100) + " Hundred ";
                number %= 100;
            }
            //if ((number / 10) > 0)
            //{
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";
            // number %= 10;
            //}
            if (number > 0)
            {
                if (words != "") words += "and ";
                var unitsMap = new[]
                {
                    "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
                };
                var tensMap = new[]
                {
                    "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
                };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            //words = words + " ONLY ";
            return words;
        }
        public DataTable ConvertExcelToDataTable(string strFilePath)
        {
            try
            {
                DataTable dtUploadData = new DataTable();
                //Open the Excel file in Read Mode using OpenXml.
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(strFilePath, false))
                {
                    //Read the first Sheet from Excel file.
                    Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();

                    //Get the Worksheet instance.
                    Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;

                    //Fetch all the rows present in the Worksheet.
                    IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>();

                    //Create a new DataTable.
                    //DataTable dt = new DataTable();

                    //Loop through the Worksheet rows.
                    foreach (Row row in rows)
                    {
                        //Use the first row to add columns to DataTable.
                        if (row.RowIndex.Value == 1)
                        {
                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                dtUploadData.Columns.Add(GetValue(doc, cell));
                            }
                        }
                        else
                        {
                            //Add rows to DataTable.
                            dtUploadData.Rows.Add();
                            int i = 0;
                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                //Cell cell = row.Descendants<Cell>().ElementAt(i);
                                int actualCellIndex = CellReferenceToIndex(cell);
                                dtUploadData.Rows[dtUploadData.Rows.Count - 1][actualCellIndex] = GetValue(doc, cell);
                                i++;
                            }
                        }
                    }
                }
                return dtUploadData;
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        private static int CellReferenceToIndex(Cell cell)
        {
            int index = 0;
            string reference = cell.CellReference.ToString().ToUpper();
            foreach (char ch in reference)
            {
                if (Char.IsLetter(ch))
                {
                    int value = (int)ch - (int)'A';
                    index = (index == 0) ? value : ((index + 1) * 26) + value;
                }
                else
                    return index;
            }
            return index;
        }

        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }

        #endregion

        #region WebAPICommonMethods

        public static DataTable GetCustomerDetailStructure()
        {
            DataTable dtCustomerStructure = new DataTable();
            dtCustomerStructure.Columns.Add("Customer", typeof(string));
            dtCustomerStructure.Columns.Add("CustomerNo", typeof(string));
            dtCustomerStructure.Columns.Add("Description", typeof(string));
            dtCustomerStructure.Columns.Add("Group", typeof(string));
            dtCustomerStructure.Columns.Add("City", typeof(string));
            dtCustomerStructure.Columns.Add("ECNo", typeof(string));
            dtCustomerStructure.Columns.Add("CSTNo", typeof(string));
            dtCustomerStructure.Columns.Add("LSTNo", typeof(string));
            dtCustomerStructure.Columns.Add("Region", typeof(string));
            dtCustomerStructure.Columns.Add("Street1", typeof(string));
            dtCustomerStructure.Columns.Add("Street2", typeof(string));
            dtCustomerStructure.Columns.Add("City1", typeof(string));
            dtCustomerStructure.Columns.Add("Pincode", typeof(string));
            dtCustomerStructure.Columns.Add("isOEM", typeof(string));
            dtCustomerStructure.Columns.Add("isOES", typeof(string));
            dtCustomerStructure.Columns.Add("isIAM", typeof(string));
            dtCustomerStructure.Columns.Add("ValidInd", typeof(string));

            return dtCustomerStructure;
        }

        public static DataTable GetPlantDetailStructure()
        {
            DataTable dtPlantStructure = new DataTable();
            dtPlantStructure.Columns.Add("SearchPlant", typeof(string));
            dtPlantStructure.Columns.Add("Werks", typeof(string));
            dtPlantStructure.Columns.Add("Name1", typeof(string));
            dtPlantStructure.Columns.Add("Name2", typeof(string));
            dtPlantStructure.Columns.Add("Name3", typeof(string));
            dtPlantStructure.Columns.Add("Ort01", typeof(string));
            dtPlantStructure.Columns.Add("Pstlz", typeof(string));
            dtPlantStructure.Columns.Add("Stras", typeof(string));
            dtPlantStructure.Columns.Add("Cinno", typeof(string));
            dtPlantStructure.Columns.Add("Bukrs", typeof(string));
            dtPlantStructure.Columns.Add("PlantName", typeof(string));

            return dtPlantStructure;
        }


        public static DataTable GetUnitDetailStructure()
        {
            DataTable dtUnitStructure = new DataTable();
            dtUnitStructure.Columns.Add("SearchUnit", typeof(string));
            dtUnitStructure.Columns.Add("Werks", typeof(string));
            dtUnitStructure.Columns.Add("Unit", typeof(string));
            dtUnitStructure.Columns.Add("UnitDesc", typeof(string));
            dtUnitStructure.Columns.Add("PlantName", typeof(string));
            dtUnitStructure.Columns.Add("UnitName", typeof(string));

            return dtUnitStructure;
        }

        public DataTable GetWebAPIPlantDetails()
        {
            try
            {
                //IPCRInputDetails objIPCRInputDetails = new IPCRInputDetails();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(WebAPIUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthorizationToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/Common/GetPlantDetails?BusinessArea=" + _Bukrs).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Read the response body as string
                    string json = response.Content.ReadAsStringAsync().Result;

                    // deserialize the JSON response returned from the Web API back to a login_info object

                    var dt = JsonConvert.DeserializeObject<DataTable>(json);

                    //DataTable dt = GetDataTableFromJsonString(json);
                    return dt;
                }
                else
                {
                    return GetPlantDetailStructure();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetWebAPIUnitDetails()
        {
            try
            {
                //IPCRInputDetails objIPCRInputDetails = new IPCRInputDetails();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(WebAPIUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthorizationToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/Common/GetUnitDetails?Plant=" + _Plant).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Read the response body as string
                    string json = response.Content.ReadAsStringAsync().Result;

                    // deserialize the JSON response returned from the Web API back to a login_info object

                    var dt = JsonConvert.DeserializeObject<DataTable>(json);

                    //DataTable dt = GetDataTableFromJsonString(json);
                    return dt;
                }
                else
                {
                    return GetUnitDetailStructure();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }

    public class CustomerDetailStructure
    {
        public string Customer { get; set; }
        public string CustomerNo { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public string City { get; set; }
        public string ECNo { get; set; }
        public string CSTNo { get; set; }
        public string LSTNo { get; set; }
        public string Region { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City1 { get; set; }
        public string Pincode { get; set; }
        public string isOEM { get; set; }
        public string isOES { get; set; }
        public string isIAM { get; set; }
        public string ValidInd { get; set; }
    }

    public class SupplierDetailStructure
    {
        public string Supplier { get; set; }
        public string SupplierCode { get; set; }
        public string Description { get; set; }
        public string Group { get; set; }
        public string City { get; set; }
        public string ECNo { get; set; }
        public string CSTNo { get; set; }
        public string LSTNo { get; set; }
        public string Region { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City1 { get; set; }
        public string Pincode { get; set; }
        public string ValidInd { get; set; }
    }

    public class MaterialDetailStructure
    {
        public string MaterialSearch { get; set; }
        public string Matnr { get; set; }
        public string Maktx { get; set; }
        public string ValidInd { get; set; }
    }

    public class MaterialRevisionDetailStructure
    {
        public string Aennr { get; set; }
        public string Matnr { get; set; }
        public string Aedat { get; set; }
        public string Revlv { get; set; }
    }

    public class PersonalDetailStructure
    {
        public string Pernr { get; set; }
        public string Ename { get; set; }
        public string Cptxt { get; set; }
        public string Cbtxt { get; set; }
        public string Orgunit { get; set; }
        public string Dtext { get; set; }
        public string Cdtxt { get; set; }
        public string Cetxt { get; set; }
        public string Fgbdt { get; set; }
        public string Zapdt { get; set; }
        public string Natxt { get; set; }
        public string Retxt { get; set; }
        public string Cmtxt { get; set; }
        public string Bgtxt { get; set; }
        public string Pfnum { get; set; }
        public string Penum { get; set; }
        public string Uanum { get; set; }
        public string Esino { get; set; }
        public string Pnume { get; set; }
        public string Spernr { get; set; }
        public string Rpernr { get; set; }
        public string Sname { get; set; }
        public string Rname { get; set; }
        public string Persk { get; set; }
        public string Parea { get; set; }
        public string Patxt { get; set; }
        public string Gesch { get; set; }
        public string Werks { get; set; }
        public string Objid { get; set; }
        public string Matxt { get; set; }
        public string Mddat { get; set; }
        public string Stell { get; set; }
        public string Wkwdy { get; set; }
        public string Mofid { get; set; }
    }

    public class PlantDetailStructure
    {
        public string SearchPlant { get; set; }
        public string Werks { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Ort01 { get; set; }
        public string Pstlz { get; set; }
        public string Stras { get; set; }
        public string Cinno { get; set; }
        public string Bukrs { get; set; }
        public string PlantName { get; set; }
    }

    public class UnitDetailStructure
    {
        public string SearchUnit { get; set; }
        public string Werks { get; set; }
        public string Unit { get; set; }
        public string UnitDesc { get; set; }
        public string PlantName { get; set; }
        public string UnitName { get; set; }
    }

    public class SendMailStructure
    {
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string MailCC { get; set; }
        public string MailBCC { get; set; }
        public string MailSubject { get; set; }
        public string MailBody { get; set; }
    }

}