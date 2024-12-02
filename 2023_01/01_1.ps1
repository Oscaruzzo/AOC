$In = Get-Content .\input1.txt
$tot = 0
$In | ForEach-Object {
    $nums = ($_ |  Select-String '[0-9]' -AllMatches).Matches.Value
    $m1 = [int] ($nums | Select-Object -First 1)
    $m2 = [int] ($nums | Select-Object -Last 1)
    # "$m1 --- $m2"
    $n = $m1 * 10 + $m2
    # $n
    $tot = $tot + $n
}

$tot