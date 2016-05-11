<Query Kind="Statements">
  <Reference Relative="..\source\RethinkLogs\bin\RethinkLogs.dll">C:\Users\jakob\Code\hgaard\RethinkLogs\source\RethinkLogs\bin\RethinkLogs.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>RethinkDb.Driver</NuGetReference>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>RethinkDb.Driver</Namespace>
  <Namespace>RethinkLogs</Namespace>
  <Namespace>System.Data.Common.CommandTrees.ExpressionBuilder</Namespace>
</Query>

var db = "logging";
var table = "log";
var R = RethinkDB.R;
var connection = R.Connection().Connect(); // No parameters means localhost port 28015

// find messages with specific content in message
var queryString = "(?i)shit"; // Regex
R.Db(db).Table(table).Filter(r => r["Message"].Match(queryString)).RunResult<IList<LogEvent>>(connection).Dump();

// Find messages with exception
R.Db(db).Table(table).Filter(r=> r["Exception"] != null).RunResult<IList<LogEvent>>(connection).Dump();

// Get all warnings (level = 3)
R.Db(db).Table(table).Filter(r=> r["Level"] == 3).RunResult<IList<LogEvent>>(connection).Dump();

// Messages that have specific property 
R.Db(db).Table(table).Filter(r=> r["Props"]["ThreadId"] == 8).RunResult<IList<LogEvent>>(connection).Dump();

// Pluck props
R.Db(db).Table(table).Pluck("Props").RunResult<IList<JObject>>(connection).Dump();


        
    