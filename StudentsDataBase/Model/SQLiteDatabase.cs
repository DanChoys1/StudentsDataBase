using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

namespace Database
{
    #region DbRowList

    public class DbRowList<T> where T : DbRow, new()
    {
        public readonly string TableName;

        private readonly List<T> _addedRows;
        private readonly List<T> _removedRows;
        private readonly List<T> _initialRows;
        private List<T> _rows;

        private readonly List<int> _autoIncrementPropertyIndexes;
        private readonly Int64[] _autoIncrementValues;

        public enum Change
        {
            Add,
            Edit,
            Delete
        }

        public DbRowList(string tableName)
        {
            TableName = tableName;

            _addedRows = new List<T>();
            _removedRows = new List<T>();
            _initialRows = new List<T>();
            _rows = new List<T>();

            DbRow dbRow = new T();
            _autoIncrementPropertyIndexes = dbRow.GetAutoIncrementPropertyIndexes();
            _autoIncrementValues = new Int64[_autoIncrementPropertyIndexes.Count];

            for (int i = 0; i < _autoIncrementValues.Length; i++)
            {
                _autoIncrementValues[i] = -1;
            }
        }

        public T this[int index]
        {
            get => _rows[index];
        }

        public T GetLast() => _rows[_rows.Count - 1];

        public void DbInitialRows(List<T> rows)
        {
            _rows = rows;

            _initialRows.Clear();
            _addedRows.Clear();
            _removedRows.Clear();

            foreach (T row in _rows)
            {
                T copyRow = new T();
                row.CopyTo(copyRow);

                _initialRows.Add(copyRow); //Можно ли использовать конструктор при обобщении?

                object[] values = row.GetPropertiesValue();

                foreach (int index in _autoIncrementPropertyIndexes)
                {
                    if ((Int64)values[index] <= _autoIncrementValues[index])
                    {
                        values[index] = _autoIncrementValues[index] + 1;
                    }

                    _autoIncrementValues[index] = (Int64)values[index];
                }
            }
        }

        public void Add(T row)
        {
            _rows.Add(row);
            _addedRows.Add(row);

            object[] values = row.GetPropertiesValue();

            foreach (int index in _autoIncrementPropertyIndexes)
            {
                if ((Int64)values[index] <= _autoIncrementValues[index])
                {
                    values[index] = _autoIncrementValues[index] + 1;
                    row.FillProperties(values);
                }

                _autoIncrementValues[index] = (Int64)values[index];
            }
        }

        public void RemoveAt(int i)
        {
            if (!_addedRows.Contains(_rows[i]))
            {
                _removedRows.Add(_rows[i]); 
            }

            _addedRows.Remove(_rows[i]);
            _rows.RemoveAt(i);
        }

        public void Remove(T row)
        {
            if (!_addedRows.Contains(row))
            {
                _removedRows.Add(row);  
            }

            _addedRows.Remove(row);
            _rows.Remove(row);
        }

        public Dictionary<Change, List<T>> GetChanges()
        {
            Dictionary<Change, List<T>> rows = new Dictionary<Change, List<T>>()
            {
                { Change.Add, new List<T>(_addedRows) },
                { Change.Edit, new List<T>(GetEditedRows()) },
                { Change.Delete, new List<T>(_removedRows) }
            };

            return rows;
        }

        public List<T> GetAddedRows() => _addedRows;

        public List<T> GetRemovedRows() => _removedRows;

        public List<T> GetEditedRows()
        {
            List<T> changeRows = new List<T>();

            for (int j = 0; j < _initialRows.Count; j++)
            {
                for (int i = 0; i < _rows.Count; i++)
                {
                    if (_rows[i].Equals(_initialRows[j]))
                    {
                        break;
                    }
                    else if (_rows[i].Same(_initialRows[j]))
                    {
                        changeRows.Add(_rows[i]);
                        break;
                    }
                }
            }

            return changeRows;
        }

        public bool IsChanged()
        {
            Dictionary<Change, List<T>> rows = GetChanges();

            bool isRowsRemoved = GetAddedRows().Count > 0;
            bool isRowsAdded = GetRemovedRows().Count > 0;
            bool isRowsEdited = GetEditedRows().Count > 0;

            return isRowsRemoved || isRowsAdded || isRowsEdited;
        }

