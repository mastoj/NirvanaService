$packageName = 'nirvanaservice'
$version = '2.0.0'
$url = "https://github.com/mastoj/NirvanaService/raw/master/releases/NirvanaService-2.0.0.zip"
$programData = $env:ProgramData
$installFolder = "$programData\nirvanaservice.$version"
Install-ChocolateyZipPackage "$packageName" "$url" "$installFolder"
