using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace SESWeb
{
    public class UserManagement
    {
        string _strBICon = SqlHelper.strConnBI ;
        string _UserID = string.Empty;
        string _FirstName = string.Empty;
        string _LastName = string.Empty;
        bool _Active = false;
        string _LoggedInUser = string.Empty;
        public string UserID { get { return _UserID; } set { _UserID = value; } }
        public string FirstName { get { return _FirstName; } set { _FirstName = value; } }
        public string LastName { get { return _LastName; } set { _LastName = value; } }
        public bool Active { get { return _Active; } set { _Active = value; } }
        public string LoggedInUser { set { _LoggedInUser = value; } }
        public bool CreateUser()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[5];
                _params[0] = new SqlParameter("@UserID", _UserID);
                _params[1] = new SqlParameter("@FirstName", _FirstName);
                _params[2] = new SqlParameter("@LastName", _LastName);
                _params[3] = new SqlParameter("@Active", _Active ? 1 : 0);
                _params[4] = new SqlParameter("@CreatedBy", _LoggedInUser);
                int retval = SqlHelper.ExecuteNonQuery(_strBICon, CommandType.StoredProcedure, "sp_UM_Create_User", _params);
                if (retval > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateUser()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[5];
                _params[0] = new SqlParameter("@UserID", _UserID);
                _params[1] = new SqlParameter("@FirstName", _FirstName);
                _params[2] = new SqlParameter("@LastName", _LastName);
                _params[3] = new SqlParameter("@Active", _Active ? 1 : 0);
                _params[4] = new SqlParameter("@ModifiedBy", _LoggedInUser);
                int retval = SqlHelper.ExecuteNonQuery(_strBICon, CommandType.StoredProcedure, "sp_UM_Update_User", _params);
                if (retval > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAllUsers()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectAllUsers");

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAllActiveUsers()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectAllActiveUsers");

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool SelectUserById()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@UserID", _UserID);
                DataSet dsUsers =  SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectUserById", _param);
                if (dsUsers.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsUsers.Tables[0].Rows[0];
                    _FirstName = dr["FirstName"].ToString();
                    _LastName = dr["LastName"].ToString();
                    _Active = dr["Active"].ToString() == "0" ? false : true;
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

    }

    public class ApplicationManagement
    {
        string _strBICon = SqlHelper.strConnBI;
        string _strEISCon = SqlHelper.strConnEIS;
        string _ApplicationID = string.Empty;
        string _ApplicationDescription = string.Empty;
        string _ApplicationName = string.Empty;
        bool _Active = false;
        string _LoggedInUser = string.Empty;
        public string ApplicationID { get { return _ApplicationID; } set { _ApplicationID = value; } }
        public string ApplicationDescription { get { return _ApplicationDescription; } set { _ApplicationDescription = value; } }
        public string ApplicationName { get { return _ApplicationName; } set { _ApplicationName = value; } }
        public bool Active { get { return _Active; } set { _Active = value; } }
        public string LoggedInUser { set { _LoggedInUser = value; } }
        public bool CreateApplication()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[5];
                _params[0] = new SqlParameter("@ApplicationName", _ApplicationName);
                _params[1] = new SqlParameter("@ApplicationDescription", _ApplicationDescription);
                _params[2] = new SqlParameter("@Active", _Active ? 1 : 0);
                _params[3] = new SqlParameter("@CreatedBy", _LoggedInUser);
                _params[4] = new SqlParameter("@Return",DBNull.Value);
                _params[4].SqlDbType = SqlDbType.Int;
                _params[4].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_strBICon, CommandType.StoredProcedure, "sp_UM_CreateApplication", _params);
                if ((int)_params[4].Value > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateApplication()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[6];
                _params[0] = new SqlParameter("@ApplicationID", _ApplicationID);
                _params[1] = new SqlParameter("@ApplicationName", _ApplicationName);
                _params[2] = new SqlParameter("@ApplicationDescription", _ApplicationDescription);
                _params[3] = new SqlParameter("@Active", _Active ? 1 : 0);
                _params[4] = new SqlParameter("@ModifiedBy", _LoggedInUser);
                _params[5] = new SqlParameter("@Return", DBNull.Value);
                _params[5].SqlDbType = SqlDbType.Int;
                _params[5].Direction = ParameterDirection.InputOutput;
                int retval = SqlHelper.ExecuteNonQuery(_strBICon, CommandType.StoredProcedure, "sp_UM_UpdateApplication", _params);
                if (retval > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAllApplications()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectAllApplications");

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAllActiveApplications()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectAllActiveApplications");

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SelectApplicationByID()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@ApplicationID", _ApplicationID);
                DataSet dsUsers = SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectApplicationById", _param);
                if (dsUsers.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsUsers.Tables[0].Rows[0];
                    _ApplicationDescription = dr["ApplicationDescription"].ToString();
                    _ApplicationName = dr["ShortName"].ToString();
                    _Active = dr["Active"].ToString() == "0" ? false : true;
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

        public DataSet SelectApplicationByName()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@ApplicationName", _ApplicationName);
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectApplicationByName", _param);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectApplicationByUserID()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@UserID", _LoggedInUser);
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectApplicationByUserID", _param);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectInternetHostedApplicationByUserID()
        {
            try
            {

                SqlParameter[] _params = new SqlParameter[2];
                _params[0] = new SqlParameter("@UserID", _LoggedInUser);
                _params[1] = new SqlParameter("@HostedOnlyOnInternet", 1);
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectApplicationByUserID", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectBirthDayUsers()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_strEISCon, CommandType.StoredProcedure, "sp_Employee_SelectBirthDayUsers");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectBirthDaysByLocation()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_strEISCon, CommandType.StoredProcedure, "sp_Employee_SelectBirthDaysByLocation", new SqlParameter("@UserId", _LoggedInUser));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectLeavingUsers()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_strEISCon, CommandType.StoredProcedure, "sp_Employee_SelectLeavingUsers");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectRecentlyJoinedUsers()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_strEISCon, CommandType.StoredProcedure, "sp_Employee_SelectRecentlyJoinedUsers");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class RoleManagement
    {
        string _strBICon = SqlHelper.strConnBI;
        string _ApplicationID = string.Empty;
        string _RoleID = string.Empty;
        string _RoleDescription = string.Empty;
        bool _Active = false;
        string _LoggedInUser = string.Empty;
        public string ApplicationID { get { return _ApplicationID; } set { _ApplicationID = value; } }
        public string RoleID { get { return _RoleID; } set { _RoleID = value; } }
        public string RoleDescription { get { return _RoleDescription; } set { _RoleDescription = value; } }
        public bool Active { get { return _Active; } set { _Active = value; } }
        public string LoggedInUser { set { _LoggedInUser = value; } }
        public bool CreateRole()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[5];
                _params[0] = new SqlParameter("ApplicationID", _ApplicationID);
                _params[1] = new SqlParameter("@RoleDescription", _RoleDescription);
                _params[2] = new SqlParameter("@Active", _Active ? 1 : 0);
                _params[3] = new SqlParameter("@CreatedBy", _LoggedInUser);
                _params[4] = new SqlParameter("@Return", DBNull.Value);
                _params[4].SqlDbType = SqlDbType.Int;
                _params[4].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_strBICon, CommandType.StoredProcedure, "sp_UM_CreateRole", _params);
                if ((int)_params[4].Value > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRole()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[6];
                _params[0] = new SqlParameter("ApplicationID", _ApplicationID);
                _params[1] = new SqlParameter("@RoleID", _RoleID);
                _params[2] = new SqlParameter("@RoleDescription", _RoleDescription);
                _params[3] = new SqlParameter("@Active", _Active ? 1 : 0);
                _params[4] = new SqlParameter("@ModifiedBy", _LoggedInUser);
                _params[5] = new SqlParameter("@Return", DBNull.Value);
                _params[5].SqlDbType = SqlDbType.Int;
                _params[5].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_strBICon, CommandType.StoredProcedure, "sp_UM_UpdateRole", _params);
                if ((int)_params[5].Value > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRolesByApplicationId()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@ApplicationID", _ApplicationID);
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectAllRoles",_param);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectActiveRoleByApplicationId()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@ApplicationID", _ApplicationID);
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectAllActiveRoles",_param);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool SelectRoleById()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@RoleID", RoleID);
                DataSet dsUsers = SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectRoleById", _param);
                if (dsUsers.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsUsers.Tables[0].Rows[0];
                    _RoleDescription = dr["RoleDescription"].ToString();
                    _Active = dr["Active"].ToString() == "0" ? false : true;
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

        public DataSet SelectAllRoles()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectAllRoles",new SqlParameter("@UserID",_LoggedInUser));

            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectAllActiveRoles()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectAllActiveRoles");

            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectActiveRoleByApplicationID()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[2];
                _params[0] = new SqlParameter("@ApplicationID", _ApplicationID);
                _params[1] = new SqlParameter("@UserID", _LoggedInUser);
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectActiveRoleByApplicationId",_params);

            }
            catch (Exception)
            {
                throw;
            }
        }



    }

    public class FunctionalityManagement
    {
        string _strBICon = SqlHelper.strConnBI;
        string _ApplicationID = string.Empty;
        string _FunctionalityID = string.Empty;
        string _FunctionalityDescription = string.Empty;
        string _FunctionalityType = string.Empty;
        string _FunctionalityPath = string.Empty;
        string _FunctionalityOrder = string.Empty;
        string _FunctionalityParent = string.Empty;
        bool _Active = false;
        string _LoggedInUser = string.Empty;
        public string ApplicationID { get { return _ApplicationID; } set { _ApplicationID = value; } }
        public string FunctionalityID { get { return _FunctionalityID; } set { _FunctionalityID = value; } }
        public string FunctionalityDescription { get { return _FunctionalityDescription; } set { _FunctionalityDescription = value; } }
        public string FunctionalityType { get { return _FunctionalityType; } set { _FunctionalityType = value; } }
        public string FunctionalityPath { get { return _FunctionalityPath; } set { _FunctionalityPath = value; } }
        public string FunctionalityOrder { get { return _FunctionalityOrder; } set { _FunctionalityOrder = value; } }
        public string FunctionalityParent { get { return _FunctionalityParent; } set { _FunctionalityParent = value; } }
        public bool Active { get { return _Active; } set { _Active = value; } }
        public string LoggedInUser { set { _LoggedInUser = value; } }
        public bool CreateFunctionality()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[8];
                _params[0] = new SqlParameter("ApplicationID", _ApplicationID);
                _params[1] = new SqlParameter("@FunctionalityDescription", _FunctionalityDescription);
                _params[2] = new SqlParameter("@FunctionalityType", _FunctionalityType);
                _params[3] = new SqlParameter("@FunctionalityPath", _FunctionalityPath);
                _params[4] = new SqlParameter("@FunctionalityParent", _FunctionalityParent);
                _params[5] = new SqlParameter("@Active", _Active ? 1 : 0);
                _params[6] = new SqlParameter("@CreatedBy", _LoggedInUser);
                _params[7] = new SqlParameter("@Return", DBNull.Value);
                _params[7].SqlDbType = SqlDbType.Int;
                _params[7].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_strBICon, CommandType.StoredProcedure, "sp_UM_CreateFunctionality", _params);
                if ((int)_params[7].Value > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateFunctionality()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[7];
                _params[0] = new SqlParameter("ApplicationID", _ApplicationID);
                _params[1] = new SqlParameter("@FunctionalityID", _FunctionalityID);
                _params[2] = new SqlParameter("@FunctionalityDescription", _FunctionalityDescription);
                _params[3] = new SqlParameter("@FunctionalityPath", _FunctionalityPath);
                _params[4] = new SqlParameter("@Active", _Active ? 1 : 0);
                _params[5] = new SqlParameter("@ModifiedBy", _LoggedInUser);
                _params[6] = new SqlParameter("@Return", DBNull.Value);
                _params[6].SqlDbType = SqlDbType.Int;
                _params[6].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_strBICon, CommandType.StoredProcedure, "sp_UM_UpdateFunctionality", _params);
                if ((int)_params[6].Value > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAllFunctionality()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectAllFunctionality",new SqlParameter("@UserID",_LoggedInUser));

            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectFunctionalityByApplicationID()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@ApplicationID",_ApplicationID);
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectAllFunctionality",_param);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectParentFunctionality()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@ApplicationID", _ApplicationID);
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectParentFunctionality",_param);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectAllActiveFunctionality()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectAllActiveFunctionality");

            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectActiveFunctionalityByApplicationID()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@ApplicationID", _ApplicationID);
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectActiveFunctionalityByApplicationID", _param);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SelectFunctionalityById()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@FunctionalityID", _FunctionalityID);
                DataSet dsUsers = SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectFunctionalityById", _param);
                if (dsUsers.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dsUsers.Tables[0].Rows[0];
                    _ApplicationID = dr["ApplicationID"].ToString();
                    _FunctionalityDescription = dr["FunctionalityDescription"].ToString();
                    _FunctionalityType = dr["FunctionalityType"].ToString();
                    _FunctionalityPath = dr["FunctionalityPath"].ToString();
                    _FunctionalityOrder = dr["FunctionalityOrder"].ToString();
                    _FunctionalityParent = dr["FunctionalityParent"].ToString();
                    _Active = dr["Active"].ToString() == "0" ? false : true;
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
    }

    public class Authorization
    {
        string _ApplicationID = String.Empty;
        string _UserID = string.Empty;
        string _UserName = string.Empty;
        string _MailID = string.Empty;
        string _RoleID = string.Empty;
        string _FunctionalityID = string.Empty;
        string _PageName = string.Empty;
        string _RemoteHost = string.Empty;
        string _Browser = string.Empty;
        string _SessionID = string.Empty;
        string _PlatForm = string.Empty;
        string _ErrorDescription = string.Empty;
        string _ErrorName = string.Empty;
        string _LPSessionID = string.Empty;
        bool _Active = false;
        string _LoggedInUser = string.Empty;
        public string ApplicationID { get { return _ApplicationID; } set { _ApplicationID = value; } }
        public string LPSessionID { get { return _LPSessionID; } set { _LPSessionID = value; } }
        public string UserID { get { return _UserID; } set { _UserID = value; } }
        public string UserName { get { return _UserName; } set { _UserName = value; } }
        public string MailID { get { return _MailID; } set { _MailID = value; } }
        public string RoleID { get { return _RoleID; } set { _RoleID = value; } }
        public string FunctionalityID { get { return _FunctionalityID; } set { _FunctionalityID = value; } }
        public string PageName { get { return _PageName; } set { _PageName = value; } }
        public string RemoteHost { get { return _RemoteHost; } set { _RemoteHost = value; } }
        public string Browser { get { return _Browser; } set { _Browser = value; } }
        public string SessionID { get { return _SessionID; } set { _SessionID = value; } }
        public string PlatForm { get { return _PlatForm; } set { _PlatForm = value; } }
        public string ErrorDescription { get { return _ErrorDescription; } set { _ErrorDescription = value; } }
        public string ErrorName { get { return _ErrorName; } set { _ErrorName = value; } }
        public bool Active { get { return _Active; } set { _Active = value; } }
        public string LoggedInUser { set { _LoggedInUser = value; } }
        private DataTable _dtUserAndRole;
        public DataTable dtUserAndRole { get { return _dtUserAndRole; } set { _dtUserAndRole = value; } }
        private DataTable _dtRoleAndFunctionality;
        public DataTable dtRoleAndFunctionality { get { return _dtRoleAndFunctionality; } set { _dtRoleAndFunctionality = value; } }
        public bool CreateUserAndRole()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[5];
                _params[0] = new SqlParameter("@ApplicationID", _ApplicationID);
                _params[1] = new SqlParameter("@UserID", _UserID);
                _params[2] = new SqlParameter("@RoleIDs", _dtUserAndRole);
                _params[2].SqlDbType = SqlDbType.Structured;
                _params[3] = new SqlParameter("@CreatedBy", _LoggedInUser);
                _params[4] = new SqlParameter("@Return", DBNull.Value);
                _params[4].SqlDbType = SqlDbType.Int;
                _params[4].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_CreateUserAndRole", _params);
                if ((int)_params[4].Value > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool CreateRoleAndFunctionality()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[5];
                _params[0] = new SqlParameter("@ApplicationID", _ApplicationID);
                _params[1] = new SqlParameter("@RoleID", _RoleID);
                _params[2] = new SqlParameter("@FunctionalityIDs", _dtRoleAndFunctionality);
                _params[2].SqlDbType = SqlDbType.Structured;
                _params[3] = new SqlParameter("@CreatedBy", _LoggedInUser);
                _params[4] = new SqlParameter("@Return", DBNull.Value);
                _params[4].SqlDbType = SqlDbType.Int;
                _params[4].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_CreateRoleAndFunctionality", _params);
                if ((int)_params[4].Value > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidateFunctionalityAuthorizationForUser()
        {
            try
            {
                if (_PageName == "SESProcess.aspx" || _PageName == "Error.aspx" || _PageName == "Logout.aspx" || _PageName == "Help.aspx" || _PageName == "appsmenu.aspx")
                    return true;
                SqlParameter[] _params = new SqlParameter[3];
                _params[0] = new SqlParameter("@UserID", _UserID);
                _params[1] = new SqlParameter("@ApplicationName", _ApplicationID);
                _params[2] = new SqlParameter("@PageName", _PageName);
                if (SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_ValidateFunctionalityAuthorizationForUser", _params).Tables[0].Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LogUserDetailsInDB()
        {
            if (_PageName == "Default.aspx" || _PageName == "Error.aspx" || _PageName == "Logout.aspx" )
                return;
            try
            {
                SqlParameter[] _params = new SqlParameter[8];
                _params[0] = new SqlParameter("@UserID", _UserID);
                _params[1] = new SqlParameter("@ApplicationID", _ApplicationID);
                _params[2] = new SqlParameter("@PageName", _PageName);
                _params[3] = new SqlParameter("@SessionID", _SessionID);
                _params[4] = new SqlParameter("@RemoteHost", _RemoteHost);
                _params[5] = new SqlParameter("@Browser", _Browser);
                _params[6] = new SqlParameter("@PlatForm", _PlatForm);
                _params[7] = new SqlParameter("@Return", DBNull.Value);
                _params[7].SqlDbType = SqlDbType.Int;
                _params[7].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_LogUserDetailsInDB", _params);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void LogErrorDetailsInDB()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[8];
                _params[0] = new SqlParameter("@UserID", _UserID);
                _params[1] = new SqlParameter("@ApplicationID", _ApplicationID);
                _params[2] = new SqlParameter("@PageName", _PageName);
                _params[3] = new SqlParameter("@SessionID", _SessionID);
                _params[4] = new SqlParameter("@RemoteHost", _RemoteHost);
                _params[5] = new SqlParameter("@ErrorName", _ErrorName);
                _params[6] = new SqlParameter("@ErrorDescription", _ErrorDescription);
                _params[7] = new SqlParameter("@Return", DBNull.Value);
                _params[7].SqlDbType = SqlDbType.Int;
                _params[7].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_LogErrorDetailsInDB", _params);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidateUser()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@UserID", _UserID);
                DataSet dsData = SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_ValidateUser", _param);
                if (dsData.Tables[0].Rows.Count > 0)
                {
                    _UserName = dsData.Tables[0].Rows[0][1].ToString();
                    _MailID = dsData.Tables[0].Rows[0][2].ToString();
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

        public bool UpdateLoginDetailsToLP()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[5];
                _params[0] = new SqlParameter("@NetSessionID", _SessionID);
                _params[1] = new SqlParameter("@LPSessionID", _LPSessionID);
                _params[2] = new SqlParameter("@EmpNo", _UserID);
                _params[3] = new SqlParameter("@ApplicationId", _ApplicationID);
                _params[4] = new SqlParameter("@RemoteIP", _RemoteHost);
                int retVal = SqlHelper.ExecuteNonQuery(SqlHelper.strConnEIS, CommandType.StoredProcedure, "Sp_LP_UpdateLogin", _params);
                if (retVal == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateLogoutDetailsToLP()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[5];
                _params[0] = new SqlParameter("@NetSessionID", _SessionID);
                _params[1] = new SqlParameter("@LPSessionID", _LPSessionID);
                _params[2] = new SqlParameter("@EmpNo", _UserID);
                _params[3] = new SqlParameter("@ApplicationId", _ApplicationID);
                _params[4] = new SqlParameter("@LoggedOutBy", "BI_USER_LOGOUT");
                int retVal = SqlHelper.ExecuteNonQuery(SqlHelper.strConnEIS, CommandType.StoredProcedure, "Sp_LP_DeleteLogin", _params);
                if (retVal == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectAllUserAndRole()
        {
            try
            {
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectAllUserAndRole");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectAllUserAndRoleByApplicationID()
        {
            try
            {
                SqlParameter _param = new SqlParameter("@ApplicaitonID", _ApplicationID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectAllUserAndRoleByApplicationID",_param);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectUserAndRoleByRoleId()
        {
            try
            {
                SqlParameter _params = new SqlParameter("@RoleID", _RoleID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectUserAndRoleByRoleId", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectUserAndRoleByApplicationAndRoleID()
        {
            try
            {
                SqlParameter _params = new SqlParameter("@RoleID", _RoleID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectUserAndRoleByApplicationAndRoleId", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectUserAndRoleByUserId()
        {
            try
            {
                SqlParameter _params = new SqlParameter("@UserID", _UserID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectUserAndRoleByUserId", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectUserAndRoleByApplicationId()
        {
            try
            {
                SqlParameter _params = new SqlParameter("@ApplicationID", _ApplicationID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectUserAndRoleByApplicationId", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectUserAndRoleByUserAndApplicationId()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[2];
                _params[0] = new SqlParameter("@UserID", _UserID);
                _params[1] = new SqlParameter("@ApplicationID", _ApplicationID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectUserAndRoleByUserAndApplicationd", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GenerateMenuByUserAndApplication()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[2];
                _params[0] = new SqlParameter("@UserID", _UserID);
                _params[1] = new SqlParameter("@ApplicationName", _ApplicationID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_GenerateMenuByUserAndApplication", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectUserAndRoleById()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[3];
                _params[0] = new SqlParameter("@UserID", _UserID);
                _params[1] = new SqlParameter("@ApplicationID", _ApplicationID);
                _params[2] = new SqlParameter("@RoleID", _RoleID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectUserAndRoleById", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectAvailableAndAssignedRoleByApplicationAndUserId()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[3];
                _params[0] = new SqlParameter("@UserID", _UserID);
                _params[1] = new SqlParameter("@ApplicationID", _ApplicationID);
                _params[2] = new SqlParameter("@LoggedUserID", _LoggedInUser);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectAvailableAndAssignedRoleByApplicationAndUserId", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectAvailableAndAssignedRoleByUserId()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[2];
                _params[0] = new SqlParameter("@UserID", _UserID);
                _params[1] = new SqlParameter("@ApplicationID", _ApplicationID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectAvailableAndAssignedRoleByUserId", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public DataSet SelectAvailableAndAssignedFunctionalityByRoleId()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[2];
                _params[0] = new SqlParameter("@UserID", _UserID);
                _params[1] = new SqlParameter("@RoleID", _RoleID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectAvailableAndAssignedFunctionalityByRoleId", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectAvailableAndAssignedFunctionalityByApplicationAndRoleId()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[3];
                _params[0] = new SqlParameter("@ApplicationID", _ApplicationID);
                _params[1] = new SqlParameter("@RoleID", _RoleID);
                _params[2] = new SqlParameter("@UserID", _LoggedInUser);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectAvailableAndAssignedFunctionalityByApplicationAndRoleId", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectAllRoleAndFunctionality()
        {
            try
            {
                SqlParameter _params = new SqlParameter("@ApplicationID", _ApplicationID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.Text, "sp_UM_SelectAllRoleAndFunctionality");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectRoleAndFunctionalityByRoleId()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[2];
                _params[0] = new SqlParameter("@ApplicationID", _ApplicationID);
                _params[1] = new SqlParameter("@RoleID", _RoleID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.Text, "sp_UM_SelectRoleAndFunctionalityByRoleId", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRoleAndFunctionalityByFunctionalityID()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[2];
                _params[0] = new SqlParameter("@ApplicationID", _ApplicationID);
                _params[1] = new SqlParameter("@FunctionalityID", _FunctionalityID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.Text, "sp_UM_SelectRoleAndFunctionalityByFunctionalityId", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectRoleAndFunctionalityById()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[3];
                _params[0] = new SqlParameter("@RoleID", _UserID);
                _params[1] = new SqlParameter("@FunctionalityID", _FunctionalityID);
                _params[2] = new SqlParameter("@ApplicationID", _ApplicationID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.Text, "sp_UM_SelectRoleAndFunctionalityById", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet SelectHomePageURLBasedOnApplication()
        {
            try
            {
                SqlParameter _params = new SqlParameter("@ApplicationID", _ApplicationID);
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectHomePageURLBasedOnApplication", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #region " Local Login "
        private string GetSHA1HashData(string data)
        {
            //create new instance of md5
            SHA1 sha1 = SHA1.Create();

            //convert the input text to array of bytes
            byte[] hashData = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));

            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return Convert.ToBase64String(hashData);// returnValue.ToString();
        }

        public bool ValidateUser(string uid, string pwd)
        {
            try
            {

                //string strSQL = "Select * from User_ where screenName=@uid and password_=@pwd";
                pwd = GetSHA1HashData(pwd);
                SqlParameter[] usrparams = new SqlParameter[2];
                usrparams[0] = new SqlParameter("@ScreenName", uid);
                usrparams[1] = new SqlParameter("@Password", pwd);
                if (SqlHelper.ExecuteScalar(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_ValidateUserLogin", usrparams) != null)
                {
                    //return true;
                    DataSet ds = new DataSet();
                    usrparams = new SqlParameter[1];
                    usrparams[0] = new SqlParameter("@ScreenName", uid);
                    ds = SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_UserEmployeeNoFromScreenName", usrparams);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        _UserID = ds.Tables[0].Rows[0]["data_"].ToString();
                        return ValidateUser();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region UIUX

        DateTime _NotificationTimeStamp = new DateTime();
        public DateTime NotificationTimeStamp { get { return _NotificationTimeStamp; } set { _NotificationTimeStamp = value; } }
        public DataSet SelectGroupwiseApplicationsById()
        {
            try
            {
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectGroupwiseApplicationsById", new SqlParameter("@UserId", _UserID));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet SearchonPortal()
        {
            try
            {
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SearchonPortal", new SqlParameter("@UserId", _UserID));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet SelectRecentActivities()
        {
            try
            {
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectRecentActivities", new SqlParameter("@UserId", _UserID));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataSet SelectPendingTasks()
        {
            try
            {
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectPendingTasks", new SqlParameter("@UserId", _UserID));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet PinOrUnpinPendingTask(string TaskID, string ApplciationName, int IsPinned, string PinStatus)
        {
            try
            {
                SqlParameter[] _Params = new SqlParameter[6];
                _Params[0] = new SqlParameter("@TaskId", TaskID);
                _Params[1] = new SqlParameter("@UserId", _UserID);
                _Params[2] = new SqlParameter("@ApplicationName", ApplciationName);
                _Params[3] = new SqlParameter("@IsPinned", IsPinned);
                _Params[4] = new SqlParameter("@PinStatus", PinStatus);
                _Params[5] = new SqlParameter("@Return", SqlDbType.Bit);
                _Params[5].Direction = ParameterDirection.Output;
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_PinOrUnpinPendingTask", _Params);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsFeaturesExplored(string ApplicationName, string PageName)
        {
            try
            {
                SqlParameter[] _Params = new SqlParameter[4];
                _Params[0] = new SqlParameter("@UserId", _UserID);
                _Params[1] = new SqlParameter("@ApplicationName", ApplicationName);
                _Params[2] = new SqlParameter("@PageName", PageName);
                _Params[3] = new SqlParameter("@Return", SqlDbType.Bit);
                _Params[3].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_IsFeaturesExplored", _Params);
                if (_Params[3].Value.ToString().ToLower().Equals("true") || _Params[1].Value.ToString().ToLower().Equals("1"))
                {
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
        public string SelectLandingPageURL()
        {
            try
            {
                SqlParameter[] _Params = new SqlParameter[1];
                _Params[0] = new SqlParameter("@RedirectURL", SqlDbType.VarChar,2000);
                _Params[0].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectLandingPageURL", _Params);
                return _Params[0].Value.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataSet SelectNotificationDetails()
        {
            try
            {
                SqlParameter[] _Params = new SqlParameter[2];
                _Params[0] = new SqlParameter("@UserID", _UserID);
                _Params[1] = new SqlParameter("@TimeStamp", SqlDbType.DateTime);
                _Params[1].Direction = ParameterDirection.Output;
                DataSet _Result = SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectNotificationDetails", _Params);
                if (_Params[1].Value.Equals(null))
                    _NotificationTimeStamp = DateTime.Now;
                else
                    _NotificationTimeStamp = DateTime.Parse(_Params[1].Value.ToString());
                return _Result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateNotificationView()
        {
            try
            {
                SqlParameter[] _Params = new SqlParameter[3];
                _Params[0] = new SqlParameter("@UserID", this._UserID);
                _Params[1] = new SqlParameter("@TimeStamp", this._NotificationTimeStamp.ToString("dd/MMM/yyyy HH:mm:ss"));
                _Params[2] = new SqlParameter("@Return", SqlDbType.Bit);
                _Params[2].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_UpdateNotificationView", _Params);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateTaskCompleted(string DataKeyValue, string ApplicationName)
        {
            try
            {
                SqlParameter[] _Params = new SqlParameter[4];
                _Params[0] = new SqlParameter("@DataKeyValue", DataKeyValue);
                _Params[1] = new SqlParameter("@UserID", this._UserID);
                _Params[2] = new SqlParameter("@ApplicationName", ApplicationName);
                _Params[3] = new SqlParameter("@Return", SqlDbType.Bit);
                _Params[3].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_UpdateTaskCompleted", _Params);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateNotificationRead(string ApplicationName, string NotificationId)
        {
            try
            {
                SqlParameter[] _Params = new SqlParameter[4];
                _Params[0] = new SqlParameter("@NotificationId", NotificationId);
                _Params[1] = new SqlParameter("@UserID", this._UserID);
                _Params[2] = new SqlParameter("@pplicationName", ApplicationName);
                _Params[3] = new SqlParameter("@Return", SqlDbType.Bit);
                _Params[3].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_UpdateNotificationRead", _Params);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
    public class common
    {
        public DataSet SearchEmployee(string EmployeeName, string Plant, string Unit, string SubGroup)
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[4];
                _params[0] = new SqlParameter("@EmployeeName", EmployeeName.Trim().Length > 0 ? EmployeeName : "*");
                _params[1] = new SqlParameter("@Plant", Plant.Trim().Length > 0 ? Plant : "*");
                _params[2] = new SqlParameter("@Unit", Unit.Trim().Length > 0 ? Unit : "*");
                _params[3] = new SqlParameter("@SubGroup", SubGroup.Trim().Length > 0 ? SubGroup : "*");
                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_np_Help_Employee", _params);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    public class Notifications
    {
        public static DataTable getNotifications(String EmpNo)
        {
            try
            {
                DataTable _dtNotifications = new DataTable();
                _dtNotifications.Columns.Add("ApplicationName", typeof(String));
                _dtNotifications.Columns.Add("PageUrl", typeof(String));
                _dtNotifications.Columns.Add("NotificationName", typeof(String));
                _dtNotifications.Columns.Add("NotificationCount", typeof(String));


                /*---------------------------------------
                 *
                 * ATTENDENCE RELATED NOTIFICATIONS
                 *
                 * --------------------------------------*/

                SqlParameter[] _params = new SqlParameter[1];
                _params[0] = new SqlParameter("@Pernr", EmpNo);
                DataSet _ds = SqlHelper.ExecuteDataset(SqlHelper.strConnEIS, CommandType.StoredProcedure, "sp_Attendance_Notifications", _params);
                foreach (DataTable dt in _ds.Tables)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        _dtNotifications.ImportRow(dr);
                    }
                }




                /*----------------------------------------------------------------------------------------------------------------
                    ADD ALL YOUR NOTIFICATIONS GETTING LOGICS ABOVE THIS LINE
                *------------------------------------------------------------------------------------------------------------------*/

                return _dtNotifications;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    public class Announcement
    {
        string _strBICon = SqlHelper.strConnBI;
        string _RoleID = string.Empty;
        public string RoleID { get { return _RoleID; } set { _RoleID = value; } }
        string _AnnouncmentID = string.Empty;
        public string AnnouncmentID { get { return _AnnouncmentID; } set { _AnnouncmentID = value; } }
        string _Title = string.Empty;
        public string Title { get { return _Title; } set { _Title = value; } }
        string _Description = string.Empty;
        public string Description { get { return _Description; } set { _Description = value; } }
        string _ExpiryDate = string.Empty;
        public string ExpiryDate { get { return _ExpiryDate; } set { _ExpiryDate = value; } }
        bool _Active = false;
        public bool Active { get { return _Active; } set { _Active = value; } }
        string _LoggedInUser = string.Empty;
        public string LoggedInUser { get { return _LoggedInUser; } set { _LoggedInUser = value; } }

        public DataSet SelectAllAnnouncement()
        {
            try
            {
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectAllAnnouncement");

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CreateAnnouncement()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[7];
                _params[0] = new SqlParameter("@RoleID", _RoleID);
                _params[1] = new SqlParameter("@Title", _Title);
                _params[2] = new SqlParameter("@Description", _Description);
                _params[3] = new SqlParameter("@ExpiryDate", _ExpiryDate);
                _params[4] = new SqlParameter("@Active", _Active ? 1 : 0);
                _params[5] = new SqlParameter("@CreatedBy", _LoggedInUser);
                _params[6] = new SqlParameter("@Return", DBNull.Value);
                _params[6].SqlDbType = SqlDbType.Int;
                _params[6].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_strBICon, CommandType.StoredProcedure, "sp_UM_CreateAnnouncement", _params);
                if ((int)_params[6].Value > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateAnnouncement()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[8];
                _params[0] = new SqlParameter("@RoleID", _RoleID);
                _params[1] = new SqlParameter("@AnnouncementID", _AnnouncmentID);
                _params[2] = new SqlParameter("@Title", _Title);
                _params[3] = new SqlParameter("@Description", _Description);
                _params[4] = new SqlParameter("@ExpiryDate", _ExpiryDate);
                _params[5] = new SqlParameter("@Active", _Active ? 1 : 0);
                _params[6] = new SqlParameter("@ModifiedBy", _LoggedInUser);
                _params[7] = new SqlParameter("@Return", DBNull.Value);
                _params[7].SqlDbType = SqlDbType.Int;
                _params[7].Direction = ParameterDirection.InputOutput;
                SqlHelper.ExecuteNonQuery(_strBICon, CommandType.StoredProcedure, "sp_UM_UpdateAnnouncement", _params);
                if ((int)_params[7].Value > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectAnnouncementByUserID()
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[1];
                _params[0] = new SqlParameter("@UserID", _LoggedInUser);
                return SqlHelper.ExecuteDataset(_strBICon, CommandType.StoredProcedure, "sp_UM_SelectAnnouncementDetailsByUserID", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}