        public void ClearChanges()
        {
            _addedRows.Clear();
            _removedRows.Clear();
            _initialRows.Clear();

            foreach (T row in _rows)
            {
                T copyRow = new T();
                row.CopyTo(copyRow);

                _initialRows.Add(copyRow);
            }
        }

        public IEnumerator GetEnumerator() => _rows.GetEnumerator();
    }

    #endregion DbRowList

    #region SQLiteDatabase
    public class SQLiteDatabase : IDisposable
    {
        private readonly SqliteConnection connection;

        public SQLiteDatabase(string database)
        {
            connection = new SqliteConnection(database);
            connection.Open();
        }

        public void InitialRows<T>(DbRowList<T> initialRows) where T : DbRow, new()
        {
            SqliteCommand readTableQuery = new SqliteCommand($"SELECT * FROM `{initialRows.TableName}`", connection);
            using SqliteDataReader table = readTableQuery.ExecuteReader();

            if (!table.HasRows)
            {
                return;
            }

            List<T> readRows = new List<T>();

            while (table.Read())
            {
                object[] values = new object[table.FieldCount];
                table.GetValues(values);

                T row = new T();
                row.FillProperties(values);
                readRows.Add(row);
            }

            initialRows.DbInitialRows(readRows);
        }

        public void SaveChanges<T>(DbRowList<T> rows) where T : DbRow, new()
        {
            Dictionary<DbRowList<T>.Change, List<T>> canges = rows.GetChanges();

            string commandText = AddRowCommand(canges[DbRowList<T>.Change.Add], rows.TableName) + Environment.NewLine;
            commandText += DeleteRowCommand(canges[DbRowList<T>.Change.Delete], rows.TableName) + Environment.NewLine;
            commandText += UpdateRowCommand(canges[DbRowList<T>.Change.Edit], rows.TableName);

            SqliteCommand query = new SqliteCommand(commandText, connection);
            query.ExecuteNonQuery();

            rows.ClearChanges();
        }

        public async Task SaveChangesAsync<T>(DbRowList<T> rows) where T : DbRow, new()
        {
            Dictionary<DbRowList<T>.Change, List<T>> canges = rows.GetChanges();

            string commandText = AddRowCommand(canges[DbRowList<T>.Change.Add], rows.TableName) + Environment.NewLine;
            commandText += DeleteRowCommand(canges[DbRowList<T>.Change.Delete], rows.TableName) + Environment.NewLine;
            commandText += UpdateRowCommand(canges[DbRowList<T>.Change.Edit], rows.TableName);

            SqliteCommand query = new SqliteCommand(commandText, connection);
            await query.ExecuteNonQueryAsync();

            rows.ClearChanges();
        }

        private string UpdateRowCommand<T>(List<T> rows, string tableName) where T : DbRow, new()
        {
            SqliteCommand readTableQuery = new SqliteCommand($"SELECT * FROM `{tableName}`", connection);
            using SqliteDataReader table = readTableQuery.ExecuteReader();

            string command = "";

            foreach (T row in rows)
            {
                command += $"UPDATE `{tableName}` SET ";

                object[] values = row.GetPropertiesValue();
                List<int> primaryIndexes = row.GetPrimaryPropertyIndexes();

                for (int i = 0; i < values.Length - 1; i++)
                {
                    command += $"`{table.GetName(i)}` = '{values[i]}', ";
                }

                command += $"`{table.GetName(values.Length - 1)}` = '{values[values.Length - 1]}' WHERE ";

                for (int i = 0; i < primaryIndexes.Count - 1; i++)
                {
                    command += $"`{table.GetName(i)}` = '{values[i]}' AND ";
                }

                command += $"`{table.GetName(primaryIndexes.Count - 1)}` = '{values[primaryIndexes.Count - 1]}';" + Environment.NewLine;
            }

            return command;
        }

        public static string AddRowCommand<T>(List<T> rows, string tableName) where T : DbRow, new()
        {
            string command = "";

            foreach (T row in rows)
            {
                object[] values = row.GetPropertiesValue();

                command += $"INSERT INTO `{tableName}` VALUES (";

                for (int i = 0; i < values.Length - 1; i++)
                {
                    command += $"'{values[i]}', ";
                }

                command += $"'{values[^1]}');" + Environment.NewLine;
            }

            return command;
        }

