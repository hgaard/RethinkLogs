<Query Kind="Statements">
  <Reference Relative="..\source\RethinkLogs\bin\RethinkLogs.dll">C:\Users\jakob\Code\hgaard\RethinkLogs\source\RethinkLogs\bin\RethinkLogs.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>RethinkDb.Driver</NuGetReference>
  <Namespace>RethinkLogs</Namespace>
  <Namespace>RethinkDb.Driver</Namespace>
</Query>

var db = "logging";
var table = "log";
var R = RethinkDB.R;
var connection = R.Connection().Connect(); // No parameters means localhost port 28015

// Add new logevent
var logEvent = new LogEvent { Id = Guid.NewGuid(), 
							Level = LogEventLevel.Fatal, 
							Message = "Message from LinqPad", 
							Timestamp = DateTimeOffset.UtcNow,  
							Props = new Dictionary<string,object> {{ "UserDomainName", Environment.UserDomainName }}};
R.Db(db).Table(table).Insert(logEvent).RunResult<LogEvent>(connection);

