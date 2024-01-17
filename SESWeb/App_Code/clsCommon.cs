using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data  ;
using System.Data.SqlClient;
using System.Web.UI;
using Telerik.Web;
using Telerik.Web.UI;

namespace SESWeb
{
    public class BICommon
    {
        public DataSet SearchMaterial(string MaterialNo, string MaterialDesc)
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[2];
                _params[0] = new SqlParameter("@MaterialNo", MaterialNo.Trim().Length > 0 ? MaterialNo : "*");
                _params[1] = new SqlParameter("@Desc", MaterialDesc.Trim().Length > 0 ? MaterialDesc : "*");
                return SqlHelper.ExecuteDataset(SqlHelper.strConnNPDI, CommandType.StoredProcedure, "sp_np_Help_Material", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void FillList(DataTable dtbl, string TextField, string DataField, RadComboBox ddl,bool IncludeSelect=false, bool IncludeAll=false, bool SelectFirst=false)
        {
            RadComboBoxItem lst;
            try
            {
                ddl.Items.Clear();
                if (dtbl.Rows.Count > 0)
                {
                    if (dtbl.Rows.Count == 1)
                    {
                        lst = new RadComboBoxItem();
                        lst.Text = dtbl.Rows[0][TextField].ToString().Trim();
                        lst.Value = dtbl.Rows[0][DataField].ToString().Trim();
                        ddl.Items.Add(lst);
                    }
                    else
                    {
                        lst = new RadComboBoxItem();
                        if (IncludeSelect)
                        {
                            lst.Text = "-Select-";
                            lst.Value = "-1";
                            ddl.Items.Add(lst);
                        }
                        else if (IncludeAll)
                        {
                            lst.Text = "All";
                            lst.Value = "All";
                            ddl.Items.Add(lst);
                        }
                        foreach (DataRow rs in dtbl.Rows)
                        {
                            lst = new RadComboBoxItem();
                            lst.Text = rs[TextField].ToString().Trim();
                            lst.Value = rs[DataField].ToString().Trim();
                            ddl.Items.Add(lst);
                        }
                        if (SelectFirst)
                            if (ddl.Items.Count > 0)
                                ddl.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                lst = null;
            }
        }
        public void FillList(DataTable dtbl, int TextField, int DataField, RadComboBox ddl, bool IncludeSelect = false, bool IncludeAll = false, bool SelectFirst = false)
        {
             RadComboBoxItem lst;
            try
            {
                ddl.Items.Clear();
                if (dtbl.Rows.Count > 0)
                {
                    if (dtbl.Rows.Count == 1)
                    {
                        lst = new RadComboBoxItem();
                        lst.Text = dtbl.Rows[0][TextField].ToString().Trim();
                        lst.Value = dtbl.Rows[0][DataField].ToString().Trim();
                        lst.Selected = true;
                        ddl.Items.Add(lst);
                    }
                    else
                    {
                        lst = new RadComboBoxItem();
                        if (IncludeSelect)
                        {
                            lst.Text = "-Select-";
                            lst.Value = "-1";
                            ddl.Items.Add(lst);
                        }
                        else if (IncludeAll)
                        {
                            lst.Text = "All";
                            lst.Value = "All";
                            ddl.Items.Add(lst);
                        }
                        foreach (DataRow rs in dtbl.Rows)
                        {
                            lst = new RadComboBoxItem();
                            lst.Text = rs[TextField].ToString().Trim();
                            lst.Value = rs[DataField].ToString().Trim();
                            ddl.Items.Add(lst);
                        }
                        if (SelectFirst)
                            if (ddl.Items.Count > 0)
                                ddl.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                lst = null;
            }

        }
        public void FillList(DataTable dtbl, string TextField, string DataField, RadDropDownList ddl, bool IncludeSelect = false, bool IncludeAll = false, bool SelectFirst = false)
        {
            DropDownListItem lst;
            try
            {
                ddl.Items.Clear();
                if (dtbl.Rows.Count > 0)
                {
                    if (dtbl.Rows.Count == 1)
                    {
                        lst = new DropDownListItem();
                        lst.Text = dtbl.Rows[0][TextField].ToString().Trim();
                        lst.Value = dtbl.Rows[0][DataField].ToString().Trim();
                        lst.Selected = true;
                        ddl.Items.Add(lst);
                    }
                    else
                    {
                        lst = new DropDownListItem();
                        if (IncludeSelect)
                        {
                            lst.Text = "-Select-";
                            lst.Value = "-1";
                            ddl.Items.Add(lst);
                        }
                        else if (IncludeAll)
                        {
                            lst.Text = "All";
                            lst.Value = "All";
                            ddl.Items.Add(lst);
                        }
                        foreach (DataRow rs in dtbl.Rows)
                        {
                            lst = new DropDownListItem();
                            lst.Text = rs[TextField].ToString().Trim();
                            lst.Value = rs[DataField].ToString().Trim();
                            ddl.Items.Add(lst);
                        }
                        if (SelectFirst)
                            if (ddl.Items.Count > 0)
                                ddl.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                lst = null;
            }
        }

        public void FillList(DataTable dtbl, int TextField, int DataField, RadDropDownList ddl, bool IncludeSelect = false, bool IncludeAll = false, bool SelectFirst = false)
        {
            DropDownListItem lst;
            try
            {
                ddl.Items.Clear();
                if (dtbl.Rows.Count > 0)
                {
                    if (dtbl.Rows.Count == 1)
                    {
                        lst = new DropDownListItem();
                        lst.Text = dtbl.Rows[0][TextField].ToString().Trim();
                        lst.Value = dtbl.Rows[0][DataField].ToString().Trim();
                        lst.Selected = true;
                        ddl.Items.Add(lst);
                    }
                    else
                    {
                        lst = new DropDownListItem();
                        if (IncludeSelect)
                        {
                            lst.Text = "-Select-";
                            lst.Value = "-1";
                            ddl.Items.Add(lst);
                        }
                        else if (IncludeAll)
                        {
                            lst.Text = "All";
                            lst.Value = "All";
                            ddl.Items.Add(lst);
                        }
                        foreach (DataRow rs in dtbl.Rows)
                        {
                            lst = new DropDownListItem();
                            lst.Text = rs[TextField].ToString().Trim();
                            lst.Value = rs[DataField].ToString().Trim();
                            ddl.Items.Add(lst);
                        }
                        if (SelectFirst)
                            if (ddl.Items.Count > 0)
                                ddl.SelectedIndex = 0;

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                lst = null;
            }
        }
        public void FillList(List<string> str, RadDropDownList ddl, bool IncludeSelect = false, bool IncludeAll = false, bool SelectFirst = false)
        {
            DropDownListItem lst;
            try
            {
                ddl.Items.Clear();
                if (str.Count > 0)
                {
                    if (str.Count == 1)
                    {
                        lst = new DropDownListItem();
                        lst.Text = str[0].Trim();
                        lst.Value = str[0].Trim();
                        lst.Selected = true;
                        ddl.Items.Add(lst);
                    }
                    else
                    {
                        lst = new DropDownListItem();
                        if (IncludeSelect)
                        {
                            lst.Text = "-Select-";
                            lst.Value = "-1";
                            ddl.Items.Add(lst);
                        }
                        else if (IncludeAll)
                        {
                            lst.Text = "All";
                            lst.Value = "All";
                            ddl.Items.Add(lst);
                        }
                        foreach (string s in str)
                        {
                            lst = new DropDownListItem();
                            lst.Text = s.Trim();
                            lst.Value = s.Trim();
                            ddl.Items.Add(lst);
                        }
                        if (SelectFirst)
                            if (ddl.Items.Count > 0)
                                ddl.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                lst = null;
            }
        }
        public void FillList(DataTable dtbl, string TextField, string DataField, RadListBox ddl)
        {
            try
            {
                ddl.Items.Clear();
                if (dtbl.Rows.Count > 0)
                {
                    RadListBoxItem lst = new RadListBoxItem();
                    foreach (DataRow rs in dtbl.Rows)
                    {
                        lst = new RadListBoxItem();
                        lst.Text = rs[TextField].ToString().Trim();
                        lst.Value = rs[DataField].ToString().Trim();
                        lst.Selected = true;
                        ddl.Items.Add(lst);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void FillList(DataTable dtbl, int TextField, int DataField, RadListBox ddl)
        {
            try
            {
                ddl.Items.Clear();
                if (dtbl.Rows.Count > 0)
                {
                    RadListBoxItem lst = new RadListBoxItem();
                    foreach (DataRow rs in dtbl.Rows)
                    {
                        lst = new RadListBoxItem();
                        lst.Text = rs[TextField].ToString().Trim();
                        lst.Value = rs[DataField].ToString().Trim();
                        ddl.Items.Add(lst);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SelectListItemByValue(RadDropDownList ddl, string strValue)
        {
            try
            {
                foreach (DropDownListItem item in ddl.Items)
                {
                    if (item.Value.Equals(strValue))
                        item.Selected = true;
                    else
                        item.Selected = false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void SelectListItemByText(RadDropDownList ddl, string strText)
        {
            try
            {
                foreach (DropDownListItem item in ddl.Items)
                {
                    if (item.Text.Equals(strText))
                        item.Selected = true;
                    else
                        item.Selected = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SelectComboMultipleItemByText(RadComboBox ddl, string strText)
        {
            try
            {
                string[] splitString = strText.Split(',');

                foreach (RadComboBoxItem item in ddl.Items)
                {
                    //if(item.Checked==true)
                    item.Checked = false;
                }

                for (int i = 0; i < splitString.Length; i++)
                {
                    foreach (RadComboBoxItem item in ddl.Items)
                    {
                        if (item.Value.Equals(splitString[i]))
                            item.Checked = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SearchEmployee(string EmployeeNo, string EmployeeName, string Plant, string Unit, string SubGroup)
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[5];
                _params[0] = new SqlParameter("@EmployeeName", EmployeeName.Trim().Length > 0 ? EmployeeName : "*");
                _params[1] = new SqlParameter("@Plant", Plant.Trim().Length > 0 ? Plant : "*");
                _params[2] = new SqlParameter("@Unit", Unit.Trim().Length > 0 ? Unit : "*");
                _params[3] = new SqlParameter("@SubGroup", SubGroup.Trim().Length > 0 ? SubGroup : "*");
                _params[4] = new SqlParameter("@EmployeeNo", EmployeeNo.Trim().Length > 0 ? EmployeeNo : "*");
                return SqlHelper.ExecuteDataset(SqlHelper.strConnNPDI, CommandType.StoredProcedure, "sp_np_Help_Employee", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Added by geetha for Radcombo box item
        public void SelectComboItemByText(RadComboBox ddl, string strText)
        {
            try
            {
                foreach (RadComboBoxItem item in ddl.Items)
                {
                    if (item.Text.Equals(strText))
                        item.Selected = true;
                    else
                        item.Selected = false;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string CurrencyInWords(double amount)
        {
            int rnd = (int)amount;
            double dec = amount - rnd;
            int paise = (int)(Math.Round(dec, 1) * 100);
            return NumbersToWords(rnd) + " Rupees " + (paise > 0 ? " and " + NumbersToWords(paise) + " Paise" : "");
        }
        private static string NumbersToWords(int inputNumber)
        {

            int inputNo = inputNumber;




            if (inputNo == 0)

                return "Zero";




            int[] numbers = new int[4];

            int first = 0;

            int u, h, t;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();




            if (inputNo < 0)
            {

                sb.Append("Minus ");

                inputNo = -inputNo;

            }




            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",

            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};

            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",

            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};

            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",

            "Seventy ","Eighty ", "Ninety "};

            string[] words3 = { "Thousand ", "Lakh ", "Crore " };




            numbers[0] = inputNo % 1000; // units

            numbers[1] = inputNo / 1000;

            numbers[2] = inputNo / 100000;

            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands

            numbers[3] = inputNo / 10000000; // crores

            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs




            for (int i = 3; i > 0; i--)
            {

                if (numbers[i] != 0)
                {

                    first = i;

                    break;

                }

            }

            for (int i = first; i >= 0; i--)
            {

                if (numbers[i] == 0) continue;

                u = numbers[i] % 10; // ones

                t = numbers[i] / 10;

                h = numbers[i] / 100; // hundreds

                t = t - 10 * h; // tens

                if (h > 0) sb.Append(words0[h] + "Hundred ");

                if (u > 0 || t > 0)
                {

                    if (h > 0 || (i == 0 && (h > 0))) sb.Append("and ");

                    if (t == 0)

                        sb.Append(words0[u]);

                    else if (t == 1)

                        sb.Append(words1[u]);

                    else

                        sb.Append(words2[t - 2] + words0[u]);

                }

                if (i != 0) sb.Append(words3[i - 1]);

            }

            return sb.ToString().TrimEnd();

        }

        public DataSet SelectSpecialEmployeeNoByUserId(string UserId)
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[1];
                _params[0] = new SqlParameter("@UserId", UserId);

                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectSpecialEmployeeNoByUserId", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectSpecialEmployeeNo(string MailID)
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[1];
                _params[0] = new SqlParameter("@MailID", MailID);

                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectSpecialEmployeeNo", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet SelectEmployeePriceApprovalRoleByUserId(string UserId)
        {
            try
            {
                SqlParameter[] _params = new SqlParameter[1];
                _params[0] = new SqlParameter("@UserId", UserId);

                return SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_SelectEmployeeRoleByUserId", _params);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}