using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
//using Oracle.ManagedDataAccess.Client;
//using Oracle.ManagedDataAccess.Types;
using System.Configuration;
namespace SESWeb
{
    public class Employee
    {
        // Private Variables
        private string _Pernr = null;
        private string _Ename = null;
        private string _Cptxt = null;
        private string _Cbtxt = null;
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
        private string _Esino = null;
        private string _Pnume = null;
        private string _Spernr = null;
        private string _Rpernr = null;
        private string _Sname = null;
        private string _Rname = null;
        private string _Persk = null;
        private string _Parea = null;
        private string _Patxt = null;
        private string _Gesch = null;
        private string _Werks = null;
        private string _Objid = null;
        private string _Matxt = null;
        private string _Mddat = null;
        private string _Stell = null;
        private string _Wkwdy = null;
        private string _Mofid = null;
        private string _Orgunit = null;
        private string _Uanum = null;
        private string _Appnr = null;
        private string _Appdept = null;
        private string _AppGrade = null;
        private SqlParameter _param;
       // private SqlParameter[] _params;
        // Properties
        public string EmployeeNo { get { return _Pernr; } set { _Pernr = value; } }
        public string EmployeeName { get { return _Ename; } }
        public string Designation { get { return _Cptxt; } }
        public string Location { get { return _Cbtxt; } }
        public string Department { get { return _Dtext; } }
        public string DepartmentName { get { return _Cdtxt; } }
        public string Grade { get { return _Cetxt; } }
        public string BirthDate { get { return _Fgbdt; } }
        public string JoningDate { get { return _Zapdt; } }
        public string Nationality { get { return _Natxt; } }
        public string Relegion { get { return _Retxt; } }
        public string Community { get { return _Cmtxt; } }
        public string BloodGroup { get { return _Bgtxt; } }
        public string PFNo { get { return _Pfnum; } }
        public string PensionNo { get { return _Penum; } }
        public string ESINo { get { return _Esino; } }
        public string PANNo { get { return _Pnume; } }
        public string ApproverNo { get { return _Spernr; } }
        public string RefererNo { get { return _Rpernr == "00000000" ? "" : _Rpernr; } }
        public string ApproverName { get { return _Sname; } }
        public string RefererName { get { return _Rname; } }
        public string Employeesubgroup { get { return _Persk; } }
        public string Payrollarea { get { return _Parea; } }
        public string PayrollAreaText { get { return _Patxt; } }
        public string GenderKey { get { return _Gesch; } }
        public string Personnelarea { get { return _Werks; } }
        public string Personnelsubarea { get { return _Objid; } }
        public string MaritalStatus { get { return _Matxt; } }
        public string ValidFromDateofCurrentMaritalStatus { get { return _Mddat; } }
        public string JobDescription { get { return _Stell; } }
        public string WeeklyWorkingDays { get { return _Wkwdy; } }
        public string HolidayCalendar { get { return _Mofid; } }
        public string OrganaisationUnit { get { return _Orgunit; } }
        public string UniversalAccountNo { get { return _Uanum; } }
        public string Appnr { get { return _Appnr; } set { _Appnr = value; } }
        public string ApproverDepartment { get { return _Appdept; } set { _Appdept = value; } }
        public string ApproverGrade { get { return _AppGrade; } set { _AppGrade = value; } }

        // Inserted By Geetha N on 19.02.2020
        private SqlParameter[] _params;
        private String _ValidationMessage = null;

        private string _AccountType = null;
        public string AccountType { get { return _AccountType; } set { _AccountType = value; } }

        private string _BankName = null;
        public string BankName { get { return _BankName; } set { _BankName = value; } }

        private string _BranchName = null;
        public string BranchName { get { return _BranchName; } set { _BranchName = value; } }

        private string _AccountNumber = null;
        public string AccountNumber { get { return _AccountNumber; } set { _AccountNumber = value; } }

        private string _IFSCCode = null;
        public string IFSCCode { get { return _IFSCCode; } set { _IFSCCode = value; } }

        private string _MICRCode = null;
        public string MICRCode { get { return _MICRCode; } set { _MICRCode = value; } }

        private string _YearMonth = string.Empty;
        public string YearMonth { get { return _YearMonth; } set { _YearMonth = value; } }
        // Ended Here

