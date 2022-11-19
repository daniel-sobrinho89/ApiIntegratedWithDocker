#addin nuget:?package=Cake.Docker&version=1.1.2

var apiDirectory = Argument("api-directory", "../src/ApiIntegratedWithDocker");
var target = Argument("target", "Default");
var sqlConnection = Argument("connectionString", "Server=localhost;Database=postgres;Port=15432;User Id=admin;Password=thisIsMyPass12345");
var dockerContainerRegistryServer = Argument("registry-server", "");
var repositoryName = Argument("repository-name","api-integrated-with-docker");
var imageVersion = Argument("image-versions", "latest");
var useCache = Argument("useCache", false);
var migrationsDirectory = Argument("migrations-directory", "../src/ApiIntegratedWithDockerMigrations");

var imageVersions = imageVersion.Split(',');
List<string> tagsApi = new();
foreach (var version in imageVersions)
{
    tagsApi.Add($"{repositoryName}:{version}");
}

string[] tagDockerApi = tagsApi.ToArray();

Task("BuildApiImage")
.Does(() => {
    Information($"Generating api image with version {string.Join(",", tagDockerApi)}");
    DockerBuild(new DockerImageBuildSettings { File = $"{apiDirectory}/Dockerfile", Tag = tagDockerApi, NoCache=!useCache }, "../");
    Information($"Image generated with sucess: {string.Join(",", tagDockerApi)}.");

    Information("Configurating environment.");
    DockerComposeUp(new DockerComposeUpSettings { ProjectName = "api-integrated-with-docker" }, "-d");
});

Task("ExecuteMigrations")
.Does(() => {
    Information($"Executing migrations to connection: {sqlConnection}");

    using(var process = StartAndReturnProcess("dotnet", new ProcessSettings{ Arguments = $"run up -s \"{sqlConnection}\"", WorkingDirectory = migrationsDirectory }))
        process.WaitForExit();

    Information("Migrations finished with success.");
});

Task("ExecuteMigrationsDown")
.Does(() => {
    Information($"Executing migrations to connection: {sqlConnection}");

    using(var process = StartAndReturnProcess("dotnet", new ProcessSettings{ Arguments = $"run down -s \"{sqlConnection}\"", WorkingDirectory = migrationsDirectory }))
        process.WaitForExit();

    Information($"Migrations finished with success.");
});

Task("Default")
.Does(() => {
});

RunTarget(target);
