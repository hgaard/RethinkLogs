<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>RethinkDb.Driver</NuGetReference>
  <Namespace>RethinkDb.Driver</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Data.Common.CommandTrees.ExpressionBuilder</Namespace>
</Query>

var db = "logging";
var table = "log";
var R = RethinkDB.R;
var connection = R.Connection().Connect(); // No parameters means localhost port 28015

// find messages with specific content
var queryString = "hi";
R.Db(db).Table(table).Filter(r => r["Message"].Match("(?i)" + queryString)).OrderBy(R.Desc("Timestamp")).RunResult<JArray>(connection).First()).Dump();
//R.Db(db).Table(table).Filter(r => r["Message"].Match("(?i)" + queryString))
//				  .OrderBy(R.Desc("Timestamp"))
//				  .RunResult<JArray>(_connection);
//((IEnumerable)result).Cast<dynamic>().ForEach(x=> Console.Write(x.ToString()));

// get all data in table
//R.Table(table).Get("7cccaf5a-c52b-468e-95dd-bbfedc436b5c").RunResult(connection).Dump();
//((IEnumerable)cursor).Cast<dynamic>().ForEach(x=> Console.Write(x.ToString()));

// filter data with artist
//var rs = R.Table(table).Filter(m => m["Artist"] == "Muse").Run(connection);
//((IEnumerable)rs).Cast<dynamic>().ForEach(x=> Console.Write(x.ToString()));

// get data with id
//var rs = R.Table(table).Get("2461ac07-bec7-4e93-bd92-001225472301").Run(connection);
//((IEnumerable)rs).Cast<dynamic>().ForEach(x => Console.Write(x.ToString()));

//rs = R.Table(table).Pluck("Artist").Group("Artist").Count().Run(connection);
//((IEnumerable)rs).Cast<dynamic>().ForEach(x => Console.Write(x.ToString()));

// Query nested properties
//rs = R.Table(table).Filter(x => x["Title"] == "Showbiz")["Members"].Run(connection);
//((IEnumerable)rs).Cast<dynamic>().ForEach(x => Console.Write(x.ToString()));