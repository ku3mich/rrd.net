function Push-Package {
  param($pkg)
	nuget push `
		$(Resolve-Path -Relative $pkg) `
		$NUGET_KEY `
		-SkipDuplicate `
		-source $NUGET_FEED
}

Get-ChildItem bin/pkgs -Filter '*.nupkg' | % { Push-Package $_ 	}
