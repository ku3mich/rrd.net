Get-ChildItem -Directory -Recurse | Where-Object { ($_.name -eq 'bin') -or ($_.name -eq 'obj') } | Remove-Item -Force -Recurse
