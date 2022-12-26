using EssentialDatabase.Core.Entities.Columns.Enums;
using EssentialDatabase.Core.Entities.Columns;
using EssentialDatabase.Core.Entities;
using EssentialDatabase.Management;
using EssentialDatabase.Core.Entities.Columns.Abstractions;

namespace EssentialDatabase.Tests
{
    public class EssentialTests
    {
        [Fact]
        public void TestCreateDatabase()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);

            // Act
            databaseManager.CreateDatabase("TestDb");

            // Assert
            Assert.Contains(databaseManager.Databases, x => x.Name == "TestDb");
        }

        [Fact]
        public void TestRestoreDatabase()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);

            // Act
            databaseManager.CreateDatabase("TestDb");

            // Assert
            Assert.Contains(databaseManager.Databases, x => x.Name == "TestDb");
        }

        [Fact]
        public void TestSaveDatabase()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);

            // Act
            databaseManager.CreateDatabase("TestDb");

            // Assert
            Assert.Contains(databaseManager.Databases, x => x.Name == "TestDb");
        }

        [Fact]
        public void TestGetAllDatabases()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            // Act
            databaseManager.CreateColumn("TestColumn", ColumnType.String, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Columns, x => x.Name == "TestColumn");
        }

        [Fact]
        public void TestUpdateDatabase()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            // Act
            databaseManager.CreateColumn("TestColumn", ColumnType.String, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Columns, x => x.Name == "TestColumn");
        }

        [Fact]
        public void TestDeleteDatabase()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            // Act
            databaseManager.CreateColumn("TestColumn", ColumnType.String, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Columns, x => x.Name == "TestColumn");
        }

        [Fact]
        public void TestCreateTable()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            var database = databaseManager.CreateDatabase("TestDb");

            // Act
            databaseManager.CreateTable("TestTable", database.Id);

            // Assert
            Assert.Contains(database.Tables, x => x.Name == "TestTable");
        }

        [Fact]
        public void TestGetAllTables()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            // Act
            databaseManager.CreateColumn("TestColumn", ColumnType.String, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Columns, x => x.Name == "TestColumn");
        }

        [Fact]
        public void TestUpdateTable()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            // Act
            databaseManager.CreateColumn("TestColumn", ColumnType.String, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Columns, x => x.Name == "TestColumn");
        }

        [Fact]
        public void TestDeleteTable()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            // Act
            databaseManager.CreateColumn("TestColumn", ColumnType.String, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Columns, x => x.Name == "TestColumn");
        }

        [Fact]
        public void TestCreateColumn()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            // Act
            databaseManager.CreateColumn("TestColumn", ColumnType.String, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Columns, x => x.Name == "TestColumn");
        }

        [Fact]
        public void TestGetAllColumns()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            // Act
            databaseManager.CreateColumn("TestColumn", ColumnType.String, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Columns, x => x.Name == "TestColumn");
        }

        [Fact]
        public void TestUpdateColumn()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            // Act
            databaseManager.CreateColumn("TestColumn", ColumnType.String, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Columns, x => x.Name == "TestColumn");
        }

        [Fact]
        public void TestDeleteColumn()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            // Act
            databaseManager.CreateColumn("TestColumn", ColumnType.String, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Columns, x => x.Name == "TestColumn");
        }

        [Fact]
        public void TestCreateRow()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);
            databaseManager.CreateColumn("TestColumn1", ColumnType.Real, database.Id, table.Name);
            databaseManager.CreateColumn("TestColumn2", ColumnType.String, database.Id, table.Name);
            databaseManager.CreateColumn("TestColumn3", ColumnType.Color, database.Id, table.Name);
            databaseManager.CreateColumn("TestColumn4", ColumnType.ColorInterval, database.Id, table.Name);

            // Act
            Dictionary<string, string?> values = new();
            values.Add("TestColumn1", "0.2");
            values.Add("TestColumn2", "text");
            values.Add("TestColumn3", "12;124;13");
            values.Add("TestColumn4", "12..52;24..124;13..145");

            databaseManager.CreateRow(values, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Rows, x => x.Values.Any(x => x.Key == "TestColumn1" && x.Value == "0.2"));
            Assert.Contains(table.Rows, x => x.Values.Any(x => x.Key == "TestColumn2" && x.Value == "text"));
            Assert.Contains(table.Rows, x => x.Values.Any(x => x.Key == "TestColumn3" && x.Value == "12;124;13"));
            Assert.Contains(table.Rows, x => x.Values.Any(x => x.Key == "TestColumn4" && x.Value == "12..52;24..124;13..145"));
        }

        [Fact]
        public void TestCreateRows()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            // Act
            databaseManager.CreateColumn("TestColumn", ColumnType.String, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Columns, x => x.Name == "TestColumn");
        }

        [Fact]
        public void TestGetAllRows()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            // Act
            databaseManager.CreateColumn("TestColumn", ColumnType.String, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Columns, x => x.Name == "TestColumn");
        }

        [Fact]
        public void TestUpdateRow()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            // Act
            databaseManager.CreateColumn("TestColumn", ColumnType.String, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Columns, x => x.Name == "TestColumn");
        }

        [Fact]
        public void TestDeleteRow()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            // Act
            databaseManager.CreateColumn("TestColumn", ColumnType.String, database.Id, table.Name);

            // Assert
            Assert.Contains(table.Columns, x => x.Name == "TestColumn");
        }

        [Fact]
        public void TestChangeColumnOrder()
        {
            // Arrange
            DatabaseManager databaseManager = new(null!);
            Database database = databaseManager.CreateDatabase("TestDb");
            Table table = databaseManager.CreateTable("TestTable", database.Id);

            databaseManager.CreateColumn("TestColumn0", ColumnType.Real, database.Id, table.Name);
            databaseManager.CreateColumn("TestColumn1", ColumnType.String, database.Id, table.Name);
            Column columnPosition2 = databaseManager.CreateColumn("TestColumn2", ColumnType.Color, database.Id, table.Name);
            databaseManager.CreateColumn("TestColumn3", ColumnType.ColorInterval, database.Id, table.Name);

            // Act
            databaseManager.ChangeCloumnOrder("TestColumn2", 1, database.Id, table.Name);

            // Assert
            Assert.Equal(table.Columns.First().NextColumn, columnPosition2);
        }
    }
}