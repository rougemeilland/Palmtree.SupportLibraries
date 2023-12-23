$baseDirectory = $PSScriptRoot
$currentDirectory = Get-Location

try {
    Set-Location (Join-Path (Join-Path $baseDirectory "package") "Debug")
    $process = (Start-Process -FilePath "nuget" -ArgumentList "pack Palmtree.IO.Console.InterOp.Unix.nuspec" -NoNewWindow -PassThru)
    $handle = $process.Handle # おまじない https://stackoverflow.com/questions/10262231/obtaining-exitcode-using-start-process-and-waitforexit-instead-of-wait
    $process.WaitForExit()
    if ($process.ExitCode -gt 1) {
        Write-Host ("nuget の実行に失敗しました。: 終了コード: " + $process.ExitCode)
        Exit 1
    }
    Move-Item -Force *.nupkg (Join-Path (Join-Path (Join-Path ".." "..") "bin") "Debug")

    Set-Location (Join-Path (Join-Path $baseDirectory "package") "Release")
    $process = (Start-Process -FilePath "nuget" -ArgumentList "pack Palmtree.IO.Console.InterOp.Unix.nuspec" -NoNewWindow -PassThru)
    $handle = $process.Handle # おまじない https://stackoverflow.com/questions/10262231/obtaining-exitcode-using-start-process-and-waitforexit-instead-of-wait
    $process.WaitForExit()
    if ($process.ExitCode -ne 0) {
        Write-Host ("nuget の実行に失敗しました。: 終了コード: " + $process.ExitCode)
        Exit 1
    }
    Move-Item -Force *.nupkg (Join-Path (Join-Path (Join-Path ".." "..") "bin") "Release")
}
finally {
    Set-Location $currentDirectory
}
