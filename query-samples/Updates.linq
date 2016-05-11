<Query Kind="Statements">
  <NuGetReference>RethinkDb.Driver</NuGetReference>
  <Namespace>RethinkDb.Driver</Namespace>
</Query>

var db = "test";
var table = "music";
var R = RethinkDB.R;
var connection = R.Connection().Connect();

var result = R.Table(table).Update(new { Rating = 1}).Run(connection);

result = R.Table(table).Filter(r=>r["Artist"] == "Muse").Update(new { Rating = 2}).Run(connection);