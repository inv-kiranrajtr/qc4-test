using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Qc4Launcher.Util.Constants;
using static Qc4Launcher.Util.Enums;

namespace Qc4Launcher.Util
{
    public class DataTableHelper
    {
        /// <summary>
        /// Joins the passed in DataTables on the colToJoinOn.
        /// <para>Returns an appropriate DataTable with zero rows if the colToJoinOn does not exist in both tables.</para>
        /// </summary>
        /// <param name="joinType"></param>  Inner/Left
        /// <param name="dtblLeft"></param> Left Table
        /// <param name="dtblRight"></param> Right Table
        /// <param name="leftTableKey1"></param> Joining Key 1 of Left table
        /// <param name="rightTableKey1"></param> Joining Key 1 of Right table
        /// <param name="isKey2Exist"></param> Boolean - Is Key 2 exist or not
        /// <param name="leftTableKey2"></param> Joining Key 2 of Left table
        /// <param name="rightTableKey2"></param> Joining Key 2 of Right table
        /// <param name="viewColumn"></param> Left/Right/Both - Return table shows columns of left/right/both combined
        /// <returns></returns>                
        /// <remarks>                         
        /// </remarks>


        public DataTable JoinTwoDataTables(JoinType joinType, DataTable dtblLeft, DataTable dtblRight, string leftTableKey1, string rightTableKey1, bool isKey2Exist = false, string leftTableKey2 = null, string rightTableKey2 = null, ColumnView viewColumn = ColumnView.Both)
        {
            //Change column name to a temp name so the LINQ for getting row data will work properly.

            string leftTableKey2ReplacedValue = DatatableSettings.ColumnNameReplacer3;
            string rightTableKey2ReplacedValue = DatatableSettings.ColumnNameReplacer4;

            if (dtblLeft.Columns.Contains(leftTableKey1))
                dtblLeft.Columns[leftTableKey1].ColumnName = DatatableSettings.ColumnNameReplacer1;

            if (dtblRight.Columns.Contains(rightTableKey1))
                dtblRight.Columns[rightTableKey1].ColumnName = DatatableSettings.ColumnNameReplacer2;

            if (isKey2Exist)
            {
                if (leftTableKey1 == leftTableKey2)
                {
                    leftTableKey2ReplacedValue = DatatableSettings.ColumnNameReplacer1;
                }

                if (rightTableKey1 == rightTableKey2)
                {
                    rightTableKey2ReplacedValue = DatatableSettings.ColumnNameReplacer2;
                }

                if (dtblLeft.Columns.Contains(leftTableKey2))
                    dtblLeft.Columns[leftTableKey2].ColumnName = leftTableKey2ReplacedValue;

                if (dtblRight.Columns.Contains(rightTableKey2))
                    dtblRight.Columns[rightTableKey2].ColumnName = rightTableKey2ReplacedValue;
            }

            DataTable dtblResult = new DataTable();
            IEnumerable<DataColumn> dt2FinalColumns = null;

            if (viewColumn == ColumnView.Left)
            {
                dtblResult = dtblLeft.Clone();
                //dt2FinalColumns = dtblLeft.Columns.OfType<DataColumn>().Select(dc => new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping));
            }
            else if (viewColumn == ColumnView.Right)
            {
                dtblResult = dtblRight.Clone();
                //dt2FinalColumns = dtblRight.Columns.OfType<DataColumn>().Select(dc => new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping));
            }
            else if (viewColumn == ColumnView.Both)
            {
                dtblResult = dtblLeft.Clone();
                IEnumerable<DataColumn> dt2Columns = dtblRight.Columns.OfType<DataColumn>().Select(dc => new DataColumn(dc.ColumnName, dc.DataType, dc.Expression, dc.ColumnMapping));
                dt2FinalColumns = from dc in dt2Columns.AsEnumerable()
                                  where !dtblResult.Columns.Contains(dc.ColumnName)
                                  select dc;
                dtblResult.Columns.AddRange(dt2FinalColumns.ToArray());
            }

            //No reason to continue if the DatatableSettings.ColumnNameReplacer1 does not exist in both DataTables.
            //if (!dtblLeft.Columns.Contains(DatatableSettings.ColumnNameReplacer1) || (!dtblRight.Columns.Contains(DatatableSettings.ColumnNameReplacer1) && !dtblRight.Columns.Contains(DatatableSettings.ColumnNameReplacer2)))
            //{
            //    if (!dtblResult.Columns.Contains(DatatableSettings.ColumnNameReplacer1))
            //        dtblResult.Columns.Add(DatatableSettings.ColumnNameReplacer1);
            //    return dtblResult;
            //}

            switch (joinType)
            {

                default:
                case JoinType.Inner:
                    #region Inner
                    //get row data
                    //To use the DataTable.AsEnumerable() extension method you need to add a reference to the System.Data.DataSetExtension assembly in your project. 

                    IEnumerable<object[]> rowDataLeftInner = null;

                    if (isKey2Exist)
                    {
                        if (viewColumn == ColumnView.Left)
                        {
                            rowDataLeftInner = from rowLeft in dtblLeft.AsEnumerable()
                                               join rowRight in dtblRight.AsEnumerable() on
                                                   new
                                                   {
                                                       JoinProperty1 = rowLeft[DatatableSettings.ColumnNameReplacer1],
                                                       JoinProperty2 = rowLeft[leftTableKey2ReplacedValue]
                                                   }
                                                   equals
                                                   new
                                                   {
                                                       JoinProperty1 = rowRight[DatatableSettings.ColumnNameReplacer2],
                                                       JoinProperty2 = rowRight[rightTableKey2ReplacedValue]
                                                   }
                                               select rowLeft.ItemArray.ToArray();
                        }
                        else if (viewColumn == ColumnView.Right)
                        {
                            rowDataLeftInner = from rowLeft in dtblLeft.AsEnumerable()
                                               join rowRight in dtblRight.AsEnumerable() on
                                                   new
                                                   {
                                                       JoinProperty1 = rowLeft[DatatableSettings.ColumnNameReplacer1],
                                                       JoinProperty2 = rowLeft[leftTableKey2ReplacedValue]
                                                   }
                                                   equals
                                                   new
                                                   {
                                                       JoinProperty1 = rowRight[DatatableSettings.ColumnNameReplacer2],
                                                       JoinProperty2 = rowRight[rightTableKey2ReplacedValue]
                                                   }
                                               select (rowRight.ItemArray).ToArray();
                        }
                        else if (viewColumn == ColumnView.Both)
                        {
                            rowDataLeftInner = from rowLeft in dtblLeft.AsEnumerable()
                                               join rowRight in dtblRight.AsEnumerable() on
                                                   new
                                                   {
                                                       JoinProperty1 = rowLeft[DatatableSettings.ColumnNameReplacer1],
                                                       JoinProperty2 = rowLeft[leftTableKey2ReplacedValue]
                                                   }
                                                   equals
                                                   new
                                                   {
                                                       JoinProperty1 = rowRight[DatatableSettings.ColumnNameReplacer2],
                                                       JoinProperty2 = rowRight[rightTableKey2ReplacedValue]
                                                   }
                                               select rowLeft.ItemArray.Concat(rowRight.ItemArray).ToArray();
                        }
                    }
                    else
                    {
                        if (viewColumn == ColumnView.Left)
                        {
                            rowDataLeftInner = from rowLeft in dtblLeft.AsEnumerable()
                                               join rowRight in dtblRight.AsEnumerable() on
                                                   new
                                                   {
                                                       JoinProperty1 = rowLeft[DatatableSettings.ColumnNameReplacer1]
                                                   }
                                                   equals
                                                   new
                                                   {
                                                       JoinProperty1 = rowRight[DatatableSettings.ColumnNameReplacer2]
                                                   }
                                               select rowLeft.ItemArray.ToArray();
                        }
                        else if (viewColumn == ColumnView.Right)
                        {
                            rowDataLeftInner = from rowLeft in dtblLeft.AsEnumerable()
                                               join rowRight in dtblRight.AsEnumerable() on
                                                   new
                                                   {
                                                       JoinProperty1 = rowLeft[DatatableSettings.ColumnNameReplacer1]
                                                   }
                                                   equals
                                                   new
                                                   {
                                                       JoinProperty1 = rowRight[DatatableSettings.ColumnNameReplacer2]
                                                   }
                                               select (rowRight.ItemArray).ToArray();
                        }
                        else if (viewColumn == ColumnView.Both)
                        {
                            rowDataLeftInner = from rowLeft in dtblLeft.AsEnumerable()
                                               join rowRight in dtblRight.AsEnumerable() on
                                                   new
                                                   {
                                                       JoinProperty1 = rowLeft[DatatableSettings.ColumnNameReplacer1]
                                                   }
                                                   equals
                                                   new
                                                   {
                                                       JoinProperty1 = rowRight[DatatableSettings.ColumnNameReplacer2]
                                                   }
                                               select rowLeft.ItemArray.Concat(rowRight.ItemArray).ToArray();
                        }

                    }
                    dtblResult.PrimaryKey = null; //Remove primary key from table for data redundancy

                    //Add row data to dtblResult
                    try
                    {
                        foreach (object[] values in rowDataLeftInner)
                            dtblResult.Rows.Add(values);
                    }
                    catch (Exception ex)
                    {
                        if(ex.Message.Contains("is constrained to be unique")==false)
                        {
                            throw;
                        }
                    }

                    #endregion
                    break;
                case JoinType.Left:
                    #region Left

                    IEnumerable<object[]> rowDataLeftOuter = null;
                    if (isKey2Exist)
                    {
                        if (viewColumn == ColumnView.Left)
                        {
                            rowDataLeftOuter = from rowLeft in dtblLeft.AsEnumerable()
                                               join rowRight in dtblRight.AsEnumerable() on
                                                   new
                                                   {
                                                       JoinProperty1 = rowLeft[DatatableSettings.ColumnNameReplacer1],
                                                       JoinProperty2 = rowLeft[leftTableKey2ReplacedValue]
                                                   }
                                                   equals
                                                   new
                                                   {
                                                       JoinProperty1 = rowRight[DatatableSettings.ColumnNameReplacer2],
                                                       JoinProperty2 = rowRight[rightTableKey2ReplacedValue]
                                                   } into gj
                                               from subRight in gj.DefaultIfEmpty()
                                               select rowLeft.ItemArray.ToArray();
                        }
                        else if (viewColumn == ColumnView.Right)
                        {
                            rowDataLeftOuter = from rowLeft in dtblLeft.AsEnumerable()
                                               join rowRight in dtblRight.AsEnumerable() on
                                                   new
                                                   {
                                                       JoinProperty1 = rowLeft[DatatableSettings.ColumnNameReplacer1],
                                                       JoinProperty2 = rowLeft[leftTableKey2ReplacedValue]
                                                   }
                                                   equals
                                                   new
                                                   {
                                                       JoinProperty1 = rowRight[DatatableSettings.ColumnNameReplacer2],
                                                       JoinProperty2 = rowRight[rightTableKey2ReplacedValue]
                                                   } into gj
                                               from subRight in gj.DefaultIfEmpty()
                                               select ((subRight == null) ? (dtblRight.NewRow().ItemArray) : subRight.ItemArray).ToArray();
                        }
                        else if (viewColumn == ColumnView.Both)
                        {
                            rowDataLeftOuter = from rowLeft in dtblLeft.AsEnumerable()
                                               join rowRight in dtblRight.AsEnumerable() on
                                                   new
                                                   {
                                                       JoinProperty1 = rowLeft[DatatableSettings.ColumnNameReplacer1],
                                                       JoinProperty2 = rowLeft[leftTableKey2ReplacedValue]
                                                   }
                                                   equals
                                                   new
                                                   {
                                                       JoinProperty1 = rowRight[DatatableSettings.ColumnNameReplacer2],
                                                       JoinProperty2 = rowRight[rightTableKey2ReplacedValue]
                                                   } into gj
                                               from subRight in gj.DefaultIfEmpty()
                                               select rowLeft.ItemArray.Concat((subRight == null) ? (dtblRight.NewRow().ItemArray) : subRight.ItemArray).ToArray();
                        }
                    }
                    else
                    {
                        if (viewColumn == ColumnView.Left)
                        {
                            rowDataLeftOuter = from rowLeft in dtblLeft.AsEnumerable()
                                               join rowRight in dtblRight.AsEnumerable() on
                                                   new
                                                   {
                                                       JoinProperty1 = rowLeft[DatatableSettings.ColumnNameReplacer1]
                                                   }
                                                   equals
                                                   new
                                                   {
                                                       JoinProperty1 = rowRight[DatatableSettings.ColumnNameReplacer2]
                                                   } into gj
                                               from subRight in gj.DefaultIfEmpty()
                                               select rowLeft.ItemArray.ToArray();
                        }
                        else if (viewColumn == ColumnView.Right)
                        {
                            rowDataLeftOuter = from rowLeft in dtblLeft.AsEnumerable()
                                               join rowRight in dtblRight.AsEnumerable() on
                                                   new
                                                   {
                                                       JoinProperty1 = rowLeft[DatatableSettings.ColumnNameReplacer1]
                                                   }
                                                   equals
                                                   new
                                                   {
                                                       JoinProperty1 = rowRight[DatatableSettings.ColumnNameReplacer2]
                                                   } into gj
                                               from subRight in gj.DefaultIfEmpty()
                                               select ((subRight == null) ? (dtblRight.NewRow().ItemArray) : subRight.ItemArray).ToArray();
                        }
                        else if (viewColumn == ColumnView.Both)
                        {
                            rowDataLeftOuter = from rowLeft in dtblLeft.AsEnumerable()
                                               join rowRight in dtblRight.AsEnumerable() on
                                                   new
                                                   {
                                                       JoinProperty1 = rowLeft[DatatableSettings.ColumnNameReplacer1]
                                                   }
                                                   equals
                                                   new
                                                   {
                                                       JoinProperty1 = rowRight[DatatableSettings.ColumnNameReplacer2]
                                                   } into gj
                                               from subRight in gj.DefaultIfEmpty()
                                               select rowLeft.ItemArray.Concat((subRight == null) ? (dtblRight.NewRow().ItemArray) : subRight.ItemArray).ToArray();
                        }
                    }




                    dtblResult.PrimaryKey = null; //Remove primary key from table for data redundancy

                    //Add row data to dtblResult
                    try
                    {
                        foreach (object[] values in rowDataLeftOuter)
                        dtblResult.Rows.Add(values);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("is constrained to be unique") == false)
                        {
                            throw;
                        }
                    }
                    #endregion
                    break;
            }

