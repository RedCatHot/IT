using EssentialDatabase.Core.Columns;
using EssentialDatabase.Core.Entities;
using EssentialDatabase.Core.Entities.Columns.Abstractions;
using EssentialDatabase.Core.Entities.Columns.Enums;
using EssentialDatabase.Management.Abstractions;

namespace EssentialDatabase.Management;

public class DatabaseManager
{
	private IPersistenceService _persistenceService;
	public List<Database> Databases { get; private set; } = new List<Database>();

	public DatabaseManager(IPersistenceService persistenceService) => _persistenceService = persistenceService;

	#region Special Methods 
	public void ChangeCloumnOrder(string columnName, int newOrder, Guid? databaseId, string? tableName)
	{
		Table table = Databases.First(x => x.Id == databaseId)
						.Tables.First(x => x.Name == tableName);

		Column? column = table.Columns.FirstOrDefault(x => x.Name == columnName);

		if (column is null)
			throw new ArgumentNullException("There is no column with name");

		if (newOrder < 0 || newOrder >= table.Columns.Count)
			throw new ArgumentOutOfRangeException("Given orden is out of range for the table");

		Column nextColumn = table.Columns[newOrder];
		Column? previousColumn = newOrder > 0 ? table.Columns[newOrder - 1] : null;

		if (previousColumn is not null)
			previousColumn.NextColumn = column;

		column.NextColumn = nextColumn;
		nextColumn.NextColumn = column;
	}
	#endregion Special Methods 

	#region Get Methods 
	public IEnumerable<Database> GetDatabases() => Databases;

	public IEnumerable<Table> GetTables(string? databaseName)
	{
		if (string.IsNullOrWhiteSpace(databaseName))
			throw new ArgumentException("Incorrect database name");

		Database? database = Databases.FirstOrDefault(x => x.Name == databaseName);

		if (database is null)
			throw new ArgumentException("There is no Database with this name");

		return database.Tables;
	}

	public IEnumerable<Column> GetColumns(string? databaseName, string? tableName)
	{
		if (string.IsNullOrWhiteSpace(databaseName) || string.IsNullOrWhiteSpace(tableName))
			throw new ArgumentException("Incorrect database or table name");

		Database? database = Databases.FirstOrDefault(x => x.Name == databaseName);

		if (database is null)
			throw new ArgumentException("There is no Database with this name");

		Table? table = database.Tables.FirstOrDefault(x => x.Name == tableName);

		if (table is null)
			throw new ArgumentException("There is no Table with this name");

		return table.Columns;
	}

	public IEnumerable<Row> GetRows(string? databaseName, string? tableName)
	{
		if (string.IsNullOrWhiteSpace(databaseName) || string.IsNullOrWhiteSpace(tableName))
			throw new ArgumentException("Incorrect database or table name");

		Database? database = Databases.FirstOrDefault(x => x.Name == databaseName);

		if (database is null)
			throw new ArgumentException("There is no Database with this name");

		Table? table = database.Tables.FirstOrDefault(x => x.Name == tableName);

		if (table is null)
			throw new ArgumentException("There is no Table with this name");

		return table.Rows;
	}
	#endregion Get Methods

	#region Create Methods 
	public Database CreateDatabase(string? databaseName)
	{
		ValidateDatabase(databaseName);

		Database database = new(databaseName!);
		database.Id = Guid.NewGuid();

		Databases.Add(database);

		return database;
	}

	public Table CreateTable(string? tableName, Guid? databaseId)
	{
		ValidateTable(tableName, databaseId);

		Table table = new(tableName!);


		Databases.First(x => x.Id == databaseId)
			.Tables.Add(table);

		return table;
	}

	public Column CreateColumn(string? columnName, ColumnType columnType, Guid? databaseId, string? tableName)
	{
		ValidateColumn(columnName, databaseId, tableName);

		Column column = InitializeColumn(columnName!, columnType);

		Table table = Databases.First(x => x.Id == databaseId)
						.Tables.First(x => x.Name == tableName);

		Column? lastColumn = table.Columns.LastOrDefault();

		if (lastColumn is not null)
			lastColumn.NextColumn = column;

		table.Columns.Add(column);

		return column;
	}

	public Row CreateRow(Dictionary<string, string?> values, Guid? databaseId, string? tableName)
	{
		Table table = Databases.First(x => x.Id == databaseId)
						.Tables.First(x => x.Name == tableName);

		Row row = InitializeRow(values, table);

		table.Rows.Add(row);

		return row;
	}
	#endregion Create Methods 

	#region Update Methods 
	public Database UpdateDatabase(Database newDatabase)
	{
		Database? database = Databases.FirstOrDefault(x => x.Id == newDatabase.Id);

		if (database is null)
			throw new ArgumentException("There is no Database to update");

		database = newDatabase;

		return database;
	}

	public Table UpdateTable(Guid? databaseId, Table newTable)
	{
		Database? database = Databases.FirstOrDefault(x => x.Id == databaseId);

		if (database is null)
			throw new ArgumentException("There is no Database to update");

		Table? table = database.Tables.FirstOrDefault(x => x.Name == newTable.Name);

		if (table is null)
			throw new ArgumentException("There is no Table to update");

		table = newTable;

		return table;
	}

