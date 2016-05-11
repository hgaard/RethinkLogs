<Query Kind="Statements">
  <NuGetReference>RethinkDb.Driver</NuGetReference>
  <Namespace>RethinkDb.Driver</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var db = "music";
var table = "products";
var R = RethinkDB.R;
var connection = R.Connection().Connect();

// Create Index on 
R.Table(table).IndexDrop("Artist").Run(connection);
R.Table(table).IndexCreate("Artist").Run(connection);

// Query index
var rs = R.Table(table).GetAll("Muse")[new { index = "Artist" }].Run(connection);
((IEnumerable)rs).Cast<dynamic>().ForEach(x => Console.Write(x.ToString()));