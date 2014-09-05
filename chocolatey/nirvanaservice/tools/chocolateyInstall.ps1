#NOTE: Please remove any commented lines to tidy up prior to releasing the package, including this one

$packageName = 'nirvanaservice' # arbitrary name for the package, used in messages
$url = 'https://github.com/mastoj/NirvanaService/blob/master/releases/NirvanaService-1.0.0.zip' # download url
$silentArgs = 'SILENT_ARGS_HERE' # "/s /S /q /Q /quiet /silent /SILENT /VERYSILENT" # try any of these to get the silent installer #msi is always /quiet
$validExitCodes = @(0) #please insert other valid exit codes here, exit codes for ms http://msdn.microsoft.com/en-us/library/aa368542(VS.85).aspx

# if removing $url64, please remove from here
Install-ChocolateyZipPackage "$packageName" "$url" "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
