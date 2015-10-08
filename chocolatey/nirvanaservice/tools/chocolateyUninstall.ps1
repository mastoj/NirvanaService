$programData = $env:ProgramData
$version = "2.0.0"
$installFolder = "$programData\nirvanaservice.$version"
Write-Host "Removing folder $installFolder"
Remove-Item $installFolder -force -recurse
