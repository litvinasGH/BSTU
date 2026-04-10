

$counts = @{
    X = 0
    Y = 0
    Z = 0
}

for ($i = 1; $i -le 50; $i++) {
    $res = Invoke-RestMethod http://localhost:5035/api -Method GET
    $nick = $res.Nick

    # увеличиваем счётчик
    if ($counts.ContainsKey($nick)) {
        $counts[$nick]++
    }

    # вывод итерации
    Write-Host "[$i] -> $nick"
}

Write-Host "`n--- Итог ---"
Write-Host "X: $($counts.X)"
Write-Host "Y: $($counts.Y)"
Write-Host "Z: $($counts.Z)"