            if (dtblLeft.Columns.Contains(DatatableSettings.ColumnNameReplacer1))
                dtblLeft.Columns[DatatableSettings.ColumnNameReplacer1].ColumnName = leftTableKey1;

            if (dtblRight.Columns.Contains(DatatableSettings.ColumnNameReplacer2))
                dtblRight.Columns[DatatableSettings.ColumnNameReplacer2].ColumnName = rightTableKey1;

            if (isKey2Exist)
            {
                if (dtblLeft.Columns.Contains(leftTableKey2ReplacedValue))
                    dtblLeft.Columns[leftTableKey2ReplacedValue].ColumnName = leftTableKey2;

                if (dtblRight.Columns.Contains(rightTableKey2ReplacedValue))
                    dtblRight.Columns[rightTableKey2ReplacedValue].ColumnName = rightTableKey2;
            }



            if (dtblResult.Columns.Contains(DatatableSettings.ColumnNameReplacer1))
                dtblResult.Columns[DatatableSettings.ColumnNameReplacer1].ColumnName = leftTableKey1;

            if (isKey2Exist)
            {
                if (dtblResult.Columns.Contains(leftTableKey2ReplacedValue))
                    dtblResult.Columns[leftTableKey2ReplacedValue].ColumnName = leftTableKey2;
            }