        // Constructor
        public Employee()
        { }
        public Employee(string Pernr)
        {
            try
            {
                _param = new SqlParameter("@Pernr", Pernr);
                DataSet dsEmployee = SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "sp_Employee_SelectEmployeeById", _param);
                if (dsEmployee.Tables[0].Rows.Count > 0)
                {
                    _Pernr = dsEmployee.Tables[0].Rows[0]["Pernr"].ToString();
                    _Ename = dsEmployee.Tables[0].Rows[0]["Ename"].ToString();
                    _Cptxt = dsEmployee.Tables[0].Rows[0]["Cptxt"].ToString();
                    _Cbtxt = dsEmployee.Tables[0].Rows[0]["Cbtxt"].ToString();
                    _Orgunit = dsEmployee.Tables[0].Rows[0]["Orgunit"].ToString();
                    _Dtext = dsEmployee.Tables[0].Rows[0]["Dtext"].ToString();
                    _Cdtxt = dsEmployee.Tables[0].Rows[0]["Cdtxt"].ToString();
                    _Cetxt = dsEmployee.Tables[0].Rows[0]["Cetxt"].ToString();
                    _Fgbdt = dsEmployee.Tables[0].Rows[0]["Fgbdt"].ToString();
                    _Zapdt = dsEmployee.Tables[0].Rows[0]["Zapdt"].ToString();
                    _Natxt = dsEmployee.Tables[0].Rows[0]["Natxt"].ToString();
                    _Retxt = dsEmployee.Tables[0].Rows[0]["Retxt"].ToString();
                    _Cmtxt = dsEmployee.Tables[0].Rows[0]["Cmtxt"].ToString();
                    _Bgtxt = dsEmployee.Tables[0].Rows[0]["Bgtxt"].ToString();
                    _Pfnum = dsEmployee.Tables[0].Rows[0]["Pfnum"].ToString();
                    _Penum = dsEmployee.Tables[0].Rows[0]["Penum"].ToString();
                    _Uanum = dsEmployee.Tables[0].Rows[0]["Uanum"].ToString();
                    _Esino = dsEmployee.Tables[0].Rows[0]["Esino"].ToString();
                    _Pnume = dsEmployee.Tables[0].Rows[0]["Pnume"].ToString();
                    _Spernr = dsEmployee.Tables[0].Rows[0]["Spernr"].ToString();
                    _Rpernr = dsEmployee.Tables[0].Rows[0]["Rpernr"].ToString();
                    _Sname = dsEmployee.Tables[0].Rows[0]["Sname"].ToString();
                    _Rname = dsEmployee.Tables[0].Rows[0]["Rname"].ToString();
                    _Persk = dsEmployee.Tables[0].Rows[0]["Persk"].ToString();
                    _Parea = dsEmployee.Tables[0].Rows[0]["Parea"].ToString();
                    _Patxt = dsEmployee.Tables[0].Rows[0]["Patxt"].ToString();
                    _Gesch = dsEmployee.Tables[0].Rows[0]["Gesch"].ToString();
                    _Werks = dsEmployee.Tables[0].Rows[0]["Werks"].ToString();
                    _Objid = dsEmployee.Tables[0].Rows[0]["Objid"].ToString();
                    _Matxt = dsEmployee.Tables[0].Rows[0]["Matxt"].ToString();
                    _Mddat = dsEmployee.Tables[0].Rows[0]["Mddat"].ToString();
                    _Stell = dsEmployee.Tables[0].Rows[0]["Stell"].ToString();
                    _Wkwdy = dsEmployee.Tables[0].Rows[0]["Wkwdy"].ToString();
                    _Mofid = dsEmployee.Tables[0].Rows[0]["Mofid"].ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get Holidays
        public DataTable SelectHolidaysById()
        {
            try
            {
                _param = new SqlParameter("@Hcode", _Mofid);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "sp_Employee_SelectHolidaysById", _param).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get Approver Details
        public bool SelectDepartmentAndGradeById()
        {
            try
            {
                _param = new SqlParameter("@Pernr", _Appnr);
                DataSet dsData = SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "sp_Employee_SelectDepartmentAndGradeById", _param);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    _Appdept = dsData.Tables[0].Rows[0][1].ToString();
                    _AppGrade = dsData.Tables[0].Rows[0][2].ToString();
                    return true;
                }
                else
                    return false;
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
                SqlParameter _param = new SqlParameter("@EmpNo", _Pernr);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "sp_Employee_SelectFullDetailsById", _param);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetMedicalBalance()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@Pernr", EmployeeNo);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "Sp_Medical_Getbalance", _param);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetPFDetails()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@Pernr", _Pernr);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_PF_Getbalance", _param);
            }
            catch (Exception)
            {
                throw;
            }
        }
        // Inserted By Geetha N on 19.02.2020
        public DataSet SelectBankUpdationDetails()
        {
            try
            {
                _params = new SqlParameter[2];
                _params[0] = new SqlParameter("@Pernr", _Pernr);
                _params[1] = new SqlParameter("@YearMonth", _YearMonth);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_Personal_GetBankAccountUpdationDetailsViewInProtal", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool CreateBankAccountDetails()
        {
            try
            {
                _params = new SqlParameter[9];
                _params[0] = new SqlParameter("@Pernr", _Pernr);
                _params[1] = new SqlParameter("@AccountType", _AccountType);
                _params[2] = new SqlParameter("@BankName", _BankName);
                _params[3] = new SqlParameter("@BranchName", _BranchName);
                _params[4] = new SqlParameter("@AccountNumber", _AccountNumber);
                _params[5] = new SqlParameter("@IFSCCode", _IFSCCode);
                _params[6] = new SqlParameter("@MICRCode", _MICRCode);
                _params[7] = new SqlParameter("@YearMonth", _YearMonth);
                _params[8] = new SqlParameter("@Return", DBNull.Value);
                _params[8].SqlDbType = SqlDbType.Int;
                _params[8].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "SP_Personal_CreateBankAccountDetails", _params);
                if ((int)_params[8].Value > 0)
                    return true;
                else
                {
                    _ValidationMessage = "Unable To Process Your Request, Please Try Again Later";
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Ended Here

        //public DataSet GetPFDetails()
        //{
        //    try
        //    {
        //        string PFtablename = ConfigurationManager.AppSettings["PFTablename"].ToString().Trim();
        //        string PFtablename1 = ConfigurationManager.AppSettings["PFTablename1"].ToString().Trim();
        //        using (OracleConnection _objConnection = new OracleConnection(SqlHelper.StrConnORACLE))
        //        {
        //            using (OracleCommand _objCommand = new OracleCommand())
        //            {
        //                String _PFQuery = "SELECT VWMAIN.EMPLOYEEID, (VWCONTBAL.mpfamt + VWINTBAL.mpfintamt) AS SUM_MPFDEBITAMT, ";
        //                _PFQuery += "(VWCONTBAL.vpfamt + VWINTBAL.vpfintamt) AS SUM_MVPFDEBITAMT, ";
        //                _PFQuery += "(VWCONTBAL.cpfamt + VWINTBAL.cpfintamt) AS SUM_MPFCREDITAMT, ";
        //                _PFQuery += "(VWCONTBAL.mpfamt + VWINTBAL.mpfintamt + VWCONTBAL.cpfamt + VWINTBAL.vpfintamt + VWCONTBAL.vpfamt + VWINTBAL.cpfintamt) AS TOTALBALANCE ";
        //                _PFQuery += "FROM (SELECT VWEMP.EMPLOYEEID FROM (SELECT ID EMPLOYEEID FROM TBLEMPLOYEEMST_01 WHERE EMPLOYEECODE=:Pernr) VWEMP) VWMAIN  ";
        //                _PFQuery += "LEFT OUTER JOIN (SELECT T1.ID,NVL(SUM(T2.mpfcreditamt - T2.mpfdebitamt),0) AS MPFAMT, ";
        //                _PFQuery += "NVL (SUM(T2.cpfcreditamt - T2.cpfdebitamt),0) AS CPFAMT, NVL(SUM (T2.vpfcreditamt - T2.vpfdebitamt),0) AS VPFAMT ";
        //                _PFQuery += "FROM tblemployeemst_01 T1 LEFT OUTER JOIN " + PFtablename1 + "  T2 ON T2.employeeid = T1.ID GROUP BY T1.ID) VWCONTBAL ON VWMAIN.EMPLOYEEID = VWCONTBAL.ID ";
        //                _PFQuery += "LEFT OUTER JOIN (SELECT T3.ID, NVL(SUM(t4.intmpfcreditamt - t4.intmpfdebitamt),0) AS MPFINTAMT, ";
        //                _PFQuery += "NVL(SUM(t4.intvpfcreditamt - t4.intvpfdebitamt),0) AS VPFINTAMT,NVL(SUM(t4.intcpfcreditamt - t4.intcpfdebitamt),0) AS CPFINTAMT ";
        //                _PFQuery += "FROM tblemployeemst_01 T3 LEFT OUTER JOIN " + PFtablename + " T4 ON T4.employeeid = T3.ID GROUP BY T3.ID ) VWINTBAL ON VWINTBAL.ID = VWMAIN.EMPLOYEEID ";

        //                //String _PFQuery = "SELECT VWMAIN.EMPLOYEEID, (VWCONTBAL.mpfamt + VWINTBAL.mpfintamt) AS SUM_MPFDEBITAMT, ";
        //                //_PFQuery += "(VWCONTBAL.vpfamt + VWINTBAL.vpfintamt) AS SUM_MVPFDEBITAMT,";
        //                //_PFQuery += "(VWCONTBAL.cpfamt + VWINTBAL.cpfintamt) AS SUM_MPFCREDITAMT,";
        //                //_PFQuery += "(VWCONTBAL.mpfamt + VWINTBAL.mpfintamt + VWCONTBAL.cpfamt + VWINTBAL.vpfintamt + VWCONTBAL.vpfamt + VWINTBAL.cpfintamt) AS TOTALBALANCE ";
        //                //_PFQuery += "FROM (SELECT VWEMP.EMPLOYEEID FROM (SELECT ID EMPLOYEEID FROM TBLEMPLOYEEMST_01 WHERE EMPLOYEECODE=:Pernr) VWEMP) VWMAIN  ";
        //                //_PFQuery += "LEFT OUTER JOIN (SELECT T1.ID,NVL(SUM(T2.mpfcreditamt - T2.mpfdebitamt),0) AS MPFAMT, ";
        //                //_PFQuery += "NVL(SUM(T2.cpfcreditamt - T2.cpfdebitamt),0) AS CPFAMT, NVL(SUM(T2.vpfcreditamt - T2.vpfdebitamt),0) AS VPFAMT ";
        //                //_PFQuery += "FROM tblemployeemst_01 T1 INNER JOIN " + PFtablename1 + " T2 ON T2.employeeid = T1.ID GROUP BY T1.ID) VWCONTBAL ON VWMAIN.EMPLOYEEID = VWCONTBAL.ID ";
        //                //_PFQuery += "LEFT OUTER JOIN (SELECT T3.ID, NVL(SUM(t4.intmpfcreditamt - t4.intmpfdebitamt),0) AS MPFINTAMT,";
        //                //_PFQuery += "NVL(SUM(t4.intvpfcreditamt - t4.intvpfdebitamt),0) AS VPFINTAMT,NVL(SUM(t4.intcpfcreditamt - t4.intcpfdebitamt),0) AS CPFINTAMT ";
        //                //_PFQuery += "FROM tblemployeemst_01 T3 INNER JOIN " + PFtablename + " T4 ON T4.employeeid = T3.ID GROUP BY T3.ID ) VWINTBAL ON VWINTBAL.ID = VWCONTBAL.ID    ";
        //                _objCommand.Connection = _objConnection;
        //                _objCommand.CommandText = _PFQuery;
        //                _objCommand.Parameters.Add("Pernr", _Pernr);
        //                using (OracleDataAdapter _objDataAdapter = new OracleDataAdapter(_objCommand))
        //                {
        //                    DataSet _dsPFData = new DataSet();
        //                    _objDataAdapter.Fill(_dsPFData);
        //                    return _dsPFData;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        //Added By S.Moharnaj on 02-Dec-2020 to display organogram details
        public DataSet SelectOrganogramDetails()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@UserId", EmployeeNo);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "uspESS_SelectOrganogramDetails", _param);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Changes ends here

        public DataSet SelectUserRolesByApplicationName(string _ApplicationID)
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[1];
                _params[0] = new SqlParameter("@ApplicationName", _ApplicationID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectUserRolesByApplicationName", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}