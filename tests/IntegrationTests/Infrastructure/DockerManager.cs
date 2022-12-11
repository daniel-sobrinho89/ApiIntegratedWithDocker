namespace IntegrationTests.Infrastructure;

public class DockerManager
{
    public static void PullImageIfDoesNotExists(string imageName, string version = "latest")
    {
        var imageId = ProcessManager.ExecuteCommand("docker", $"images -q {imageName}:{version}");

        if (string.IsNullOrEmpty(imageId))
            ProcessManager.ExecuteCommand("docker", $"pull {imageName}:{version}");
    }

    public static void KillContainer(string containerName)
        => ProcessManager.ExecuteCommand("docker", $"rm {containerName} -f");

    internal static void KillVolume(string volumeName)
        => ProcessManager.ExecuteCommand("docker", $"volume rm {volumeName} -f");

    public static void RunContainerIfIsNotRunning(string containerName, string command)
    {
        var containerId = ProcessManager.ExecuteCommand("docker", $"ps -q --filter name={containerName}");

        if (string.IsNullOrEmpty(containerId))
            ProcessManager.ExecuteCommand("docker", command);
    }
}