            try
            {
                if (dtblResult.Columns.Contains(DatatableSettings.ColumnNameReplacer2))
                    dtblResult.Columns[DatatableSettings.ColumnNameReplacer2].ColumnName = rightTableKey1;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("already belongs to this DataTable")) // left key and right key is same. So deleting one from the result table
                {
                    dtblResult.Columns.Remove(DatatableSettings.ColumnNameReplacer2);
                }
            }

            if (isKey2Exist)
            {
                try
                {
                    if (dtblResult.Columns.Contains(rightTableKey2ReplacedValue))
                        dtblResult.Columns[rightTableKey2ReplacedValue].ColumnName = rightTableKey2;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("already belongs to this DataTable")) // left key and right key is same. So deleting one from the result table
                    {
                        dtblResult.Columns.Remove(rightTableKey2ReplacedValue);
                    }
                }
            }


            return dtblResult;
        }

        public DataTable GetColumnsByIndex(DataTable dt, List<int> indeces)
        {
            List<string> columnList = new List<string>();
            if (dt.Columns.Count > 0)
            {
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    // Here we are taking the column names which are not wanted in the datatable
                    List<int> selectedIndex = indeces.Where(field => field == i).ToList();
                    if (selectedIndex == null || selectedIndex.Count == 0)
                    {
                        columnList.Add(dt.Columns[i].ColumnName);
                    }
                }

                foreach (string columnName in columnList)
                {
                    dt.Columns.Remove(columnName);
                }
            }
            return dt;
        }

        public string GetUniqueColumnName(string columnName, DataTable dt)
        {
            if (dt.Columns.Contains(columnName))
            {
                return GetUniqueColumnName(ResolveColumnNameDuplication(columnName), dt);
            }
            else
            {
                return columnName;
            }
        }

        private string ResolveColumnNameDuplication(string columnName)
        {
            return columnName + "_1";
        }

        public DataTable AddColumnsFromLeftTableToRight(DataTable leftTable, DataTable rightTable, List<string> columnNames)
        {
            foreach (string columnName in columnNames)
            {
                if (rightTable.Columns.Contains(columnName))
                    rightTable.Columns[columnName].ColumnName = GetUniqueColumnName(columnName, rightTable); ;
                rightTable.Columns.Add(columnName);
            }

            for (int i = 0; i <= leftTable.Rows.Count - 1; i++)
            {
                if (rightTable.Rows.Count >= (i+1))
                {
                    foreach (string columnName in columnNames)
                    {
                        rightTable.Rows[i][columnName] = leftTable.Rows[i][columnName];
                    }
                }
                else
                {
                    break;
                }
            }
            return rightTable;
        }

        public DataTable ListToDataTable<T>(List<T> items)
        {
            try
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                return dataTable;
            }
            catch { return null; }
        }
    }
}
