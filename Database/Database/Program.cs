using EssentialDatabase.Core;
using EssentialDatabase.CLI;
using EssentialDatabase.Management;

const string storeDbPath = @"C:\Users\vladd\OneDrive\Робочий стіл\essential-db\essential-db\Database";

PersistenceService persistenceService = new(storeDbPath);
DatabaseManager databaseManager = new(persistenceService);

CLIManager cliManager = new CLIManager(databaseManager);
cliManager.StartConsoleReading();
