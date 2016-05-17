<Query Kind="Statements">
  <Reference Relative="..\source\RethinkLogs\bin\RethinkLogs.dll">C:\Users\jakob\Code\hgaard\RethinkLogs\source\RethinkLogs\bin\RethinkLogs.dll</Reference>
  <NuGetReference>RethinkDb.Driver</NuGetReference>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>RethinkDb.Driver</Namespace>
  <Namespace>RethinkLogs</Namespace>
</Query>

var db = "logging";
var table = "log";
var R = RethinkDB.R;
var connection = R.Connection().Connect();

// Create Index on 
R.Db(db).Table(table).IndexDrop("Level").Run(connection);
R.Db(db).Table(table).IndexCreate("Level").Run(connection);

// Query index
var rs = R.Db(db).Table(table).GetAll(3)[new { index = "Level" }].RunResult<IList<LogEvent>>(connection).Dump();
