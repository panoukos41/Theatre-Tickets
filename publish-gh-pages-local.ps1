# Modified version from: https://jkdev.me/publish-client-side-blazor-to-github-pages/

# Do dotnet publish work!

$savePath = "./dist"
$tempPath = $savePath + "/temp"
$pathToSolution = "./src/TheatreTickets.Client"

$projectName = "TheatreTickets.Client"
$repoName = "Theatre-Tickets"


try {
    Write-Output "- Dotnet publish $pathToSolution `n"
    dotnet publish $pathToSolution -c Release -o $tempPath
}
catch {
    exit
}

Write-Output ""

Write-Output "- Copy $tempPath/$projectName/dist to $savePath"
Copy-Item -Path "$tempPath/$projectName/dist/*" -Destination $savePath -Recurse -Force

Write-Output "- Delete $tempPath folder"
Remove-Item $tempPath -Recurse

$indexFile = "$savePath/index.html"
$originalBaseUrlText = "<base href=""/""";
$targetBaseUrlText = "<base href=""/$repoName/""";

Write-Output "- Replace base href in $indexFile to be /$repoName/"
((Get-Content -Path $indexFile -Raw) -Replace $originalBaseUrlText, $targetBaseUrlText) | Set-Content -NoNewline -Path $indexFile

Write-Output "- Create .nojekyl"
New-Item -Path "$savePath/.nojekyll" -ItemType "file" -Force | Out-Null

$404File = "$savePath/404.html"

Write-Output "- Create 404.html"
New-Item -Path $404File -ItemType "file" -Force | Out-Null

Write-Output "- Copy index.html to 404.html"
Get-Content -Path $indexFile -Raw | Set-Content -Path $404File
Write-Output ""

# Do Git work here!

$gitMessage = "publish"
$gitMessage = Read-Host -Prompt "- Commit message: "

Write-Output "- Git stage dist and commit"
git add dist
git commit -m "$gitMessage"

Write-Output "- Git subtree push dist to gh-pages"
git subtree split --prefix dist -b gh-pages
git push -f origin gh-pages:gh-pages 
git branch -D gh-pages

Write-Output "- Git remove commit"
git reset --soft HEAD^

Write-Output "- Git unstage dist"
git rm -r -f dist

Write-Output "- Delete $savePath folder"
Remove-Item $savePath -Recurse

Write-Output "- Done!"