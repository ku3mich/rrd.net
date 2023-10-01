function Push-Package {
  param($pkg)
	nuget push `
		$pkg `
		$env:NUGET_KEY `
		-SkipDuplicate `
		-source $env:NUGET_FEED
}

Get-ChildItem bin/pkgs -Filter '*.nupkg' | % { Push-Package $_.FullName	}