        public string DeleteRowCommand<T>(List<T> rows, string tableName) where T : DbRow, new()
        {
            string command = "";

            SqliteCommand readTableQuery = new SqliteCommand($"SELECT * FROM `{tableName}`", connection);
            using SqliteDataReader table = readTableQuery.ExecuteReader();

            foreach (T row in rows)
            {
                object[] values = row.GetPropertiesValue();
                List<int> primaryIndexes = row.GetPrimaryPropertyIndexes();

                command += $"DELETE FROM `{tableName}` WHERE ";

                for (int i = 0; i < primaryIndexes.Count - 1; i++)
                {
                    command += $"`{table.GetName(i)}` = '{values[i]}' AND ";
                }

                command += $"`{table.GetName(primaryIndexes.Count - 1)}` = '{values[primaryIndexes.Count - 1]}';" + Environment.NewLine;
            }

            return command;
        }

        public void Dispose()
        {
            connection.CloseAsync();

            GC.SuppressFinalize(this);
        }
    }

    #endregion SQLiteDatabase

    #region DbRow

    public class DbRow
    {
        public void FillProperties(object[] values)
        {
            PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            int minCountValues = Math.Min(properties.Length, values.Length);

            for (int i = 0; i < minCountValues; i++)
            {
                if (properties[i].PropertyType == values[i].GetType())
                {
                    properties[i].SetValue(this, values[i]);
                }
                else if (properties[i].PropertyType == typeof(string))
                {
                    properties[i].SetValue(this, values[i]);
                }
            }
        }

        public object[] GetPropertiesValue()
        {
            PropertyInfo[] propertys = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            object[] values = new object[propertys.Length];

            for (int i = 0; i < propertys.Length; i++)
            {
                values[i] = propertys[i].GetValue(this);
            }

            return values;
        }

        public List<int> GetPrimaryPropertyIndexes()
        {
            List<int> indexes = new List<int>();

            PropertyInfo[] propertys = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < propertys.Length; i++)
            {
                /*                if (property[i].Name.Length < 3)
                                {
                                    continue;
                                }

                                if (property[i].Name[0] == 'p' && property[i].Name[1] == 'r')
                                {
                                    indexes.Add(i);
                                }*/

                Attribute? attribute = propertys[i].GetCustomAttribute(typeof(PrimaryAttribute));

                if (attribute != null)
                {
                    indexes.Add(i);
                }
            }

            return indexes;
        }

        public List<int> GetAutoIncrementPropertyIndexes()
        {
            List<int> autoIncrementPropertys = new List<int>();

            PropertyInfo[] propertys = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            for (int i = 0; i < propertys.Length; i++)
            {
                if (propertys[i].PropertyType != typeof(Int64))
                {
                    continue;
                }

                Attribute? attribute = propertys[i].GetCustomAttribute(typeof(AutoIncrementAttribute));

                if (attribute != null)
                {
                    autoIncrementPropertys.Add(i);
                }
            }

            return autoIncrementPropertys;
        }

        /// <summary>
        /// Проверяет является ли объект той же строкой по уникальным ключам.
        /// </summary>
        public bool Same(DbRow row)
        {
            if (this.GetType() != row.GetType())
            {
                return false;
            }

            bool isEqual = false;

            PropertyInfo[] property = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<int> primaryIndexes = GetPrimaryPropertyIndexes();

            foreach (int index in primaryIndexes)
            {
                if (property[index].GetValue(this).ToString() == property[index].GetValue(row).ToString())
                {
                    isEqual = true;
                }
                else
                {
                    isEqual = false;
                    break;
                }
            }

            return isEqual;
        }

        public override bool Equals(object row)
        {
            if (this.GetType() != row.GetType())
            {
                return false;
            }

            bool isEqual = false;

            foreach (PropertyInfo property in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.GetValue(this).ToString() == property.GetValue(row).ToString())
                {
                    isEqual = true;
                }
                else
                {
                    isEqual = false;
                    break;
                }
            }

            return isEqual;
        }

        public void CopyTo(DbRow row)
        {
            foreach (PropertyInfo property in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                property.SetValue(row, property.GetValue(this));
            }
        }
    }

    #endregion DbRow

    #region Attributes

    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryAttribute : Attribute
    {
    }

    // INotifyPropertyChanged
    // ReactiveUI, Fody - библиотеки
    [AttributeUsage(AttributeTargets.Property)]
    public class AutoIncrementAttribute : Attribute
    {
    }

    //Roslyn, introduction

    #endregion Attributes
}
