param($installPath, $toolsPath, $package, $project)

$lib = $project.ProjectItems.Item("Lib")
$lib.ProjectItems.Item("dolapi.dll").Properties.Item("CopyToOutputDirectory").Value = 1
$lib.ProjectItems.Item("nvdaControllerClient.dll").Properties.Item("CopyToOutputDirectory").Value = 1
$lib.ProjectItems.Item("saapi32.dll").Properties.Item("CopyToOutputDirectory").Value = 1
$lib.ProjectItems.Item("ScreenReaderAPI.dll").Properties.Item("CopyToOutputDirectory").Value = 1