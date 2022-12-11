using System.Diagnostics;
using System.Runtime.InteropServices;

namespace IntegrationTests.Infrastructure;

public class ProcessManager
{
    public static string ExecuteCommand(string file, string arguments)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = FindExecutable(file),
            Arguments = arguments,
            UseShellExecute = false,
            WorkingDirectory = string.Empty,
            WindowStyle = ProcessWindowStyle.Hidden,
            RedirectStandardOutput = true
        };

        var process = Process.Start(processStartInfo);

        if (process == null)
            throw new Exception($"Failed to start process: {file} {arguments}");

        process.WaitForExit();

        if (process.ExitCode == 0)
            return string.Empty;

        return process.StandardOutput.ReadToEnd();
    }

    private static string FindExecutable(string name) =>
        Environment.GetEnvironmentVariable("PATH")?.Split(Path.PathSeparator)
            .Select(p => Path.Combine(p, name))
            .Select(p =>
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                    ? new[] { $"{p}.cmd", $"{p}.exe" }
                    : new[] { p })
            .SelectMany(a => a)
            .FirstOrDefault(File.Exists) ??
        throw new FileNotFoundException("Could not find executable.", name);
}
