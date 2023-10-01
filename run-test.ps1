Get-ChildItem -Recurse -Directory -Filter "*.Tests" |`
	% { dotnet test `
		--configuration Release `
		/p:CollectCoverage=true `
		/p:CoverletOutputFormat=opencover `
		$_.FullName 
	}