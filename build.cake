#addin "nuget:?package=Cake.SqlServer"
#addin "Cake.FluentMigrator"
#addin "Cake.Docker"

var dbName = "UNPData";
var connectionString = @"Server=localhost;User=sa;Password=M@ster123;";
var databaseName = $@"Database={dbName};";
var delay = Argument<int>("Delay", 60) * 1000;
var target = Argument("target", "Default");

Task("Docker-Compose-Down")
    .Does(() => {
        DockerComposeDown();
    });

Task("Docker-Compose-Up")
    .Does(() => {
        DockerComposeUp(new DockerComposeUpSettings { DetachedMode = true, Build = true });

        Information("Esperando por {0} segundos...", delay / 1000);

        System.Threading.Thread.Sleep(delay);
    });
    
Task("Uninstall-FluentMigrator-Cli")
    .ContinueOnError()
    .Does(() => {
        DotNetCoreTool("tool uninstall -g FluentMigrator.DotNet.Cli");
    });
    
Task("Install-FluentMigrator-Cli")
    .ContinueOnError()
    .Does(() => {
        DotNetCoreTool("tool install -g FluentMigrator.DotNet.Cli");
    });    

Task("Apply-Migrations")
    .Does(() => {
        DotNetCoreTool("fm migrate -p sqlserver -c '" + connectionString + databaseName + "' -a './UnpDataMigration/bin/Debug/netstandard2.0/UnpDataMigration.dll'");
    });

Task("Create-Database")
    .Does(() => {
        CreateDatabaseIfNotExists(connectionString, dbName);
    });

 Task("Build-Migrations-Project")
    .Does(() =>
    {
         MSBuild("./UnpDataMigration/UnpDataMigration.csproj", configurator => 
            configurator
                .SetConfiguration("Debug")
                .SetVerbosity(Verbosity.Minimal)
                .UseToolVersion(MSBuildToolVersion.VS2017)
                .SetMSBuildPlatform(MSBuildPlatform.Automatic));
    });

Task("Default")
    .IsDependentOn("Docker-Compose-Down")
    .IsDependentOn("Docker-Compose-Up")
    .IsDependentOn("Build-Migrations-Project")
    .IsDependentOn("Create-Database")
    .IsDependentOn("Uninstall-FluentMigrator-Cli")
    .IsDependentOn("Install-FluentMigrator-Cli")
     .IsDependentOn("Apply-Migrations");
    
RunTarget(target);