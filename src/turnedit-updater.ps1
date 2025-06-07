chcp 65001 | Out-Null

Write-Output "****************************************"
Write-Output "TurnEdit アップデーター
Write-Output "****************************************"

$currentappversion = [version]"1.1"
$apiurl = "https://api.github.com/repos/suzuki3932/TurnEdit/releases"
$choice = Read-Host "TurnEdit をアップデートしますか(y\n)"
if ($choice -ceq "y") {
    Write-Output "GitHub から最新のバージョン情報を取得しています..."
    try {
        # FIX: Use array indexing to ensure only the first (latest) release object is selected
        $latestrelease = (Invoke-RestMethod -Uri $apiurl)[0]
    } catch {
        Write-Error "Failed to retrieve latest release information from GitHub. Error: $($_.Exception.Message)"
        Write-Output "どれかのキーを押すと続行します..."
        Read-Host | Out-Null
        Exit 1
    }

    if ($latestrelease) {

        Write-Output "最新のリリースタグ: $($latestrelease.tag_name)"
        $latest_version_string = $latestrelease.tag_name

        # Attempt to clean the version string if it contains 'v' or other prefixes
        # This will remove 'v' if present, e.g., "v1.2.3" becomes "1.2.3"
        $latest_version_string = $latest_version_string -replace '^v'

        try {
            $latest_version = [version]$latest_version_string
        } catch {
            Write-Error "Failed to parse latest version string '$latest_version_string'. Error: $($_.Exception.Message)"
            Write-Output "どれかのキーを押すと続行します..."
            Read-Host | Out-Null
            Exit 1
        }

        Write-Output "Latest version parsed: $($latest_version.ToString())"
        Write-Output "Current application version: $($currentappversion.ToString())"

        if ($latest_version -gt $currentappversion) {
            Write-Output "新しいバージョンの TurnEdit が利用可能です !"
            $assetdownloadurl = "https://github.com/suzuki3932/TurnEdit/releases/download/$($latestrelease.tag_name)/turnedit-setup.exe"
            $assetdownloaddestination = "$($env:TEMP)\turnedit-setup.exe"

            Write-Output "ダウンロード元: $assetdownloadurl"
            Write-Output "保存先: $assetdownloaddestination"

            try {
                Invoke-WebRequest -Uri $assetdownloadurl -OutFile $assetdownloaddestination -ErrorAction Stop
                Write-Information "正常に Github から最新バージョンをダウンロードしました。"
            } catch {
                Write-Error "Failed to download latest version of TurnEdit. Error: $($_.Exception.Message)"
                Write-Output "どれかのキーを押すと続行します..."
                Read-Host | Out-Null
                Exit 1
            }

            Write-Output "インストールしています..."
            if ([System.IO.File]::Exists($assetdownloaddestination)) {
                Start-Process -FilePath $assetdownloaddestination -Wait
                Write-Output "インストールが完了しました。"
            } else {
                Write-Error "The latest installer file is not found at '$assetdownloaddestination'."
                Write-Output "どれかのキーを押すと続行します..."
                Read-Host | Out-Null
                Exit 1
            }
        } else {
            Write-Output "TurnEdit は最新です。"
            Write-Output "どれかのキーを押すと続行します..."
            Read-Host | Out-Null
            Exit 0
        }
    } else {
        Write-Error "Failed to get latest release information from GitHub. The returned object was null or empty."
        Write-Output "どれかのキーを押すと続行します..."
        Read-Host | Out-Null
        Exit 1
    }
} elseif ($choice -ceq "n") {
    Write-Output "キャンセルされました。"
    Write-Output "どれかのキーを押すと続行します..."
    Read-Host | Out-Null
} else {
    Write-Error "選択が無効です。"
    Write-Output "どれかのキーを押すと続行します..."
    Read-Host | Out-Null
    Exit 1
}