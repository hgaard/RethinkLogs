<Query Kind="Statements">
  <NuGetReference>RethinkDb.Driver</NuGetReference>
  <Namespace>RethinkDb.Driver</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var db = "test";
var table = "music";
var R = RethinkDB.R;
var connection = R.Connection().Connect();


var album = new[] {
				new { Title = "Origin of Symmetry", Artist = "Muse", Year = "2001"},
				new { Title = "Absolution", Artist = "Muse", Year = "2004"},
				new { Title = "Reign in blood", Artist = "Slayer", Year = "1986"},
				new { Title = "Dreams", Artist = "The whitest boy alive", Year = "2006"},
				new { Title = "The sickness within", Artist = "Hatesphere", Year = "2005"},
				new { Title = "Kill 'em all", Artist = "Metallica", Year = "1983"	}
			};

var output = R.Db(db).Table(table)
				.Insert(album).Run(connection);

JObject.Pars
output.Dump();