	public Column UpdateColumn(Guid? databaseId, string? tableName, Column newColumn)
	{
		Database? database = Databases.FirstOrDefault(x => x.Id == databaseId);

		if (database is null)
			throw new ArgumentException("There is no Database to update");

		Table? table = database.Tables.FirstOrDefault(x => x.Name == tableName);

		if (table is null)
			throw new ArgumentException("There is no Table to update");

		Column? column = table.Columns.FirstOrDefault(x => x.Name == newColumn.Name);

		if (column is null)
			throw new ArgumentException("There is no Column to update");

		if (column.Type != newColumn.Type)
			throw new ArgumentException("You can not change type of Column");

		column = newColumn;

		return column;
	}
	#endregion Update Methods 

	#region Delete Methods 
	public Database DeleteDatabase(Guid? databaseId)
	{
		Database? database = Databases.FirstOrDefault(x => x.Id == databaseId);

		if (database is null)
			throw new ArgumentException("There is no Database to delete");

		Databases.Remove(database);

		return database;
	}

	public Table DeleteTable(Guid? databaseId, string? tableName)
	{
		Database? database = Databases.FirstOrDefault(x => x.Id == databaseId);

		if (database is null)
			throw new ArgumentException("There is no Database to delete");

		Table? table = database.Tables.FirstOrDefault(x => x.Name == tableName);

		if (table is null)
			throw new ArgumentException("There is no Table to delete");

		database.Tables.Remove(table);

		return table;
	}

	public Column DeleteColumn(Guid? databaseId, string? tableName, string columnName)
	{
		Database? database = Databases.FirstOrDefault(x => x.Id == databaseId);

		if (database is null)
			throw new ArgumentException("There is no Database to update");

		Table? table = database.Tables.FirstOrDefault(x => x.Name == tableName);

		if (table is null)
			throw new ArgumentException("There is no Table to update");

		Column? column = table.Columns.FirstOrDefault(x => x.Name == columnName);

		if (column is null)
			throw new ArgumentException("There is no Column to update");

		table.Columns.Remove(column);

		return column;
	}
	#endregion Delete Methods 

	#region Save Methods
	public async Task SaveChangesAsync() => await _persistenceService.SaveDatabasesAsync(Databases);
	#endregion

	#region Validations
	public void ValidateDatabase(string? databaseName)
	{
		if (string.IsNullOrWhiteSpace(databaseName))
			throw new ArgumentException("Incorrect database name");
		if (Databases.Any(x => x.Name == databaseName))
			throw new ArgumentException("Database name should be unique");
	}

	public void ValidateTable(string? tableName, Guid? databaseId)
	{
		if (string.IsNullOrWhiteSpace(tableName))
			throw new ArgumentException("Incorrect table name");

		if (databaseId.GetValueOrDefault() == Guid.Empty)
			throw new ArgumentException("DatabaseId is null");

		var database = Databases.FirstOrDefault(x => x.Id == databaseId);
		if (database is null)
			throw new ArgumentException("Database with this database id is not found");

		if (database.Tables.Any(x => x.Name == tableName))
			throw new ArgumentException("Table name should be unique");
	}

	public void ValidateColumn(string? columnName, Guid? databaseId, string? tableName)
	{
		if (string.IsNullOrWhiteSpace(columnName))
			throw new ArgumentException("Incorrect column name");

		if (string.IsNullOrWhiteSpace(tableName))
			throw new ArgumentException("Incorrect table name");

		if (databaseId.GetValueOrDefault() == Guid.Empty)
			throw new ArgumentException("DatabaseId is null");

		var table = Databases.FirstOrDefault(x => x.Id == databaseId)?.Tables.FirstOrDefault(x => x.Name == tableName);
		if (table is null)
			throw new ArgumentException("Database or Table is not found");

		if (table.Columns.Any(x => x.Name == columnName))
			throw new ArgumentException("Column name should be unique");
	}
	#endregion Validations

	#region Private Methods
	private Column InitializeColumn(string columnName, ColumnType columnType) => columnType switch
	{
		ColumnType.Char => new CharColumn(columnName),
		ColumnType.String => new StringColumn(columnName),
		ColumnType.Color => new ColorColumn(columnName),
		ColumnType.ColorInterval => new ColorIntervalColumn(columnName),
		ColumnType.Int => new IntColumn(columnName),
		ColumnType.Real => new RealColumn(columnName),
		_ => throw new NotImplementedException(),
	};

	private Row InitializeRow(Dictionary<string, string?> columnToValues, Table table)
	{
		if (columnToValues.Count > table.Columns.Count)
			throw new ArgumentException("Columns are more than exist in the table");

		if (columnToValues.Any(columnToValue => string.IsNullOrWhiteSpace(columnToValue.Key)))
			throw new ArgumentException("Wrong column name");

		if (columnToValues.All(columnToValue => table.Columns.Any(column => column.Name == columnToValue.Key && columnToValue.Value is not null && column.Validate(columnToValue.Value))))
			throw new ArgumentException("Column name is not exists in the talbe or value for this column is invalid");

		return new()
		{
			Values = columnToValues,
		};
	}
	#endregion Private Methods
}
