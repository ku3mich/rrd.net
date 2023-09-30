Get-ChildItem -Recurse -Directory -Filter "*.Tests" |`
	% { dotnet test --configuration Release `
		/p:CollectCoverage=true `
		/p:CoverletOutputFormat=opencover `
		/p:CoverletOutput=../.coverage/ `
		/p:MergeWith=../.coverage/ `
		$_.FullName 
	}